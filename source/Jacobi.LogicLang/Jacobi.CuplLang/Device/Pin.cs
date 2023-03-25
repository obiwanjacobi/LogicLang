namespace Jacobi.CuplLang.Device;

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
    Power = 0x01,
    /// <summary>A general purpose input or output.</summary>
    GPIO = 0x02,

    /// <summary>A dedicated clock (input) for clocking the FlipFlops in the MacroCells.</summary>
    Clock = 0x10,
    /// <summary>A dedicate output enable (input) for enabling configured outputs.</summary>
    OutputEnable = 0x20,
}

internal class Pin
{
    public Pin(int number, PinDirection direction, PinFunction function)
    {
        Number = number;
        Direction = direction;
        Function = function;
    }

    public int Number { get; }
    public PinDirection Direction { get; }
    public PinFunction Function { get; }
}
