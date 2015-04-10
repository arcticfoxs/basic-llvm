using LLVM;
using System;

namespace BASICLLVM.AST
{
	class SimpleNumericVariable : NumericVariable
	{
		public string name;

		public SimpleNumericVariable(string _name)
		{
			name = _name.ToUpper();
		}
		public override Value code(IRBuilder builder)
		{
			AllocaInstruction alloc;
			if (Parser.variables.numbers.ContainsKey(name)) alloc = Parser.variables.numbers[name];
			else throw new NotImplementedException();
			return builder.CreateLoad(alloc, "temp");
		}
	}
}
