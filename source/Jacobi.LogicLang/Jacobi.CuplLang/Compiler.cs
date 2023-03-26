using System;
using System.Collections.Generic;
using System.IO;
using Antlr4.Runtime;
using Jacobi.CuplLang.Ast;
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
        parser.AddErrorListener(new DiagnosticErrorListener(_diagnostics));
#if DEBUG
        parser.AddErrorListener(new Antlr4.Runtime.DiagnosticErrorListener());
#endif
        // TODO: Error handling
        var context = parser.file();
        return context;
    }

    internal AstDocument BuildAst(FileContext fileCtx)
        => new AstBuilder().File(fileCtx);

    internal Placement.Placement DoPlacement(AstDocument astDoc)
    {
        return null!;
    }

    internal void GenerateJedec(Placement.Placement placement, string output)
    {
        
    }
}

internal sealed class DiagnosticErrorListener : IAntlrErrorListener<IToken>
{
    private readonly List<Diagnostic> _diagnostics;

    public DiagnosticErrorListener(List<Diagnostic> diagnostics)
        => _diagnostics = diagnostics;

    public void SyntaxError(TextWriter output, IRecognizer recognizer,
        IToken offendingSymbol, int line, int charPositionInLine, string msg,
        RecognitionException e)
    {
        _diagnostics.Add(new Diagnostic(line, charPositionInLine,
            $"Syntax Error ({e?.GetType().Name}): {msg} ({line}:{charPositionInLine}).\nToken: {offendingSymbol})"));
    }
}