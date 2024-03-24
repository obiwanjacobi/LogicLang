using Jacobi.CuplLang.Ast;

namespace Jacobi.CuplLang.IR;

internal sealed class IrEquation
{
    public IrEquation(string symbol, SymbolExtension extension, IrExpression expression)
    {
        Symbol = symbol;
        Extension = extension;
        Expression = expression;
    }

    public string Symbol { get; }
    public SymbolExtension Extension { get; }
    public IrExpression Expression { get; }
}
