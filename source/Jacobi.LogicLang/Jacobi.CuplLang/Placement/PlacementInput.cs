using System;
using System.Collections.Generic;
using Jacobi.CuplLang.Ast;
using Jacobi.CuplLang.Device;

namespace Jacobi.CuplLang.Placement;

internal sealed class PlacementInput
{
    public PlacementInput(Pin pin)
    {
        Pin = pin;
        Symbol = String.Empty;
    }
    public Pin Pin { get; }

    public int Number => Pin.Number;
    
    public string Symbol { get; set; }

    // may need a different expression type here...
    private readonly List<AstExpression> _expressions = new();
    public IList<AstExpression> Expressions => _expressions;
}
