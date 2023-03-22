using Jacobi.CuplLang.Tests.Ast;

namespace Jacobi.CuplLang.Tests
{
    public class ExpressionTests
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
            var expression = equation.Expression;

            expression.Should().NotBeNull();
        }
    }
}