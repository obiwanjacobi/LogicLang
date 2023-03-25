namespace Jacobi.CuplLang.Device.Gal16V8;

internal class G16V18MacroCell : MacroCell
{
    private G16V8Mode _mode;

    public G16V18MacroCell(Pin pin)
        : base(pin)
    {
        _mode = new G16V8SimpleMode(this);
    }

    public override bool TrySetMode(MacroCellMode mode)
    {
        if (_mode.Mode == mode)
            return true;

        switch (mode)
        {
            case MacroCellMode.Simple:
                _mode = new G16V8SimpleMode(this);
                break;
            case MacroCellMode.Registered:
                _mode = new G16V8RegisteredMode(this);
                break;
            case MacroCellMode.Complex:
                _mode = new G16V8ComplexMode(this);
                break;
            default:
                return false;
        }

        Mode = mode;
        return true;
    }

    public override MacroCellOutputEnable OutputEnable => _mode.OutputEnable;
    public override int ProductTermCount => _mode.ProductTermCount;
    
    public override MacroCellFunction Function => _mode.Function;
    public override bool TrySetFunction(MacroCellFunction function)
        => _mode.TrySetFunction(function);
}
