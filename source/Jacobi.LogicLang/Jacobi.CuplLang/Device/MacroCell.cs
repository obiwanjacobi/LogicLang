namespace Jacobi.CuplLang.Device;

internal enum MacroCellMode
{
    /// <summary>Not initialized.</summary>
    None,
    /// <summary>Simple mode.</summary>
    Simple,
    /// <summary>Using FlipFlops on the outputs.</summary>
    Registered,
    /// <summary>Combination of combinatorial and registered.</summary>
    Complex
}

internal enum MacroCellFunction
{
    /// <summary>Not active / not initialized.</summary>
    None,
    /// <summary>Configured as an input.</summary>
    Input,
    /// <summary>Configured as an output.</summary>
    Output,
    /// <summary>Configured as an output with feedback back into the matrix.</summary>
    OutputFeedback,
    /// <summary>Configured as a flipflop.</summary>
    FlipFlop,
}

internal enum MacroCellOutputEnable
{
    /// <summary>No OE available.</summary>
    None,
    /// <summary>OE from matrix.</summary>
    Combinatorial,
    /// <summary>OE from a dedicated device pin.</summary>
    Device
}

internal abstract class MacroCell
{
    protected MacroCell(Pin pin)
    {
        Pin = pin;
    }

    public Pin Pin { get; }
    public virtual int ProductTermCount { get; }
    public virtual bool InvertOutput { get; set; }

    public MacroCellMode Mode { get; protected set; }
    public virtual bool TrySetMode(MacroCellMode mode)
    {
        return false;
    }

    public virtual MacroCellFunction Function { get; }
    public virtual bool TrySetFunction(MacroCellFunction function)
    {
        return false;
    }

    public virtual MacroCellOutputEnable OutputEnable { get; }
}
