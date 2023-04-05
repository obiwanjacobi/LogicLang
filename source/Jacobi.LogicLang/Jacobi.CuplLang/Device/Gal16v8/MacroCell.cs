namespace Jacobi.CuplLang.Device.Gal16V8;

internal class G16V18MacroCell : MacroCell
{
    public G16V18MacroCell(Pin pin)
        : base(pin)
    {
        Mode = new G16V8SimpleMode(this);
    }

    public override bool TrySetMode(MacroCellModeKind mode)
    {
        if (ModeKind == mode)
            return true;

        switch (mode)
        {
            case MacroCellModeKind.Simple:
                Mode = new G16V8SimpleMode(this);
                break;
            case MacroCellModeKind.Registered:
                Mode = new G16V8RegisteredMode(this);
                break;
            case MacroCellModeKind.Complex:
                Mode = new G16V8ComplexMode(this);
                break;
            default:
                return false;
        }

        ModeKind = mode;
        return true;
    }
}
