using System;
using BASICLLVM.AST;
using LLVM;

namespace BASICLLVM
{
	class Line_Let_Int : Line
	{
		public NumericVariable var;

		public NumericExpression value;

		public Line_Let_Int()
		{

		}

		public override BasicBlock code()
		{
			BasicBlock block = bb();
			IRBuilder builder = new IRBuilder(block);
			Value alloc;

			if (var is SimpleNumericVariable)
			{
				SimpleNumericVariable simpleVar = (SimpleNumericVariable)var;
				if (Parser.variables.numbers.ContainsKey(simpleVar.name)) alloc = Parser.variables.numbers[simpleVar.name];
				else
				{
					Parser.variables.numbers[simpleVar.name] = builder.CreateAlloca(Parser.dbl, simpleVar.name);
					alloc = Parser.variables.numbers[simpleVar.name];
				}
			}
			else {
				NumericArrayElement arrayElement = (NumericArrayElement)var;
				alloc = Parser.variables.arrayItem(builder, arrayElement.numericarrayname, arrayElement.index.code(builder));
			}

			Value exprVal = value.code(builder);

			builder.CreateStore(exprVal, alloc);

			firstBlock = block;
			lastBlock = block;
			return block;
		}

	}
}
