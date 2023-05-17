using System;
using System.Diagnostics;

namespace Jacobi.CuplLang.Ast;

[DebuggerDisplay("{Symbol}({PinNumber})")]
internal class AstPin
{
    public int PinNumber { get; internal set; }
    public string Symbol { get; internal set; } = String.Empty;
    public bool Inverted { get; internal set; }
}
