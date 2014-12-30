using TUVienna.CS_CUP.Runtime;
%%
%cup
%%
";" { return new BASICParser.Symbol(BASICParser.Symbol.sym.SEMI); }
"+" { return new BASICParser.Symbol(BASICParser.Symbol.sym.PLUS); }
"-" { return new BASICParser.Symbol(BASICParser.Symbol.sym.MINUS); }
"*" { return new BASICParser.Symbol(BASICParser.Symbol.sym.TIMES); }
"(" { return new BASICParser.Symbol(BASICParser.Symbol.sym.LPAREN); }
")" { return new BASICParser.Symbol(BASICParser.Symbol.sym.RPAREN); }
"=" { return new BASICParser.Symbol(BASICParser.Symbol.sym.EQUALS); }

"PRINT" { return new BASICParser.Symbol(BASICParser.Symbol.sym.PRINT); }
"END" { return new BASICParser.Symbol(BASICParser.Symbol.sym.END); }
"LET" { return new BASICParser.Symbol(BASICParser.Symbol.sym.LET); }
"GOTO" { return new BASICParser.Symbol(BASICParser.Symbol.sym.GOTO); }

([A-Z])([0-9])? { return new BASICParser.Symbol(BASICParser.Symbol.sym.INTVAR, yytext()); }

[0-9]+ { return new BASICParser.Symbol(BASICParser.Symbol.sym.INTLITERAL, yytext()); }
\".*\" { return new BASICParser.Symbol(BASICParser.Symbol.sym.STRINGLITERAL, yytext()); }

[ \t\r\n\f] { /* ignore white space. */break; }
. { System.Console.Error.WriteLine("Illegal character: "+yytext());break; }
