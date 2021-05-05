using XGamePad;

namespace ARPGGamepadCore
{

    public class MovementData
    {
        public GamepadThumbs Side;
        public bool Active;
        public bool KeepingPressed;
        public double X, Y;
        public double AspectRatio;
        public AnalogConfig SourceConfig;
    }

    public class ButtonData
    {
        public bool Pressed;
        public string ButtonId;
    }

    public interface IGamepadTranslator
    {
        void Init(object owner);
        void Dispose();

        void Start();
        void Stop();
        void SendButtonUp(ButtonConfig button);
        void SendButtonDown(ButtonConfig button);
        void SendThumbstickChange(int xPosition, int yPosition, AnalogConfig analogConfig, GamepadThumbs side);
        void Process(ResolutionConfig selectedResolution);
    }
}
