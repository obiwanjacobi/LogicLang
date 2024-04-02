using Jacobi.CuplLang.Gal16V8;
using Xunit.Abstractions;

namespace Jacobi.CuplLang.Tests.Placement;

public class JedecTests
{
    private readonly ITestOutputHelper _output;
    public JedecTests(ITestOutputHelper output)
        => _output = output;

    [Fact]
    public void WriteJedec()
    {
        const string cupl =
            "Device G16V8;" +
            "Pin 1 = A;" +
            "Pin 2 = B;" +
            "Pin 14 = Q;" +
            "Q = A & B;"
            ;

        var placement = FusePlacement.DoPlacement(cupl, _output);

        var stream = new MemoryStream();
        var writer = new JedWriter(stream);
        writer.Write(placement);

        stream.Length.Should().BeGreaterThan(20);

        stream.Position = 0;
        var reader = new StreamReader(stream);
        _output.WriteLine(reader.ReadToEnd());
    }

    [Fact]
    public void ReadJedec()
    {
        var filePath = Path.Combine(Environment.CurrentDirectory, "Samples", "Jedec", "BINARYCOUNTERHI.jed");
        File.Exists(filePath).Should().BeTrue();

        var stream = File.OpenRead(filePath);
        var reader = new JedReader(stream);
        var placement = reader.Read();
    }
}