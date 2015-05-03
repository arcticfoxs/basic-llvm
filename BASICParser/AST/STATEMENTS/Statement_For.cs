using LLVM;

namespace BASICLLVM.AST
{
	class Statement_For : Statement
	{
		public SimpleNumericVariable v;
		public NumericExpression initialvalue, limit;
		public NumericExpression increment;
		public BasicBlock nextBlock;

		public Statement_For(SimpleNumericVariable _v, NumericExpression _inital, NumericExpression _limit, NumericExpression _increment = null)
		{
			v = _v;
			initialvalue = _inital;
			limit = _limit;
			increment = (_increment == null) ? one() : _increment;
		}

		NumericExpression one()
		{
			NumericRep rep_one = new NumericRep(new Significand(new Fraction("1")));
			Factor factor = new Factor();
			factor.add(new NumericConstant(rep_one));
			Term term = new Term();
			term.add(factor);
			NumericExpression ne = new NumericExpression();
			ne.add(term);
			return ne;
		}

		public override BasicBlock code()
		{
			block = bb();
			IRBuilder builder = new IRBuilder(block);

			AllocaInstruction alloc;

			// store control variable
			string varName = v.name;
			if (Parser.variables.numbers.ContainsKey(varName)) alloc = Parser.variables.numbers[varName];
			else
			{
				alloc = builder.CreateAlloca(Parser.dbl, varName);
				Parser.variables.numbers[varName] = alloc;
			}
			Value exprVal = initialvalue.code(builder);
			builder.CreateStore(exprVal, alloc);

			// store limit
			if (Parser.variables.limits.ContainsKey(varName)) alloc = Parser.variables.limits[varName];
			else
			{
				alloc = builder.CreateAlloca(Parser.dbl, "limit_" + varName);
				Parser.variables.limits[varName] = alloc;
			}
			exprVal = limit.code(builder);
			builder.CreateStore(exprVal, alloc);

			// store increment
			if (Parser.variables.increments.ContainsKey(varName)) alloc = Parser.variables.increments[varName];
			else
			{
				alloc = builder.CreateAlloca(Parser.dbl, "increment_" + varName);
				Parser.variables.increments[varName] = alloc;
			}
			exprVal = increment.code(builder);
			builder.CreateStore(exprVal, alloc);

			// store the FOR statement so the NEXT statement can find it
			Parser.variables.forLines[varName] = this;

			return block;
		}

		public override void jumpToNext(Statement nextLine)
		{
			nextBlock = nextLine.block;
			addJump(nextLine);
		}
	}
}
