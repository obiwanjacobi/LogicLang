using Jacobi.CuplLang.Ast;
using Jacobi.CuplLang.IR;
using Jacobi.CuplLang.Tests.Ast;
using Xunit.Abstractions;

namespace Jacobi.CuplLang.Tests.IR;

public class IrBuilderTests
{
    private readonly ITestOutputHelper _output;
    public IrBuilderTests(ITestOutputHelper output)
        => _output = output;

    private IReadOnlyList<IrEquation> Build(string cupl)
    {
        var doc = CuplParser.ParseDocument(cupl, _output);
        
        var irEquations = new List<IrEquation>();

        var builder = new IrBuilder(doc.Pins);
        var optimizer = new IrOptimizer();
        foreach (var astEquation in doc.Equations)
        {
            var irEq = builder.Build(astEquation);
            var optEq = optimizer.Optimize(irEq);
            irEquations.Add(irEq);

            _output.WriteLine($"{irEq.Symbol} == {irEq.Expression}");
            _output.WriteLine($"{irEq.Symbol} => {optEq.Expression}");
        }

        return irEquations;
    }

    [Fact]
    public void Input2Output1Append1()
    {
        const string cupl =
            "Device G16V8;" +
            "Pin 1 = !A;" +
            "Pin 2 = B;" +
            "Pin 19 = !Q;" +
            "Q = A & B;" +
            "Append Q = !A & !B;" +
            "";

        var equations = Build(cupl);
        equations.Should().HaveCount(1);
    }
}