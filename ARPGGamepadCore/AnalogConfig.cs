
namespace ARPGGamepadCore
{
    public record AnalogConfig(double AspectRatioX, double AspectRatioY, bool FixedRadius, int OffsetX, int OffsetY, double Radius,
        double InnerRadius, float DeadZone, ButtonConfig Button, bool KeepPressed, bool SpringMode);
}
