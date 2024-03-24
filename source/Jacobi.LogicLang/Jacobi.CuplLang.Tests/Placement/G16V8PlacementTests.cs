using Jacobi.CuplLang.Tests.Ast;
using Xunit.Abstractions;

namespace Jacobi.CuplLang.Tests.Placement;

public class G16V8PlacementTests
{
    private readonly ITestOutputHelper _output;
    public G16V8PlacementTests(ITestOutputHelper output)
        => _output = output;

    [Fact]
    public void Input4Output1Append2()
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

        var placement = FusePlacement.DoPlacement(cupl, _output);
        placement.Fuses.Should().HaveCount(14);

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