parser grammar CuplParser;
options { tokenVocab=CuplLexer; }

file: header pin* equation* EOF;
header: (assembly | company | date | designer | device | format | location | name | partno | revision | rev)*;

assembly: Assembly freeText EndUseSpaces;
company: Company freeText EndUseSpaces;
date: Date freeText EndUseSpaces;
designer: Designer freeText EndUseSpaces;
device: Device DeviceName SemiColon;
format: Format FormatName SemiColon;
location: Location freeText EndUseSpaces;
name: Name freeText EndUseSpaces;
partno: Partno freeText EndUseSpaces;
revision: Revision freeText EndUseSpaces;
rev: Rev freeText EndUseSpaces;
freeText: FreeText+;

// TODO: these different pin notations can be used combined in one line.
// pin [2..5, 7, 9] = ...
pin: pinSingle | pinList | pinRange;
pinSingle: Pin Number Eq LogicNot? Symbol SemiColon;
pinList: Pin numberList Eq LogicNot? symbolList SemiColon;
pinRange: Pin numberRange Eq LogicNot? symbolRange SemiColon;
numberList: BracketOpen Number (Comma Number)* BracketClose;
numberRange: BracketOpen Number Range Number BracketClose;
symbolList: BracketOpen Symbol (Comma Symbol)* BracketClose;
symbolRange: BracketOpen Symbol Range (Symbol | Number) BracketClose;

equation: Append? LogicNot? symbol extension? Eq expression SemiColon;
symbol: Symbol | symbolList | symbolRange;
expression: 
    LogicNot expression                         #expressionUnaryNot
    | expression LogicAnd expression            #expressionBinaryAnd
    | expression LogicOr expression             #expressionBinaryOr
    | expression Dollar expression              #expressionBinaryXor
    | ParenOpen expression ParenClose           #expressionPrecedence
    | Symbol                                    #expressionSymbol
    | number                                    #expressionNumber
    ;
number: binNumber | octNumber | decNumber | hexNumber | dontCareNumber;
binNumber: PrefixBinary BinNumber;
octNumber: PrefixOctal OctNumber;
decNumber: PrefixDecimal? Number;
hexNumber: PrefixHex HexNumber;
dontCareNumber: PrefixBinary DontCareNumber;
extension: Dot Extension;
