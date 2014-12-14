using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASICParser
{
    class Symbol
    {
        public enum sym { SEMI,PLUS,TIMES,LPAREN,RPAREN,INTLITERAL,STRINGLITERAL }

        public sym symType;
        public int intPayload;
        public string stringPayload;

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
            if (symbolType == sym.INTLITERAL) intPayload = Convert.ToInt32(payload);
            else if (symbolType == sym.STRINGLITERAL) stringPayload = payload;
        }
    }
}
