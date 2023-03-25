using Jacobi.CuplLang.Ast;

namespace Jacobi.CuplLang.Tests.Ast;

public class ComplementTests
{
    [Fact]
    public void TryComplement_Or_Symbol()
    {
        const string cupl =
            "Device G22V10;" +
            "x = A # !A;"
            ;

        var doc = CuplParser.ParseDocument(cupl);
        var equation = doc.Equations[0];
        var expression = equation.Expression;
        expression.Kind.Should().Be(AstExpressionKind.BinOperator);

        var success = AstExpressionLaws.TryComplement(expression, out var result);
        success.Should().BeTrue();
        result!.Kind.Should().Be(AstExpressionKind.Number);
        result.Number!.Value.Should().Be(1);
    }

    [Fact]
    public void TryComplement_And_Symbol()
    {
        const string cupl =
            "Device G22V10;" +
            "x = A & !A;"
            ;

        var doc = CuplParser.ParseDocument(cupl);
        var equation = doc.Equations[0];
        var expression = equation.Expression;
        expression.Kind.Should().Be(AstExpressionKind.BinOperator);

        var success = AstExpressionLaws.TryComplement(expression, out var result);
        success.Should().BeTrue();
        result!.Kind.Should().Be(AstExpressionKind.Number);
        result.Number!.Value.Should().Be(0);
    }

    [Fact]
    public void TryComplement_Or_Expression()
    {
        const string cupl =
            "Device G22V10;" +
            "x = (A & B) # !(A&B);"
            ;

        var doc = CuplParser.ParseDocument(cupl);
        var equation = doc.Equations[0];
        var expression = equation.Expression;
        expression.Kind.Should().Be(AstExpressionKind.BinOperator);

        var success = AstExpressionLaws.TryComplement(expression, out var result);
        success.Should().BeTrue();
        result!.Kind.Should().Be(AstExpressionKind.Number);
        result.Number!.Value.Should().Be(1);
    }

    [Fact]
    public void TryComplement_And_Expression()
    {
        const string cupl =
            "Device G22V10;" +
            "x = (A # B) & !(A#B);"
            ;

        var doc = CuplParser.ParseDocument(cupl);
        var equation = doc.Equations[0];
        var expression = equation.Expression;
        expression.Kind.Should().Be(AstExpressionKind.BinOperator);

        var success = AstExpressionLaws.TryComplement(expression, out var result);
        success.Should().BeTrue();
        result!.Kind.Should().Be(AstExpressionKind.Number);
        result.Number!.Value.Should().Be(0);
    }
}