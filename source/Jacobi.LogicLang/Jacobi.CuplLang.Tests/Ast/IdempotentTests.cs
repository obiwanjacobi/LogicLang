using Jacobi.CuplLang.Ast;
using Xunit.Abstractions;

namespace Jacobi.CuplLang.Tests.Ast;

public class IdempotentTests
{
    private readonly ITestOutputHelper _output;
    public IdempotentTests(ITestOutputHelper output)
        => _output = output;

    [Fact]
    public void TryIdempotent_Symbol()
    {
        const string cupl =
            "Device G22V10;" +
            "x = A # A;"
            ;

        var doc = CuplParser.ParseDocument(cupl, _output);
        var equation = doc.Equations[0];
        var expression = equation.Expression;
        expression.Kind.Should().Be(AstExpressionKind.BinOperator);

        var success = AstExpressionLaws.TryIdempotent(expression, out var result);
        success.Should().BeTrue();
        result!.Kind.Should().Be(AstExpressionKind.Symbol);
        result.Symbol.Should().Be("A");
    }

    [Fact]
    public void TryIdempotent_Expression()
    {
        const string cupl =
            "Device G22V10;" +
            "x = (A # B) & (A#B);"
            ;

        var doc = CuplParser.ParseDocument(cupl, _output);
        var equation = doc.Equations[0];
        var expression = equation.Expression;
        expression.Kind.Should().Be(AstExpressionKind.BinOperator);

        var success = AstExpressionLaws.TryIdempotent(expression, out var result);
        success.Should().BeTrue();
        result!.Kind.Should().Be(AstExpressionKind.BinOperator);
        result.Operator.Should().Be(AstOperator.Or);
        result.Left!.Symbol.Should().Be("A");
        result.Right!.Symbol.Should().Be("B");
    }
}