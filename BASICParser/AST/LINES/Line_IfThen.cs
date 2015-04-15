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

		public override BasicBlock code()
		{
			block = bb();
			builder = new IRBuilder(block);

			resultOfExpression = expression.code(builder);

			return block;
		}

		public override void processGoto()
		{
			Line trueLine = Parser.variables.lines[target];
			builder.CreateCondBranch(resultOfExpression, trueLine.block, falseLine.block);
		}

		public override void jumpToNext(Line nextLine)
		{
			// Don't add a jump to the next line (this is an IF statement)
			// but now we know what the next line is :)
			falseLine = nextLine;
		}
	}
}
