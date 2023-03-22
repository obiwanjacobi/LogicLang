using Jacobi.CuplLang.Ast;

namespace Jacobi.CuplLang.Tests.Ast
{
    public class InvertSymbolTests
    {
        [Fact]
        public void Symbol_Invert()
        {
            const string cupl =
                "Device G22V10;" +
                "PIN 1   = !Symbol1;" +
                "x = Symbol1;"
                ;

            var doc = CuplParser.ParseDocument(cupl);
            var equation = doc.Equations[0];

            var expression = equation.Expression.InvertSymbol("Symbol1");
            expression.Operator.Should().Be(AstOperator.Not);
            expression.Left.Should().NotBeNull();
            expression.Left!.Symbol.Should().Be("Symbol1");
        }

        [Fact]
        public void Symbol_SimplifyDoubleInvert()
        {
            const string cupl =
                "Device G22V10;" +
                "PIN 1   = !Symbol1;" +
                "x = !Symbol1;"
                ;

            var doc = CuplParser.ParseDocument(cupl);
            var equation = doc.Equations[0];

            var expression = equation.Expression.InvertSymbol("Symbol1");
            expression.Symbol.Should().Be("Symbol1");
        }
    }
}