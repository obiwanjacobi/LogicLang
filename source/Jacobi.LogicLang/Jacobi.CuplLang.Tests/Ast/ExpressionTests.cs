using Jacobi.CuplLang.Ast;

namespace Jacobi.CuplLang.Tests.Ast
{
    public class ExpressionTests
    {
        [Fact]
        public void Number_Binary()
        {
            const string cupl =
                "Device G22V10;" +
                "x = 'b'10;"
                ;

            var doc = CuplParser.ParseDocument(cupl);

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

            var doc = CuplParser.ParseDocument(cupl);

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

            var doc = CuplParser.ParseDocument(cupl);

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

            var doc = CuplParser.ParseDocument(cupl);

            var equation = doc.Equations[0];
            var expression = equation.Expression;

            expression.Number.Value.Should().Be(255);
        }
    }
}