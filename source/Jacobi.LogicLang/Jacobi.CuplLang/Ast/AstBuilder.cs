using Jacobi.CuplLang.Parser;
using static Jacobi.CuplLang.Parser.CuplParser;

namespace Jacobi.CuplLang.Ast;

internal sealed class AstBuilder : CuplParserBaseVisitor<object>
{
    public AstDocument File(FileContext context)
    {
        var header = new AstHeader();
        var pins = new List<AstPin>();
        var equations = new List<AstEquation>();

        foreach (var child in context.children)
        {
            switch (child)
            {
                case HeaderContext ctx:
                    header = (AstHeader)VisitHeader(ctx);
                    break;
                case PinContext ctx:
                    pins.Add((AstPin)VisitPin(ctx));
                    break;
                case EquationContext ctx:
                    equations.Add((AstEquation)VisitEquation(ctx));
                    break;
            }
        }

        return new AstDocument()
        {
            Header = header,
            Pins = pins,
            Equations = equations
        };
    }

    // header

    public override object VisitHeader(HeaderContext context)
    {
        var header = new AstHeader();

        foreach (var child in context.children)
        {
            switch (child)
            {
                case AssemblyContext ctx:
                    header.Assembly = (string)VisitAssembly(ctx);
                    break;
                case CompanyContext ctx:
                    header.Company = (string)VisitCompany(ctx);
                    break;
                case DateContext ctx:
                    header.Date = (string)VisitDate(ctx);
                    break;
                case DesignerContext ctx:
                    header.Designer = (string)VisitDesigner(ctx);
                    break;
                case DeviceContext ctx:
                    header.Device = (string)VisitDevice(ctx);
                    break;
                case FormatContext ctx:
                    header.Format = (string)VisitFormat(ctx);
                    break;
                case LocationContext ctx:
                    header.Location = (string)VisitLocation(ctx);
                    break;
                case NameContext ctx:
                    header.Name = (string)VisitName(ctx);
                    break;
                case PartnoContext ctx:
                    header.PartNo = (string)VisitPartno(ctx);
                    break;
                case RevisionContext ctx:
                    header.Revision = (string)VisitRevision(ctx);
                    break;
            }
        }

        return header;
    }

    public override object VisitAssembly(AssemblyContext context)
        => context.freeText()?.GetText()?.Trim() ?? String.Empty;

    public override object VisitCompany(CompanyContext context)
        => context.freeText()?.GetText()?.Trim() ?? String.Empty;

    public override object VisitDate(DateContext context)
        => context.freeText()?.GetText()?.Trim() ?? String.Empty;

    public override object VisitDesigner(DesignerContext context)
        => context.freeText()?.GetText()?.Trim() ?? String.Empty;

    public override object VisitDevice(DeviceContext context)
        => context.Symbol()?.GetText()?.Trim() ?? String.Empty;

    public override object VisitFormat(FormatContext context)
        => context.Symbol()?.GetText()?.Trim() ?? String.Empty;

    public override object VisitLocation(LocationContext context)
        => context.freeText()?.GetText()?.Trim() ?? String.Empty;

    public override object VisitName(NameContext context)
        => context.freeText()?.GetText()?.Trim() ?? String.Empty;

    public override object VisitPartno(PartnoContext context)
        => context.freeText()?.GetText()?.Trim() ?? String.Empty;

    public override object VisitRevision(RevisionContext context)
        => context.freeText()?.GetText()?.Trim() ?? String.Empty;
}
