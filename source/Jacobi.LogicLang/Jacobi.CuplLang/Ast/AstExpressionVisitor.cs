using System;

namespace Jacobi.CuplLang.Ast;

internal abstract class AstExpressionVisitor<T>
{
    public virtual T Visit(AstExpression expression)
        => expression.Kind switch
            {
                AstExpressionKind.Symbol => VisitSymbol(expression),
                AstExpressionKind.Number => VisitNumber(expression),
                AstExpressionKind.BinOperator => VisitBinaryOperator(expression),
                AstExpressionKind.UniOperator => VisitUnaryOperator(expression),
                _ => throw new InvalidOperationException("Invalid AstExpression.Kind."),
            };

    protected virtual T VisitUnaryOperator(AstExpression expression)
        => Visit(expression.Left!);

    protected virtual T VisitBinaryOperator(AstExpression expression)
    {
        var left = Visit(expression.Left!);
        var right = Visit(expression.Right!);

        return left;
    }

    protected virtual T VisitNumber(AstExpression expression)
    {
        return default(T);
    }

    protected virtual T VisitSymbol(AstExpression expression)
    {
        return default(T);
    }
}
