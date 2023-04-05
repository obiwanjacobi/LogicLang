using System.Collections.Generic;
using System.Linq;
using Jacobi.CuplLang.Ast;

namespace Jacobi.CuplLang.Device;

internal class DeviceFunction
{
    public DeviceFunction(Device device)
        => Device = device;

    public Device Device { get; }

    private readonly List<Diagnostic> _diagnostics = new();
    public IReadOnlyList<Diagnostic> Diagnostics => _diagnostics;

    public bool AssignPinFunctions(IEnumerable<AstPin> astPins, IEnumerable<AstEquation> equations)
    {
        // all assigned symbols in equations
        var potentialOutputSymbols = equations
            .Select(e => e.Symbol)
            .ToList();
        // all pins that match any of the assigned symbols
        var outputPins = astPins
            .Where(p => potentialOutputSymbols.Contains(p.Symbol))
            .ToDictionary(p => p.PinNumber, p => p);
        // the other pins are inputs
        var inputPins = astPins
            .Except(outputPins.Values)
            .ToDictionary(p => p.PinNumber, p => p);
        // output pins that are used as inputs in equations.
        var foldbackPins = outputPins
            .Where(kvp => equations.Any(e => e.Expression.Find(kvp.Value.Symbol)))
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        foreach (var pin in Device.Pins)
        {
            var function = PinFunction.None;
            var direction = PinDirection.None;

            if (outputPins.ContainsKey(pin.Number))
            {
                direction |= PinDirection.Output;
            }
            if (inputPins.ContainsKey(pin.Number))
            {
                direction |= PinDirection.Input;
            }
            if (foldbackPins.ContainsKey(pin.Number))
            {
                function |= PinFunction.Foldback;
            }

            if (!pin.TrySetFunction(direction, function))
            {
                _diagnostics.Add(new Diagnostic($"Cannot set pin {pin.Number} to {direction} direction and {function} function, it only supports {pin.Direction} direction and {pin.Function} function."));
            }
        }

        return _diagnostics.Count == 0;
    }
}
