using System.Collections.Generic;
using Jacobi.CuplLang.Device;

namespace Jacobi.CuplLang.Placement;

internal sealed class PlacementOutput
{
    public PlacementOutput(MacroCell macroCell)
    {
        _macroCell = macroCell;
    }

    private readonly MacroCell _macroCell;
    public MacroCell MacroCell => _macroCell;
}