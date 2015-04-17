using LLVM;

namespace BASICLLVM.AST
{
	class Line_End : Line
	{
		public static bool existsEnd = false;

		public Line_End()
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

		public override void jumpToNext(Line nextLine)
		{
			// Do nothing! This is an END instruction
		}
	}
}
