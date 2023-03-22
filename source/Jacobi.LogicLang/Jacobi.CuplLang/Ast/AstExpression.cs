using System.Collections.Specialized;

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

    internal struct AstBitValue
    {
        private readonly BitVector32 _value;

        public AstBitValue(int value)
            => _value = new BitVector32(value);

        public static AstBitValue FromDontCareNumber(string value)
        {
            throw new NotImplementedException();
        }

        public static AstBitValue FromBinary(string value)
            => new(Convert.ToInt32(value, 2));

        public static AstBitValue FromDecimal(string value)
            => new(Convert.ToInt32(value, 10));

        public static AstBitValue FromOctal(string value)
            => new(Convert.ToInt32(value, 8));

        public static AstBitValue FromHex(string value)
            => new(Convert.ToInt32(value, 16));

        public int Value
            => _value.Data;

        public bool this[int index]
            => _value[index];

        public override string ToString()
            => _value.ToString();
    }

    internal class AstExpression
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
    }
}