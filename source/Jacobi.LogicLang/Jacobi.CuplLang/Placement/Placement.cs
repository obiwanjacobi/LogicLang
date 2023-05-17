using System.Collections.Generic;
using System.Linq;
using Jacobi.CuplLang.Ast;
using Jacobi.CuplLang.Device;

namespace Jacobi.CuplLang.Placement;

internal abstract class Placement
{
    // inputs => and equation (pin and foldback)
    // output (MacroCell) => product terms or-ed
    //  flipflop / OE / CLK / XOR / foldback/input

    protected Placement(Device.Device device)
    {
        Device = device;
        _inputs = device.Pins
            .Where(pin => (pin.Direction is PinDirection.Input or PinDirection.InputAndOutput)
                        && (pin.Function.HasFlag(PinFunction.GPIO) || pin.Function.HasFlag(PinFunction.Foldback)))
            .Select(pin => new PlacementInput(pin))
            .ToList();
        _outputs = device.MacroCells
            .Where(mc => (mc.Pin.Direction is PinDirection.Output or PinDirection.InputAndOutput)
                        && mc.Pin.Function.HasFlag(PinFunction.GPIO))
            .Select(cell => new PlacementOutput(cell))
            .ToList();
    }
    public Device.Device Device { get; }

    private readonly List<PlacementInput> _inputs;
    public IReadOnlyList<PlacementInput> Inputs => _inputs;

    private readonly List<PlacementOutput> _outputs;
    public IReadOnlyList<PlacementOutput> Outputs => _outputs;

    public abstract void Add(AstEquation equation);
}
