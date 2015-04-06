using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LLVM;

namespace BASICLLVM.AST
{
	class Line_End : Line
	{

		public override BasicBlock code(LLVM.LLVMContext context, LLVM.Module module, LLVM.Function mainFn)
		{
			BasicBlock block = new BasicBlock(context, mainFn, "line" + lineNumber.ToString());
			
			IRBuilder builder = new IRBuilder(block);

			Constant zero = new Constant(context, 32, 0L);
			builder.CreateReturn(zero);

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
