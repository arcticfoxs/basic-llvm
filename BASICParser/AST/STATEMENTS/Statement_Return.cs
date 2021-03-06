﻿using LLVM;

namespace BASICLLVM.AST
{
	class Statement_Return : Statement
	{
		IRBuilder builder;
		public override LLVM.BasicBlock code()
		{
			block = bb();
			builder = new IRBuilder(block);

			return block;
		}

		public override void jumpToNext(Statement nextLine)
		{
			AllocaInstruction alloc = Parser.variables.returnAddresses.Pop();
			Value addr = builder.CreateLoad(alloc, "block2Address");
			IndirectBrInst ind = builder.CreateIndirectBr(addr, 1);
			foreach(BasicBlock returnBlock in Parser.variables.returnBlocks)
				ind.addDestination(returnBlock);
		}
	}
}
