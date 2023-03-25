using System.Collections.Generic;

namespace Jacobi.CuplLang.Ast;

internal class AstEquationAppender
{
    private readonly List<Diagnostic> _diagnostics = new();
    private readonly Dictionary<string, AstEquation> _equationMap = new();

    public IReadOnlyList<Diagnostic> Diagnostics => _diagnostics;
    public IReadOnlyCollection<AstEquation> Equations => _equationMap.Values;

    public void AppendAll(IEnumerable<AstEquation> equations)
    {
        foreach (var eq in equations)
        {
            Append(eq);
        }
    }

    public void Append(AstEquation equation)
    {
        if (!_equationMap.TryAdd(equation.Symbol, equation))
        {
            if (equation.Append)
            {
                var eq = _equationMap[equation.Symbol];
                eq.Expression = AstExpression.FromOperator(
                    eq.Expression, AstOperator.Or, equation.Expression);
            }
            else
            {
                _diagnostics.Add(new Diagnostic(
                    $"The symbol '{equation.Symbol}' is defined multiple times. Did you forget to use 'Append'?"));
            }
        }
    }
}
