using System.Collections.Generic;
using System.Linq;

namespace Jacobi.CuplLang.Ast;

/// <summary>
/// Expands the equations intermediate symbols and the Xor operator into And/Or operators.
/// Removes equations for intermediate terms (equations without a pin).
/// </summary>
internal sealed class AstExpressionExpander : AstExpressionRewriter
{
    private readonly IReadOnlyList<AstEquation> _equations;

    public AstExpressionExpander(IReadOnlyList<AstEquation> equations)
    {
        _equations = equations;
    }

    public IReadOnlyList<AstEquation> ExpandAll()
        => _equations
            .Where(e => e.Pin is not null)
            .Select(Expand)
            .ToList();

    private AstEquation Expand(AstEquation equation)
    {
        var expression = Rewrite(equation.Expression);
        
        if (expression == equation.Expression)
            return equation;

        return new AstEquation
        {
            Append = equation.Append,
            Expression = expression,
            Extension = equation.Extension,
            Symbol = equation.Symbol
        };
    }

    protected override AstExpression RewriteBinaryOperator(AstExpression expression)
    {
        // a xor b == (~A and B) or (A and ~B)
        if (expression.Operator == AstOperator.Xor)
        {
            var left = expression.Left!;
            var right = expression.Right!;
            var notLeft = AstExpression.FromOperator(left, AstOperator.Not);
            var notRight = AstExpression.FromOperator(right, AstOperator.Not);
            var newLeft = AstExpression.FromOperator(notLeft, AstOperator.And, right);
            var newRight = AstExpression.FromOperator(left, AstOperator.And, notRight);
            return AstExpression.FromOperator(newLeft, AstOperator.Or, newRight);
        }

        return base.RewriteBinaryOperator(expression);
    }

    protected override AstExpression RewriteSymbol(AstExpression expression)
    {
        // only expand equations that do not represent output pins
        var equation = _equations.SingleOrDefault(e => e.Symbol == expression.Symbol && e.Pin is null);
        return equation is null
            ? expression
            : Rewrite(equation.Expression);
    }
}
