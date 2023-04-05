using Jacobi.CuplLang.Device;
using Jacobi.CuplLang.Placement;
using Jacobi.CuplLang.Tests.Ast;
using Xunit.Abstractions;

namespace Jacobi.CuplLang.Tests.Placement;

public class PlacementTests
{
    private readonly ITestOutputHelper _output;
    public PlacementTests(ITestOutputHelper output)
        => _output = output;

    [Fact]
    public void Test1()
    {
        const string cupl =
                    "Device G16V8;" +
                    "Pin 14 = A;" +
                    "A = 'b'1;"
                    ;

        var doc = CuplParser.ParseDocument(cupl, _output);
        // TODO: doc expression optimization
        //       determine pin input or output
        var device = Device.Device.Create(doc.Header.Device);
        var functions = new DeviceFunction(device);
        var success = functions.AssignPinFunctions(doc.Pins, doc.Equations);
        success.Should().BeTrue();

        var placement = new AndOrPlacement(device);

    }
}