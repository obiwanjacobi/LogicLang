using System;

namespace Jacobi.CuplLang.Ast;

internal class AstPin
{
    public int PinNumber { get; internal set; }
    public string Symbol { get; internal set; } = String.Empty;
    public bool Inverted { get; internal set; }
}
