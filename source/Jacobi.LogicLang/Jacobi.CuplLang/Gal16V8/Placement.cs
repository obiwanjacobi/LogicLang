using System.Collections.Generic;

namespace Jacobi.CuplLang.Gal16V8;

internal sealed class Placement
{
    public Placement(IReadOnlyList<Fuse> fuses)
        => Fuses = fuses;

    public IReadOnlyList<Fuse> Fuses { get; }
}