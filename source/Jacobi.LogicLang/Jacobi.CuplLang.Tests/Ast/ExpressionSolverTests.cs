using Jacobi.CuplLang.Ast;
using Jacobi.CuplLang.Tests.Ast;
using Xunit.Abstractions;

namespace Jacobi.CuplLang.Tests.Ast;

public class ExpressionSolverTests
{
    private readonly ITestOutputHelper _output;
    public ExpressionSolverTests(ITestOutputHelper output)
        => _output = output;

    private IReadOnlyList<AstExpression> Solve(string cupl)
    {
        var doc = CuplParser.ParseDocument(cupl, _output);
        var solver = new AstExpressionSolver();
        var expressions = solver.Solve(doc.Equations[0]);
        return expressions;
    }

    [Fact]
    public void Solve2()
    {
        const string cupl =
            "Device G22V10;" +
            "Pin 1 = A;" +
            "Pin 2 = B;" +
            "Pin 3 = C;" +
            "Pin 4 = D;" +
            "Pin 14 = Q;" +
            "Q = A & B;" +
            "Append Q = C & D;" +   // or
            "";

        var results = Solve(cupl);
        results.Should().HaveCount(2);
    }

    [Fact]
    public void Solve3()
    {
        const string cupl =
            "Device G22V10;" +
            "Pin 1 = A;" +
            "Pin 2 = B;" +
            "Pin 3 = C;" +
            "Pin 4 = D;" +
            "Pin 14 = Q;" +
            "Q = A & B;" +
            "Append Q = C & D;" +   // or
            "Append Q = A & C;" +   // or
            "";

        var results = Solve(cupl);
        results.Should().HaveCount(3);
    }
}