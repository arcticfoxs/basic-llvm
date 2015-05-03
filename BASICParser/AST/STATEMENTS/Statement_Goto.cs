using LLVM;

namespace BASICLLVM.AST
{
	class Statement_Goto : Statement
	{
		public int gotoTarget;
		public Statement_Goto(int _lineNumber, int _gotoLineNumber)
		{
			lineNumber = _lineNumber;
			gotoTarget = _gotoLineNumber;
		}

		public override BasicBlock code()
		{
			block = bb();

			IRBuilder builder = new IRBuilder(block);

			return block;

			// This is just an empty block, but we can't process gotos until all lines are created!
		}

		public override void processGoto()
		{
			if (!Parser.variables.lines.ContainsKey(gotoTarget))
			{
				CompileException ex = new CompileException("Unknown GOTO target");
				ex.message = "The target of this GOTO statement does not exist";
				throw ex;
			}
			Statement target = Parser.variables.lines[gotoTarget];
			this.addJump(target);
		}

		public override void jumpToNext(Statement nextLine)
		{
			// Don't add a jump to the next line (this is a GOTO)
		}

	}
}
