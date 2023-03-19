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
                    pins.AddRange((IEnumerable<AstPin>)VisitPin(ctx));
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
        => GetFreeText(context.freeText());

    public override object VisitCompany(CompanyContext context)
        => GetFreeText(context.freeText());

    public override object VisitDate(DateContext context)
        => GetFreeText(context.freeText());

    public override object VisitDesigner(DesignerContext context)
        => GetFreeText(context.freeText());

    public override object VisitDevice(DeviceContext context)
        => context.DeviceName()?.GetText()?.Trim() ?? String.Empty;

    public override object VisitFormat(FormatContext context)
        => context.FormatName()?.GetText()?.Trim() ?? String.Empty;

    public override object VisitLocation(LocationContext context)
        => GetFreeText(context.freeText());

    public override object VisitName(NameContext context)
        => GetFreeText(context.freeText());

    public override object VisitPartno(PartnoContext context)
        => GetFreeText(context.freeText());

    public override object VisitRevision(RevisionContext context)
        => GetFreeText(context.freeText());

    private static string GetFreeText(FreeTextContext context)
        => context?.GetText()?.Trim() ?? String.Empty;

    // Pins

    public override object VisitPinSimple(PinSimpleContext context)
    {
        var inverted = context.LogicNot() is not null;
        var numTxt = context.Number()?.GetText();
        // TODO: validate symbol
        var symbol = context.Symbol().GetText() ?? String.Empty;

        return new[] { new AstPin
            {
                PinNumber = Int32.Parse(numTxt),
                Symbol = symbol,
                Inverted = inverted
            }
        };
    }

    public override object VisitPinList(PinListContext context)
    {
        var pins = new List<AstPin>();

        var inverted = context.LogicNot() is not null;
        var numList = context.numberList()?.Number();
        var symbolList = context.symbolList()?.Symbol()!;

        // TODO: number of items in numList and symbolList must match
        for (int i = 0; i < numList.Length; i++)
        {
            var numTxt = numList[i].GetText()!;
            var symbol = symbolList[i].GetText()!;

            pins.Add(new AstPin
            {
                PinNumber = Int32.Parse(numTxt),
                Symbol = symbol,
                Inverted = inverted
            });
        }

        return pins;
    }

    public override object VisitPinRange(PinRangeContext context)
    {
        var pins = new List<AstPin>();

        var inverted = context.LogicNot() is not null;
        var numRng = context.numberRange().Number();
        var symbolRng = context.symbolRange();
        var symbol = new AstSymbol(symbolRng.Symbol().GetText());
        var numTxt = symbolRng.Number().GetText()!;
        var pinNumbers = Enumerable.Range(Int32.Parse(numRng[0].GetText()), Int32.Parse(numRng[1].GetText())).ToArray();
        var symbolNumbers = Enumerable.Range(symbol.Digits.Value, Int32.Parse(numTxt)).ToArray();

        // TODO: check span of both ranges are the same

        for (int i = 0; i < pinNumbers.Length; i++)
        {
            pins.Add(new AstPin
            {
                PinNumber = pinNumbers[i],
                Symbol = $"{symbol.Name}{symbolNumbers[i]}",
                Inverted = inverted
            });
        }

        return pins;
    }
}
