using LLVM;

namespace BASICLLVM.AST
{
	class Line_End : Line
	{

		public override BasicBlock code()
		{
			BasicBlock block = new BasicBlock(Parser.context, Parser.function, "line" + lineNumber.ToString());
			
			IRBuilder builder = new IRBuilder(block);

			builder.CreateReturn(Parser.zero);

			firstBlock = block;
			lastBlock = block;
			return block;
		}

		public override void jumpToNext(Line nextLine)
		{
			// Do nothing! This is an END instruction
		}
	}
}
