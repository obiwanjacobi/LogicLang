using System;
using System.Diagnostics.CodeAnalysis;
using Jacobi.CuplLang.IR;

namespace Jacobi.CuplLang.Ir;

internal static class IrExpressionLaws
{
    // UniOperator
    public static bool TryDoubleNegation(IrExpression expression, [NotNullWhen(true)] out IrExpression? result)
    {
        // remove double not operators
        if (expression is IrExpressionUnary unaryExpr &&
            unaryExpr.Expression is IrExpressionUnary uniExprChild)
        {
            result = uniExprChild.Expression;
            return true;
        }

        result = null;
        return false;
    }

    // BinOperator
    public static bool TryIdempotent(IrExpression expression, [NotNullWhen(true)] out IrExpression? result)
    {
        if (expression is IrExpressionBinary binExpr &&
            binExpr.Left.Equals(binExpr.Right))
        {
            result = binExpr.Left;
            return true;
        }

        result = null;
        return false;
    }

    // BinOperator
    public static bool TryComplement(IrExpression expression, [NotNullWhen(true)] out IrExpression? result)
    {
        if (expression is IrExpressionBinary binExpr &&
            binExpr.Operator is IrBinaryOperator.And or IrBinaryOperator.Or)
        {
            var value = binExpr.Operator != IrBinaryOperator.And;

            IrExpression? notResult;
            if (TrySkipNotExpression(binExpr.Left, out notResult))
            {
                if (binExpr.Right.Equals(notResult))
                {
                    result = new IrExpressionLiteral(value);
                    return true;
                }
            }
            else if (TrySkipNotExpression(binExpr.Right, out notResult))
            {
                if (binExpr.Left.Equals(notResult))
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
        {
            result = new IrExpressionBinary(binExpr.Right!, binExpr.Operator, binExpr.Left!);
            return true;
        }

        result = null;
        return false;
    }

    public static bool TryCommunatativeSort(IrExpression expression, [NotNullWhen(true)] out IrExpression? result)
    {
        if (expression is IrExpressionBinary binExpr &&
            binExpr.Left is IrExpressionSymbol leftSymbol &&
            binExpr.Right is IrExpressionSymbol rightSymbol &&
            String.Compare(leftSymbol.Symbol, rightSymbol.Symbol) > 0)
        {
            result = new IrExpressionBinary(binExpr.Right, binExpr.Operator, binExpr.Left);
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
