using Jacobi.CuplLang.Ast;
using Xunit.Abstractions;
using static Jacobi.CuplLang.Parser.CuplParser;

namespace Jacobi.CuplLang.Tests.Ast
{
    internal static class CuplParser
    {
        public static FileContext ParseFile(string cupl, ITestOutputHelper? output = null)
        {
            var compiler = new Compiler();
            var ctx = compiler.Parse(cupl, nameof(HeaderTests));
            if (output is not null)
                FailIfDiagnostics(compiler.Diagnostics, output);
            return ctx;
        }

        public static AstDocument ParseDocument(string cupl, ITestOutputHelper? output = null)
        {
            var ctx = ParseFile(cupl, output);
            var builder = new AstBuilder();
            var doc = builder.File(ctx);
            if (output is not null)
                FailIfDiagnostics(doc.Diagnostics, output);
            return doc;
        }

        public static void FailIfDiagnostics(AstDocument document, ITestOutputHelper? output = null)
            => FailIfDiagnostics(document.Diagnostics, output);

        public static void FailIfDiagnostics(IReadOnlyCollection<Diagnostic> diagnostics, ITestOutputHelper? output = null)
        {
            if (output is not null)
            {
                foreach (var diag in diagnostics)
                {
                    output.WriteLine($"{diag.Message} ({diag.Location})");
                }
            }

            diagnostics.Count.Should().Be(0);
        }
    }
}
