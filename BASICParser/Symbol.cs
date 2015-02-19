using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASICLLVM
{
    class Symbol
    {
        public enum sym { SEMI,PLUS,TIMES,MINUS, LPAREN,RPAREN,INTLITERAL,STRINGLITERAL, PRINT, END, LET, GOTO, INTVAR, EQUALS }

        public sym symType;
        public int intPayload;
        public string stringPayload;
		public bool hasPayload = false;

        public Symbol()
        {

        }

        public Symbol(sym symbolType)
        {
            symType = symbolType;
        }

        public Symbol(sym symbolType, string payload)
        {
            symType = symbolType;
			intPayload = -1;
			stringPayload = null;
			if (symbolType == sym.INTLITERAL) intPayload = Convert.ToInt32(payload);
			else if (symbolType == sym.STRINGLITERAL) stringPayload = payload;
			else if (symbolType == sym.INTVAR) stringPayload = payload;
			hasPayload = true;
        }
    }
}
