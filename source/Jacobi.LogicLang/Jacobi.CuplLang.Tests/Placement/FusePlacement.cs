using Jacobi.CuplLang.Tests.Ast;
using Xunit.Abstractions;

namespace Jacobi.CuplLang.Tests.Placement;

internal static class FusePlacement
{
    internal static Gal16V8.Placement DoPlacement(string cupl, ITestOutputHelper output)
    {
        var doc = CuplParser.ParseDocument(cupl, output);

        var device = Gal16V8.Gal16V8.FromDeviceName("G16V8");
        var placement = device.CreatePlacement(doc.Pins, doc.Equations);
        return placement;
    }
}
