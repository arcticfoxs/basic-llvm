using System;
namespace BASICLLVM
{
	class CompileException : Exception
	{
		public int lineNumber;
		public int codeLineNumber;
		public string message;

		public string name = "Unknown Error";

		public CompileException(int _lineNumber, string _errorText)
		{
			lineNumber = _lineNumber+1;
			name = _errorText;
			getCodeLineNumber();
		}

		public CompileException(int _lineNumber)
		{
			lineNumber = _lineNumber + 1;
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
			Console.WriteLine(name + " at input line " + lineNumber.ToString());
			if (codeLineNumber > 0) Console.WriteLine("Labelled line " + codeLineNumber.ToString());
			if (message != null) Console.WriteLine(message);
		}

		public void print(string errorType)
		{
			ConsoleColor originalColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(errorType);
			print();
			Console.ForegroundColor = originalColor;
		}
	}
}
