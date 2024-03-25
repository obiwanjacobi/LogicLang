using System;
using System.Diagnostics.CodeAnalysis;
using Jacobi.CuplLang.IR;

namespace Jacobi.CuplLang.Ir;

internal static class IrExpressionLaws
{
    // UniOperator
    public static bool TryDoubleNegation(IrExpression expression, [NotNullWhen(true)] out IrExpression? result)
    {
        if (expression is IrExpressionUnary unaryExpr)
            return TryDoubleNegation(unaryExpr, out result);

        result = null;
        return false;
    }
    public static bool TryDoubleNegation(IrExpressionUnary expression, [NotNullWhen(true)] out IrExpression? result)
    {
        // remove double not operators
        if (expression.Expression is IrExpressionUnary uniExpr)
        {
            result = uniExpr.Expression;
            return true;
        }

        result = null;
        return false;
    }

    // BinOperator
    public static bool TryIdempotent(IrExpression expression, [NotNullWhen(true)] out IrExpression? result)
    {
        if (expression is IrExpressionBinary binExpr)
            return TryIdempotent(binExpr, out result);

        result = null;
        return false;
    }
    public static bool TryIdempotent(IrExpressionBinary expression, [NotNullWhen(true)] out IrExpression? result)
    {
        if (expression.Left.Equals(expression.Right))
        {
            result = expression.Left;
            return true;
        }

        result = null;
        return false;
    }

    // BinOperator
    public static bool TryComplement(IrExpression expression, [NotNullWhen(true)] out IrExpression? result)
    {
        if (expression is IrExpressionBinary binExpr)
            return TryComplement(binExpr, out result);

        result = null;
        return false;
    }
    public static bool TryComplement(IrExpressionBinary expression, [NotNullWhen(true)] out IrExpression? result)
    {
        if (expression.Operator is IrBinaryOperator.And or IrBinaryOperator.Or)
        {
            var value = expression.Operator != IrBinaryOperator.And;

            IrExpression? notResult;
            if (TrySkipNotExpression(expression.Left, out notResult))
            {
                if (expression.Right.Equals(notResult))
                {
                    result = new IrExpressionLiteral(value);
                    return true;
                }
            }
            else if (TrySkipNotExpression(expression.Right, out notResult))
            {
                if (expression.Left.Equals(notResult))
                {
                    result = new IrExpressionLiteral(value);
                    return true;
                }
            }
        }

        result = null;
        return false;
    }

    public static bool TryCommunatativeSwap(IrExpression expression, [NotNullWhen(true)] out IrExpression? result)
    {
        if (expression is IrExpressionBinary binExpr)
            return TryCommunatativeSwap(binExpr, out result);

        result = null;
        return false;
    }
    public static bool TryCommunatativeSwap(IrExpressionBinary expression, [NotNullWhen(true)] out IrExpression? result)
    {
        result = new IrExpressionBinary(expression.Right, expression.Operator, expression.Left);
        return true;
    }

    public static bool TryCommunatativeSort(IrExpression expression, [NotNullWhen(true)] out IrExpression? result)
    {
        if (expression is IrExpressionBinary binExpr)
            return TryCommunatativeSort(binExpr, out result);

        result = null;
        return false;
    }
    public static bool TryCommunatativeSort(IrExpressionBinary expression, [NotNullWhen(true)] out IrExpression? result)
    {
        if (expression.Left is IrExpressionSymbol leftSymbol &&
            expression.Right is IrExpressionSymbol rightSymbol &&
            String.Compare(leftSymbol.Symbol, rightSymbol.Symbol) > 0)
        {
            result = new IrExpressionBinary(expression.Right, expression.Operator, expression.Left);
            return true;
        }

        result = null;
        return false;
    }

    public static IrExpression CommunatativeSort(IrExpression expression)
    {
        if (TryCommunatativeSort(expression, out var result))
            return result;
        
        return expression;
    }

    public static bool TrySkipNotExpression(IrExpression expression, [NotNullWhen(true)] out IrExpression? result)
    {
        if (expression is IrExpressionUnary unaryExpr)
        {
            result = unaryExpr.Expression;
            return true;
        }

        result = null;
        return false;
    }

    //public static bool Try_(IrExpression expression, [NotNullWhen(true)] out IrExpression? result)
    //{
    //    if (expression is IrExpressionBinary)
    //    {
    //        result = null;
    //        return true;
    //    }
    //    result = null;
    //    return false;
    //}
}
