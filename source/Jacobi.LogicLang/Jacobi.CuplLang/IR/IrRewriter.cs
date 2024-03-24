using System;

namespace Jacobi.CuplLang.IR;

internal abstract class IrRewriter
{
    protected virtual IrEquation RewriteEquation(IrEquation equation)
    {
        var expression = RewriteExpression(equation.Expression);

        if (expression == equation.Expression)
            return equation;

        return new IrEquation(equation.Symbol, equation.Extension, expression);
    }

    protected virtual IrExpression RewriteExpression(IrExpression expression)
    {
        return expression switch
        {
            IrExpressionBinary binExpr => RewriteExpressionBinary(binExpr),
            IrExpressionLiteral litExpr => RewriteExpressionLiteral(litExpr),
            IrExpressionSymbol symExpr => RewriteExpressionSymbol(symExpr),
            IrExpressionUnary uniExpr => RewriteExpressionUnary(uniExpr),
            _ => throw new NotSupportedException()
        };
    }

    protected virtual IrExpression RewriteExpressionBinary(IrExpressionBinary expression)
    {
        var left = RewriteExpression(expression.Left);
        var right = RewriteExpression(expression.Right);

        if (left == expression.Left &&
            right == expression.Right)
            return expression;

        return new IrExpressionBinary(left, expression.Operator, right);
    }

    protected virtual IrExpression RewriteExpressionUnary(IrExpressionUnary expression)
    {
        var notExpr = RewriteExpression(expression.Expression);

        if (notExpr == expression.Expression)
            return expression;

        return new IrExpressionUnary(notExpr);
    }

    protected virtual IrExpression RewriteExpressionSymbol(IrExpressionSymbol expression)
    {
        return expression;
    }

    protected virtual IrExpression RewriteExpressionLiteral(IrExpressionLiteral expression)
    {
        return expression;
    }
}
