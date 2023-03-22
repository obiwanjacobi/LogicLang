namespace Jacobi.CuplLang.Ast;

internal static class AstExpressionExtensions
{
    // replaces all symbols with a !symbol.
    public static AstExpression InvertSymbol(this AstExpression expression, string symbol)
    {
        var invertSymbol = new AstExpressionInvertSymbol(symbol);
        return invertSymbol.Rewrite(expression);
    }
}
