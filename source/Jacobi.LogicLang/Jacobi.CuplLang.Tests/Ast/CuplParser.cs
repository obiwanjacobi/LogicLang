using Jacobi.CuplLang.Ast;
using static Jacobi.CuplLang.Parser.CuplParser;

namespace Jacobi.CuplLang.Tests.Ast
{
    internal static class CuplParser
    {
        public static FileContext ParseFile(string cupl)
        {
            var compiler = new Compiler();
            var ctx = compiler.Parse(cupl, nameof(HeaderTests));
            return ctx;
        }

        public static AstDocument ParseDocument(string cupl)
        {
            var ctx = ParseFile(cupl);
            var builder = new AstBuilder();
            var doc = builder.File(ctx);
            return doc;
        }
    }
}
