namespace Jacobi.CuplLang.Ast;

internal static class AstExpressionExtensions
{
    // replaces all symbols with a !symbol.
    public static AstExpression InvertSymbol(this AstExpression expression, string symbol)
        => new AstExpressionSymbolInvert(symbol).Rewrite(expression);

    // checks if symbol is used in expression
    public static bool Contains(this AstExpression expression, string symbol)
        => new AstExpressionSymbolExists(symbol).Visit(expression);
}
