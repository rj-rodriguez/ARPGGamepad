using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPGGamepadCore
{
    public interface IOverlayHelper : IDisposable
    {
        void Init(object owner);
        void Start();
        void Stop();
        void SetCursorVisibility(bool visible);
        void SetCursorPosition(int x, int y);
    }
}
