using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASICParser
{
	class Line_Print : Line
	{
		public List<Symbol> printTokens = new List<Symbol>();

		public Line_Print(Line parentLine,List<Symbol> printExpression)
		{
			lineNumber = parentLine.lineNumber;
			tokens = parentLine.tokens;
			parsed = parentLine.parsed;

			printTokens = printExpression;
		}

		public void parse()
		{
			// parse the print tokens into a valid string expression
			if (printTokens.Count == 1)
			{
				// we need a single string literal
				if (printTokens[0].symType == Symbol.sym.STRINGLITERAL)
				{
					// great! now form a simple expression

					parsed = true;
				}
				else
				{
					// throw an exception
				}
			}
			else
			{
				// do some clever parsing
			}
		}
	}
}
