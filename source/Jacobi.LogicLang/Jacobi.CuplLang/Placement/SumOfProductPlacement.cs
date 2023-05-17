using Jacobi.CuplLang.Ast;

namespace Jacobi.CuplLang.Placement;

internal class SumOfProductPlacement : Placement
{
    // inputs => and equation
    // output (MacroCell) => product terms or-ed
    //  flipflop / OE / CLK / XOR / feedback/input
    public SumOfProductPlacement(Device.Device device)
        : base(device)
    { }

    public override void Add(AstEquation equation)
    {

    }
}
