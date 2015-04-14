using Antlr4.Runtime;
using System;

namespace BASICLLVM
{
	public class AntlrErrorListener : BaseErrorListener
	{
		public static AntlrErrorListener INSTANCE = new AntlrErrorListener();
		public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
		{
			// suppress this error if we're not in debug mode
			if (!Program.debug)
				return;

			string position = String.Format("{0}:{1}: ", line, charPositionInLine);
			CompileException.printColour(position + msg,ConsoleColor.Cyan);
		}
	}
}