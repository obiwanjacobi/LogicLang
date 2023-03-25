namespace Jacobi.CuplLang.Ast;

internal class AstExpressionSorter : AstExpressionRewriter
{
    public AstExpression Sort(AstExpression expression)
    {
        var expr = Rewrite(expression);

        while (OperationsPerformed > 0)
        {
            OperationsPerformed = 0;
            expr = Rewrite(expr);
        }

        return expr;
    }

    protected override AstExpression RewriteBinaryOperator(AstExpression expression)
    {
        var result = base.RewriteBinaryOperator(expression);

        if (AstExpressionLaws.TryCommunatativeSort(result, out var sorted))
            return sorted;

        return result;
    }
}
