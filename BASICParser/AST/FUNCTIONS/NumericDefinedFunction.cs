using System;
namespace BASICLLVM.AST
{
	class NumericDefinedFunction
	{
		public string name;

		public NumericDefinedFunction(string letter)
		{
			if (letter.Length > 1) throw new Exception();
			name = letter;
		}
	}
}
