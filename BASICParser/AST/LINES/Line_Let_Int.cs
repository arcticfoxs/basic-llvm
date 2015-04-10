using BASICLLVM.AST;
using LLVM;

namespace BASICLLVM
{
	class Line_Let_Int : Line
	{
		public string varName;

		public NumericExpression value;

		public Line_Let_Int()
		{

		}

		public override BasicBlock code()
		{
			BasicBlock block = new BasicBlock(Parser.context, Parser.function, "line" + lineNumber.ToString());
			IRBuilder builder = new IRBuilder(block);

			AllocaInstruction alloc;
			if (Parser.variables.numbers.ContainsKey(varName)) alloc = Parser.variables.numbers[varName];
			else
			{
				alloc = builder.CreateAlloca(Parser.dbl, varName);
				Parser.variables.numbers[varName] = alloc;
			}


			Value exprVal = value.code(builder);
			
			builder.CreateStore(exprVal, alloc);

			firstBlock = block;
			lastBlock = block;
			return block;
		}

	}
}
