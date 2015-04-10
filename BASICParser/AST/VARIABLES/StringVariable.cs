using System;
using LLVM;

namespace BASICLLVM.AST
{
	class StringVariable : StringExpression,Variable
	{
		public string name;
		public StringVariable(string _name)
		{
			name = _name.ToUpper();
		}

		public override Value code(IRBuilder builder)
		{
			AllocaInstruction loadAlloc;
			Value output;
			if (Parser.variables.stringIsPointer[name]) {
				loadAlloc = Parser.variables.stringPointers[name];
				output = builder.CreateLoad(loadAlloc, "temp");
			} else {
				loadAlloc = Parser.variables.strings[name];
				output =  builder.CreateGEP(loadAlloc, Parser.zero, "tempStringVariable");
			}
			return output;
		}
	}
}
