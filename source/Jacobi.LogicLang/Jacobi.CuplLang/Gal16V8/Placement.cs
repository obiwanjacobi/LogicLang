using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;

namespace Jacobi.CuplLang.Gal16V8;

internal sealed class Placement
{
    public Placement(string device, int pinCount, int fuseCount, IReadOnlyList<Fuse> fuses)
    {
        DeviceName = device;
        PinCount = pinCount;
        ProductTermSize = 32;   // TODO
        FuseCount = fuseCount;
        Fuses = fuses.OrderBy(f => f.Number).ToList();
    }

    public string DeviceName { get; }
    public int PinCount { get; }
    public int ProductTermSize { get; }
    public int FuseCount { get; }
    public bool Protect { get; }

    public IReadOnlyList<Fuse> Fuses { get; }

    public IEnumerable<IEnumerable<Fuse>> GetFuseLines()
    {
        var lines = new List<List<Fuse>>();
        var fuses = new List<Fuse>();

        var lastFuseNumber = 0;
        foreach (var fuse in Fuses)
        {
            if (fuse.Number > lastFuseNumber + 1
                && fuses.Count > 0)
            {
                lines.Add(fuses);
                fuses = new List<Fuse>();
            }
            fuses.Add(fuse);
            lastFuseNumber = fuse.Number;
        }

        return lines;
    }
}