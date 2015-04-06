using System.Collections.Generic;

namespace BASICLLVM
{
	class VariableStore
	{
		public Dictionary<string, LLVM.AllocaInstruction> strings;
		public Dictionary<string, LLVM.AllocaInstruction> numbers;
		public Dictionary<string, LLVM.AllocaInstruction> limits;
		public Dictionary<string, LLVM.AllocaInstruction> increments;
		public Dictionary<string, AST.Line_For> forLines;
		public Dictionary<int, AST.Line> lines;

		public VariableStore()
		{
			strings = new Dictionary<string, LLVM.AllocaInstruction>();
			numbers = new Dictionary<string, LLVM.AllocaInstruction>();
			limits = new Dictionary<string, LLVM.AllocaInstruction>();
			increments = new Dictionary<string, LLVM.AllocaInstruction>();
			forLines = new Dictionary<string, AST.Line_For>();
			lines = new Dictionary<int, AST.Line>();
		}
	}
}
