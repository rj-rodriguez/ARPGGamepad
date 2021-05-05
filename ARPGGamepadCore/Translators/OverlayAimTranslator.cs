using System;
using System.Collections.Generic;
using System.Drawing;
using XGamePad;

namespace ARPGGamepadCore
{
    public class OverlayAimTranslator : IGamepadTranslator
    {
        private IInputHelper InputHelper { get; init; }
        private IOverlayHelper OverlayHelper { get; init; }

        private ResolutionConfig selectedResolution;
        private readonly Stack<bool> Pressing;
        private readonly Stack<ButtonConfig> PressedButtons;
        private readonly Stack<ButtonConfig> ReleasedButtons;

        private readonly Dictionary<string, bool> buttonsPressed = new Dictionary<string, bool>();
        private readonly Dictionary<string, bool> toggledButtons = new Dictionary<string, bool>();

        private MovementData LeftAnalog { get; set; }
        private MovementData RightAnalog { get; set; }

        private readonly List<MovementData> analogs = new List<MovementData>();

        private bool wasMoving = false;
        private int buttonsChange = 0;

        private bool AreButtonsBeingPressed
        {
            get
            {
                return Pressing.Count > 0 || PressedButtons.Count > 0;
            }
        }

        public OverlayAimTranslator(IInputHelper inputHelper, IOverlayHelper overlayHelper)
        {
            InputHelper = inputHelper;
            OverlayHelper = overlayHelper;
            Pressing = new Stack<bool>();
            PressedButtons = new Stack<ButtonConfig>();
            ReleasedButtons = new Stack<ButtonConfig>();
            LeftAnalog = new MovementData();
            RightAnalog = new MovementData();
            analogs.Add(RightAnalog);
            analogs.Add(LeftAnalog);
        }

        public void Init(object owner)
        {
            OverlayHelper.Init(owner);
        }

        public void Dispose()
        {
            OverlayHelper.Dispose();
        }

        public void SendButtonUp(ButtonConfig button)
        {
            ReleasedButtons.Push(button);
        }

        public void SendButtonDown(ButtonConfig button)
        {
            PressedButtons.Push(button);
        }

        public void SendThumbstickChange(int xPosition, int yPosition, AnalogConfig analogConfig, GamepadThumbs side)
        {
            MovementData data;

            if (side == GamepadThumbs.Left)
            {
                data = LeftAnalog;
            }
            else
            {
                data = RightAnalog;
            }

            float deadZone = 0;

            deadZone = analogConfig.DeadZone;
            double x = xPosition / (double)InputHelper.ThumbMaxValue;
            double y = yPosition / (double)InputHelper.ThumbMaxValue;

            if (data == null)
            {
                data = new MovementData();
            }

            data.Side = side;
            data.SourceConfig = analogConfig;
            data.AspectRatio = analogConfig.AspectRatioY / analogConfig.AspectRatioX;

            if (Math.Abs(x) > deadZone || Math.Abs(y) > deadZone)
            {
                data.Active = true;
                data.X = x;
                data.Y = y;
            }
            else
            {
                data.Active = false;
                data.X = 0;
                data.Y = 0;
            }

        }

        public void Process(ResolutionConfig selectedResolution)
        {
            this.selectedResolution = selectedResolution;

            //Analogs Process
            if (LeftAnalog.Active)
            {
                ProcessThumbstick(LeftAnalog, selectedResolution);
                wasMoving = true;
            }
            else if (LeftAnalog.KeepingPressed)
            {
                ReleaseThumbstick(LeftAnalog);
            }
            if (RightAnalog.Active)
            {
                ProcessThumbstick(RightAnalog, selectedResolution, !AreButtonsBeingPressed);
            }
            else if (RightAnalog.KeepingPressed)
            {
                ReleaseThumbstick(RightAnalog, !AreButtonsBeingPressed);
                buttonsChange = 0;
            }

            if (buttonsChange < 5 && RightAnalog.Active && AreButtonsBeingPressed)
            {
                buttonsChange++;
                return;
            }

            while (PressedButtons.TryPop(out var button))
            {
                PressButton(button);
                Pressing.Push(true);
            }
            while (ReleasedButtons.TryPop(out var button))
            {
                if (buttonsPressed.ContainsKey(button.Button) && buttonsPressed[button.Button])
                {
                    if (Pressing.Count > 0)
                    {
                        Pressing.Pop();
                    }
                }
                ReleaseButton(button);
            }


        }

        private void PressButton(ButtonConfig button)
        {
            if (button.Toggle)
            {
                if (!toggledButtons.ContainsKey(button.Button) || toggledButtons[button.Button] == false)
                {
                    InputHelper.SendKeyUp(button.Key, button.MouseClick, button.Modifier);
                    if (!toggledButtons.ContainsKey(button.Button))
                    {
                        toggledButtons.Add(button.Button, true);
                    }
                    toggledButtons[button.Button] = true;
                }
                else
                {
                    InputHelper.SendKeyDown(button.Key, button.MouseClick, button.Modifier);
                    toggledButtons[button.Button] = false;
                }
            }
            else
            {
                InputHelper.SendKeyDown(button.Key, button.MouseClick, button.Modifier);
            }
            if (!buttonsPressed.ContainsKey(button.Button))
            {
                buttonsPressed.Add(button.Button, true);
            }
            buttonsPressed[button.Button] = true;
        }

        private void ReleaseButton(ButtonConfig button)
        {
            if (buttonsPressed.ContainsKey(button.Button) && buttonsPressed[button.Button])
            {
                if (!button.Toggle)
                {
                    InputHelper.SendKeyUp(button.Key, button.MouseClick, button.Modifier);
                }
                buttonsPressed[button.Button] = false;
            }
        }

        private void ReleaseThumbstick(MovementData data, bool aim = false)
        {
            if (!aim)
            {
                if (data.SourceConfig.SpringMode)
                {
                    Vector2D screenPos = new Vector2D(0, 0);
                    screenPos.Y *= data.AspectRatio;
                    screenPos = screenPos.ToScreenPosition(selectedResolution.ScreenWidth, selectedResolution.ScreenHeight);
                    var p = new Point((int)screenPos.X + data.SourceConfig.OffsetX, (int)screenPos.Y + data.SourceConfig.OffsetY);

                    if (wasMoving && !RightAnalog.Active)
                    {
                        wasMoving = false;
                        SetMouseCursor(p, data, true);
                    }

                }
                if (data.SourceConfig != null && data.SourceConfig.Button != null)
                {
                    if (data.SourceConfig.KeepPressed)
                    {
                        ReleaseButton(data.SourceConfig.Button);
                        data.KeepingPressed = false;
                    }
                }
            }
            else
            {
                OverlayHelper.SetCursorVisibility(false);
            }
        }

        private void ProcessThumbstick(MovementData data, ResolutionConfig selectedResolution, bool aim = false)
        {
            Vector2D sourcePos = new Vector2D(data.X, data.Y);
            Vector2D joyPos = sourcePos.Normalize();

            double Magnitude = sourcePos.Magnitude;

            if (data.SourceConfig.FixedRadius)
            {
                Magnitude = 1.0;
            }
            else
            {
                if (Magnitude > 1.0)
                {
                    Magnitude = 1.0;
                }

                double minMagnitude = (double)data.SourceConfig.DeadZone;
                double Range = 1.0 - minMagnitude;
                Magnitude = (Magnitude - minMagnitude) / Range;
            }

            Vector2D screenPos = joyPos * data.SourceConfig.Radius * Magnitude;

            screenPos.Y *= data.AspectRatio;

            screenPos = screenPos.ToScreenPosition(selectedResolution.ScreenWidth, selectedResolution.ScreenHeight);

            //Set cursor Position
            var p = new Point((int)screenPos.X + data.SourceConfig.OffsetX, (int)screenPos.Y + data.SourceConfig.OffsetY);

            if (aim)
            {
                DrawAim(p, data);
            }
            else
            {
                SetMouseCursor(p, data);
            }

        }

        private void SetMouseCursor(Point p, MovementData data, bool force = false)
        {
            if ((data.SourceConfig.KeepPressed && !data.KeepingPressed) || !data.SourceConfig.KeepPressed || !buttonsPressed[data.SourceConfig.Button.Button])
            {
                if (data.SourceConfig.KeepPressed)
                {
                    data.KeepingPressed = true;
                }

                if (data.SourceConfig.Button != null && !AreButtonsBeingPressed)
                {
                    PressButton(data.SourceConfig.Button);
                    if (!data.SourceConfig.KeepPressed)
                    {
                        ReleaseButton(data.SourceConfig.Button);
                    }
                }
            }
            InputHelper.SetCursorPosition(p);
        }

        private void DrawAim(Point p, MovementData data)
        {
            OverlayHelper.SetCursorVisibility(true);
            OverlayHelper.SetCursorPosition(p.X, p.Y);
            data.KeepingPressed = true;
        }

        public void Start()
        {
            OverlayHelper.Start();
        }

        public void Stop()
        {
            OverlayHelper.Stop();
        }
    }
}
