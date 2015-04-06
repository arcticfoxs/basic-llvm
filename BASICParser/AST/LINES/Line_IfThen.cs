using System.Collections.Generic;
using LLVM;

namespace BASICLLVM.AST
{
	class Line_IfThen : Line
	{
		public RelationalExpression expression;
		public int target;
		Value resultOfExpression;
		Line falseLine;
		IRBuilder builder;

		public Line_IfThen(RelationalExpression _expr, int _goto)
		{
			expression = _expr;
			target = _goto;
		}

		public override BasicBlock code(LLVM.LLVMContext context, LLVM.Module module, LLVM.Function mainFn)
		{
			BasicBlock block = new BasicBlock(context, mainFn, "line" + lineNumber.ToString());
			builder = new IRBuilder(block);

			resultOfExpression = expression.code(context,module,builder);

			firstBlock = block;
			lastBlock = block;
			return block;
		}

		public override void processGoto()
		{
			Line trueLine = Parser.variables.lines[target];
			builder.CreateCondBranch(resultOfExpression, trueLine.firstBlock, falseLine.firstBlock);
		}

		public override void jumpToNext(Line nextLine)
		{
			// Don't add a jump to the next line (this is an IF statement)
			// but now we know what the next line is :)
			falseLine = nextLine;
		}
	}
}
