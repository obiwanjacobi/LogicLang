//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.12.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from CuplParser.g4 by ANTLR 4.12.0

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace Jacobi.CuplLang.Parser {
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="CuplParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.12.0")]
[System.CLSCompliant(false)]
public interface ICuplParserVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="CuplParser.file"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFile([NotNull] CuplParser.FileContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CuplParser.header"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitHeader([NotNull] CuplParser.HeaderContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CuplParser.assembly"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssembly([NotNull] CuplParser.AssemblyContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CuplParser.company"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCompany([NotNull] CuplParser.CompanyContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CuplParser.date"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDate([NotNull] CuplParser.DateContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CuplParser.designer"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDesigner([NotNull] CuplParser.DesignerContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CuplParser.device"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDevice([NotNull] CuplParser.DeviceContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CuplParser.format"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFormat([NotNull] CuplParser.FormatContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CuplParser.location"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLocation([NotNull] CuplParser.LocationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CuplParser.name"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitName([NotNull] CuplParser.NameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CuplParser.partno"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPartno([NotNull] CuplParser.PartnoContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CuplParser.revision"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRevision([NotNull] CuplParser.RevisionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CuplParser.freeText"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFreeText([NotNull] CuplParser.FreeTextContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CuplParser.pin"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPin([NotNull] CuplParser.PinContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CuplParser.pinSingle"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPinSingle([NotNull] CuplParser.PinSingleContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CuplParser.pinList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPinList([NotNull] CuplParser.PinListContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CuplParser.pinRange"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPinRange([NotNull] CuplParser.PinRangeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CuplParser.numberList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNumberList([NotNull] CuplParser.NumberListContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CuplParser.numberRange"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNumberRange([NotNull] CuplParser.NumberRangeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CuplParser.symbolList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSymbolList([NotNull] CuplParser.SymbolListContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CuplParser.symbolRange"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSymbolRange([NotNull] CuplParser.SymbolRangeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CuplParser.equation"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEquation([NotNull] CuplParser.EquationContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>expressionUnaryPrefix</c>
	/// labeled alternative in <see cref="CuplParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpressionUnaryPrefix([NotNull] CuplParser.ExpressionUnaryPrefixContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>expressionNumber</c>
	/// labeled alternative in <see cref="CuplParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpressionNumber([NotNull] CuplParser.ExpressionNumberContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>expressionSymbol</c>
	/// labeled alternative in <see cref="CuplParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpressionSymbol([NotNull] CuplParser.ExpressionSymbolContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>expressionBinary</c>
	/// labeled alternative in <see cref="CuplParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpressionBinary([NotNull] CuplParser.ExpressionBinaryContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>expressionPrecedence</c>
	/// labeled alternative in <see cref="CuplParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpressionPrecedence([NotNull] CuplParser.ExpressionPrecedenceContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CuplParser.number"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNumber([NotNull] CuplParser.NumberContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CuplParser.binNumber"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBinNumber([NotNull] CuplParser.BinNumberContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CuplParser.octNumber"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOctNumber([NotNull] CuplParser.OctNumberContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CuplParser.decNumber"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDecNumber([NotNull] CuplParser.DecNumberContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CuplParser.hexNumber"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitHexNumber([NotNull] CuplParser.HexNumberContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CuplParser.dontCareNumber"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDontCareNumber([NotNull] CuplParser.DontCareNumberContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CuplParser.extension"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExtension([NotNull] CuplParser.ExtensionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CuplParser.binOp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBinOp([NotNull] CuplParser.BinOpContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CuplParser.uniOp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitUniOp([NotNull] CuplParser.UniOpContext context);
}
} // namespace Jacobi.CuplLang.Parser
