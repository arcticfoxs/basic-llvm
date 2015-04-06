﻿using LLVM;

namespace BASICLLVM.AST
{
	class Line_Return : Line
	{
		BasicBlock block;
		IRBuilder builder;
		public override LLVM.BasicBlock code(LLVM.LLVMContext context, LLVM.Module module, LLVM.Function mainFn)
		{
			block = new BasicBlock(context, mainFn, "line" + lineNumber.ToString());
			builder = new IRBuilder(block);

			firstBlock = block;
			lastBlock = block;
			return block;
		}

		public override void jumpToNext(Line nextLine)
		{
			AllocaInstruction alloc = Parser.variables.returnAddresses.Pop();
			Value addr = builder.CreateLoad(alloc, "block2Address");
			IndirectBrInst ind = builder.CreateIndirectBr(addr, 1);
			foreach(BasicBlock returnBlock in Parser.variables.returnBlocks)
				ind.addDestination(returnBlock);
		}
	}
}
