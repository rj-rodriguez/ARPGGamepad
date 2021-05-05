
namespace ARPGGamepadCore
{
    public record ResolutionConfig
    {
        public int ScreenWidth { get; init; }
        public int ScreenHeight { get; init; }
        public AnalogConfig LeftAnalog { get; init; }
        public AnalogConfig RightAnalog { get; init; }
        public string Name => $"{ScreenWidth}x{ScreenHeight}";

        public ResolutionConfig(int screenWidth, int screenHeight, AnalogConfig leftAnalog, AnalogConfig rightAnalog) =>
            (ScreenWidth, ScreenHeight, LeftAnalog, RightAnalog) = (screenWidth, screenHeight, LeftAnalog, RightAnalog);

        public ResolutionConfig() : this(GamepadProfile.DefaultWidth, GamepadProfile.DefaultHeight)
        { }

        public ResolutionConfig(int screenWidth, int screenHeight)
        {
            LeftAnalog = new AnalogConfig
            (
                AspectRatioX: 5,
                AspectRatioY: 5,
                FixedRadius: true,
                OffsetX: 0,
                OffsetY: 0,
                Radius: 50,
                InnerRadius: 0,
                DeadZone: 1.0f,
                Button: new ButtonConfig("LeftAnalog", string.Empty, MouseClick.None, Modifiers.None, false),
                KeepPressed: true,
                SpringMode: false
            );
            RightAnalog = new AnalogConfig
            (
                AspectRatioX: 16,
                AspectRatioY: 9,
                FixedRadius: false,
                OffsetX: 0,
                OffsetY: 0,
                Radius: screenHeight * 0.80,
                InnerRadius: 80,
                DeadZone: 0.3f,
                Button: new ButtonConfig("RightAnalog", string.Empty, MouseClick.None, Modifiers.None, false),
                KeepPressed: false,
                SpringMode: false
            );
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
        }
    }
}
