using System.Collections.Generic;
namespace BASICLLVM
{
	class VariableStore
	{
		public static Dictionary<string, LLVM.AllocaInstruction> strings = new Dictionary<string, LLVM.AllocaInstruction>();
	}
}
