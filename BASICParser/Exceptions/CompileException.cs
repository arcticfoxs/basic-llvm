using System;
namespace BASICLLVM
{
	class CompileException : Exception
	{
		public int lineNumber;
		public int codeLineNumber;
		public string message;

		public string name = "Unknown Error";

		public CompileException(string _errorText)
		{
			lineNumber = Parser.currentLine+1;
			name = _errorText;
			getCodeLineNumber();
		}

		public void getCodeLineNumber()
		{
			if (Parser.variables.codeLineNumbers.ContainsKey(lineNumber-1))
				codeLineNumber = Parser.variables.codeLineNumbers[lineNumber-1];
			else
				codeLineNumber = -1;
		}

		public void print()
		{
			if (lineNumber > 0)
			{
				Console.Write("at source file line " + lineNumber.ToString());
				if (codeLineNumber > 0) Console.WriteLine(" (label " + codeLineNumber.ToString() + ")");
				else Console.WriteLine();
			}

			Console.WriteLine("- " + name);


			if (message != null) Console.WriteLine("  - "+message);
		}

		public void print(string errorType)
		{
			ConsoleColor originalColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(errorType);
			print();
			Console.ForegroundColor = originalColor;
		}

		public static void printColour(string output,ConsoleColor colour)
		{
			ConsoleColor originalColor = Console.ForegroundColor;
			Console.ForegroundColor = colour;
			Console.WriteLine(output);
			Console.ForegroundColor = originalColor;
		}
	}
}
