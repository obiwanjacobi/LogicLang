using Jacobi.CuplLang.Ast;
using Xunit.Abstractions;

namespace Jacobi.CuplLang.Tests.Ast;

public class ExpressionSorterTests
{
    private readonly ITestOutputHelper _output;
    public ExpressionSorterTests(ITestOutputHelper output)
        => _output = output;

    [Fact]
    public void Sort_Expression()
    {
        const string cupl =
            "Device G22V10;" +
            "x = D & A # C & B;"
            ;

        var doc = CuplParser.ParseDocument(cupl, _output);
        var equation = doc.Equations[0];

        var sorter = new AstExpressionSorter();
        var expression = sorter.Sort(equation.Expression);

        expression.ToString().Should().Be("A&D#B&C");
    }
}