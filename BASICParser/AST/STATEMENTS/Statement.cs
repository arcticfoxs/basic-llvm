using LLVM;

namespace BASICLLVM.AST
{
	class Statement
	{
		public int lineNumber;
		public bool hasLineNumber = false;
		public BasicBlock block;

		public Statement()
		{

		}
		public void addJump(Statement jumpTo)
		{
			IRBuilder builder = new IRBuilder(block);
			builder.CreateBranch(jumpTo.block);
		}

		public virtual void jumpToNext(Statement nextLine)
		{
			addJump(nextLine);
		}

		public virtual BasicBlock code()
		{
			block = new BasicBlock(Parser.context, Parser.function, "remark" + lineNumber.ToString());
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