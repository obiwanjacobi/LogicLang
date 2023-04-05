namespace Jacobi.CuplLang.Placement;

internal class AndOrPlacement : Placement
{
    // inputs => and equation
    // output (MacroCell) => product terms or-ed
    //  flipflop / OE / CLK / XOR / feedback/input
    public AndOrPlacement(Device.Device device)
        : base(device)
    { }
}


