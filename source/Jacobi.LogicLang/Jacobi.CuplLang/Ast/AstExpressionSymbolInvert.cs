namespace Jacobi.CuplLang.Ast;

internal sealed class AstExpressionSymbolInvert : AstExpressionRewriter
{
    private readonly string _symbol;

    public AstExpressionSymbolInvert(string symbol)
        => _symbol = symbol;

    protected override AstExpression RewriteUnaryOperator(AstExpression expression)
    {
        var expr = base.RewriteUnaryOperator(expression);

        // remove double not operators
        if (expr.Operator == AstOperator.Not &&
            expr.Left!.Operator == AstOperator.Not)
        {
            return expr.Left!.Left!;
        }

        return expr;
    }

    protected override AstExpression RewriteSymbol(AstExpression expression)
    {
        if (expression.Symbol == _symbol)
        {
            return AstExpression.FromOperator(expression, AstOperator.Not);
        }

        return expression;
    }
}
