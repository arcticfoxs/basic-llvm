using System;
using LLVM;

namespace BASICLLVM.AST
{
	class StringVariable : StringExpression,Variable
	{
		public string name;
		public StringVariable(string _name)
		{
			name = _name;
		}

		public override Value code(LLVMContext context, Module module, IRBuilder builder)
		{
			Constant zero = new Constant(context, 32, 0);
			AllocaInstruction loadAlloc;
			Value output;
			if (Parser.variables.stringIsPointer[name]) {
				loadAlloc = Parser.variables.stringPointers[name];
				output = builder.CreateLoad(loadAlloc, "temp");
			} else {
				loadAlloc = Parser.variables.strings[name];
				output =  builder.CreateGEP(loadAlloc, zero, "tempStringVariable");
				// output = builder.CreateLoad(loadAlloc, "temp");
			}
			return output;
		}
	}
}
