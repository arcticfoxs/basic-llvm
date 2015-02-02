using System.Collections.Generic;
using LLVM;

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

		public virtual void code(LLVMContext context, Module module, Function mainFn)
		{
			
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
						case Symbol.sym.GOTO:
							if (tokens[i + 1].symType == Symbol.sym.INTLITERAL)
							{
								Line_Goto gotoLine = new Line_Goto(this, tokens[i + 1].intPayload);
								return gotoLine;
							}
							else { 
								// invalid goto 
							}
							break;
						case Symbol.sym.PRINT:
							List<Symbol> printTokens = tokens.GetRange(i + 1, tokens.Count - i - 1);
							Line_Print printLine = new Line_Print(this,printTokens);
							printLine.parse();
							return printLine;
						case Symbol.sym.END:
							Line_Exit exitLine = new Line_Exit(this);
							return exitLine;
						case Symbol.sym.LET:
							if(tokens[i+2].symType == Symbol.sym.EQUALS) {
								// this is a valid assignment
								if ((tokens[i + 1].symType == Symbol.sym.INTVAR))
								{
									// this is an integer assignment
									Line_Let_Int assignmentLine = new Line_Let_Int(this, tokens[i + 1].stringPayload, tokens.GetRange(i + 3, tokens.Count - i - 3));
									return assignmentLine;
								}
							} else {
								// this is not a valid assignment!
							}
							break;
					}
				}

				// something's gone wrong!
				

			}

			return this;
		}

    }
}
