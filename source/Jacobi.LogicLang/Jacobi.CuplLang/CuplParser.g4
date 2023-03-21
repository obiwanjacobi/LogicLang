parser grammar CuplParser;
options { tokenVocab=CuplLexer; }

file: header pin* equation*;
header: (assembly | company | date | designer | device | format | location | name | partno | revision)*;

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
freeText: FreeText+;

pin: pinSingle | pinList | pinRange;
pinSingle: Pin Number Eq LogicNot? Symbol SemiColon;
pinList: Pin numberList Eq LogicNot? symbolList SemiColon;
pinRange: Pin numberRange Eq LogicNot? symbolRange SemiColon;
numberList: BracketOpen Number (Comma Number)* BracketClose;
numberRange: BracketOpen Number Range Number BracketClose;
symbolList: BracketOpen Symbol (Comma Symbol)* BracketClose;
symbolRange: BracketOpen Symbol Range Number BracketClose;

equation: Append? LogicNot? Symbol extension? Eq expression SemiColon;
expression: 
    expression binOp expression                 #expressionBinary
    | uniOp expression                          #expressionUnaryPrefix
    | ParenOpen expression ParenClose           #expressionPrecedence
    | Symbol                                    #expressionIdentifier
    | (PrefixHex | PrefixBinary | PrefixDecimal | PrefixOctal)? (Number | DontCareNumber)  #expressionNumber
    ;
extension: Dot Extension;
binOp: (LogicAnd | LogicOr | Dollar /*XOR*/);
uniOp: LogicNot;
