using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Jacobi.CuplLang.Parser;
using static Jacobi.CuplLang.Parser.CuplParser;

namespace Jacobi.CuplLang.Ast;

internal sealed class AstBuilder : CuplParserBaseVisitor<object>
{
    public List<Diagnostic> Diagnostics = new();

#if DEBUG
    protected override object AggregateResult(object aggregate, object nextResult)
    {
        if (nextResult is null)
            return aggregate;
        if (aggregate is null)
            return nextResult;

        throw new InvalidOperationException(
            "Aggregation of multiple return values is not implemented.");
    }

    public override object VisitChildren(IRuleNode node)
    {
        return base.VisitChildren(node);
    }
#endif

    protected override bool ShouldVisitNextChild(IRuleNode node, object? currentResult)
    {
        if (node is ParserRuleContext context &&
            context.exception is not null)
        {
            Diagnostics.Add(new Diagnostic(
                context.Start.Line,
                context.Start.Column,
                context.GetText()
            ));
            // usually pointless to continue
            return false;
        }
        return true;
    }

    public override object VisitErrorNode(IErrorNode node)
    {
        Diagnostics.Add(new Diagnostic(
            node.Symbol.Line,
            node.Symbol.Column,
            node.GetText()
        ));

        return null;
    }

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
                    equations.AddRange((IEnumerable<AstEquation>)VisitEquation(ctx));
                    break;
            }
        }

        return new AstDocument()
        {
            Diagnostics = Diagnostics,
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

    public override object VisitPinSingle(PinSingleContext context)
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
        var numList = context.numberList()?.Number()!;
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
        // TODO: does not support reversed ranges (from hi to lo).
        var start = Int32.Parse(numRng[0].GetText());
        var count = Int32.Parse(numRng[1].GetText()) - start + 1;
        var pinNumbers = Enumerable.Range(start, count).ToArray();
        var symbols = (AstSymbol[])VisitSymbolRange(context.symbolRange());

        if (pinNumbers.Length != symbols.Length)
        {
            Diagnostics.Add(new Diagnostic(context.Start.Line, context.Start.Column, 
                $"Number of Pin name Symbols ({symbols.Length}) and the number of Pin numbers ({pinNumbers.Length}) mismatch."));
        }
        else
        {
            for (int i = 0; i < pinNumbers.Length; i++)
            {
                pins.Add(new AstPin
                {
                    PinNumber = pinNumbers[i],
                    Symbol = symbols[i].Value,
                    Inverted = inverted
                });
            }
        }
        return pins;
    }

    // equations and expressions

    public override object VisitEquation(EquationContext context)
    {
        var expression = (AstExpression)Visit(context.expression());
        var symbols = (AstSymbol[])VisitSymbol(context.symbol());
        var append = context.Append() is not null;

        return symbols.Select(symbol => new AstEquation
        {
            Append = append,
            Symbol = symbol.Value,
            Expression = expression
        });
    }

    // returns: AstSymbol[]
    public override object VisitSymbol(SymbolContext context)
    {
        var symbol = context.Symbol();
        if (symbol is not null)
            return new[] { new AstSymbol(symbol.GetText()) };

        var symbolList = context.symbolList();
        if (symbolList is not null)
            return VisitSymbolList(symbolList);

        var symbolRange = context.symbolRange();
        if (symbolRange is not null)
            return VisitSymbolRange(symbolRange);

        return null;
    }

    public override object VisitSymbolList(SymbolListContext context)
        => context.Symbol().Select(sym => new AstSymbol(sym.GetText())).ToArray();

    public override object VisitSymbolRange(SymbolRangeContext context)
    {
        var symbol = new AstSymbol(context.Symbol().GetText());
        var numTxt = context.Number().GetText()!;
        // TODO: does not support reversed ranges (from hi to lo).
        var count = Int32.Parse(numTxt) - symbol.Digits.Value + 1;
        var symbolNumbers = Enumerable.Range(symbol.Digits.Value, count)
            .Skip(1).ToArray();

        var symbols = new List<AstSymbol> { symbol };
        symbols.AddRange(symbolNumbers.Select(symNo => new AstSymbol(symbol.Name, symNo)));
        return symbols.ToArray();
    }

    public override object VisitExpressionBinary(ExpressionBinaryContext context)
    {
        var expressions = context.expression();
        var left = (AstExpression)Visit(expressions[0]);
        var right = (AstExpression)Visit(expressions[1]);
        AstOperator op = AstOperator.None;

        var binOp = context.binOp();
        if (binOp.LogicAnd() is not null)
            op = AstOperator.And;
        else if (binOp.LogicOr() is not null)
            op = AstOperator.Or;
        else if (binOp.Dollar() is not null)
            op = AstOperator.Xor;

        return AstExpression.FromOperator(left, op, right);
    }

    public override object VisitExpressionUnaryPrefix(ExpressionUnaryPrefixContext context)
    {
        var expression = (AstExpression)Visit(context.expression());
        return AstExpression.FromOperator(expression, AstOperator.Not);
    }

    public override object VisitExpressionSymbol(ExpressionSymbolContext context)
        => AstExpression.FromSymbol(context.Symbol().GetText());

    public override object VisitBinNumber(BinNumberContext context)
        => AstExpression.FromNumber(AstBitValue.FromBinary(context.BinNumber().GetText()));

    public override object VisitOctNumber(OctNumberContext context)
        => AstExpression.FromNumber(AstBitValue.FromOctal(context.OctNumber().GetText()));

    public override object VisitDecNumber(DecNumberContext context)
        => AstExpression.FromNumber(AstBitValue.FromDecimal(context.Number().GetText()));

    public override object VisitHexNumber(HexNumberContext context)
        => AstExpression.FromNumber(AstBitValue.FromHex(context.HexNumber().GetText()));

    public override object VisitExpressionPrecedence(ExpressionPrecedenceContext context)
    {
        var expression = (AstExpression)Visit(context.expression());
        expression.Precedence = true;
        return expression;
    }
}
