using Xunit.Abstractions;

namespace Jacobi.CuplLang.Tests.Ast
{
    public class PinTests
    {
        private readonly ITestOutputHelper _output;
        public PinTests(ITestOutputHelper output)
            => _output = output;

        [Fact]
        public void SinglePin()
        {
            const string cupl =
                "Device G22V10;" +
                "PIN 1   = !Symbol1;" +
                "PIN 11  = Symbol11;"
                ;

            var doc = CuplParser.ParseDocument(cupl, _output);

            doc.Pins.Should().HaveCount(2);
            var pin = doc.Pins[0];
            pin.PinNumber.Should().Be(1);
            pin.Symbol.Should().Be("Symbol1");
            pin.Inverted.Should().BeTrue();

            pin = doc.Pins[1];
            pin.PinNumber.Should().Be(11);
            pin.Symbol.Should().Be("Symbol11");
            pin.Inverted.Should().BeFalse();
        }

        [Fact]
        public void PinList()
        {
            const string cupl =
                "Device G22V10;" +
                "PIN [1, 11]   = ![Symbol1, Symbol11];"
                ;

            var doc = CuplParser.ParseDocument(cupl, _output);

            doc.Pins.Should().HaveCount(2);
            var pin = doc.Pins[0];
            pin.PinNumber.Should().Be(1);
            pin.Symbol.Should().Be("Symbol1");
            pin.Inverted.Should().BeTrue();

            pin = doc.Pins[1];
            pin.PinNumber.Should().Be(11);
            pin.Symbol.Should().Be("Symbol11");
            pin.Inverted.Should().BeTrue();
        }

        [Fact]
        public void PinRange()
        {
            const string cupl =
                "Device G22V10;" +
                "PIN [1..3]   = ![Symbol1..3];"
                ;

            var doc = CuplParser.ParseDocument(cupl, _output);

            doc.Pins.Should().HaveCount(3);
            var pin = doc.Pins[0];
            pin.PinNumber.Should().Be(1);
            pin.Symbol.Should().Be("Symbol1");
            pin.Inverted.Should().BeTrue();

            pin = doc.Pins[1];
            pin.PinNumber.Should().Be(2);
            pin.Symbol.Should().Be("Symbol2");
            pin.Inverted.Should().BeTrue();

            pin = doc.Pins[2];
            pin.PinNumber.Should().Be(3);
            pin.Symbol.Should().Be("Symbol3");
            pin.Inverted.Should().BeTrue();
        }
    }
}