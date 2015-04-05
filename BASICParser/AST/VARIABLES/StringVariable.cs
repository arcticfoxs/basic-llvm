using LLVM;

namespace BASICLLVM.AST
{
	class StringVariable : StringExpression
	{
		public string name;
		public StringVariable(string _name)
		{
			name = _name;
		}

		public override Value code(LLVMContext context, Module module, IRBuilder builder)
		{
			AllocaInstruction loadAlloc = Parser.variables.strings[name];
			return builder.CreateLoad(loadAlloc, "temp");
		}
	}
}
