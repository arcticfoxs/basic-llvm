using System.Collections.Generic;

namespace BASICLLVM
{
	class VariableStore
	{
		public Dictionary<string, LLVM.AllocaInstruction> strings;
		public Dictionary<string, LLVM.AllocaInstruction> numbers;

		public VariableStore()
		{
			strings = new Dictionary<string, LLVM.AllocaInstruction>();
			numbers = new Dictionary<string, LLVM.AllocaInstruction>();
		}
	}
}
