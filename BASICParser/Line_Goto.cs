﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LLVM;

namespace BASICParser
{
	class Line_Goto : Line
	{
		public int gotoTarget;
		public Line_Goto(Line parentLine, int gotoLineNumber)
		{
			lineNumber = parentLine.lineNumber;
			tokens = parentLine.tokens;
			gotoTarget = gotoLineNumber;
			parsed = true;
		}

		public override BasicBlock code(LLVM.LLVMContext context, LLVM.Module module, LLVM.Function mainFn)
		{
			BasicBlock block = new BasicBlock(context, mainFn, "line" + lineNumber.ToString());

			IRBuilder builder = new IRBuilder(block);

			firstBlock = block;
			lastBlock = block;
			return block;

			// This is just an empty block, but we can't process gotos until all lines are created!
		}

		public void processGoto(Dictionary<int, Line> lookup)
		{
			Line target = lookup[gotoTarget];
			this.addJump(target);
		}

		public override void jumpToNext(Line nextLine)
		{
			// Don't add a jump to the next line (this is a GOTO)
		}

	}
}
