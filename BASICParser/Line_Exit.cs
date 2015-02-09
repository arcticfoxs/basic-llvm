using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LLVM;

namespace BASICParser
{
	class Line_Exit : Line
	{
		public Line_Exit(Line parentLine)
		{
			lineNumber = parentLine.lineNumber;
			tokens = parentLine.tokens;
			parsed = true;
			// done! Exit is so easy to parse
		}

		public override BasicBlock code(LLVM.LLVMContext context, LLVM.Module module, LLVM.Function mainFn)
		{
			BasicBlock block = new BasicBlock(context, mainFn, "line" + lineNumber.ToString());
			
			IRBuilder builder = new IRBuilder(block);

			builder.CreateReturn();

			firstBlock = block;
			lastBlock = block;
			return block;
		}
	}
}
