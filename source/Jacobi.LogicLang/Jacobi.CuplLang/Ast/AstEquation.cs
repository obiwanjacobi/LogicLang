using System;

namespace Jacobi.CuplLang.Ast;

internal class AstEquation
{
    public bool Append { get; set; }
    public string Symbol { get; set; } = String.Empty;
    public AstExpression Expression { get; set; } = AstExpression.Empty;
}