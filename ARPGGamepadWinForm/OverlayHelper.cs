using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARPGGamepadCore;

namespace ARPGGamepadWinForm
{
    public class OverlayHelper : IOverlayHelper
    {
        private DirectXOverlay virtualAimOverlay;

        public void Dispose()
        {
            if (virtualAimOverlay != null)
            {
                virtualAimOverlay.StopDxThread();
                virtualAimOverlay.Dispose();
                virtualAimOverlay = null;
            }
        }

        public void Init(object owner)
        {
            if (virtualAimOverlay != null)
                Dispose();

            virtualAimOverlay = new DirectXOverlay(40);
        }

        public void SetCursorPosition(int x, int y)
        {
            if (virtualAimOverlay != null)
                virtualAimOverlay.SetCursorPosition(x, y);

        }

        public void SetCursorVisibility(bool visible)
        {
            if (virtualAimOverlay != null)
                virtualAimOverlay.SetCursorVisibility(false);
        }

        public void Start()
        {
            virtualAimOverlay.SetCursorPosition(0, 0);
            virtualAimOverlay.Show();
        }

        public void Stop()
        {
            
        }
    }
}
