namespace Jacobi.CuplLang.Ast;

internal sealed class AstExpressionFindSymbol : AstExpressionVisitor<bool>
{
    private readonly string _symbol;

    public AstExpressionFindSymbol(string symbolToFind)
        => _symbol = symbolToFind;

    protected override bool VisitBinaryOperator(AstExpression expression)
        => Visit(expression.Left!) || Visit(expression.Right!);

    protected override bool VisitSymbol(AstExpression expression)
        => expression.Symbol == _symbol;
}

internal static class FindSymbolExtension
{
    public static bool Find(this AstExpression expression, string symbol)
        => new AstExpressionFindSymbol(symbol).Visit(expression);
}