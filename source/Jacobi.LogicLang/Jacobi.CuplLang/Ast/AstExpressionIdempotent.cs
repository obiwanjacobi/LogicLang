namespace Jacobi.CuplLang.Ast;

internal class AstExpressionIdempotent : AstExpressionRewriter
{
    protected override AstExpression RewriteBinaryOperator(AstExpression expression)
    {
        if ((expression.Operator is AstOperator.And or AstOperator.Or) &&
            expression.Left!.Kind == AstExpressionKind.Symbol &&
            expression.Right!.Kind == AstExpressionKind.Symbol &&
            expression.Left.Symbol == expression.Right.Symbol)
        {
            return expression.Left;
        }

        return base.RewriteBinaryOperator(expression);
    }
}
