using System;
using System.Collections.Generic;
using System.Linq;
using Jacobi.CuplLang.Ast;

namespace Jacobi.CuplLang.Placement
{
    internal class Evaluator
    {
        private readonly List<string> _inputSymbols = new();
        private readonly Dictionary<string, ExpressionNode> _outputs = new();

        public Evaluator(IEnumerable<AstEquation> equations)
        {
            foreach (var equation in equations)
            {
                var expr = MakeExpressionNode(equation.Expression);
                _outputs[equation.Symbol] = expr;
            }

            foreach (var key in _outputs.Keys)
            {
                _inputSymbols.Remove(key);
            }
        }

        private ExpressionNode MakeExpressionNode(AstExpression astExpression)
        {
            switch (astExpression.Kind)
            {
                case AstExpressionKind.BinOperator:
                    return new ExpressionBinary(MakeExpressionNode(astExpression.Left!), astExpression.Operator, MakeExpressionNode(astExpression.Right!));
                case AstExpressionKind.UniOperator:
                    return new ExpressionUnary(MakeExpressionNode(astExpression.Left!));
                case AstExpressionKind.Number:
                    return new ExpressionValue(astExpression.Number[0]);
                case AstExpressionKind.Symbol:
                    if (!_inputSymbols.Contains(astExpression.Symbol!))
                        _inputSymbols.Add(astExpression.Symbol!);
                    return new ExpressionSymbol(astExpression.Symbol!, _outputs);
                default:
                    throw new NotSupportedException();
            }
        }

        public IReadOnlyCollection<string> Inputs
            => _inputSymbols;

        public IReadOnlyCollection<string> Outputs
            => _outputs.Keys;

        public Dictionary<string, bool> NewInputs(bool initialValue = false)
            => _inputSymbols.ToDictionary(i => i, _ => initialValue);

        public Dictionary<string, bool> Eval(Dictionary<string, bool> inputs)
        {
            var outputs = new Dictionary<string, bool>();

            foreach (var kvp in _outputs)
            {
                outputs[kvp.Key] = kvp.Value.Eval(inputs);
            }

            return outputs;
        }

        private abstract class ExpressionNode
        {
            public abstract bool Eval(Dictionary<string, bool> inputs);
        }

        private sealed class ExpressionBinary : ExpressionNode
        {
            public ExpressionBinary(ExpressionNode left, AstOperator op, ExpressionNode right)
            {
                Left = left;
                Operator = op;
                Right = right;
            }

            public ExpressionNode Right { get; }
            public ExpressionNode Left { get; }
            public AstOperator Operator { get; }

            public override bool Eval(Dictionary<string, bool> inputs)
            {
                var leftValue = Left.Eval(inputs);
                var rightValue = Right.Eval(inputs);

                return Operator switch
                {
                    AstOperator.And => leftValue && rightValue,
                    AstOperator.Or => leftValue || rightValue,
                    AstOperator.Xor => (leftValue || rightValue) && !(leftValue && rightValue),
                    _ => throw new NotSupportedException()
                };
            }
        }

        private sealed class ExpressionUnary : ExpressionNode
        {
            public ExpressionUnary(ExpressionNode expression)
                => Expression = expression;

            public ExpressionNode Expression { get; }

            public override bool Eval(Dictionary<string, bool> inputs)
                => !Expression.Eval(inputs);
        }

        private sealed class ExpressionValue : ExpressionNode
        {
            public ExpressionValue(bool value)
                => Value = value;

            public bool Value {  get; }

            public override bool Eval(Dictionary<string, bool> inputs)
                => Value;
        }

        private sealed class ExpressionSymbol : ExpressionNode
        {
            private readonly IDictionary<string, ExpressionNode> _outputs;

            public ExpressionSymbol(string symbol, IDictionary<string, ExpressionNode> outputs)
            {
                Symbol = symbol;
                _outputs = outputs;
            }

            public string Symbol { get; }
            
            public override bool Eval(Dictionary<string, bool> inputs)
            {
                if (_outputs.ContainsKey(Symbol))
                    return _outputs[Symbol].Eval(inputs);

                return inputs[Symbol];
            }
        }
    }
}
