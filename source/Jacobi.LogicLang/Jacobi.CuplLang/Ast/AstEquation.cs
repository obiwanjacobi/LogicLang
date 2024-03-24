using System;

namespace Jacobi.CuplLang.Ast;

internal enum SymbolExtension
{
    None,
    Data,
    AsyncReset,
    SyncPreset,
    OutputEnable,
}

internal class AstEquation
{
    public bool Append { get; init; }
    public string Symbol { get; init; } = String.Empty;
    public AstPin? Pin { get; init; }
    public SymbolExtension Extension { get; init; }
    public AstExpression Expression { get; set; } = AstExpression.Empty;
}