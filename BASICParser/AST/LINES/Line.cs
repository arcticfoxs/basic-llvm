using System.Collections.Generic;
using LLVM;

namespace BASICLLVM.AST
{
	class Line
	{
		public int lineNumber = -2;
		public BasicBlock firstBlock, lastBlock;

		public void addJump(Line jumpTo)
		{
			IRBuilder builder = new IRBuilder(lastBlock);
			builder.CreateBranch(jumpTo.firstBlock);
		}

		public virtual void jumpToNext(Line nextLine)
		{
			addJump(nextLine);
		}

		public virtual BasicBlock code(LLVMContext context, Module module, Function mainFn)
		{
			return new BasicBlock(context, mainFn, "dummy");
		}

		public virtual void processGoto(Dictionary<int, Line> lookup)
		{
			// most lines don't need to process GOTOs so this is blank
		}

	}
}