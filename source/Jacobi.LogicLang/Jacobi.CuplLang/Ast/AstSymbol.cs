using System;
using System.Text.RegularExpressions;

namespace Jacobi.CuplLang.Ast;

internal struct AstSymbol
{
    private static readonly Regex _regEx = new Regex(@"\d+$", RegexOptions.RightToLeft | RegexOptions.Compiled);
    private readonly string _symbol;
    private readonly string _name;
    private readonly int? _digits;

    public AstSymbol(string name, int digits)
    {
        _name = name;
        _digits = digits;
        _symbol = $"{_name}{_digits}";
    }

    public AstSymbol(string symbol)
    { 
        _symbol = symbol;

        var match = _regEx.Match(symbol);
        if (match.Success)
        {
            _digits = Int32.Parse(match.Value);
            var nameLen = symbol.Length - match.Value.Length;
            _name = symbol[..nameLen];
        }
        else
        {
            _digits = null;
            _name = symbol;
        }
    }

    public string Value => _symbol;
    public string Name => _name;
    public int? Digits => _digits;

    public override string ToString()
        => _symbol;
}
