// BASIC Parser Grammar
grammar BASIC;

DIGIT : [0-9] ;
LETTER : ([A-Z]|[a-z]);

// LINES

line : linenumber statement | endline;
linenumber : integer;
endline : linenumber endstatement;
endstatement : 'END';
// don't forget to reinsert optionstatement
statement : datastatement | defstatement | dimensionstatement | gosubstatement | gotostatement | ifthenstatement | inputstatement | letstatement | ongotostatement | printstatement | randomizestatement | readstatement | remarkstatement | restorestatement | returnstatement | stopstatement;

// CONSTANTS

numericconstant : sign? numericrep;
sign : PLUSSIGN | MINUSSIGN;
numericrep : significand exrad?;
significand : integer FULLSTOP? | integer? fraction;
integer : DIGIT DIGIT*;
fraction : FULLSTOP DIGIT DIGIT*;
exrad : 'E' sign? integer;
stringconstant : quotedstring;

// VARIABLES

variable : numericvariable | stringvariable;
numericvariable : simplenumericvariable | numericarrayelement;
simplenumericvariable : LETTER DIGIT?;
numericarrayelement : numericarrayname subscript;
numericarrayname : LETTER;
subscript : LEFTPARENTHESIS numericexpression (COMMA numericexpression)? RIGHTPARENTHESIS;
stringvariable : LETTER DOLLARSIGN;

// expressions

expression : numericexpression | stringexpression;
numericexpression : sign? term (sign term)*;
term : factor (multiplier factor)*;
factor : primary (CIRCUMFLEXACCENT primary)*;
multiplier : ASTERISK | SOLIDUS;
primary : numericvariable | numericrep | numericfunctionref | LEFTPARENTHESIS numericexpression RIGHTPARENTHESIS;
numericfunctionref : numericfunctionname argumentlist?;
numericfunctionname : numericdefinedfunction | numericsuppliedfunction;
argumentlist : LEFTPARENTHESIS argument RIGHTPARENTHESIS;
argument : numericexpression;
stringexpression : stringvariable | stringconstant;

// implementation supplied functions

numericsuppliedfunction : 'ABS' | 'ATN' | 'COS' | 'EXP' | 'INT' | 'LOG' | 'RND' | 'SGN' | 'SIN' | 'SQR' | 'TAN';

// user defined functions

defstatement : 'DEF' numericdefinedfunction parameterlist? EQUALSSIGN numericexpression;
numericdefinedfunction : 'FN' LETTER;
parameterlist : LEFTPARENTHESIS parameter RIGHTPARENTHESIS;
parameter : simplenumericvariable;

// let statement

letstatement : numericletstatement | stringletstatement;
numericletstatement : 'LET' numericvariable EQUALSSIGN numericexpression;
stringletstatement : 'LET' stringvariable EQUALSSIGN stringexpression;

// control statements

gotostatement : 'GO' SPACE* 'TO' linenumber;
ifthenstatement : 'IF' relationalexpression 'THEN' linenumber;
relationalexpression : numericexpression relation numericexpression | stringexpression equalityrelation stringexpression;
relation : equalityrelation | LESSTHANSIGN | GREATERTHANSIGN | notless | notgreater;
equalityrelation : EQUALSSIGN | notequals;
notless : GREATERTHANSIGN EQUALSSIGN;
notgreater : LESSTHANSIGN EQUALSSIGN;
notequals : LESSTHANSIGN GREATERTHANSIGN;
gosubstatement : 'GO' SPACE* 'SUB' linenumber;
returnstatement : 'RETURN';
ongotostatement : 'ON' numericexpression 'GO' SPACE* 'TO' linenumber (COMMA linenumber)*;
stopstatement : 'STOP';

// for and next statements

forline : linenumber forstatement;
nextline : linenumber nextstatement;
forstatement : 'FOR' controlvariable EQUALSSIGN initialvalue 'TO' limit ('STEP' increment)?;
controlvariable : simplenumericvariable;
initialvalue : numericexpression;
limit : numericexpression;
increment : numericexpression;
nextstatement : 'NEXT' controlvariable;

// print statement

printstatement : 'PRINT' printlist?;
printlist : (printitem? printseparator)* printitem+;
printitem : expression | tabcall;
tabcall : 'TAB' LEFTPARENTHESIS numericexpression RIGHTPARENTHESIS;
printseparator : COMMA | SEMICOLON;

// input statement

inputstatement : 'INPUT' variablelist;
variablelist : variable (COMMA variable)*;
inputprompt : 'INPUT?'; // implementationdefined
inputreply : inputlist;
inputlist : paddeddatum (COMMA paddeddatum)*;
paddeddatum : SPACE* datum SPACE*;
datum : quotedstring | unquotedstring;

// read and restore statements

readstatement : 'READ' variablelist;
restorestatement : 'restore';

// data statement

datastatement : 'DATA' datalist;
datalist : datum (COMMA datum)*;

// array declarations

dimensionstatement : 'DIM' arraydeclaration (COMMA arraydeclaration)*;
arraydeclaration : numericarrayname LEFTPARENTHESIS bounds RIGHTPARENTHESIS;
bounds : integer (COMMA integer)?;
// this is totally causing problems!
// optionstatement : 'OPTION BASE' ('0'|'1');

// remark statement

remarkstatement : 'REM' remarkstring;

// randomize statement

randomizestatement : 'RANDOMIZE';

// ignore whitespace
WS : [ \t\r\n]+ -> skip ;

// LEXICAL TOKENS

stringcharacter : QUOTATIONMARK | quotedstringcharacter;
quotedstringcharacter : EXCLAMATIONMARK | NUMBERSIGN | DOLLARSIGN | PERCENTSIGN | AMPERSAND | APOSTROPHE | LEFTPARENTHESIS | RIGHTPARENTHESIS | ASTERISK | COMMA | SOLIDUS | COLON | SEMICOLON | LESSTHANSIGN | EQUALSSIGN | GREATERTHANSIGN | QUESTIONMARK | CIRCUMFLEXACCENT | UNDERLINE | unquotedstringcharacter;
unquotedstringcharacter : SPACE | plainstringcharacter;
plainstringcharacter : PLUSSIGN | MINUSSIGN | FULLSTOP | DIGIT | LETTER;
remarkstring : stringcharacter+;
quotedstring : QUOTATIONMARK quotedstringcharacter* QUOTATIONMARK;
unquotedstring : plainstringcharacter | plainstringcharacter unquotedstringcharacter* plainstringcharacter;

// SYMBOLS

SPACE : ' ';
EXCLAMATIONMARK : '!';
QUOTATIONMARK : '"';
NUMBERSIGN : '#';
DOLLARSIGN : '$';
PERCENTSIGN : '%';
AMPERSAND : '&';
APOSTROPHE : '\'';
LEFTPARENTHESIS : '(';
RIGHTPARENTHESIS : ')';
ASTERISK : '*';
PLUSSIGN : '+';
COMMA : ',';
MINUSSIGN : '-';
FULLSTOP : '.';
SOLIDUS : '/';
COLON : ':';
SEMICOLON : ';';
LESSTHANSIGN : '<';
EQUALSSIGN : '=';
GREATERTHANSIGN : '>';
QUESTIONMARK : '?';
CIRCUMFLEXACCENT : '^';
UNDERLINE : '_';