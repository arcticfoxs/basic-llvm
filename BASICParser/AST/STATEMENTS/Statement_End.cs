using LLVM;

namespace BASICLLVM.AST
{
	class Statement_End : Statement
	{
		public static bool existsEnd = false;

		public Statement_End()
		{
			existsEnd = true;
		}
		public override BasicBlock code()
		{
			block = bb();
			
			IRBuilder builder = new IRBuilder(block);

			builder.CreateReturn(Parser.zero32);

			return block;
		}

		public override void jumpToNext(Statement nextLine)
		{
			// Do nothing! This is an END instruction
		}
	}
}
