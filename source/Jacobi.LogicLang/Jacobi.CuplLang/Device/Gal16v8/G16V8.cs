namespace Jacobi.CuplLang.Device.Gal16V8;

internal class G16V8 : Device
{
    // immutable, so static
    private static readonly Pin[] AllPinInfos = new[]
    {
        new Pin(1, PinDirection.Input, PinFunction.GPIO | PinFunction.Clock),
        new Pin(2, PinDirection.Input, PinFunction.GPIO),
        new Pin(3, PinDirection.Input, PinFunction.GPIO),
        new Pin(4, PinDirection.Input, PinFunction.GPIO),
        new Pin(5, PinDirection.Input, PinFunction.GPIO),
        new Pin(6, PinDirection.Input, PinFunction.GPIO),
        new Pin(7, PinDirection.Input, PinFunction.GPIO),
        new Pin(8, PinDirection.Input, PinFunction.GPIO),
        new Pin(9, PinDirection.Input, PinFunction.GPIO),
        new Pin(10, PinDirection.None, PinFunction.Power),

        new Pin(11, PinDirection.Input, PinFunction.GPIO | PinFunction.OutputEnable),
        new Pin(12, PinDirection.InputAndOutput, PinFunction.GPIO),
        new Pin(13, PinDirection.InputAndOutput, PinFunction.GPIO),
        new Pin(14, PinDirection.InputAndOutput, PinFunction.GPIO),
        new Pin(15, PinDirection.InputAndOutput, PinFunction.GPIO),
        new Pin(16, PinDirection.InputAndOutput, PinFunction.GPIO),
        new Pin(17, PinDirection.InputAndOutput, PinFunction.GPIO),
        new Pin(18, PinDirection.InputAndOutput, PinFunction.GPIO),
        new Pin(19, PinDirection.InputAndOutput, PinFunction.GPIO),
        new Pin(20, PinDirection.None, PinFunction.Power)
    };

    // mutable, so not static
    private readonly G16V18MacroCell[] AllMacroCells = new[]
    {
        new G16V18MacroCell(AllPinInfos[11]),
        new G16V18MacroCell(AllPinInfos[12]),
        new G16V18MacroCell(AllPinInfos[13]),
        new G16V18MacroCell(AllPinInfos[14]),
        new G16V18MacroCell(AllPinInfos[15]),
        new G16V18MacroCell(AllPinInfos[16]),
        new G16V18MacroCell(AllPinInfos[17]),
        new G16V18MacroCell(AllPinInfos[18])
    };

    public override IReadOnlyList<Pin> PinInfos => AllPinInfos;
    public override IReadOnlyList<MacroCell> MacroCells => AllMacroCells;
}
