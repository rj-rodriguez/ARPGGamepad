using System;
using XGamePad;

namespace ARPGGamepadCore
{
    public class GamepadHelper : IGamepadHelper
    {
        private ButtonConfig lastDPadButton = null;
        private GamepadProfile profile = null;

        private Gamepad Gamepad { get; set; }

        public event EventHandler OnConnected;
        public event EventHandler OnDisconnected;
        public event EventHandler<GamepadHelperEventArgs> OnButtonDown;
        public event EventHandler<GamepadHelperEventArgs> OnButtonUp;
        public event EventHandler<GamepadHelperEventArgs> OnAnalogUpdate;

        public bool Connected { get; private set; }
        public int DeviceId { get; private set; }

        public void OpenGamepad(int gamepadIndex, GamepadProfile profile)
        {
            this.profile = profile;
            Gamepad = new Gamepad(gamepadIndex);

            Gamepad.OnConnect += new Gamepad.ConnectionHandler(this.OnConnect);
            Gamepad.OnDisconnect += new Gamepad.ConnectionHandler(this.OnDisconnect);
            Gamepad.OnRightThumbUpdate += new Gamepad.ThumbHandler(this.OnThumbUpdate);
            Gamepad.OnLeftThumbUpdate += new Gamepad.ThumbHandler(this.OnThumbUpdate);
            Gamepad.OnAButtonPress += new Gamepad.ButtonHandler(this.OnButtonPress);
            Gamepad.OnAButtonRelease += new Gamepad.ButtonHandler(this.OnButtonPress);
            Gamepad.OnBButtonPress += new Gamepad.ButtonHandler(this.OnButtonPress);
            Gamepad.OnBButtonRelease += new Gamepad.ButtonHandler(this.OnButtonPress);
            Gamepad.OnXButtonPress += new Gamepad.ButtonHandler(this.OnButtonPress);
            Gamepad.OnXButtonRelease += new Gamepad.ButtonHandler(this.OnButtonPress);
            Gamepad.OnYButtonPress += new Gamepad.ButtonHandler(this.OnButtonPress);
            Gamepad.OnYButtonRelease += new Gamepad.ButtonHandler(this.OnButtonPress);
            Gamepad.OnBackButtonPress += new Gamepad.ButtonHandler(this.OnButtonPress);
            Gamepad.OnBackButtonRelease += new Gamepad.ButtonHandler(this.OnButtonPress);
            Gamepad.OnStartButtonPress += new Gamepad.ButtonHandler(this.OnButtonPress);
            Gamepad.OnStartButtonRelease += new Gamepad.ButtonHandler(this.OnButtonPress);
            Gamepad.OnLeftShoulderButtonPress += new Gamepad.ButtonHandler(this.OnButtonPress);
            Gamepad.OnLeftShoulderButtonRelease += new Gamepad.ButtonHandler(this.OnButtonPress);
            Gamepad.OnRightShoulderButtonPress += new Gamepad.ButtonHandler(this.OnButtonPress);
            Gamepad.OnRightShoulderButtonRelease += new Gamepad.ButtonHandler(this.OnButtonPress);
            Gamepad.OnLeftThumbButtonPress += new Gamepad.ButtonHandler(this.OnButtonPress);
            Gamepad.OnLeftThumbButtonRelease += new Gamepad.ButtonHandler(this.OnButtonPress);
            Gamepad.OnRightThumbButtonPress += new Gamepad.ButtonHandler(this.OnButtonPress);
            Gamepad.OnRightThumbButtonRelease += new Gamepad.ButtonHandler(this.OnButtonPress);
            Gamepad.OnDPadPress += new Gamepad.DPadHandler(this.OnDPadPress);
            Gamepad.OnDPadRelease += new Gamepad.DPadHandler(this.OnDPadPress);
            Gamepad.OnDPadChange += new Gamepad.DPadHandler(this.OnDPadPress);
            Gamepad.OnLeftTriggerPress += new Gamepad.TriggerHandler(this.OnTriggerPress);
            Gamepad.OnLeftTriggerRelease += new Gamepad.TriggerHandler(this.OnTriggerPress);
            Gamepad.OnRightTriggerPress += new Gamepad.TriggerHandler(this.OnTriggerPress);
            Gamepad.OnRightTriggerRelease += new Gamepad.TriggerHandler(this.OnTriggerPress);
        }

        public void Update()
        {
            Gamepad.Update();
        }

        private ButtonConfig GetCurrentButton(GamepadButtons button)
        {
            ButtonConfig currentButton = null;
            switch (button)
            {
                case GamepadButtons.DPadLeft:
                    currentButton = profile.DLeft;
                    break;
                case GamepadButtons.DPadRight:
                    currentButton = profile.DRight;
                    break;
                case GamepadButtons.DPadUp:
                    currentButton = profile.DUp;
                    break;
                case GamepadButtons.DPadDown:
                    currentButton = profile.DDown;
                    break;
                case GamepadButtons.A:
                    currentButton = profile.A;
                    break;
                case GamepadButtons.B:
                    currentButton = profile.B;
                    break;
                case GamepadButtons.X:
                    currentButton = profile.X;
                    break;
                case GamepadButtons.Y:
                    currentButton = profile.Y;
                    break;
                case GamepadButtons.Back:
                    currentButton = profile.Select;
                    break;
                case GamepadButtons.Start:
                    currentButton = profile.Start;
                    break;
                case GamepadButtons.LeftShoulder:
                    currentButton = profile.LB;
                    break;
                case GamepadButtons.RightShoulder:
                    currentButton = profile.RB;
                    break;
                case GamepadButtons.LeftThumb:
                    currentButton = profile.LC;
                    break;
                case GamepadButtons.RightThumb:
                    currentButton = profile.RC;
                    break;
            }

            return currentButton;
        }

        private void OnConnect(object sender, GamepadEventArgs evt)
        {
            Connected = true;
            DeviceId = evt.DeviceID;
            OnConnected?.Invoke(this, evt);
        }

        private void OnDisconnect(object sender, GamepadEventArgs evt)
        {
            Connected = false;
            OnDisconnected?.Invoke(this, evt);
        }

        private void OnDPadPress(object sender, GamepadDPadEventArgs args)
        {
            ButtonConfig currentButton = GetCurrentButton(args.Buttons);

            if (lastDPadButton == null)
            {
                if (currentButton != null)
                {
                    OnButtonDown?.Invoke(this, new GamepadHelperEventArgs(currentButton));
                }
                lastDPadButton = currentButton;
            }
            else
            {
                if (lastDPadButton != currentButton)
                {
                    if (lastDPadButton != null)
                    {
                        OnButtonUp?.Invoke(this, new GamepadHelperEventArgs(lastDPadButton));
                    }
                    if (currentButton != null)
                    {
                        OnButtonDown?.Invoke(this, new GamepadHelperEventArgs(currentButton));
                    }
                    lastDPadButton = currentButton;
                }
            }

        }

        private void OnButtonPress(object sender, GamepadButtonEventArgs args)
        {
            ButtonConfig currentButton = GetCurrentButton(args.Button);

            if (currentButton != null && args.IsPressed)
            {
                OnButtonDown?.Invoke(this, new GamepadHelperEventArgs(currentButton));
            }
            else
            {
                OnButtonUp?.Invoke(this, new GamepadHelperEventArgs(currentButton));
            }
        }

        private void OnTriggerPress(object sender, GamepadTriggerEventArgs args)
        {
            ButtonConfig currentButton = args.Trigger == GamepadTriggers.Left ? profile.LT : profile.RT;

            if (args.Value > 0)
            {
                OnButtonDown?.Invoke(this, new GamepadHelperEventArgs(currentButton));
            }
            else
            {
                OnButtonUp?.Invoke(this, new GamepadHelperEventArgs(currentButton));
            }
        }

        void OnThumbUpdate(object sender, GamepadThumbEventArgs evt)
        {
            var analog = evt.Thumb == GamepadThumbs.Right ? profile.SelectedResolution.RightAnalog : profile.SelectedResolution.LeftAnalog;
            OnAnalogUpdate(this, new GamepadHelperEventArgs(evt.XPosition, evt.YPosition, evt.Thumb, analog));
        }

    }
}
