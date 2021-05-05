using System;
using XGamePad;

namespace ARPGGamepadCore
{
    public class GamepadHelperEventArgs : EventArgs
    {
        public ButtonConfig Button { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public AnalogConfig AnalogConfig { get; set; }
        public GamepadThumbs Side { get; set; }

        public GamepadHelperEventArgs(ButtonConfig button)
        {
            Button = button;
        }

        public GamepadHelperEventArgs(int x, int y, GamepadThumbs side, AnalogConfig analog)
        {
            XPosition = x;
            YPosition = y;
            Side = side;
            AnalogConfig = analog;
        }
    }
}
