using System.Collections.Generic;

namespace BASICParser
{
    class Line
    {
		public bool parsed = false;
		public List<Symbol> lines;
        public int lineNumber = -2;

		public Line(List<Symbol> lexLines) {
			lines = lexLines;
		}

		public void parse()
		{
			foreach (Symbol token in lines)
			{
				if (lineNumber == -2)
				{
					// No line number entered yet
					if (token.symType == Symbol.sym.INTLITERAL)
					{
						lineNumber = token.intPayload; // Found a line number
						continue;
					}
					else lineNumber = -1; // this line doesn't have a line number
				}


			}
		}
    }
}
