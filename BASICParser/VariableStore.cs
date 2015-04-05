using System.Collections.Generic;

namespace BASICLLVM
{
	class VariableStore
	{
		public Dictionary<string, LLVM.AllocaInstruction> strings;

		public VariableStore()
		{
			strings = new Dictionary<string, LLVM.AllocaInstruction>();
		}
	}
}
