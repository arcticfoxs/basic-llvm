using System.Collections.Generic;

namespace BASICLLVM
{
	class VariableStore
	{
		public Dictionary<string, LLVM.AllocaInstruction> strings;
		public Dictionary<string, LLVM.AllocaInstruction> stringPointers;
		public Dictionary<string, bool> stringIsPointer;
		public Dictionary<string, LLVM.AllocaInstruction> numbers;

		public Dictionary<string, LLVM.AllocaInstruction> limits;
		public Dictionary<string, LLVM.AllocaInstruction> increments;
		public Stack<LLVM.AllocaInstruction> returnAddresses;
		public Dictionary<string, AST.Line_For> forLines;
		public Dictionary<string, AST.Line_GoSub> gosubLines;
		public Dictionary<int, AST.Line> lines;
		public List<LLVM.BasicBlock> returnBlocks;

		public VariableStore()
		{
			strings = new Dictionary<string, LLVM.AllocaInstruction>();
			stringPointers = new Dictionary<string, LLVM.AllocaInstruction>();
			stringIsPointer = new Dictionary<string, bool>();
			numbers = new Dictionary<string, LLVM.AllocaInstruction>();
			limits = new Dictionary<string, LLVM.AllocaInstruction>();
			increments = new Dictionary<string, LLVM.AllocaInstruction>();
			forLines = new Dictionary<string, AST.Line_For>();
			gosubLines = new Dictionary<string, AST.Line_GoSub>();
			lines = new Dictionary<int, AST.Line>();
			returnAddresses = new Stack<LLVM.AllocaInstruction>();
			returnBlocks = new List<LLVM.BasicBlock>();
		}
	}
}
