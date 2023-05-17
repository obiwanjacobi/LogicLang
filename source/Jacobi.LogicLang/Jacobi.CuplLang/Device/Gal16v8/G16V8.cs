using System.Collections.Generic;

namespace Jacobi.CuplLang.Device.Gal16V8;

internal class G16V8 : Device
{
    private readonly List<Pin> _pins = new()
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

    private readonly List<G16V18MacroCell> _macroCells;

    public G16V8()
    {
        _macroCells = new List<G16V18MacroCell>() {
            new G16V18MacroCell(_pins[11]),
            new G16V18MacroCell(_pins[12]),
            new G16V18MacroCell(_pins[13]),
            new G16V18MacroCell(_pins[14]),
            new G16V18MacroCell(_pins[15]),
            new G16V18MacroCell(_pins[16]),
            new G16V18MacroCell(_pins[17]),
            new G16V18MacroCell(_pins[18])
        };
    }

    public override IReadOnlyList<Pin> Pins => _pins;
    public override IReadOnlyList<MacroCell> MacroCells => _macroCells;

    public override bool TrySetDeviceMode(DeviceMode mode)
    {
        var macroCellMode = ToMacroCellMode(mode);
        foreach (var macroCell in _macroCells)
        {
            if (!macroCell.TrySetMode(macroCellMode))
            {
                return false;
            }
        }
        return true;
    }
}
