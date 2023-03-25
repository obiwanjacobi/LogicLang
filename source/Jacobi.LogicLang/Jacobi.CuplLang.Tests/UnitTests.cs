using Xunit.Abstractions;

namespace Jacobi.CuplLang.Tests.Ast;

public class UnitTests
{
    private readonly ITestOutputHelper _output;
    public UnitTests(ITestOutputHelper output)
        => _output = output;

    [Fact]
    public void Test1()
    {
        const string cupl =
                    "Device G22V10;" +
                    "Pin 1 = A;" +
                    "x = A;"
                    ;

        var doc = CuplParser.ParseDocument(cupl);
        CuplParser.FailIfDiagnostics(doc, _output);
    }
}