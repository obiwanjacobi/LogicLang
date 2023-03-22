using Antlr4.Runtime;
using Jacobi.CuplLang.Parser;
using static Jacobi.CuplLang.Parser.CuplParser;

namespace Jacobi.CuplLang;

internal class Compiler
{
    private readonly List<Diagnostic> _diagnostics = new();

    public IReadOnlyList<Diagnostic> Diagnostics
        => _diagnostics;

    public FileContext Parse(string cupl, string sourceName = "")
    {
        var inputStream = new AntlrInputStream(cupl)
        {
            name = sourceName
        };
        var lexer = new CuplLexer(inputStream);
        var tokens = new CommonTokenStream(lexer);
        var parser = new CuplParser(tokens);

        var context = parser.file();
        return context;
    }
}
