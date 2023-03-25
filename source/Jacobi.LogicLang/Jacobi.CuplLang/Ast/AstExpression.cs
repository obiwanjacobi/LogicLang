using System;
using System.Text;

namespace Jacobi.CuplLang.Ast
{
    internal enum AstOperator
    {
        None,
        And,
        Or,
        Xor,
        Not
    }

    internal enum AstExpressionKind
    {
        None,
        Symbol,
        Number,
        BinOperator,
        UniOperator
    }

    internal class AstExpression : IEquatable<AstExpression>
    {
        private AstExpression(AstExpressionKind kind)
            => Kind = kind;

        public static AstExpression Empty => new(AstExpressionKind.None);

        public static AstExpression FromNumber(AstBitValue value)
            => new(AstExpressionKind.Number) { Number = value };
        public static AstExpression FromOperator(AstExpression left, AstOperator op, AstExpression right)
        {
            if (op is not AstOperator.And and not AstOperator.Or and not AstOperator.Xor)
                throw new ArgumentException($"Operator '{op}' is not a binary operator.", nameof(op));
            return new(AstExpressionKind.BinOperator) { Left = left, Operator = op, Right = right };
        }
        public static AstExpression FromOperator(AstExpression expression, AstOperator op)
        {
            if (op is not AstOperator.Not)
                throw new ArgumentException($"Operator '{op}' is not a unary operator.", nameof(op));
            return new(AstExpressionKind.UniOperator) { Left = expression, Operator = op };
        }
        public static AstExpression FromSymbol(string symbol)
            => new(AstExpressionKind.Symbol) { Symbol = symbol };

        public AstExpressionKind Kind { get; }
        public bool Precedence { get; set; }

        // set when binary or unary operator expr
        public AstExpression? Left { get; init;}
        // set when binary operator expr
        public AstExpression? Right { get; init; }
        // set when binary or unary operator expr
        public AstOperator Operator { get; init; }

        // set when Symbol expr
        public string? Symbol { get; init; }
        // set when Number expr
        public AstBitValue Number { get; init; }

        public override string ToString()
        {
            var txt = Kind switch
            {
                AstExpressionKind.Symbol => Symbol!,
                AstExpressionKind.Number => Number.ToString(),
                AstExpressionKind.UniOperator => UniOperatorToString(),
                AstExpressionKind.BinOperator => BinOperatorToString(),
                _ => "<Invalid Expression Kind>"
            };

            if (Precedence)
                return $"({txt})";
            
            return txt;
        }

        private string BinOperatorToString()
            => new StringBuilder()
                .Append(Left!.ToString())
                .Append(OperatorToString())
                .Append(Right!.ToString())
                .ToString();

        private string UniOperatorToString()
            => new StringBuilder()
                .Append(OperatorToString())
                .Append(Left!.ToString())
                .ToString();

        private string OperatorToString()
            => Operator switch
            {
                AstOperator.And => "&",
                AstOperator.Not => "!",
                AstOperator.Or => "#",
                AstOperator.Xor => "$",
                _ => String.Empty
            };

        public bool Equals(AstExpression? other)
        {
            if (other is null) return false;

            if (Kind == other.Kind)
            {
                return Kind switch
                {
                    AstExpressionKind.Symbol => Symbol!.Equals(other.Symbol),
                    AstExpressionKind.Number => Number.Equals(other.Number),
                    AstExpressionKind.BinOperator => Operator == other.Operator &&
                        Left!.Equals(other.Left) && Right!.Equals(other.Right),
                    AstExpressionKind.UniOperator => Operator == other.Operator && 
                        Left!.Equals(other.Left),
                    _ => false,
                };
            }

            return false;
        }
    }
}