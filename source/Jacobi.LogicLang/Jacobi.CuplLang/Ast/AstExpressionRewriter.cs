using Antlr4.Runtime.Tree;

namespace Jacobi.CuplLang.Ast;

internal abstract class AstExpressionRewriter
{
    public int OperationsPerformed { get; protected set; }

    public virtual AstExpression Rewrite(AstExpression expression)
    {
        var expr = expression.Kind switch
        {
            AstExpressionKind.Symbol => RewriteSymbol(expression),
            AstExpressionKind.Number => RewriteNumber(expression),
            AstExpressionKind.BinOperator => RewriteBinaryOperator(expression),
            AstExpressionKind.UniOperator => RewriteUnaryOperator(expression),
            _ => throw new InvalidOperationException("Invalid AstExpression.Kind."),
        };
        return expr;
    }

    protected virtual AstExpression RewriteUnaryOperator(AstExpression expression)
    {
        var left = Rewrite(expression.Left!);

        if (left == expression.Left)
            return expression;

        OperationsPerformed++;
        return AstExpression.FromOperator(left, AstOperator.Not);
    }

    protected virtual AstExpression RewriteBinaryOperator(AstExpression expression)
    {
        var left = Rewrite(expression.Left!);
        var right = Rewrite(expression.Right!);

        if (left == expression.Left && right == expression.Right)
            return expression;

        OperationsPerformed++;
        return AstExpression.FromOperator(left, expression.Operator, right);
    }

    protected virtual AstExpression RewriteNumber(AstExpression expression)
    {
        return expression;
    }

    protected virtual AstExpression RewriteSymbol(AstExpression expression)
    {
        return expression;
    }
}
