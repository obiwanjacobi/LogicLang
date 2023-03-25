using Jacobi.CuplLang.Ast;
using Xunit.Abstractions;

namespace Jacobi.CuplLang.Tests.Ast;

public class DoubleNegationTests
{
    private readonly ITestOutputHelper _output;
    public DoubleNegationTests(ITestOutputHelper output)
        => _output = output;

    [Fact]
    public void TryDoubleNegation_Symbol()
    {
        const string cupl =
            "Device G22V10;" +
            "x = !(!A);"
            ;

        var doc = CuplParser.ParseDocument(cupl, _output);
        var equation = doc.Equations[0];
        var expression = equation.Expression;
        expression.Kind.Should().Be(AstExpressionKind.UniOperator);

        var success = AstExpressionLaws.TryDoubleNegation(expression, out var result);
        success.Should().BeTrue();
        result!.Kind.Should().Be(AstExpressionKind.Symbol);
        result.Symbol.Should().Be("A");
    }

    [Fact]
    public void TryDoubleNegation_Expression()
    {
        const string cupl =
            "Device G22V10;" +
            "x = !(!(A & B));"
            ;

        var doc = CuplParser.ParseDocument(cupl, _output);
        var equation = doc.Equations[0];
        var expression = equation.Expression;
        expression.Kind.Should().Be(AstExpressionKind.UniOperator);

        var success = AstExpressionLaws.TryDoubleNegation(expression, out var result);
        success.Should().BeTrue();
        result!.Kind.Should().Be(AstExpressionKind.BinOperator);
        result.Operator.Should().Be(AstOperator.And);
        result.Left!.Symbol.Should().Be("A");
        result.Right!.Symbol.Should().Be("B");
    }
}