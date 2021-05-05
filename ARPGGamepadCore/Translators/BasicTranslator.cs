using System;
using System.Collections.Generic;
using XGamePad;

namespace ARPGGamepadCore.Translators
{
    public class BasicTranslator : IGamepadTranslator
    {
        private IInputHelper InputHelper { get; init; }
        private MovementData LeftAnalog { get; set; }
        private MovementData RightAnalog { get; set; }
        private readonly Dictionary<string, bool> toggledButtons = new Dictionary<string, bool>();

        public BasicTranslator(IInputHelper inputHelper)
        {
            this.InputHelper = inputHelper;
            LeftAnalog = new MovementData();
            RightAnalog = new MovementData();
        }

        public void Init(object owner)
        {

        }

        public void Dispose()
        {
        }

        public void SendButtonUp(ButtonConfig button)
        {
            if (!button.Toggle)
            {
                InputHelper.SendKeyUp(button.Key, button.MouseClick, button.Modifier);
            }
        }

        public void SendButtonDown(ButtonConfig button)
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
            double x = xPosition / (double)InputHelper.ThumbMaxValue;// 32767.0;
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
            MovementData CycleMovement = new MovementData()
            {
                Active = false
            };

            //Do the actual movement with the selected thumbstick
            if (RightAnalog.Active)
            {
                CycleMovement = RightAnalog;
            }
            else if (RightAnalog.KeepingPressed)
            {
                this.ReleaseThumbstick(RightAnalog);
            }
            if (LeftAnalog.Active)
            {
                if (RightAnalog.KeepingPressed)
                {
                    this.RightAnalog.Active = false;
                    this.ReleaseThumbstick(RightAnalog);
                }
                CycleMovement = LeftAnalog;
            }
            else if (LeftAnalog.KeepingPressed)
            {
                this.ReleaseThumbstick(LeftAnalog);
            }

            if (CycleMovement.Active)
            {
                this.ProcessThumbstick(CycleMovement, selectedResolution);
            }
        }

        private void ReleaseThumbstick(MovementData data)
        {
            if (data.SourceConfig.Button != null)
            {
                if (data.SourceConfig.Button != null && data.SourceConfig.KeepPressed)
                {
                    InputHelper.SendKeyUp(data.SourceConfig.Button.Key, data.SourceConfig.Button.MouseClick, data.SourceConfig.Button.Modifier);
                    data.KeepingPressed = false;
                }
            }
        }

        private void ProcessThumbstick(MovementData data, ResolutionConfig selectedResolution)
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

            InputHelper.SetCursorPosition((int)screenPos.X + data.SourceConfig.OffsetX, (int)screenPos.Y + data.SourceConfig.OffsetY);
            if (data.SourceConfig.Button != null)
            {
                if ((data.SourceConfig.KeepPressed && !data.KeepingPressed) || !data.SourceConfig.KeepPressed)
                {
                    InputHelper.SendKeyDown(data.SourceConfig.Button.Key, data.SourceConfig.Button.MouseClick, data.SourceConfig.Button.Modifier);

                    if (!data.SourceConfig.KeepPressed)
                    {
                        InputHelper.SendKeyUp(data.SourceConfig.Button.Key, data.SourceConfig.Button.MouseClick, data.SourceConfig.Button.Modifier);
                    }
                    else
                    {
                        data.KeepingPressed = true;
                    }
                }

            }
            
        }

        public void Start()
        {
        }

        public void Stop()
        {
        }
    }
}
