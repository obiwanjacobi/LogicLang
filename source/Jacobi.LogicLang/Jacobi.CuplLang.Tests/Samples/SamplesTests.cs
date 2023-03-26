using System.Reflection;
using Jacobi.CuplLang.Ast;
using Jacobi.CuplLang.Tests.Ast;
using Xunit.Abstractions;

namespace Jacobi.CuplLang.Tests.Samples;

public class SamplesTests
{
    private readonly ITestOutputHelper _output;
    private readonly string _basePath;

    public SamplesTests(ITestOutputHelper output)
    {
        _output = output;
        _basePath = Environment.CurrentDirectory;
    }

    private AstDocument ParseFile(string filePath)
    {
        var reader = File.OpenText(filePath);
        return ParseFile(reader);
    }
    private AstDocument ParseFile(StreamReader reader)
    {
        var cupl = reader.ReadToEnd();
        var doc = CuplParser.ParseDocument(cupl, _output);
        return doc;
    }

    [Theory]
    //[InlineData("ADDER.PLD")]     => function
    //[InlineData("BARREL22.PLD")]  => field
    //[InlineData("COUNT10.PLD")]   => field and $define
    [InlineData("GATES.PLD")]
    //[InlineData("IODECODE.PLD")]  => G20V8 device (not G16V8/G22V10)
    //[InlineData("LOOKUP.PLD")]    => field
    //[InlineData("TTL.PLD")]       => G20V8 device (not G16V8/G22V10)
    public void WinCUPL_Parse(string fileName)
    {
        var doc = ParseFile(Path.Combine(_basePath, "Samples", "WinCupl", fileName));
        doc.Diagnostics.Should().HaveCount(0);
    }
}