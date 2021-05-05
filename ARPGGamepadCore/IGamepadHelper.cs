using System;

namespace ARPGGamepadCore
{
    public interface IGamepadHelper
    {
        event EventHandler OnConnected;
        event EventHandler OnDisconnected;
        event EventHandler<GamepadHelperEventArgs> OnButtonDown;
        event EventHandler<GamepadHelperEventArgs> OnButtonUp;
        event EventHandler<GamepadHelperEventArgs> OnAnalogUpdate;
        bool Connected { get; }
        int DeviceId { get; }

        void OpenGamepad(int gamepadIndex, GamepadProfile profile);

        void Update();

    }
}
