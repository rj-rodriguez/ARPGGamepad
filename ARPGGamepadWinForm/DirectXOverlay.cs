using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARPGGamepadWinForm
{
    public partial class DirectXOverlay : Form
    {
        private Margins marg;

        internal struct Margins
        {
            public int Left, Right, Top, Bottom;
        }

        [DllImport("user32.dll", SetLastError = true)]

        private static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]

        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll")]

        static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        public const int GWL_EXSTYLE = -20;

        public const int WS_EX_LAYERED = 0x80000;

        public const int WS_EX_TRANSPARENT = 0x20;

        public const int LWA_ALPHA = 0x2;

        public const int LWA_COLORKEY = 0x1;

        [DllImport("dwmapi.dll")]
        static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref Margins pMargins);

        private Device device = null;

        private Texture texture;
        private Rectangle textureSize;
        private Sprite sprite;

        Thread dx;

        private float WindowsBarHeight { get; set; }
        private Rectangle CursorHalfSize { get; set; }
        private float X { get; set; }
        private float Y { get; set; }
        private bool CursorVisible { get; set; }

        public DirectXOverlay(float winBarHeight)
        {
            InitializeComponent();

            //Make the window's border completely transparant
            SetWindowLong(this.Handle, GWL_EXSTYLE,
                    (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) ^ WS_EX_LAYERED ^ WS_EX_TRANSPARENT));

            //Set the Alpha on the Whole Window to 255 (solid)
            SetLayeredWindowAttributes(this.Handle, 0, 255, LWA_ALPHA);

            //Init DirectX
            //This initializes the DirectX device. It needs to be done once.
            //The alpha channel in the backbuffer is critical.
            PresentParameters presentParameters = new PresentParameters();
            presentParameters.Windowed = true;
            presentParameters.SwapEffect = SwapEffect.Discard;
            presentParameters.BackBufferFormat = Format.A8R8G8B8;

            this.device = new Device(0, DeviceType.Hardware, this.Handle,
            CreateFlags.HardwareVertexProcessing, presentParameters);

            texture = new Texture(device, new Bitmap("Reticle.png"), Usage.None, Pool.Managed);
            using (Surface surface = texture.GetSurfaceLevel(0))
            {
                textureSize = new Rectangle(0, 0, surface.Description.Width, surface.Description.Height);
                CursorHalfSize = new Rectangle(0, 0, textureSize.Width / 2, textureSize.Height / 2);
            }
            WindowsBarHeight = winBarHeight;
            sprite = new Sprite(device);

            X = 0;
            Y = 0;

            StartDxThread();
        }

        public new void Dispose()
        {
            sprite.Dispose();
            texture.Dispose();
            device.Dispose();

            base.Dispose();
        }
        
        public void StartDxThread()
        {
            this.TopMost = true;
            dx = new Thread(new ThreadStart(this.dxThread));
            dx.IsBackground = true;
            dx.Start();
        }

        public void StopDxThread()
        {
            //dx.Abort();
        }

        public void SetCursorVisibility(bool visible)
        {
            CursorVisible = visible;
        }

        public void SetCursorPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        private void dxThread()
        {
            try
            {
                while (true)
                {
                    //Place your update logic here
                    device.Clear(ClearFlags.Target, Color.FromArgb(0, 0, 0, 0), 1.0f, 0);
                    device.RenderState.SourceBlend = Blend.SourceAlpha;
                    device.RenderState.DestinationBlend = Blend.InvSourceAlpha;
                    device.RenderState.AlphaBlendEnable = true;
                    device.RenderState.ZBufferEnable = false;
                    device.RenderState.Lighting = false;
                    device.RenderState.CullMode = Cull.None;
                    device.Transform.Projection = Matrix.OrthoOffCenterLH(0, this.Width, this.Height, 0, 0, 1);
                    device.BeginScene();

                    //Place your rendering logic here
                    if (CursorVisible)
                    {
                        float factorX = ((float)this.Width + (float)CursorHalfSize.Width) / (float)this.Width;
                        float factorY = ((float)this.Height - WindowsBarHeight + (float)CursorHalfSize.Height) / (float)this.Height;

                        sprite.Begin(SpriteFlags.None);
                        sprite.Draw(texture, textureSize,
                        new Vector3(16, 16, 0),
                        //To debug with real mouse position
                        //new Vector3(Cursor.Position.X * factorX, Cursor.Position.Y * factorY, 0),
                        new Vector3(X * factorX, Y * factorY, 0), 
                        Color.White);
                        sprite.End();
                    }

                    device.EndScene();
                    device.Present();
                }
            }catch(ThreadAbortException)
            {
                //Exit thread
            }

        }

        private void DirectXOverlay_Paint(object sender, PaintEventArgs e)
        {
            //Create a margin (the whole form)
            marg.Left = 0;
            marg.Top = 0;
            marg.Right = this.Width;
            marg.Bottom = this.Height;

            //Expand the Aero Glass Effect Border to the WHOLE form.
            // since we have already had the border invisible we now
            // have a completely invisible window - apart from the DirectX
            // renders NOT in black.
            DwmExtendFrameIntoClientArea(this.Handle, ref marg);  
        }  
    }
}
