using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LLVM;

namespace BASICLLVM.AST
{
	class Line_Goto : Line
	{
		public int gotoTarget;
		public Line_Goto(int _lineNumber, int _gotoLineNumber)
		{
			lineNumber = _lineNumber;
			gotoTarget = _gotoLineNumber;
		}

		public override BasicBlock code()
		{
			BasicBlock block = bb();

			IRBuilder builder = new IRBuilder(block);

			firstBlock = block;
			lastBlock = block;
			return block;

			// This is just an empty block, but we can't process gotos until all lines are created!
		}

		public override void processGoto()
		{
			Line target = Parser.variables.lines[gotoTarget];
			this.addJump(target);
		}

		public override void jumpToNext(Line nextLine)
		{
			// Don't add a jump to the next line (this is a GOTO)
		}

	}
}
