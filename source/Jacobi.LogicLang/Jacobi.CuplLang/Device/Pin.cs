using System;
using System.Diagnostics;

namespace Jacobi.CuplLang.Device;

[Flags]
internal enum PinDirection
{
    /// <summary>Ignore / Not initialized.</summary>
    None = 0,
    /// <summary>Pin is an input.</summary>
    Input = 1,
    /// <summary>Pin is an output.</summary>
    Output = 2,
    /// <summary>Pin can be both an input and an output at the same time.</summary>
    InputAndOutput = 3,
    /// <summary>Pin can be either an input or an output.</summary>
    InputOrOutput = 4,
}

[Flags]
internal enum PinFunction
{
    /// <summary>Not initialized.</summary>
    None = 0x00,
    /// <summary>Power pin.</summary>
    Power = 0x10,
    /// <summary>A general purpose input or output.</summary>
    GPIO = 0x20,

    /// <summary>A dedicated clock (input) for clocking the FlipFlops in the MacroCells.</summary>
    Clock = 0x100,
    /// <summary>A dedicate output enable (input) for enabling configured outputs.</summary>
    OutputEnable = 0x200,

    /// <summary>Output routed back to the matrix, not a physical pin .</summary>
    Foldback = 0x1000,
}

[DebuggerDisplay("{Number} {Direction}-{Function}")]
internal class Pin
{
    private readonly int _capabilities;

    public Pin(int number, PinDirection direction, PinFunction function)
    {
        Number = number;
        Direction = direction;
        Function = function;
        _capabilities = (int)direction | (int)function;
    }

    public int Number { get; }
    public PinDirection Direction { get; private set; }
    public PinFunction Function { get; private set; }

    public virtual bool TrySetFunction(PinDirection direction, PinFunction function)
    {
        var dirToAssign = Direction;
        var funToAssign = Function;

        if (direction != PinDirection.None)
        {
            if (direction == PinDirection.InputOrOutput)
            {
                return false;
            }
            var dirCaps = _capabilities & 0xF;
            if (dirCaps == (int)PinDirection.InputOrOutput &&
                direction == PinDirection.InputAndOutput)
            {
                return false;
            }
            if (dirCaps == (int)PinDirection.Output &&
                direction == PinDirection.Input)
            {
                return false;
            }
            if (dirCaps == (int)PinDirection.Input &&
                direction == PinDirection.Output)
            {
                return false;
            }

            dirToAssign = direction;
        }
        
        if (function != PinFunction.None)
        {
            if ((function & PinFunction.Power) == PinFunction.Power)
            {
                return false;
            }
            if ((_capabilities & (int)function) != (int)function)
            {
                return false;
            }
            if ( ((function & PinFunction.Clock) == PinFunction.Clock ||
                  (function & PinFunction.OutputEnable) == PinFunction.OutputEnable) &&
                   direction != PinDirection.Input)
            {
                return false;
            }

            funToAssign = function;
        }

        Direction = dirToAssign;
        Function = funToAssign;
        return true;
    }
}
