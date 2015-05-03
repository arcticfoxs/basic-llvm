using LLVM;

namespace BASICLLVM.AST
{
	class Statement_IfThen : Statement
	{
		public RelationalExpression expression;
		public int target;
		Value resultOfExpression;
		Statement falseLine;
		IRBuilder builder;

		public Statement_IfThen(RelationalExpression _expr, int _goto)
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
			Statement trueLine = Parser.variables.lines[target];
			builder.CreateCondBranch(resultOfExpression, trueLine.block, falseLine.block);
		}

		public override void jumpToNext(Statement nextLine)
		{
			// Don't add a jump to the next line (this is an IF statement)
			// but now we know what the next line is :)
			falseLine = nextLine;
		}
	}
}
