namespace Jacobi.CuplLang.Device.Gal16V8;


// product terms = 8
// no OE
// pins 15, 16: output, no feedback
// other pins: 
// - output
//   feedback
// - input
internal sealed class G16V8SimpleMode : MacroCellMode
{
    public G16V8SimpleMode(G16V18MacroCell macroCell)
        : base(macroCell)
    {}

    public override int ProductTermCount => 8;
    public override MacroCellModeKind Mode => MacroCellModeKind.Simple;

    private MacroCellFunction _function;
    public override MacroCellFunction Function => _function;
    public override bool TrySetFunction(MacroCellFunction function)
    {
        if (_function == function)
            return true;

        var num = MacroCell.Pin.Number;

        switch (function)
        {
            case MacroCellFunction.Input:
                if (num is 15 or 16)
                    return false;
                break;
            case MacroCellFunction.Output:
                break;
            case MacroCellFunction.OutputFeedback:
                if (num is 15 or 16)
                    return false;
                break;
            default:
                return false;
        }

        _function = function;
        return true;
    }

    public override MacroCellOutputEnable OutputEnable => MacroCellOutputEnable.None;
}

// pin 1 = Clock
// Pin 11 = /OE
// Function:
// - I&O
//   OE is combinatorial (from matrix)
//   product terms = 7
//   input / feedback
// - FF
//   OE is from external pin
//   product terms = 8
//   feedback
internal sealed class G16V8RegisteredMode : MacroCellMode
{
    public G16V8RegisteredMode(G16V18MacroCell macroCell)
        : base(macroCell)
    { }

    public override int ProductTermCount 
        => _function == MacroCellFunction.FlipFlop ? 8 : 7;
    public override MacroCellModeKind Mode => MacroCellModeKind.Registered;

    private MacroCellFunction _function;
    public override MacroCellFunction Function => _function;
    public override bool TrySetFunction(MacroCellFunction function)
    {
        switch (function)
        {
            case MacroCellFunction.Input:
                break;
            case MacroCellFunction.Output:
                break;
            case MacroCellFunction.OutputFeedback:
                break;
            case MacroCellFunction.FlipFlop:
                break;
            default:
                return false;
        }

        _function = function;
        return true;
    }

    public override MacroCellOutputEnable OutputEnable
        => _function == MacroCellFunction.FlipFlop ? MacroCellOutputEnable.Device : MacroCellOutputEnable.Combinatorial;
}

// product terms = 7
// OE is combinatorial (from matrix)
// outputs
// Pin 13-18 have input/feedback
// Pin 12, 19 do not have feedback
internal sealed class G16V8ComplexMode : MacroCellMode
{
    public G16V8ComplexMode(G16V18MacroCell macroCell)
        : base(macroCell)
    { }

    public override int ProductTermCount => 7;
    public override MacroCellModeKind Mode => MacroCellModeKind.Complex;

    private MacroCellFunction _function;
    public override MacroCellFunction Function => _function;
    public override bool TrySetFunction(MacroCellFunction function)
    {
        var num = MacroCell.Pin.Number;

        switch (function)
        {
            case MacroCellFunction.Input:
                if (num is 12 or 19)
                    return false;
                break;
            case MacroCellFunction.Output:
                break;
            case MacroCellFunction.OutputFeedback:
                if (num is 12 or 19)
                    return false;
                break;
            default:
                return false;
        }

        _function = function;
        return true;
    }

    public override MacroCellOutputEnable OutputEnable => MacroCellOutputEnable.Combinatorial;
}
