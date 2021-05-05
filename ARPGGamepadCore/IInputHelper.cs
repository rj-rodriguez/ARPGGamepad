
using System.Drawing;

namespace ARPGGamepadCore
{
    public interface IInputHelper
    {
        short ThumbMaxValue { get; }
        int GetLetterKey(string letter);
        void SendKeyDown(string key, MouseClick mouse, Modifiers modifier);
        void SendKeyUp(string key, MouseClick mouse, Modifiers modifier);
        void SendModifierDown(Modifiers modifier);
        void SendModifierUp(Modifiers modifier);
        void SendClickDown(MouseClick mouse);
        void SendClickUp(MouseClick mouse);
        void SetCursorPosition(int x, int y);
        void SetCursorPosition(Point p);
    }
}
