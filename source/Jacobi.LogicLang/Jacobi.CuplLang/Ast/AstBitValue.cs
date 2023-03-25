using System;
using System.Collections.Specialized;

namespace Jacobi.CuplLang.Ast
{
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
}