using Jacobi.CuplLang.Ast;

namespace Jacobi.CuplLang.Tests.Ast
{
    public class EquationTests
    {
        [Fact]
        public void Symbol()
        {
            const string cupl =
                "Device G22V10;" +
                "x = !Symbol1;"
                ;

            var doc = CuplParser.ParseDocument(cupl);

            doc.Equations.Should().HaveCount(1);
            var equation = doc.Equations[0];
            equation.Symbol.Should().Be("x");
            equation.Append.Should().BeFalse();

            var expr = equation.Expression;
            expr.Should().NotBeNull();
            expr.Kind.Should().Be(AstExpressionKind.UniOperator);
            expr.Operator.Should().Be(AstOperator.Not);
            expr.Left!.Symbol.Should().Be("Symbol1");
        }

        [Fact]
        public void SymbolList()
        {
            const string cupl =
                "Device G22V10;" +
                "[x, y] = Symbol1;"
                ;

            var doc = CuplParser.ParseDocument(cupl);

            doc.Equations.Should().HaveCount(2);
            var equation = doc.Equations[0];
            equation.Symbol.Should().Be("x");
            equation.Append.Should().BeFalse();

            var expr = equation.Expression;
            expr.Should().NotBeNull();
            expr.Symbol.Should().Be("Symbol1");

            equation = doc.Equations[1];
            equation.Symbol.Should().Be("y");
            equation.Append.Should().BeFalse();

            expr = equation.Expression;
            expr.Should().NotBeNull();
            expr.Symbol.Should().Be("Symbol1");
        }

        [Fact]
        public void SymbolRange()
        {
            const string cupl =
                "Device G22V10;" +
                "[x0..2] = Symbol1;"
                ;

            var doc = CuplParser.ParseDocument(cupl);

            doc.Equations.Should().HaveCount(3);
            var equation = doc.Equations[0];
            equation.Symbol.Should().Be("x0");
            equation.Append.Should().BeFalse();

            var expr = equation.Expression;
            expr.Should().NotBeNull();
            expr.Symbol.Should().Be("Symbol1");

            equation = doc.Equations[1];
            equation.Symbol.Should().Be("x1");
            equation.Append.Should().BeFalse();

            expr = equation.Expression;
            expr.Should().NotBeNull();
            expr.Symbol.Should().Be("Symbol1");

            equation = doc.Equations[2];
            equation.Symbol.Should().Be("x2");
            equation.Append.Should().BeFalse();

            expr = equation.Expression;
            expr.Should().NotBeNull();
            expr.Symbol.Should().Be("Symbol1");
        }

        [Fact]
        public void Symbol_DuplicateDiag()
        {
            const string cupl =
                "Device G22V10;" +
                "x = !Symbol1;" +
                "x = Symbol2;"
                ;

            var doc = CuplParser.ParseDocument(cupl);
            doc.Equations.Should().HaveCount(2);
            
            var appender = new AstEquationAppender();
            appender.AppendAll(doc.Equations);
            appender.Diagnostics.Should().HaveCount(1);
            appender.Equations.Should().HaveCount(1);
        }

        [Fact]
        public void Symbol_Append()
        {
            const string cupl =
                "Device G22V10;" +
                "Append x = !Symbol1;" +
                "Append x = Symbol2;"
                ;

            var doc = CuplParser.ParseDocument(cupl);
            doc.Equations.Should().HaveCount(2);

            var appender = new AstEquationAppender();
            appender.AppendAll(doc.Equations);
            appender.Diagnostics.Should().HaveCount(0);
            appender.Equations.Should().HaveCount(1);
        }
    }
}