﻿using LLVM;

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

				GlobalVariable global = new GlobalVariable(
					module,
				  valConst.GetType(),
				  true, // constant
				  LinkageType.PrivateLinkage, // only visible in this module
				  valConst,
				  ".str"); // the name of the global constant



				Constant valLength = new Constant(context,8,(ulong) strConst.Length+1);
				Type i8Type = Type.GetInteger8PointerType(context);

				AllocaInstruction alloc;

				if (Parser.variables.strings.ContainsKey(var.name))
					alloc = Parser.variables.strings[var.name]; // already allocated
				else
				{
					// new allocation
					alloc = builder.CreateAlloca(i8Type, valLength, var.name);
					// remember allocation
					Parser.variables.strings[var.name] = alloc;
				}

				
				Constant zero = new Constant(context, 32, 0);
				Value stringVal = ConstantExpr.GEP(global,zero,zero);

				builder.CreateStore(stringVal, alloc);

			}
			else
			{

			}

			firstBlock = block;
			lastBlock = block;
			return block;
		}
	}
}
