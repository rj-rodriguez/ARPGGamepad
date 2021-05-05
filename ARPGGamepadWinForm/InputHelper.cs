using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ARPGGamepadCore;

namespace ARPGGamepadWinForm
{
    public class InputHelper : IInputHelper
    {
        //[DllImport("user32.dll")]
        //static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData,
        //   UIntPtr dwExtraInfo);

        //public const short ThumbMaxValue = 32767;
        public short ThumbMaxValue => 32767;

        private const UInt32 MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const UInt32 MOUSEEVENTF_LEFTUP = 0x0004;
        private const UInt32 MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const UInt32 MOUSEEVENTF_RIGHTUP = 0x0010;
        private const UInt32 MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const UInt32 MOUSEEVENTF_MIDDLEUP = 0x0040;

        public const int KEYEVENTF_EXTENDEDKEY = 0x0001; //Key down flag 
        public const int KEYEVENTF_KEYUP = 0x0002; //Key up flag 
        public const int VK_SHIFT = 0x10;
        public const int VK_CONTROL = 0x11;
        public const int VK_MENU = 0x12; //Alt
        public const int VK_ESCAPE = 0x1B;
        public const int VK_LEFT = 0x25;
        public const int VK_UP = 0x26;
        public const int VK_RIGHT = 0x27;
        public const int VK_DOWN = 0x28;

        [DllImport("user32.dll")]
        private static extern void mouse_event(
               UInt32 dwFlags, // motion and click options
               UInt32 dx, // horizontal position or change
               UInt32 dy, // vertical position or change
               UInt32 dwData, // wheel movement
               IntPtr dwExtraInfo // application-defined information
        );

        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(
            byte bVk,
            byte bScan,
            int dwFlags,
            int dwExtraInfo);

        public int GetLetterKey(string letter)
        {
            Keys key;
            if (Enum.TryParse<Keys>(letter, out key))
            {
                return (int)key;
            }
            else
            {
                return 0;
            }
        }

        public void SendKeyDown(string key, MouseClick mouse, Modifiers modifier)
        {
            if (modifier != Modifiers.None)
            {
                SendModifierDown(modifier);
            }
            if (!String.IsNullOrEmpty(key))
            {
                keybd_event((byte)GetLetterKey(key), 0, KEYEVENTF_EXTENDEDKEY, 0);
            }
            if (mouse != MouseClick.None)
            {
                SendClickDown(mouse);
            }
        }

        public void SendKeyUp(string key, MouseClick mouse, Modifiers modifier)
        {
            if (mouse != MouseClick.None)
            {
                SendClickUp(mouse);
            }
            if (!String.IsNullOrEmpty(key))
            {
                keybd_event((byte)GetLetterKey(key), 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
            }
            if (modifier != Modifiers.None)
            {
                SendModifierUp(modifier);
            }
        }

        public void SendModifierDown(Modifiers modifier)
        {
            switch (modifier)
            {
                case Modifiers.Shift:
                    keybd_event(VK_SHIFT, 0, KEYEVENTF_EXTENDEDKEY, 0);
                    break;
                case Modifiers.Control:
                    keybd_event(VK_CONTROL, 0, KEYEVENTF_EXTENDEDKEY, 0);
                    break;
                case Modifiers.Alt:
                    keybd_event(VK_MENU, 0, KEYEVENTF_EXTENDEDKEY, 0);
                    break;
            }
        }

        public void SendModifierUp(Modifiers modifier)
        {
            switch (modifier)
            {
                case Modifiers.Shift:
                    keybd_event(VK_SHIFT, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
                    break;
                case Modifiers.Control:
                    keybd_event(VK_CONTROL, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
                    break;
                case Modifiers.Alt:
                    keybd_event(VK_MENU, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
                    break;
            }
        }

        public void SendClickDown(MouseClick button)
        {
            switch (button)
            {
                case MouseClick.LeftClick:
                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, new System.IntPtr());
                    break;
                case MouseClick.MiddleClick:
                    mouse_event(MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, new System.IntPtr());
                    break;
                case MouseClick.RightClick:
                    mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, new System.IntPtr());
                    break;
            }
        }

        public void SendClickUp(MouseClick button)
        {
            switch (button)
            {
                case MouseClick.LeftClick:
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, new System.IntPtr());
                    break;
                case MouseClick.MiddleClick:
                    mouse_event(MOUSEEVENTF_MIDDLEUP, 0, 0, 0, new System.IntPtr());
                    break;
                case MouseClick.RightClick:
                    mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, new System.IntPtr());
                    break;
            }
        }

        public void SetCursorPosition(int x, int y)
        {
            var p = new Point(x, y);
            SetCursorPosition(p);
        }

        public void SetCursorPosition(Point p)
        {
            Cursor.Position = p;
        }

    }
}
