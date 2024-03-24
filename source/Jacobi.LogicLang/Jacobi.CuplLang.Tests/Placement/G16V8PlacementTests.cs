using Jacobi.CuplLang.Tests.Ast;
using Xunit.Abstractions;

namespace Jacobi.CuplLang.Tests.Placement;

public class G16V8PlacementTests
{
    private readonly ITestOutputHelper _output;
    public G16V8PlacementTests(ITestOutputHelper output)
        => _output = output;

    private Gal16V8.Placement DoPlacement(string cupl)
    {
        var doc = CuplParser.ParseDocument(cupl, _output);

        var device = Gal16V8.Gal16V8.FromDeviceName("G16V8");
        var placement = device.CreatePlacement(doc.Pins, doc.Equations);
        return placement;
    }

    [Fact]
    public void Test1()
    {
        const string cupl =
            "Device G16V8;" +
            "Pin 1 = A;" +
            "Pin 2 = B;" +
            "Pin 3 = C;" +
            "Pin 4 = D;" +
            "Pin 14 = Q;" +
            "Q = A & B;" +
            "Append Q = C & D;" +
            "";

        var placement = DoPlacement(cupl);
        placement.Fuses.Should().HaveCount(10);

        // simple mode
        placement.Fuses.Should().Contain(new Gal16V8.Fuse(2192));
        placement.Fuses.Should().Contain(new Gal16V8.Fuse(2193, false));

        // input pins on output pin 14
        placement.Fuses.Should().Contain(new Gal16V8.Fuse(1282));
        placement.Fuses.Should().Contain(new Gal16V8.Fuse(1312));
        placement.Fuses.Should().Contain(new Gal16V8.Fuse(1348));
        placement.Fuses.Should().Contain(new Gal16V8.Fuse(1384));

        // disabled product terms
        placement.Fuses.Should().Contain(new Gal16V8.Fuse(2172));
        placement.Fuses.Should().Contain(new Gal16V8.Fuse(2173));
        placement.Fuses.Should().Contain(new Gal16V8.Fuse(2174));
        placement.Fuses.Should().Contain(new Gal16V8.Fuse(2175));
    }
}