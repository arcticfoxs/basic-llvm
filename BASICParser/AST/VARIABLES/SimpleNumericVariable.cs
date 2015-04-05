using LLVM;
using System;

namespace BASICLLVM.AST
{
	class SimpleNumericVariable : NumericVariable
	{
		public string name;

		public SimpleNumericVariable(string _name)
		{
			name = _name;
		}
		public override Value code(LLVMContext context, IRBuilder builder)
		{
			AllocaInstruction alloc;
			if (Parser.variables.numbers.ContainsKey(name)) alloc = Parser.variables.numbers[name];
			else throw new NotImplementedException();

			LLVM.Type fpType = LLVM.Type.GetDoubleType(context);
			return builder.CreateLoad(alloc, "temp");
		}
	}
}
