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

		public override BasicBlock code(LLVMContext context, Module module, Function mainFn)
		{
			BasicBlock block = new BasicBlock(context, mainFn, "line" + lineNumber.ToString());
			IRBuilder builder = new IRBuilder(block);

			Type fpType = Type.GetDoubleType(context);

			AllocaInstruction alloc;
			if (Parser.variables.numbers.ContainsKey(varName)) alloc = Parser.variables.numbers[varName];
			else
			{
				alloc = builder.CreateAlloca(fpType, varName);
				Parser.variables.numbers[varName] = alloc;
			}


			Value exprVal = value.code(context, module, builder);
			
			builder.CreateStore(exprVal, alloc);

			firstBlock = block;
			lastBlock = block;
			return block;
		}

	}
}
