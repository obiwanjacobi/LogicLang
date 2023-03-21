namespace Jacobi.CuplLang.Tests.Ast
{
    public class EquationTests
    {
        [Fact]
        public void Test1()
        {
            const string cupl =
                "Device G22V10;" +
                "PIN 1   = !Symbol1;" +
                "x = !Symbol1;"
                ;

            var doc = CuplParser.ParseDocument(cupl);

            doc.Equations.Should().HaveCount(1);
            var equation = doc.Equations[0];
            equation.Symbol.Should().Be("x");
            equation.Append.Should().BeFalse();

            var expr = equation.Expression;
            expr.Should().NotBeNull();
            expr.Kind.Should().Be(CuplLang.Ast.AstExpressionKind.UniOperator);
            expr.Left!.Symbol.Should().Be("Symbol1");
        }
    }
}