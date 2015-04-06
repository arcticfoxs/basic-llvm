﻿using LLVM;
namespace BASICLLVM.AST
{
	class Line_Next : Line
	{
		public SimpleNumericVariable controlVariable;
		BasicBlock block;
		Value limit, increment;
		BasicBlock nextBlock;
		IRBuilder builder;

		public Line_Next(SimpleNumericVariable v)
		{
			controlVariable = v;
		}

		public override LLVM.BasicBlock code(LLVM.LLVMContext context, LLVM.Module module, LLVM.Function mainFn)
		{
			block = new BasicBlock(context, mainFn, "line" + lineNumber.ToString());
			builder = new IRBuilder(block);

			AllocaInstruction limitAlloc = Parser.variables.limits[controlVariable.name];
			limit = builder.CreateLoad(limitAlloc, "limit_"+controlVariable.name);

			AllocaInstruction incrementAlloc = Parser.variables.increments[controlVariable.name];
			increment = builder.CreateLoad(incrementAlloc, "increment_" + controlVariable.name);

			// Can't do any actual stuff now because we don't know where the FOR statement is!

			firstBlock = block;
			lastBlock = block;
			return block;
		}

		public override void jumpToNext(Line nextLine)
		{
			nextBlock = nextLine.firstBlock;
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