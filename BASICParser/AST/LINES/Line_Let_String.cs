using LLVM;

namespace BASICLLVM.AST
{
	class Line_Let_String : Line
	{
		StringVariable var;
		StringExpression expr;

		public Line_Let_String(StringVariable _var, StringExpression _expr)
		{
			var = _var;
			expr = _expr;
		}

		public override BasicBlock code(LLVMContext context, Module module, Function mainFn)
		{
			BasicBlock block = new BasicBlock(context, mainFn, "line" + lineNumber.ToString());
			IRBuilder builder = new IRBuilder(block);

			if (expr is StringConstant)
			{
				string strConst = ((StringConstant)expr).value;
				Constant valConst = new Constant(context, strConst);
				Constant valLength = new Constant(context,8,(ulong) strConst.Length+1);
				Type i8Type = Type.GetInteger8Type(context);

				// this doesn't work yet because need arrays to handle string
				/*
				AllocaInstruction alloc = builder.CreateAlloca(i8Type, valLength, var.name);
				builder.CreateStore(valConst, alloc);
				*/
			}

			firstBlock = block;
			lastBlock = block;
			return block;
		}
	}
}
