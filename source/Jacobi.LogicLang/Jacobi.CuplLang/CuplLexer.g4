lexer grammar CuplLexer;

// comments
Comments: '/*' .*? '*/' -> skip;

// header fields
Assembly: (A S S E M B L Y) -> mode(UseSpacesMode);
Company: (C O M P A N Y) -> mode(UseSpacesMode);
Date: (D A T E) -> mode(UseSpacesMode);
Designer: (D E S I G N E R) -> mode(UseSpacesMode);
Device: (D E V I C E);
Format: (F O R M A T);
Location: (L O C A T I O N) -> mode(UseSpacesMode);
Name: (N A M E) -> mode(UseSpacesMode);
Partno: (P A R T N O) -> mode(UseSpacesMode);
Revision: (R E V I S I O N) -> mode(UseSpacesMode);

DeviceName: (G '16' V '8') | (G '22' V '10');
FormatName: J;

// keywords
Pin: (P I N);
Append: (A P P E N D);

Symbol: ALPHA (ALPHA | DIGIT10 | Underscore)*;
Number: (DIGIT10)+;
DontCareNumber: (DIGIT2 | X)+;

PrefixHex: Quote H Quote -> mode(HexNumberMode);
PrefixOctal: Quote O Quote -> mode(OctNumberMode);
PrefixDecimal: Quote D Quote;
PrefixBinary: Quote B Quote -> mode(BinNumberMode);

// extensions
Extension: D | (O E) | (A R) | (S P);

fragment ALPHA: [a-zA-Z];
fragment DIGIT2: [01];
fragment DIGIT8: [0-7];
fragment DIGIT10: [0-9];

fragment A: [aA];
fragment B: [bB];
fragment C: [cC];
fragment D: [dD];
fragment E: [eE];
fragment F: [fF];
fragment G: [gG];
fragment H: [hH];
fragment I: [iI];
fragment J: [jJ];
fragment K: [kK];
fragment L: [lL];
fragment M: [mM];
fragment N: [nN];
fragment O: [oO];
fragment P: [pP];
fragment Q: [qQ];
fragment R: [rR];
fragment S: [sS];
fragment T: [tT];
fragment U: [uU];
fragment V: [vV];
fragment W: [wW];
fragment X: [xX];
fragment Y: [yY];
fragment Z: [zZ];

// preprocessor
Include: 'INCLUDE';
Define: 'DEFINE';
UnDef: 'UNDEF';
IfDef: 'IFDEF';
IfNotDef: 'IFNDEF';
EndIf: 'ENDIF';
Else: 'ELSE';
Repeat: 'REPEAT';
RepEnd: 'REPEND';
Macro: 'MACRO';
MEnd: 'MEND';

// symbols
Eq: '=';
LogicNot: '!';
LogicAnd: '&';
LogicOr: '#';
SemiColon: ';';
Comma: ',';
Dot: '.';
Range: '..';
BracketOpen: '[';
BracketClose: ']';
ParenOpen: '(';
ParenClose: ')';
Dollar: '$';
Quote: '\'';
Underscore: '_';

// ignore whitespace and line endings
WS: [ \t\r\n]+ -> skip;

//---------------------------------------------------------

mode UseSpacesMode;
EndUseSpaces: ';' -> mode(DEFAULT_MODE);
FreeText: .+?;

//---------------------------------------------------------

mode BinNumberMode;
BinNumber: (DIGIT2)+ -> mode(DEFAULT_MODE);

//---------------------------------------------------------

mode OctNumberMode;
OctNumber: (DIGIT8)+ -> mode(DEFAULT_MODE);

//---------------------------------------------------------

mode HexNumberMode;
HexNumber: ((A|B|C|D|E|F) | DIGIT10)+ -> mode(DEFAULT_MODE);
