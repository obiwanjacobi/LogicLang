using Jacobi.CuplLang.Ast;

namespace Jacobi.CuplLang.Tests.Ast
{
    public class InvertSymbolTests
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
            var equation = doc.Equations[0];

            var invertSymbol = new AstExpressionInvertSymbol("Symbol1");
            var expression = invertSymbol.Rewrite(equation.Expression);
            expression.Symbol.Should().Be("Symbol1");
        }
    }
}