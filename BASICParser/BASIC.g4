// BASIC Parser Grammar
grammar BASIC;

// LEXICAL TOKENS

LETTER : ('a'..'z' |'A'..'Z' );
DIGIT : ('0'..'9');
STRINGCHARACTER : QUOTATIONMARK | QUOTEDSTRINGCHARACTER;
QUOTEDSTRINGCHARACTER : EXCLAMATIONMARK | NUMBERSIGN | DOLLARSIGN | PERCENTSIGN | AMPERSAND | APOSTROPHE | LEFTPARENTHESIS | RIGHTPARENTHESIS | ASTERISK | COMMA | SOLIDUS | COLON | SEMICOLON | LESSTHANSIGN | EQUALSSIGN | GREATERTHANSIGN | QUESTIONMARK | CIRCUMFLEXACCENT | UNDERLINE | UNQUOTEDSTRINGCHARACTER;
UNQUOTEDSTRINGCHARACTER : SPACE | PLAINSTRINGCHARACTER;
PLAINSTRINGCHARACTER : PLUSSIGN | MINUSSIGN | FULLSTOP | DIGIT | LETTER;
REMARKSTRING : STRINGCHARACTER*;
QUOTEDSTRING : QUOTATIONMARK QUOTEDSTRINGCHARACTER* QUOTATIONMARK;
UNQUOTEDSTRING : PLAINSTRINGCHARACTER | PLAINSTRINGCHARACTER UNQUOTEDSTRINGCHARACTER* PLAINSTRINGCHARACTER;

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

// LINES

line : linenumber statement;
linenumber : DIGIT{1,4};
endline : linenumber endstatement;
endstatement : 'end';
statement : datastatement | defstatement | dimensionstatement | gosubstatement | gotostatement | ifthenstatement | inputstatement | letstatement | ongotostatement | optionstatement | printstatement | randomizestatement | readstatement | remarkstatement | restorestatement | returnstatement | stopstatement;

// CONSTANTS

numericconstant : sign? numericrep;
sign : PLUSSIGN | MINUSSIGN;
numericrep : significand exrad?;
significand : integer FULLSTOP? | integer? fraction;
integer : DIGIT DIGIT*;
fraction : FULLSTOP DIGIT DIGIT*;
exrad : 'E' sign? integer;
stringconstant : QUOTEDSTRING;

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
printlist : (printitem? printseparator)* printitem?;
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
datum : QUOTEDSTRING | UNQUOTEDSTRING;

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
optionstatement : 'OPTION BASE' ('0'|'1');

// remark statement

remarkstatement : 'REM' REMARKSTRING;

// randomize statement

randomizestatement : 'RANDOMIZE';