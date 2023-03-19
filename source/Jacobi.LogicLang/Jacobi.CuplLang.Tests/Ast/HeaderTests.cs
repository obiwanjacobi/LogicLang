namespace Jacobi.CuplLang.Tests.Ast
{
    public class HeaderTests
    {
        [Fact]
        public void HeaderFields()
        {
            const string cupl = 
                "Name   TestName;" +
                "Partno TestPartNo ;" +
                "Revision TestRev   ;" +
                "Designer TestDesigner;" +
                "Company  TestCompany;" +
                "Assembly TestAssembly;" +
                "Location       TestLocation;" +
                "Device             G22V10;"
                ;

            var doc = CuplParser.ParseDocument(cupl);

            doc.Header.Assembly.Should().Be("TestAssembly");
            doc.Header.Date.Should().BeEmpty();
            doc.Header.Designer.Should().Be("TestDesigner");
            doc.Header.Device.Should().Be("G22V10");
            doc.Header.Format.Should().BeEmpty();
            doc.Header.Location.Should().Be("TestLocation");
            doc.Header.Name.Should().Be("TestName");
            doc.Header.PartNo.Should().Be("TestPartNo");
            doc.Header.Revision.Should().Be("TestRev");
        }
    }
}