using System;
using System.Collections.Generic;
using System.Linq;
using Jacobi.CuplLang.Ast;

namespace Jacobi.CuplLang.IR;

internal sealed class IrBuilder
{
    private readonly IReadOnlyList<AstPin> _pins;

    public IrBuilder(IReadOnlyList<AstPin> pins)
    {
        _pins = pins;
    }

    // all terms are appended (Append)
    // all (intermediate) terms are expanded
    // all xor operators are expanded
    // no Number expressions
    public IrEquation Build(AstEquation equation)
    {
        var expression = OnExpression(equation.Expression);
        var symbol = equation.Symbol!;
        
        var pin = _pins.Single(p => p.Symbol == symbol);
        if (pin.Inverted)
            expression = new IrExpressionUnary(expression);

        return new(symbol, equation.Extension, expression);
    }

    private IrExpression OnExpression(AstExpression expression)
    {
        return expression.Kind switch
        {
            AstExpressionKind.Symbol => OnExpressionSymbol(expression),
            AstExpressionKind.UniOperator => OnExpressionUnary(expression),
            AstExpressionKind.BinOperator => OnExpressionBinary(expression),
            _ => throw new NotSupportedException(),
        };
    }

    private IrExpression OnExpressionBinary(AstExpression expression)
    {
        var left = OnExpression(expression.Left!);
        var right = OnExpression(expression.Right!);
        var op = OnExpressionBinaryOperator(expression.Operator);
        
        return new IrExpressionBinary(left, op, right);
    }

    private IrBinaryOperator OnExpressionBinaryOperator(AstOperator @operator)
    {
        return @operator switch
        {
            AstOperator.And => IrBinaryOperator.And,
            AstOperator.Or => IrBinaryOperator.Or,
            _ => throw new NotSupportedException()
        };
    }

    private IrExpression OnExpressionUnary(AstExpression expression)
    {
        var notExpr = OnExpression(expression.Left!);
        return new IrExpressionUnary(notExpr);
    }

    private IrExpression OnExpressionSymbol(AstExpression expression)
    {
        var symbol = expression.Symbol!;
        var symbolExpr = new IrExpressionSymbol(symbol);
        var pin = _pins.Single(p => p.Symbol == symbol);

        return pin.Inverted
            ? new IrExpressionUnary(symbolExpr)
            : symbolExpr;
    }
}
