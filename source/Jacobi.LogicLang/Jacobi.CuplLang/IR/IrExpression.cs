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
}

// implicit NOT operator
internal sealed class IrExpressionUnary : IrExpression
{
    public IrExpressionUnary(IrExpression expression)
    {
        Expression = expression;
    }

    public IrExpression Expression { get; }
}

internal sealed class IrExpressionSymbol : IrExpression
{
    public IrExpressionSymbol(string symbol)
    {
        Symbol = symbol;
    }

    public string Symbol { get; }
}

internal sealed class IrExpressionLiteral : IrExpression
{
    public IrExpressionLiteral(bool value)
    {
        Value = value;
    }

    public bool Value { get; }
}