using System.Diagnostics.CodeAnalysis;

namespace Jacobi.CuplLang.Ast;

internal static class AstExpressionLaws
{
    // UniOperator
    public static bool TryDoubleNegation(AstExpression expression, [NotNullWhen(true)] out AstExpression? result)
    {
        // remove double not operators
        if (expression.Kind == AstExpressionKind.UniOperator &&
            expression.Operator == AstOperator.Not &&
            expression.Left!.Operator == AstOperator.Not)
        {
            result = expression.Left!.Left!;
            return true;
        }

        result = null;
        return false;
    }

    // BinOperator
    public static bool TryIdempotent(AstExpression expression, [NotNullWhen(true)] out AstExpression? result)
    {
        if (IsBinaryLawOperator(expression) &&
            expression.Left!.ToString() == expression.Right!.ToString())
        {
            result = expression.Left;
            return true;
        }

        result = null;
        return false;
    }

    // BinOperator
    public static bool TryComplement(AstExpression expression, [NotNullWhen(true)] out AstExpression? result)
    {
        if (expression.IsBinaryLawOperator())
        {
            var value = expression.Operator == AstOperator.And ? 0 : 1;

            AstExpression? notResult;
            if (TrySkipNotExpression(expression.Left!, out notResult))
            {
                if (expression.Right!.Equals(notResult))
                {
                    result = AstExpression.FromNumber(new(value));
                    return true;
                }
            }
            else if (TrySkipNotExpression(expression.Right!, out notResult))
            {
                if (expression.Left!.Equals(notResult))
                {
                    result = AstExpression.FromNumber(new(value));
                    return true;
                }
            }
        }

        result = null;
        return false;
    }

    public static bool TryCommunatativeSwap(AstExpression expression, [NotNullWhen(true)] out AstExpression? result)
    {
        if (expression.Kind == AstExpressionKind.BinOperator)
        {
            result = AstExpression.FromOperator(expression.Right!, expression.Operator, expression.Left!);
            return true;
        }

        result = null;
        return false;
    }

    public static bool TryCommunatativeSort(AstExpression expression, [NotNullWhen(true)] out AstExpression? result)
    {
        if (expression.Kind == AstExpressionKind.BinOperator &&
            expression.Left!.IsSymbol() && expression.Right!.IsSymbol() &&
            String.Compare(expression.Left!.Symbol, expression.Right!.Symbol) > 0)
        {
            result = AstExpression.FromOperator(expression.Right!, expression.Operator, expression.Left!);
            return true;
        }

        result = null;
        return false;
    }

    public static bool TrySkipNotExpression(AstExpression expression, [NotNullWhen(true)] out AstExpression? result)
    {
        if (expression.Operator == AstOperator.Not)
        {
            result = expression.Left!;
            return true;
        }

        result = null;
        return false;
    }

    public static bool IsSymbol(this AstExpression expression, string? symbol = null)
        => expression.Kind == AstExpressionKind.Symbol &&
            (expression.Symbol == symbol || symbol is null);

    public static bool IsBinaryLawOperator(this AstExpression expression)
        => expression.Kind == AstExpressionKind.BinOperator && 
            expression.Operator is AstOperator.And or AstOperator.Or;

    public static bool Try_(AstExpression expression, [NotNullWhen(true)] out AstExpression? result)
    {
        if (expression.Kind == AstExpressionKind.BinOperator)
        {
            result = null;
            return true;
        }

        result = null;
        return false;
    }
}
