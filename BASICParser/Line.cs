using System.Collections.Generic;

namespace BASICParser
{
    class Line
    {
		public bool parsed = false;
		public List<Symbol> tokens;
        public int lineNumber = -2;

		public Line()
		{

		}

		public Line(List<Symbol> lexTokens) {
			tokens = lexTokens;
		}

		public Line parse()
		{
			int numTokens = tokens.Count;
			bool foundKeyword = false;

			for (int i = 0; i < tokens.Count; i++)
			{
				Symbol thisToken = tokens[i];

				if (i == 0 && thisToken.symType == Symbol.sym.INTLITERAL)
				{
					// we found a line number!
					lineNumber = thisToken.intPayload;
					continue;
				}

				if (!foundKeyword)
				{
					switch (thisToken.symType)
					{
						case Symbol.sym.PRINT:
							List<Symbol> printTokens = tokens.GetRange(i + 1, tokens.Count - i - 1);
							Line_Print printLine = new Line_Print(this,printTokens);
							printLine.parse();
							return printLine;
						case Symbol.sym.END:
							Line_Exit exitLine = new Line_Exit(this);
							return exitLine;
					}
				}

				// something's gone wrong!
				

			}

			return this;
		}

    }
}
