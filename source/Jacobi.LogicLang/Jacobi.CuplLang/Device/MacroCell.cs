using System.Diagnostics;

namespace Jacobi.CuplLang.Device;

internal enum MacroCellModeKind
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

[DebuggerDisplay("{Pin} - {ModeKind}/{ProductTermCount}")]
internal abstract class MacroCell
{
    protected MacroCell(Pin pin)
    {
        Pin = pin;
        Mode = new NullMode(this);
    }

    protected MacroCellMode Mode { get; set; }

    public Pin Pin { get; }
    public virtual int ProductTermCount { get; }
    public virtual bool InvertOutput { get; set; }

    public MacroCellModeKind ModeKind { get; protected set; }
    public virtual bool TrySetMode(MacroCellModeKind mode)
    {
        return false;
    }

    public virtual MacroCellFunction Function { get; }
    public virtual bool TrySetFunction(MacroCellFunction function)
    {
        return false;
    }

    public virtual MacroCellOutputEnable OutputEnable { get; }

    private sealed class NullMode : MacroCellMode
    {
        internal NullMode(MacroCell macroCell)
            : base(macroCell)
        { }

        public override MacroCellModeKind Mode => MacroCellModeKind.None;
        public override int ProductTermCount => 0;
        public override MacroCellFunction Function => MacroCellFunction.None;

        public override bool TrySetFunction(MacroCellFunction function)
            => false;

        public override MacroCellOutputEnable OutputEnable => MacroCellOutputEnable.None;
    }
}

internal abstract class MacroCellMode
{
    protected MacroCellMode(MacroCell macroCell)
    {
        MacroCell = macroCell;
    }

    protected MacroCell MacroCell { get; }

    public abstract MacroCellModeKind Mode { get; }
    public abstract int ProductTermCount { get; }

    public abstract MacroCellFunction Function { get; }
    public abstract bool TrySetFunction(MacroCellFunction function);

    public abstract MacroCellOutputEnable OutputEnable { get; }
}
