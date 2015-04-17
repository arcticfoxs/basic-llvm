using LLVM;

namespace BASICLLVM.AST
{
	class Line_GoSub : Line
	{
		int gotoTarget;
		BasicBlock nextBlock;
		IRBuilder builder;

		public Line_GoSub(int target)
		{
			gotoTarget = target;
		}

		public override void jumpToNext(Line nextLine)
		{
			nextBlock = nextLine.block;
			AllocaInstruction alloc = builder.CreateAlloca(Parser.i8p, "returnAddress");
			BlockAddress addr = BlockAddress.Get(Parser.function, nextBlock);
			builder.CreateStore(addr, alloc);
			Parser.variables.returnAddresses.Push(alloc);
			Parser.variables.returnBlocks.Add(nextLine.block);
		}

		public override BasicBlock code()
		{
			block = bb();

			builder = new IRBuilder(block);

			return block;

			// This is just an empty block, but we can't process gotos until all lines are created!
		}

		public override void processGoto()
		{
			Line target = Parser.variables.lines[gotoTarget];
			this.addJump(target);
		}
	}
}
