using Jacobi.CuplLang.Ast;
using Xunit.Abstractions;

namespace Jacobi.CuplLang.Tests.Ast;

public class ExpressionTests
{
    private readonly ITestOutputHelper _output;
    public ExpressionTests(ITestOutputHelper output)
        => _output = output;

    [Fact]
    public void Number_Binary()
    {
        const string cupl =
            "Device G22V10;" +
            "x = 'b'10;"
            ;

        var doc = CuplParser.ParseDocument(cupl, _output);

        var equation = doc.Equations[0];
        var expression = equation.Expression;

        expression.Number.Value.Should().Be(2);
    }

    [Fact]
    public void Number_Octal()
    {
        const string cupl =
            "Device G22V10;" +
            "x = 'O'10;"
            ;

        var doc = CuplParser.ParseDocument(cupl, _output);

        var equation = doc.Equations[0];
        var expression = equation.Expression;

        expression.Number.Value.Should().Be(8);
    }

    [Fact]
    public void Number_Decimal()
    {
        const string cupl =
            "Device G22V10;" +
            "x = 'd'42;"
            ;

        var doc = CuplParser.ParseDocument(cupl, _output);

        var equation = doc.Equations[0];
        var expression = equation.Expression;

        expression.Number.Value.Should().Be(42);
    }

    [Fact]
    public void Number_Hex()
    {
        const string cupl =
            "Device G22V10;" +
            "x = 'h'FF;"
            ;

        var doc = CuplParser.ParseDocument(cupl, _output);

        var equation = doc.Equations[0];
        var expression = equation.Expression;

        expression.Number.Value.Should().Be(255);
    }

    [Fact]
    public void Equals_Same()
    {
        const string cupl =
            "Device G22V10;" +
            "x = !(A & B) # A # 0;"
            ;

        var doc = CuplParser.ParseDocument(cupl, _output);

        var equation = doc.Equations[0];
        var expression = equation.Expression;

        expression.Equals(expression).Should().BeTrue();
    }

    [Fact]
    public void AndPrecedence1()
    {
        const string cupl =
            "Device G22V10;" +
            "x = A & B # C;"
            ;

        var doc = CuplParser.ParseDocument(cupl, _output);

        var equation = doc.Equations[0];
        var expression = equation.Expression;

        expression.Operator.Should().Be(AstOperator.Or);
    }

    [Fact]
    public void AndPrecedence2()
    {
        const string cupl =
            "Device G22V10;" +
            "x = A # B & C;"
            ;

        var doc = CuplParser.ParseDocument(cupl, _output);

        var equation = doc.Equations[0];
        var expression = equation.Expression;

        expression.Operator.Should().Be(AstOperator.Or);
    }
}