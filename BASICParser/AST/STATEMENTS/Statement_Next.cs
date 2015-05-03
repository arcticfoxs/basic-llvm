using LLVM;
namespace BASICLLVM.AST
{
	class Statement_Next : Statement
	{
		public SimpleNumericVariable controlVariable;
		Value limit, increment;
		BasicBlock nextBlock;
		IRBuilder builder;

		public Statement_Next(SimpleNumericVariable v)
		{
			controlVariable = v;
		}

		public override LLVM.BasicBlock code()
		{
			block = bb();
			builder = new IRBuilder(block);

			if (!Parser.variables.limits.ContainsKey(controlVariable.name))
				throw new CompileException("NEXT statement has no matching FOR statement");

			AllocaInstruction limitAlloc = Parser.variables.limits[controlVariable.name];
			limit = builder.CreateLoad(limitAlloc, "limit_" + controlVariable.name);

			AllocaInstruction incrementAlloc = Parser.variables.increments[controlVariable.name];
			increment = builder.CreateLoad(incrementAlloc, "increment_" + controlVariable.name);

			// Can't do any actual stuff now because we don't know where the FOR statement is!

			return block;
		}

		public override void jumpToNext(Statement nextLine)
		{
			nextBlock = nextLine.block;
		}

		public override void processGoto()
		{
			BasicBlock loopBlock = Parser.variables.forLines[controlVariable.name].nextBlock;
			AllocaInstruction alloc = Parser.variables.numbers[controlVariable.name];
			Value controlVal = builder.CreateLoad(alloc, "controlVal");
			controlVal = builder.CreateFAdd(controlVal, increment, "tempIncrement");
			builder.CreateStore(controlVal, alloc);
			Value comparison = builder.CreateFCmp(Predicate.OrderedGreaterThan, controlVal, limit, "tempLimitTest");

			builder.CreateCondBranch(comparison, nextBlock, loopBlock);
		}
	}
}
