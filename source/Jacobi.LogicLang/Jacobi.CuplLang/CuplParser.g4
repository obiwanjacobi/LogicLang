parser grammar CuplParser;
options { tokenVocab=CuplLexer; }

file: header pin+ equation+;
header: (assembly | company | date | designer | device | format | location | name | partno | revision)*;

assembly: Assembly freeText EndUseSpaces;
company: Company freeText EndUseSpaces;
date: Date freeText EndUseSpaces;
designer: Designer freeText EndUseSpaces;
device: Device Symbol SemiColon;
format: Format Symbol SemiColon;
location: Location freeText EndUseSpaces;
name: Name freeText EndUseSpaces;
partno: Partno freeText EndUseSpaces;
revision: Revision freeText EndUseSpaces;
freeText: FreeText+;

pin: Pin numberOrList Eq LogicNot? (Symbol | list) SemiColon;
nameOrNumber: Symbol | Number;
numberOrList: Number | numberList;
list: BracketOpen symbolExpr (Comma symbolExpr)* BracketClose;
numberList: BracketOpen Number (Comma Number)* BracketClose;
symbolExpr: nameOrNumber | range;
range: nameOrNumber Range nameOrNumber;

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
