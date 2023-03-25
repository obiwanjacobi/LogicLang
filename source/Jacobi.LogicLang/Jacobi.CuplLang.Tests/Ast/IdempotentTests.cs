using Jacobi.CuplLang.Ast;

namespace Jacobi.CuplLang.Tests.Ast;

public class IdempotentTests
{
    [Fact]
    public void Symbol_Or()
    {
        const string cupl =
            "Device G22V10;" +
            "x = A # A;"
            ;

        var doc = CuplParser.ParseDocument(cupl);
        var equation = doc.Equations[0];
        var expression = equation.Expression;
        expression.Kind.Should().Be(AstExpressionKind.BinOperator);

        var idempotent = new AstExpressionIdempotent();
        var simplified = idempotent.Rewrite(expression);
        simplified.Kind.Should().Be(AstExpressionKind.Symbol);
        simplified.Symbol.Should().Be("A");
    }

    [Fact]
    public void Symbol_And()
    {
        const string cupl =
            "Device G22V10;" +
            "x = A & A;"
            ;

        var doc = CuplParser.ParseDocument(cupl);
        var equation = doc.Equations[0];
        var expression = equation.Expression;
        expression.Kind.Should().Be(AstExpressionKind.BinOperator);

        var idempotent = new AstExpressionIdempotent();
        var simplified = idempotent.Rewrite(expression);
        simplified.Kind.Should().Be(AstExpressionKind.Symbol);
        simplified.Symbol.Should().Be("A");
    }
}