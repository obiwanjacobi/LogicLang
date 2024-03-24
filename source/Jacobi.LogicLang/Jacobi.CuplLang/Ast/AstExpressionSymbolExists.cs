namespace Jacobi.CuplLang.Ast;

internal sealed class AstExpressionSymbolExists : AstExpressionVisitor<bool>
{
    private readonly string _symbol;

    public AstExpressionSymbolExists(string symbolToFind)
        => _symbol = symbolToFind;

    protected override bool VisitBinaryOperator(AstExpression expression)
        => Visit(expression.Left!) || Visit(expression.Right!);

    protected override bool VisitSymbol(AstExpression expression)
        => expression.Symbol == _symbol;
}
