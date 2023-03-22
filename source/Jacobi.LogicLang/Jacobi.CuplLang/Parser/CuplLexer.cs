//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.12.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from CuplLexer.g4 by ANTLR 4.12.0

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace Jacobi.CuplLang.Parser {
using System;
using System.IO;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.12.0")]
[System.CLSCompliant(false)]
public partial class CuplLexer : Lexer {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		Comments=1, Assembly=2, Company=3, Date=4, Designer=5, Device=6, Format=7, 
		Location=8, Name=9, Partno=10, Revision=11, DeviceName=12, FormatName=13, 
		Pin=14, Append=15, Extension=16, Symbol=17, Number=18, DontCareNumber=19, 
		PrefixHex=20, PrefixOctal=21, PrefixDecimal=22, PrefixBinary=23, Include=24, 
		Define=25, UnDef=26, IfDef=27, IfNotDef=28, EndIf=29, Else=30, Repeat=31, 
		RepEnd=32, Macro=33, MEnd=34, Eq=35, LogicNot=36, LogicAnd=37, LogicOr=38, 
		SemiColon=39, Comma=40, Dot=41, Range=42, BracketOpen=43, BracketClose=44, 
		ParenOpen=45, ParenClose=46, Dollar=47, Quote=48, WS=49, EndUseSpaces=50, 
		FreeText=51, BinNumber=52, OctNumber=53, HexNumber=54;
	public const int
		UseSpacesMode=1, BinNumberMode=2, OctNumberMode=3, HexNumberMode=4;
	public static string[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static string[] modeNames = {
		"DEFAULT_MODE", "UseSpacesMode", "BinNumberMode", "OctNumberMode", "HexNumberMode"
	};

	public static readonly string[] ruleNames = {
		"Comments", "Assembly", "Company", "Date", "Designer", "Device", "Format", 
		"Location", "Name", "Partno", "Revision", "DeviceName", "FormatName", 
		"Pin", "Append", "Extension", "Symbol", "Number", "DontCareNumber", "PrefixHex", 
		"PrefixOctal", "PrefixDecimal", "PrefixBinary", "ALPHA", "DIGIT2", "DIGIT8", 
		"DIGIT10", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", 
		"M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", 
		"Include", "Define", "UnDef", "IfDef", "IfNotDef", "EndIf", "Else", "Repeat", 
		"RepEnd", "Macro", "MEnd", "Eq", "LogicNot", "LogicAnd", "LogicOr", "SemiColon", 
		"Comma", "Dot", "Range", "BracketOpen", "BracketClose", "ParenOpen", "ParenClose", 
		"Dollar", "Quote", "WS", "EndUseSpaces", "FreeText", "BinNumber", "OctNumber", 
		"HexNumber"
	};


	public CuplLexer(ICharStream input)
	: this(input, Console.Out, Console.Error) { }

	public CuplLexer(ICharStream input, TextWriter output, TextWriter errorOutput)
	: base(input, output, errorOutput)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, null, null, null, null, null, 
		"'INCLUDE'", "'DEFINE'", "'UNDEF'", "'IFDEF'", "'IFNDEF'", "'ENDIF'", 
		"'ELSE'", "'REPEAT'", "'REPEND'", "'MACRO'", "'MEND'", "'='", "'!'", "'&'", 
		"'#'", null, "','", "'.'", "'..'", "'['", "']'", "'('", "')'", "'$'", 
		"'''"
	};
	private static readonly string[] _SymbolicNames = {
		null, "Comments", "Assembly", "Company", "Date", "Designer", "Device", 
		"Format", "Location", "Name", "Partno", "Revision", "DeviceName", "FormatName", 
		"Pin", "Append", "Extension", "Symbol", "Number", "DontCareNumber", "PrefixHex", 
		"PrefixOctal", "PrefixDecimal", "PrefixBinary", "Include", "Define", "UnDef", 
		"IfDef", "IfNotDef", "EndIf", "Else", "Repeat", "RepEnd", "Macro", "MEnd", 
		"Eq", "LogicNot", "LogicAnd", "LogicOr", "SemiColon", "Comma", "Dot", 
		"Range", "BracketOpen", "BracketClose", "ParenOpen", "ParenClose", "Dollar", 
		"Quote", "WS", "EndUseSpaces", "FreeText", "BinNumber", "OctNumber", "HexNumber"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "CuplLexer.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ChannelNames { get { return channelNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override int[] SerializedAtn { get { return _serializedATN; } }

	static CuplLexer() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}
	private static int[] _serializedATN = {
		4,0,54,568,6,-1,6,-1,6,-1,6,-1,6,-1,2,0,7,0,2,1,7,1,2,2,7,2,2,3,7,3,2,
		4,7,4,2,5,7,5,2,6,7,6,2,7,7,7,2,8,7,8,2,9,7,9,2,10,7,10,2,11,7,11,2,12,
		7,12,2,13,7,13,2,14,7,14,2,15,7,15,2,16,7,16,2,17,7,17,2,18,7,18,2,19,
		7,19,2,20,7,20,2,21,7,21,2,22,7,22,2,23,7,23,2,24,7,24,2,25,7,25,2,26,
		7,26,2,27,7,27,2,28,7,28,2,29,7,29,2,30,7,30,2,31,7,31,2,32,7,32,2,33,
		7,33,2,34,7,34,2,35,7,35,2,36,7,36,2,37,7,37,2,38,7,38,2,39,7,39,2,40,
		7,40,2,41,7,41,2,42,7,42,2,43,7,43,2,44,7,44,2,45,7,45,2,46,7,46,2,47,
		7,47,2,48,7,48,2,49,7,49,2,50,7,50,2,51,7,51,2,52,7,52,2,53,7,53,2,54,
		7,54,2,55,7,55,2,56,7,56,2,57,7,57,2,58,7,58,2,59,7,59,2,60,7,60,2,61,
		7,61,2,62,7,62,2,63,7,63,2,64,7,64,2,65,7,65,2,66,7,66,2,67,7,67,2,68,
		7,68,2,69,7,69,2,70,7,70,2,71,7,71,2,72,7,72,2,73,7,73,2,74,7,74,2,75,
		7,75,2,76,7,76,2,77,7,77,2,78,7,78,2,79,7,79,2,80,7,80,2,81,7,81,2,82,
		7,82,2,83,7,83,1,0,1,0,1,0,1,0,5,0,178,8,0,10,0,12,0,181,9,0,1,0,1,0,1,
		0,1,0,1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,1,2,1,2,1,2,
		1,2,1,2,1,2,1,2,1,2,1,2,1,3,1,3,1,3,1,3,1,3,1,3,1,3,1,4,1,4,1,4,1,4,1,
		4,1,4,1,4,1,4,1,4,1,4,1,4,1,5,1,5,1,5,1,5,1,5,1,5,1,5,1,6,1,6,1,6,1,6,
		1,6,1,6,1,6,1,7,1,7,1,7,1,7,1,7,1,7,1,7,1,7,1,7,1,7,1,7,1,8,1,8,1,8,1,
		8,1,8,1,8,1,8,1,9,1,9,1,9,1,9,1,9,1,9,1,9,1,9,1,9,1,10,1,10,1,10,1,10,
		1,10,1,10,1,10,1,10,1,10,1,10,1,10,1,11,1,11,1,11,1,11,1,11,1,11,1,11,
		1,11,1,11,1,11,1,11,1,11,1,11,1,11,1,11,3,11,294,8,11,1,12,1,12,1,13,1,
		13,1,13,1,13,1,14,1,14,1,14,1,14,1,14,1,14,1,14,1,15,1,15,1,15,1,15,1,
		15,1,15,1,15,1,15,1,15,1,15,3,15,319,8,15,1,16,4,16,322,8,16,11,16,12,
		16,323,1,16,5,16,327,8,16,10,16,12,16,330,9,16,1,17,4,17,333,8,17,11,17,
		12,17,334,1,18,1,18,4,18,339,8,18,11,18,12,18,340,1,19,1,19,1,19,1,19,
		1,19,1,19,1,20,1,20,1,20,1,20,1,20,1,20,1,21,1,21,1,21,1,21,1,22,1,22,
		1,22,1,22,1,22,1,22,1,23,1,23,1,24,1,24,1,25,1,25,1,26,1,26,1,27,1,27,
		1,28,1,28,1,29,1,29,1,30,1,30,1,31,1,31,1,32,1,32,1,33,1,33,1,34,1,34,
		1,35,1,35,1,36,1,36,1,37,1,37,1,38,1,38,1,39,1,39,1,40,1,40,1,41,1,41,
		1,42,1,42,1,43,1,43,1,44,1,44,1,45,1,45,1,46,1,46,1,47,1,47,1,48,1,48,
		1,49,1,49,1,50,1,50,1,51,1,51,1,52,1,52,1,53,1,53,1,53,1,53,1,53,1,53,
		1,53,1,53,1,54,1,54,1,54,1,54,1,54,1,54,1,54,1,55,1,55,1,55,1,55,1,55,
		1,55,1,56,1,56,1,56,1,56,1,56,1,56,1,57,1,57,1,57,1,57,1,57,1,57,1,57,
		1,58,1,58,1,58,1,58,1,58,1,58,1,59,1,59,1,59,1,59,1,59,1,60,1,60,1,60,
		1,60,1,60,1,60,1,60,1,61,1,61,1,61,1,61,1,61,1,61,1,61,1,62,1,62,1,62,
		1,62,1,62,1,62,1,63,1,63,1,63,1,63,1,63,1,64,1,64,1,65,1,65,1,66,1,66,
		1,67,1,67,1,68,1,68,1,69,1,69,1,70,1,70,1,71,1,71,1,71,1,72,1,72,1,73,
		1,73,1,74,1,74,1,75,1,75,1,76,1,76,1,77,1,77,1,78,4,78,525,8,78,11,78,
		12,78,526,1,78,1,78,1,79,1,79,1,79,1,79,1,80,4,80,536,8,80,11,80,12,80,
		537,1,81,4,81,541,8,81,11,81,12,81,542,1,81,1,81,1,82,4,82,548,8,82,11,
		82,12,82,549,1,82,1,82,1,83,1,83,1,83,1,83,1,83,1,83,3,83,560,8,83,1,83,
		4,83,563,8,83,11,83,12,83,564,1,83,1,83,2,179,537,0,84,5,1,7,2,9,3,11,
		4,13,5,15,6,17,7,19,8,21,9,23,10,25,11,27,12,29,13,31,14,33,15,35,16,37,
		17,39,18,41,19,43,20,45,21,47,22,49,23,51,0,53,0,55,0,57,0,59,0,61,0,63,
		0,65,0,67,0,69,0,71,0,73,0,75,0,77,0,79,0,81,0,83,0,85,0,87,0,89,0,91,
		0,93,0,95,0,97,0,99,0,101,0,103,0,105,0,107,0,109,0,111,24,113,25,115,
		26,117,27,119,28,121,29,123,30,125,31,127,32,129,33,131,34,133,35,135,
		36,137,37,139,38,141,39,143,40,145,41,147,42,149,43,151,44,153,45,155,
		46,157,47,159,48,161,49,163,50,165,51,167,52,169,53,171,54,5,0,1,2,3,4,
		31,2,0,65,90,97,122,1,0,48,49,1,0,48,55,1,0,48,57,2,0,65,65,97,97,2,0,
		66,66,98,98,2,0,67,67,99,99,2,0,68,68,100,100,2,0,69,69,101,101,2,0,70,
		70,102,102,2,0,71,71,103,103,2,0,72,72,104,104,2,0,73,73,105,105,2,0,74,
		74,106,106,2,0,75,75,107,107,2,0,76,76,108,108,2,0,77,77,109,109,2,0,78,
		78,110,110,2,0,79,79,111,111,2,0,80,80,112,112,2,0,81,81,113,113,2,0,82,
		82,114,114,2,0,83,83,115,115,2,0,84,84,116,116,2,0,85,85,117,117,2,0,86,
		86,118,118,2,0,87,87,119,119,2,0,88,88,120,120,2,0,89,89,121,121,2,0,90,
		90,122,122,3,0,9,10,13,13,32,32,554,0,5,1,0,0,0,0,7,1,0,0,0,0,9,1,0,0,
		0,0,11,1,0,0,0,0,13,1,0,0,0,0,15,1,0,0,0,0,17,1,0,0,0,0,19,1,0,0,0,0,21,
		1,0,0,0,0,23,1,0,0,0,0,25,1,0,0,0,0,27,1,0,0,0,0,29,1,0,0,0,0,31,1,0,0,
		0,0,33,1,0,0,0,0,35,1,0,0,0,0,37,1,0,0,0,0,39,1,0,0,0,0,41,1,0,0,0,0,43,
		1,0,0,0,0,45,1,0,0,0,0,47,1,0,0,0,0,49,1,0,0,0,0,111,1,0,0,0,0,113,1,0,
		0,0,0,115,1,0,0,0,0,117,1,0,0,0,0,119,1,0,0,0,0,121,1,0,0,0,0,123,1,0,
		0,0,0,125,1,0,0,0,0,127,1,0,0,0,0,129,1,0,0,0,0,131,1,0,0,0,0,133,1,0,
		0,0,0,135,1,0,0,0,0,137,1,0,0,0,0,139,1,0,0,0,0,141,1,0,0,0,0,143,1,0,
		0,0,0,145,1,0,0,0,0,147,1,0,0,0,0,149,1,0,0,0,0,151,1,0,0,0,0,153,1,0,
		0,0,0,155,1,0,0,0,0,157,1,0,0,0,0,159,1,0,0,0,0,161,1,0,0,0,1,163,1,0,
		0,0,1,165,1,0,0,0,2,167,1,0,0,0,3,169,1,0,0,0,4,171,1,0,0,0,5,173,1,0,
		0,0,7,187,1,0,0,0,9,198,1,0,0,0,11,208,1,0,0,0,13,215,1,0,0,0,15,226,1,
		0,0,0,17,233,1,0,0,0,19,240,1,0,0,0,21,251,1,0,0,0,23,258,1,0,0,0,25,267,
		1,0,0,0,27,293,1,0,0,0,29,295,1,0,0,0,31,297,1,0,0,0,33,301,1,0,0,0,35,
		318,1,0,0,0,37,321,1,0,0,0,39,332,1,0,0,0,41,338,1,0,0,0,43,342,1,0,0,
		0,45,348,1,0,0,0,47,354,1,0,0,0,49,358,1,0,0,0,51,364,1,0,0,0,53,366,1,
		0,0,0,55,368,1,0,0,0,57,370,1,0,0,0,59,372,1,0,0,0,61,374,1,0,0,0,63,376,
		1,0,0,0,65,378,1,0,0,0,67,380,1,0,0,0,69,382,1,0,0,0,71,384,1,0,0,0,73,
		386,1,0,0,0,75,388,1,0,0,0,77,390,1,0,0,0,79,392,1,0,0,0,81,394,1,0,0,
		0,83,396,1,0,0,0,85,398,1,0,0,0,87,400,1,0,0,0,89,402,1,0,0,0,91,404,1,
		0,0,0,93,406,1,0,0,0,95,408,1,0,0,0,97,410,1,0,0,0,99,412,1,0,0,0,101,
		414,1,0,0,0,103,416,1,0,0,0,105,418,1,0,0,0,107,420,1,0,0,0,109,422,1,
		0,0,0,111,424,1,0,0,0,113,432,1,0,0,0,115,439,1,0,0,0,117,445,1,0,0,0,
		119,451,1,0,0,0,121,458,1,0,0,0,123,464,1,0,0,0,125,469,1,0,0,0,127,476,
		1,0,0,0,129,483,1,0,0,0,131,489,1,0,0,0,133,494,1,0,0,0,135,496,1,0,0,
		0,137,498,1,0,0,0,139,500,1,0,0,0,141,502,1,0,0,0,143,504,1,0,0,0,145,
		506,1,0,0,0,147,508,1,0,0,0,149,511,1,0,0,0,151,513,1,0,0,0,153,515,1,
		0,0,0,155,517,1,0,0,0,157,519,1,0,0,0,159,521,1,0,0,0,161,524,1,0,0,0,
		163,530,1,0,0,0,165,535,1,0,0,0,167,540,1,0,0,0,169,547,1,0,0,0,171,562,
		1,0,0,0,173,174,5,47,0,0,174,175,5,42,0,0,175,179,1,0,0,0,176,178,9,0,
		0,0,177,176,1,0,0,0,178,181,1,0,0,0,179,180,1,0,0,0,179,177,1,0,0,0,180,
		182,1,0,0,0,181,179,1,0,0,0,182,183,5,42,0,0,183,184,5,47,0,0,184,185,
		1,0,0,0,185,186,6,0,0,0,186,6,1,0,0,0,187,188,3,59,27,0,188,189,3,95,45,
		0,189,190,3,95,45,0,190,191,3,67,31,0,191,192,3,83,39,0,192,193,3,61,28,
		0,193,194,3,81,38,0,194,195,3,107,51,0,195,196,1,0,0,0,196,197,6,1,1,0,
		197,8,1,0,0,0,198,199,3,63,29,0,199,200,3,87,41,0,200,201,3,83,39,0,201,
		202,3,89,42,0,202,203,3,59,27,0,203,204,3,85,40,0,204,205,3,107,51,0,205,
		206,1,0,0,0,206,207,6,2,1,0,207,10,1,0,0,0,208,209,3,65,30,0,209,210,3,
		59,27,0,210,211,3,97,46,0,211,212,3,67,31,0,212,213,1,0,0,0,213,214,6,
		3,1,0,214,12,1,0,0,0,215,216,3,65,30,0,216,217,3,67,31,0,217,218,3,95,
		45,0,218,219,3,75,35,0,219,220,3,71,33,0,220,221,3,85,40,0,221,222,3,67,
		31,0,222,223,3,93,44,0,223,224,1,0,0,0,224,225,6,4,1,0,225,14,1,0,0,0,
		226,227,3,65,30,0,227,228,3,67,31,0,228,229,3,101,48,0,229,230,3,75,35,
		0,230,231,3,63,29,0,231,232,3,67,31,0,232,16,1,0,0,0,233,234,3,69,32,0,
		234,235,3,87,41,0,235,236,3,93,44,0,236,237,3,83,39,0,237,238,3,59,27,
		0,238,239,3,97,46,0,239,18,1,0,0,0,240,241,3,81,38,0,241,242,3,87,41,0,
		242,243,3,63,29,0,243,244,3,59,27,0,244,245,3,97,46,0,245,246,3,75,35,
		0,246,247,3,87,41,0,247,248,3,85,40,0,248,249,1,0,0,0,249,250,6,7,1,0,
		250,20,1,0,0,0,251,252,3,85,40,0,252,253,3,59,27,0,253,254,3,83,39,0,254,
		255,3,67,31,0,255,256,1,0,0,0,256,257,6,8,1,0,257,22,1,0,0,0,258,259,3,
		89,42,0,259,260,3,59,27,0,260,261,3,93,44,0,261,262,3,97,46,0,262,263,
		3,85,40,0,263,264,3,87,41,0,264,265,1,0,0,0,265,266,6,9,1,0,266,24,1,0,
		0,0,267,268,3,93,44,0,268,269,3,67,31,0,269,270,3,101,48,0,270,271,3,75,
		35,0,271,272,3,95,45,0,272,273,3,75,35,0,273,274,3,87,41,0,274,275,3,85,
		40,0,275,276,1,0,0,0,276,277,6,10,1,0,277,26,1,0,0,0,278,279,3,71,33,0,
		279,280,5,49,0,0,280,281,5,54,0,0,281,282,1,0,0,0,282,283,3,101,48,0,283,
		284,5,56,0,0,284,294,1,0,0,0,285,286,3,71,33,0,286,287,5,50,0,0,287,288,
		5,50,0,0,288,289,1,0,0,0,289,290,3,101,48,0,290,291,5,49,0,0,291,292,5,
		48,0,0,292,294,1,0,0,0,293,278,1,0,0,0,293,285,1,0,0,0,294,28,1,0,0,0,
		295,296,3,77,36,0,296,30,1,0,0,0,297,298,3,89,42,0,298,299,3,75,35,0,299,
		300,3,85,40,0,300,32,1,0,0,0,301,302,3,59,27,0,302,303,3,89,42,0,303,304,
		3,89,42,0,304,305,3,67,31,0,305,306,3,85,40,0,306,307,3,65,30,0,307,34,
		1,0,0,0,308,319,3,65,30,0,309,310,3,87,41,0,310,311,3,67,31,0,311,319,
		1,0,0,0,312,313,3,59,27,0,313,314,3,93,44,0,314,319,1,0,0,0,315,316,3,
		95,45,0,316,317,3,89,42,0,317,319,1,0,0,0,318,308,1,0,0,0,318,309,1,0,
		0,0,318,312,1,0,0,0,318,315,1,0,0,0,319,36,1,0,0,0,320,322,3,51,23,0,321,
		320,1,0,0,0,322,323,1,0,0,0,323,321,1,0,0,0,323,324,1,0,0,0,324,328,1,
		0,0,0,325,327,3,57,26,0,326,325,1,0,0,0,327,330,1,0,0,0,328,326,1,0,0,
		0,328,329,1,0,0,0,329,38,1,0,0,0,330,328,1,0,0,0,331,333,3,57,26,0,332,
		331,1,0,0,0,333,334,1,0,0,0,334,332,1,0,0,0,334,335,1,0,0,0,335,40,1,0,
		0,0,336,339,3,53,24,0,337,339,3,105,50,0,338,336,1,0,0,0,338,337,1,0,0,
		0,339,340,1,0,0,0,340,338,1,0,0,0,340,341,1,0,0,0,341,42,1,0,0,0,342,343,
		3,159,77,0,343,344,3,73,34,0,344,345,3,159,77,0,345,346,1,0,0,0,346,347,
		6,19,2,0,347,44,1,0,0,0,348,349,3,159,77,0,349,350,3,87,41,0,350,351,3,
		159,77,0,351,352,1,0,0,0,352,353,6,20,3,0,353,46,1,0,0,0,354,355,3,159,
		77,0,355,356,3,65,30,0,356,357,3,159,77,0,357,48,1,0,0,0,358,359,3,159,
		77,0,359,360,3,61,28,0,360,361,3,159,77,0,361,362,1,0,0,0,362,363,6,22,
		4,0,363,50,1,0,0,0,364,365,7,0,0,0,365,52,1,0,0,0,366,367,7,1,0,0,367,
		54,1,0,0,0,368,369,7,2,0,0,369,56,1,0,0,0,370,371,7,3,0,0,371,58,1,0,0,
		0,372,373,7,4,0,0,373,60,1,0,0,0,374,375,7,5,0,0,375,62,1,0,0,0,376,377,
		7,6,0,0,377,64,1,0,0,0,378,379,7,7,0,0,379,66,1,0,0,0,380,381,7,8,0,0,
		381,68,1,0,0,0,382,383,7,9,0,0,383,70,1,0,0,0,384,385,7,10,0,0,385,72,
		1,0,0,0,386,387,7,11,0,0,387,74,1,0,0,0,388,389,7,12,0,0,389,76,1,0,0,
		0,390,391,7,13,0,0,391,78,1,0,0,0,392,393,7,14,0,0,393,80,1,0,0,0,394,
		395,7,15,0,0,395,82,1,0,0,0,396,397,7,16,0,0,397,84,1,0,0,0,398,399,7,
		17,0,0,399,86,1,0,0,0,400,401,7,18,0,0,401,88,1,0,0,0,402,403,7,19,0,0,
		403,90,1,0,0,0,404,405,7,20,0,0,405,92,1,0,0,0,406,407,7,21,0,0,407,94,
		1,0,0,0,408,409,7,22,0,0,409,96,1,0,0,0,410,411,7,23,0,0,411,98,1,0,0,
		0,412,413,7,24,0,0,413,100,1,0,0,0,414,415,7,25,0,0,415,102,1,0,0,0,416,
		417,7,26,0,0,417,104,1,0,0,0,418,419,7,27,0,0,419,106,1,0,0,0,420,421,
		7,28,0,0,421,108,1,0,0,0,422,423,7,29,0,0,423,110,1,0,0,0,424,425,5,73,
		0,0,425,426,5,78,0,0,426,427,5,67,0,0,427,428,5,76,0,0,428,429,5,85,0,
		0,429,430,5,68,0,0,430,431,5,69,0,0,431,112,1,0,0,0,432,433,5,68,0,0,433,
		434,5,69,0,0,434,435,5,70,0,0,435,436,5,73,0,0,436,437,5,78,0,0,437,438,
		5,69,0,0,438,114,1,0,0,0,439,440,5,85,0,0,440,441,5,78,0,0,441,442,5,68,
		0,0,442,443,5,69,0,0,443,444,5,70,0,0,444,116,1,0,0,0,445,446,5,73,0,0,
		446,447,5,70,0,0,447,448,5,68,0,0,448,449,5,69,0,0,449,450,5,70,0,0,450,
		118,1,0,0,0,451,452,5,73,0,0,452,453,5,70,0,0,453,454,5,78,0,0,454,455,
		5,68,0,0,455,456,5,69,0,0,456,457,5,70,0,0,457,120,1,0,0,0,458,459,5,69,
		0,0,459,460,5,78,0,0,460,461,5,68,0,0,461,462,5,73,0,0,462,463,5,70,0,
		0,463,122,1,0,0,0,464,465,5,69,0,0,465,466,5,76,0,0,466,467,5,83,0,0,467,
		468,5,69,0,0,468,124,1,0,0,0,469,470,5,82,0,0,470,471,5,69,0,0,471,472,
		5,80,0,0,472,473,5,69,0,0,473,474,5,65,0,0,474,475,5,84,0,0,475,126,1,
		0,0,0,476,477,5,82,0,0,477,478,5,69,0,0,478,479,5,80,0,0,479,480,5,69,
		0,0,480,481,5,78,0,0,481,482,5,68,0,0,482,128,1,0,0,0,483,484,5,77,0,0,
		484,485,5,65,0,0,485,486,5,67,0,0,486,487,5,82,0,0,487,488,5,79,0,0,488,
		130,1,0,0,0,489,490,5,77,0,0,490,491,5,69,0,0,491,492,5,78,0,0,492,493,
		5,68,0,0,493,132,1,0,0,0,494,495,5,61,0,0,495,134,1,0,0,0,496,497,5,33,
		0,0,497,136,1,0,0,0,498,499,5,38,0,0,499,138,1,0,0,0,500,501,5,35,0,0,
		501,140,1,0,0,0,502,503,5,59,0,0,503,142,1,0,0,0,504,505,5,44,0,0,505,
		144,1,0,0,0,506,507,5,46,0,0,507,146,1,0,0,0,508,509,5,46,0,0,509,510,
		5,46,0,0,510,148,1,0,0,0,511,512,5,91,0,0,512,150,1,0,0,0,513,514,5,93,
		0,0,514,152,1,0,0,0,515,516,5,40,0,0,516,154,1,0,0,0,517,518,5,41,0,0,
		518,156,1,0,0,0,519,520,5,36,0,0,520,158,1,0,0,0,521,522,5,39,0,0,522,
		160,1,0,0,0,523,525,7,30,0,0,524,523,1,0,0,0,525,526,1,0,0,0,526,524,1,
		0,0,0,526,527,1,0,0,0,527,528,1,0,0,0,528,529,6,78,0,0,529,162,1,0,0,0,
		530,531,5,59,0,0,531,532,1,0,0,0,532,533,6,79,5,0,533,164,1,0,0,0,534,
		536,9,0,0,0,535,534,1,0,0,0,536,537,1,0,0,0,537,538,1,0,0,0,537,535,1,
		0,0,0,538,166,1,0,0,0,539,541,3,53,24,0,540,539,1,0,0,0,541,542,1,0,0,
		0,542,540,1,0,0,0,542,543,1,0,0,0,543,544,1,0,0,0,544,545,6,81,5,0,545,
		168,1,0,0,0,546,548,3,55,25,0,547,546,1,0,0,0,548,549,1,0,0,0,549,547,
		1,0,0,0,549,550,1,0,0,0,550,551,1,0,0,0,551,552,6,82,5,0,552,170,1,0,0,
		0,553,560,3,59,27,0,554,560,3,61,28,0,555,560,3,63,29,0,556,560,3,65,30,
		0,557,560,3,67,31,0,558,560,3,69,32,0,559,553,1,0,0,0,559,554,1,0,0,0,
		559,555,1,0,0,0,559,556,1,0,0,0,559,557,1,0,0,0,559,558,1,0,0,0,560,563,
		1,0,0,0,561,563,3,57,26,0,562,559,1,0,0,0,562,561,1,0,0,0,563,564,1,0,
		0,0,564,562,1,0,0,0,564,565,1,0,0,0,565,566,1,0,0,0,566,567,6,83,5,0,567,
		172,1,0,0,0,20,0,1,2,3,4,179,293,318,323,328,334,338,340,526,537,542,549,
		559,562,564,6,6,0,0,2,1,0,2,4,0,2,3,0,2,2,0,2,0,0
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
} // namespace Jacobi.CuplLang.Parser
