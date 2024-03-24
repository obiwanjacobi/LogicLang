using System;
using System.Collections.Generic;

namespace Jacobi.CuplLang.Ast;

/// <summary>
/// Extracts all 'OR' operands.
/// </summary>
internal sealed class AstExpressionSolver : AstExpressionVisitor<object?>
{
    private readonly List<AstExpression> _orExpressions = new();

    public IReadOnlyList<AstExpression> Solve(AstEquation equation)
    {
        if (equation.Expression.Operator == AstOperator.And)
            throw new ArgumentException("Cannot solve equations that have an AND operator at the root of the Expression tree.");

        var expression = (AstExpression)Visit(equation.Expression)!;

        if (_orExpressions.Count == 0)
            return [expression];

        return _orExpressions;
    }

    protected override object? VisitBinaryOperator(AstExpression expression)
    {
        if (expression.Operator == AstOperator.Or)
        {
            var left = (AstExpression?)Visit(expression.Left!);
            var right = (AstExpression?)Visit(expression.Right!);

            if (left is not null)
                _orExpressions.Add(left);
            if (right is not null)
                _orExpressions.Add(right);

            return null;
        }

        return expression;
    }
}
