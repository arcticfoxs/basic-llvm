using LLVM;

namespace BASICLLVM.AST
{
	class Line
	{
		public int lineNumber;
		public bool hasLineNumber = false;
		public BasicBlock firstBlock, lastBlock;

		public Line()
		{
			
		}
		public void addJump(Line jumpTo)
		{
			IRBuilder builder = new IRBuilder(lastBlock);
			builder.CreateBranch(jumpTo.firstBlock);
		}

		public virtual void jumpToNext(Line nextLine)
		{
			addJump(nextLine);
		}

		public virtual BasicBlock code()
		{
			BasicBlock block = new BasicBlock(Parser.context, Parser.function, "remark"+lineNumber.ToString());
			firstBlock = block;
			lastBlock = block;
			return block;
		}

		public virtual void processGoto()
		{
			// most lines don't need to process GOTOs so this is blank
		}

		public BasicBlock bb()
		{
			string lineLabel = hasLineNumber ? "line" + lineNumber.ToString() : "_" + lineNumber.ToString() + "_";
			return new BasicBlock(Parser.context, Parser.function, lineLabel);
		}

	}
}