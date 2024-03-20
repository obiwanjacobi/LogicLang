using Jacobi.CuplLang.Ast;
using Jacobi.CuplLang.Device;
using Jacobi.CuplLang.Placement;
using Jacobi.CuplLang.Tests.Ast;
using Xunit.Abstractions;

namespace Jacobi.CuplLang.Tests.Placement;

public class PlacementTests
{
    private readonly ITestOutputHelper _output;
    public PlacementTests(ITestOutputHelper output)
        => _output = output;

    private void WriteDiagnostics(IEnumerable<Diagnostic> diagnostics)
    {
        foreach (var diag in diagnostics)
        {
            _output.WriteLine(diag.ToString());
        }
    }

    [Fact]
    public void PlacementPins()
    {
        const string cupl =
            "Device G16V8;" +
            "Pin 1 = A;" +
            "Pin 2 = B;" +
            "Pin 14 = Q;" +
            "Q = A & B;"
            ;

        var doc = CuplParser.ParseDocument(cupl, _output);
        // TODO: doc expression optimization

        var device = Device.Device.Create(doc.Header.Device);
        var functions = new DeviceFunction(device);
        var success = functions.AssignPinFunctions(doc.Pins, doc.Equations);
        WriteDiagnostics(functions.Diagnostics);
        success.Should().BeTrue();

        var placement = new SumOfProductsPlacement(device);
        placement.Inputs.Should().HaveCount(17);
        placement.Outputs.Should().HaveCount(1);
    }

    [Fact]
    public void Evaluator_TruthTable()
    {
        const string cupl =
            "Device G16V8;" +
            "Pin 1 = A;" +
            "Pin 2 = B;" +
            "Pin 14 = Q;" +
            "X = A & B;" +
            "Append Q = A $ B;" +
            "Append Q = A # X;"
            ;

        var doc = CuplParser.ParseDocument(cupl, _output);
        var truthTable = new TruthTable(doc.Equations);
        _output.WriteLine(truthTable.ToString());
    }
}