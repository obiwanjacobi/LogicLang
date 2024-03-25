using static Antlr4.Runtime.Atn.SemanticContext;

namespace Jacobi.CuplLang.IR;

internal abstract class IrExpression
{ }

internal enum IrBinaryOperator
{
    None,
    And,
    Or
}

internal sealed class IrExpressionBinary : IrExpression
{
    public IrExpressionBinary(IrExpression left, IrBinaryOperator @operator, IrExpression right)
    {
        Left = left;
        Operator = @operator;
        Right = right;
    }

    public IrExpression Left { get; }
    public IrExpression Right { get; }
    public IrBinaryOperator Operator { get; }

    public override string ToString()
        => $"({Left}) {Operator} ({Right})";
}

// implicit NOT operator
internal sealed class IrExpressionUnary : IrExpression
{
    public IrExpressionUnary(IrExpression expression)
    {
        Expression = expression;
    }

    public IrExpression Expression { get; }

    public override string ToString()
        => $"Not ({Expression})";
}

internal sealed class IrExpressionSymbol : IrExpression
{
    public IrExpressionSymbol(string symbol)
    {
        Symbol = symbol;
    }

    public string Symbol { get; }

    public override string ToString()
        => Symbol;
}

internal sealed class IrExpressionLiteral : IrExpression
{
    public IrExpressionLiteral(bool value)
    {
        Value = value;
    }

    public bool Value { get; }

    public override string ToString()
        => Value ? "1" : "0";
}