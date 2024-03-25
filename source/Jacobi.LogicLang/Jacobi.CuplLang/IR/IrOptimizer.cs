using System;
using Jacobi.CuplLang.Ir;

namespace Jacobi.CuplLang.IR;

internal class IrOptimizer : IrRewriter
{
    public IrEquation Optimize(IrEquation irEq)
        => RewriteEquation(irEq);

    protected override IrExpression RewriteExpressionUnary(IrExpressionUnary expression)
    {
        var notExpr = RewriteExpression(expression.Expression);

        if (IrExpressionLaws.TryDoubleNegation(expression, out var result))
            return result;

        if (notExpr == expression.Expression)
            return expression;

        return new IrExpressionUnary(notExpr);
    }
}
