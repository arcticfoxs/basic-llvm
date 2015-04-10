namespace BASICLLVM
{
	public class SyntaxErrorException : System.Exception
	{
		public int lineNumber;
		public string errorText;
		public int codeLineNumber;
		public SyntaxErrorException(int _lineNumber, string _errorText)
		{
			lineNumber = _lineNumber+1;
			codeLineNumber = -2;
			errorText = _errorText;
		}

		public SyntaxErrorException(int _lineNumber, int _codeLineNumber, string _errorText)
		{
			lineNumber = _lineNumber+1;
			errorText = _errorText;
			codeLineNumber = _codeLineNumber;
		}
	}

}
