lexer grammar CuplLexer;

// comments
Comments: '/*' .*? '*/' -> skip;

// header fields
Assembly: (A S S E M B L Y) -> mode(UseSpaces);
Company: (C O M P A N Y) -> mode(UseSpaces);
Date: (D A T E) -> mode(UseSpaces);
Designer: (D E S I G N E R) -> mode(UseSpaces);
Device: (D E V I C E);
Format: (F O R M A T);
Location: (L O C A T I O N) -> mode(UseSpaces);
Name: (N A M E) -> mode(UseSpaces);
Partno: (P A R T N O) -> mode(UseSpaces);
Revision: (R E V I S I O N) -> mode(UseSpaces);

// keywords
Pin: (P I N);
Append: (A P P E N D);

// extensions
Extension: 'd' | 'oe' | 'ar' | 'sp';

DeviceName: (G '16' V '8') | (G '22' V '10');
FormatName: J;

Symbol: ALPHA+ DIGIT10*;
Number: (DIGIT10)+;
DontCareNumber: (DIGIT10 | 'X')+;

PrefixHex: Quote H Quote;
PrefixOctal: Quote O Quote;
PrefixDecimal: Quote D Quote;
PrefixBinary: Quote B Quote;

fragment ALPHA: [a-zA-Z];
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

// ignore whitespace and line endings
WS: [ \t\r\n]+ -> skip;

//---------------------------------------------------------

mode UseSpaces;
EndUseSpaces: ';' -> mode(DEFAULT_MODE);
FreeText: .+?;
