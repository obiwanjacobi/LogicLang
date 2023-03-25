namespace Jacobi.CuplLang.Ast;

internal class AstExpressionDoubleNegation : AstExpressionRewriter
{
    protected override AstExpression RewriteUnaryOperator(AstExpression expression)
    {
        // remove double not operators
        if (expression.Operator == AstOperator.Not &&
            expression.Left!.Operator == AstOperator.Not)
        {
            return expression.Left!.Left!;
        }

        return base.RewriteUnaryOperator(expression);
    }
}
