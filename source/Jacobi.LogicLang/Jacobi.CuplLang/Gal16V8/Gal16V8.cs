using System;
using System.Collections.Generic;
using System.Linq;
using Jacobi.CuplLang.Ast;
using FuseNumber = System.Int32;

namespace Jacobi.CuplLang.Gal16V8;

internal sealed class Gal16V8
{
    private G16V8DeviceMode? _deviceMode;

    public static Gal16V8 FromDeviceName(string device)
    {
        // TODO: optional device postfix indicates mode
        return new Gal16V8();
    }

    public Placement CreatePlacement(IReadOnlyList<AstPin> pins, IReadOnlyList<AstEquation> equations)
    {
        if (equations.Any(e => e.Pin is null))
            throw new ArgumentException("The equations contain intermediate expression. They should be expanded and removed first.");

        if (_deviceMode is null)
            _deviceMode = DetermineDeviceMode(equations);

        // all assigned symbols in equations
        var potentialOutputSymbols = equations
            .Select(e => e.Symbol)
            .ToList();
        // all pins that match any of the assigned symbols
        var outputPins = pins
            .Where(p => potentialOutputSymbols.Contains(p.Symbol))
            .ToDictionary(p => p.Symbol, p => p);
        // the other pins are inputs
        var inputPins = pins
            .Except(outputPins.Values)
            .ToDictionary(p => p.Symbol, p => p);

        // double check output pins
        var eqPins = equations.Select(e => e.Pin);
        if (!outputPins.Values.All(p => eqPins.Contains(p)))
            throw new Exception("Mismatch between equation pins and output pins.");

        // TODO: determine the mode for each MacroCell.

        var placement = new PlacementBuilder(_deviceMode);

        foreach (var equation in equations)
        {
            var solver = new AstExpressionSolver();
            var expressions = solver.Solve(equation);

            int productTerm = 0;
            // symbols in expressions to pin numbers to metadata
            foreach (var expression in expressions)
            {
                var pinRefs = ToPinReferences(pins, expression);
                productTerm = placement.Add(equation.Pin!, pinRefs, productTerm);
            }

            placement.DisableProductTerms(equation.Pin!, productTerm);
        }
        return placement.Build();
    }

    private IReadOnlyList<PinReference> ToPinReferences(IReadOnlyList<AstPin> pins, AstExpression expression)
    {
        var pinRefs = new List<PinReference>();

        GatherPinReferences(pinRefs, pins, expression);

        return pinRefs;
    }

    private void GatherPinReferences(List<PinReference> pinRefs, IReadOnlyList<AstPin> pins, AstExpression expression)
    {
        switch (expression.Kind)
        {
            case AstExpressionKind.Symbol:
                pinRefs.Add(Create(pins, expression.Symbol!));
                break;
            case AstExpressionKind.Number:
                throw new Exception($"A constant cannot be programmed {expression}");
            case AstExpressionKind.BinOperator:
                if (expression.Operator != AstOperator.And)
                    throw new Exception($"Invalid operator {expression.Operator} for a product term: {expression}");
                GatherPinReferences(pinRefs, pins, expression.Left!);
                GatherPinReferences(pinRefs, pins, expression.Right!);
                break;
            case AstExpressionKind.UniOperator:
                if (expression.Left!.Kind != AstExpressionKind.Symbol)
                    throw new Exception($"Invalid expression after Not operator: {expression}");
                pinRefs.Add(Create(pins, expression.Left!.Symbol!, PinMode.Inverted));
                break;
            default:
                throw new Exception($"Internal error - this should never be reached: {expression}");
        }

        PinReference Create(IReadOnlyList<AstPin> pins, string symbol, PinMode mode = PinMode.None)
        {
            var pin = pins.Single(p => p.Symbol == symbol);
            return new PinReference(pin.PinNumber, mode);
        }
    }

    private static G16V8DeviceMode DetermineDeviceMode(IReadOnlyList<AstEquation> equations)
    {
        // Registered: equations that use .d extension
        if (equations.Any(e => e.Extension == SymbolExtension.Data))
            return G16V8DeviceMode.Registered;

        // Complex: equations that use .oe extension
        // equations that require bidirectional macrocells/outputs (TODO)
        if (equations.Any(e => e.Extension == SymbolExtension.OutputEnable))
            return G16V8DeviceMode.Complex;

        // Simple:
        return G16V8DeviceMode.Simple;
    }

    //private G16V8MacroCell DetermineMacroCellMode(AstEquation equation)
    //{

    //}
}

internal enum PinMode
{
    None,
    Inverted
}

internal sealed class PlacementBuilder
{
    private readonly G16V8DeviceMode _deviceMode;
    private readonly List<Fuse> _fuses = [];

    public PlacementBuilder(G16V8DeviceMode deviceMode)
    {
        _deviceMode = deviceMode;
        _fuses.Add(deviceMode.AC0);
        _fuses.Add(deviceMode.SYN);
    }

    public int Add(AstPin outputPin, IReadOnlyList<PinReference> pinReferences, int productTerm)
    {
        var macroCell = _deviceMode.MacroCells.SingleOrDefault(mc => mc.Pin.Number == outputPin.PinNumber)
            ?? throw new ArgumentException($"The pin {outputPin.Symbol} ({outputPin.PinNumber}) is not an output pin.");

        if (macroCell.ProductTermCount < pinReferences.Count)
            throw new ArgumentException($"The pin {outputPin.Symbol} ({outputPin.PinNumber}) has not enough product terms ({macroCell.ProductTermCount}) to accommodate {pinReferences.Count} products..");

        var devicePins = pinReferences.Select(p => (p, _deviceMode.Pins[p.PinNumber - 1]));

        if (macroCell.ProductTermCount == 7)    // flacky test
            throw new NotImplementedException("Complex Mode is not implemented yet.");

        // TODO: macro cell config
        _fuses.Add(new Fuse(macroCell.FuseAC1, false));
        _fuses.Add(new Fuse(macroCell.FuseXOR, false));

        foreach (var (pin, devicePin) in devicePins)
        {
            if (devicePin.ValueFuseBase is null)
                throw new Exception($"The pin {devicePin.Number} is a dedicated pin and cannot be used.");

            var fuseBase = macroCell.FuseBase + (productTerm * G16V8DeviceMode.FuseMatrixColumnCount);
            var fuseNumber = fuseBase +
                (pin.Mode == PinMode.Inverted
                    ? devicePin.InvertedFuseBase
                    : devicePin.ValueFuseBase);

            _fuses.Add(new Fuse(fuseNumber!.Value));

            productTerm++;
        }

        return productTerm;
    }

    public void DisableProductTerms(AstPin outputPin, int productTerm)
    {
        var macroCell = _deviceMode.MacroCells.SingleOrDefault(mc => mc.Pin.Number == outputPin.PinNumber);
        if (macroCell is null)
            throw new ArgumentException($"The pin {outputPin.Symbol} ({outputPin.PinNumber}) is not an output pin.");

        // disable unused product terms
        for (int i = productTerm; i < macroCell.ProductTermCount; i++)
        {
            _fuses.Add(new Fuse(macroCell.ProductTermDisableFuseBase + i));
        }
    }

    public Placement Build()
        => new Placement(_deviceMode.DeviceName, _deviceMode.Pins.Count, _deviceMode.FuseCount, _fuses);
}

internal enum PinCapability
{
    /// <summary>Ignore / Not initialized.</summary>
    None = 0,
    /// <summary>Pin is only an input.</summary>
    Input = 1,
    /// <summary>Pin is only an output.</summary>
    Output = 2,
    /// <summary>Pin can be an input or be both an input and an output at the same time.</summary>
    InputOrInputOutput = 3,
    /// <summary>Pin can be either an input or an output.</summary>
    InputOrOutput = 4,
}

internal enum MacroCellFoldback
{
    /// <summary>No foldback.</summary>
    None,
    /// <summary>The output is folded back into the matrix.</summary>
    Output,
    /// <summary>The inverse of the Flip-Flop output (/Q) is folded back into the matrix.</summary>
    NotQ
}

internal sealed class G16V8DeviceMode
{
    private static readonly DevicePin[] _pinsRegistered =
    {
        new(1, PinCapability.Input),     // clk
        new(2, PinCapability.Input, 0, 1),
        new(3, PinCapability.Input, 4, 5),
        new(4, PinCapability.Input, 8, 9),
        new(5, PinCapability.Input, 12, 13),
        new(6, PinCapability.Input, 16, 17),
        new(7, PinCapability.Input, 20, 21),
        new(8, PinCapability.Input, 24, 25),
        new(9, PinCapability.Input, 28, 29),
        new(10, PinCapability.None),     // gnd
        new(11, PinCapability.Input),    // oe
        new(12, PinCapability.InputOrInputOutput, 30, 31),
        new(13, PinCapability.InputOrInputOutput, 26, 27),
        new(14, PinCapability.InputOrInputOutput, 22, 23),
        new(15, PinCapability.InputOrInputOutput, 18, 19),
        new(16, PinCapability.InputOrInputOutput, 14, 15),
        new(17, PinCapability.InputOrInputOutput, 10, 11),
        new(18, PinCapability.InputOrInputOutput, 6, 7),
        new(19, PinCapability.InputOrInputOutput, 2, 3),
        new(20, PinCapability.None),     // vcc
    };

    internal static readonly G16V8MacroCell[] _macroCellsRegistered =
    {
        new(_pinsRegistered[11], 2127, 2055, 8, 1792, 2184),
        new(_pinsRegistered[12], 2126, 2054, 8, 1536, 2176),
        new(_pinsRegistered[13], 2125, 2053, 8, 1280, 2168),
        new(_pinsRegistered[14], 2124, 2052, 8, 1024, 2160),
        new(_pinsRegistered[15], 2123, 2051, 8, 768, 2152),
        new(_pinsRegistered[16], 2122, 2050, 8, 512, 2144),
        new(_pinsRegistered[17], 2121, 2049, 8, 256, 2136),
        new(_pinsRegistered[18], 2120, 2048, 8, 0, 2128),
    };

    private static readonly DevicePin[] _pinsComplex =
    {
        new(1, PinCapability.Input, 2, 3),
        new(2, PinCapability.Input, 0, 1),
        new(3, PinCapability.Input, 4, 5),
        new(4, PinCapability.Input, 8, 9),
        new(5, PinCapability.Input, 12, 13),
        new(6, PinCapability.Input, 16, 17),
        new(7, PinCapability.Input, 20, 21),
        new(8, PinCapability.Input, 24, 25),
        new(9, PinCapability.Input, 28, 29),
        new(10, PinCapability.None),    // gnd
        new(11, PinCapability.Input, 30, 31),
        new(12, PinCapability.Output),
        new(13, PinCapability.InputOrInputOutput, 26, 27),
        new(14, PinCapability.InputOrInputOutput, 22, 23),
        new(15, PinCapability.InputOrInputOutput, 18, 19),
        new(16, PinCapability.InputOrInputOutput, 14, 15),
        new(17, PinCapability.InputOrInputOutput, 10, 11),
        new(18, PinCapability.InputOrInputOutput, 6, 7),
        new(19, PinCapability.Output),
        new(20, PinCapability.None),    // vcc
    };

    internal static readonly G16V8MacroCell[] _macroCellsComplex =
    {
        new(_pinsComplex[11], 2127, 2055, 7, 1792, 2184),
        new(_pinsComplex[12], 2126, 2054, 7, 1536, 2176),
        new(_pinsComplex[13], 2125, 2053, 7, 1280, 2168),
        new(_pinsComplex[14], 2124, 2052, 7, 1024, 2160),
        new(_pinsComplex[15], 2123, 2051, 7, 768, 2152),
        new(_pinsComplex[16], 2122, 2050, 7, 512, 2144),
        new(_pinsComplex[17], 2121, 2049, 7, 256, 2136),
        new(_pinsComplex[18], 2120, 2048, 7, 0, 2128),
    };

    private static readonly DevicePin[] _pinsSimple =
    {
        new(1, PinCapability.Input, 2, 3),
        new(2, PinCapability.Input, 0, 1),
        new(3, PinCapability.Input, 4, 5),
        new(4, PinCapability.Input, 8, 9),
        new(5, PinCapability.Input, 12, 13),
        new(6, PinCapability.Input, 16, 17),
        new(7, PinCapability.Input, 20, 21),
        new(8, PinCapability.Input, 24, 25),
        new(9, PinCapability.Input, 28, 29),
        new(10, PinCapability.None),    // gnd
        new(11, PinCapability.Input, 30, 31),
        new(12, PinCapability.InputOrOutput, 26, 27),
        new(13, PinCapability.InputOrOutput, 22, 23),
        new(14, PinCapability.InputOrOutput, 18, 19),
        new(15, PinCapability.Output),
        new(16, PinCapability.Output),
        new(17, PinCapability.InputOrOutput, 14, 15),
        new(18, PinCapability.InputOrOutput, 10, 11),
        new(19, PinCapability.InputOrOutput, 6, 7),
        new(20, PinCapability.None),    // vcc
    };

    internal static readonly G16V8MacroCell[] _macroCellsSimple =
    {
        new(_pinsSimple[11], 2127, 2055, 8, 1792, 2184),
        new(_pinsSimple[12], 2126, 2054, 8, 1536, 2176),
        new(_pinsSimple[13], 2125, 2053, 8, 1280, 2168),
        new(_pinsSimple[14], 2124, 2052, 8, 1024, 2160),
        new(_pinsSimple[15], 2123, 2051, 8, 768, 2152),
        new(_pinsSimple[16], 2122, 2050, 8, 512, 2144),
        new(_pinsSimple[17], 2121, 2049, 8, 256, 2136),
        new(_pinsSimple[18], 2120, 2048, 8, 0, 2128),
    };

    internal const int FuseMatrixColumnCount = 32;

    public static G16V8DeviceMode Registered
        => new G16V8DeviceMode(_pinsRegistered, _macroCellsRegistered, syn: false, ac0: true);
    public static G16V8DeviceMode Complex
        => new G16V8DeviceMode(_pinsComplex, _macroCellsComplex, syn: true, ac0: true);
    public static G16V8DeviceMode Simple
        => new G16V8DeviceMode(_pinsSimple, _macroCellsSimple, syn: true, ac0: false);

    private G16V8DeviceMode(IReadOnlyList<DevicePin> pins, IReadOnlyList<G16V8MacroCell> macroCells, bool syn, bool ac0)
    {
        Pins = pins;
        MacroCells = macroCells;
        SYN = new Fuse(2192, syn);
        AC0 = new Fuse(2193, ac0);
    }

    public IReadOnlyList<DevicePin> Pins { get; }
    public IReadOnlyList<G16V8MacroCell> MacroCells { get; }
    public Fuse SYN { get; }
    public Fuse AC0 { get; }

    public string DeviceName => "G16V8";
    public int FuseCount => 2194;
}

internal class G16V8MacroCell
{
    public G16V8MacroCell(DevicePin pin, FuseNumber fuseAc1, FuseNumber fuseXor, int ptCount, FuseNumber fuseBase, FuseNumber ptdFuseBase)
    {
        Pin = pin;
        FuseAC1 = fuseAc1;
        FuseXOR = fuseXor;
        ProductTermCount = ptCount;
        FuseBase = fuseBase;
        ProductTermDisableFuseBase = ptdFuseBase;
    }

    public int ProductTermCount { get; }

    public FuseNumber FuseAC1 { get; }
    public FuseNumber FuseXOR { get; }
    public FuseNumber FuseBase { get; }
    public FuseNumber ProductTermDisableFuseBase { get; }

    public DevicePin Pin { get; }
}
