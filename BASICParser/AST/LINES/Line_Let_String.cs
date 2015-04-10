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

			Constant zero = new Constant(context, 32, 0);
			Type i8p = Type.GetInteger8PointerType(context);

			AllocaInstruction alloc;
			Value stringVal = expr.code(context, module, builder); // get value of RHS


			if (expr is StringConstant)
			{
				if (Parser.variables.stringPointers.ContainsKey(var.name))
					alloc = Parser.variables.stringPointers[var.name]; // already allocated
				else
				{
					alloc = builder.CreateAlloca(i8p, zero, var.name); // new allocation
					Parser.variables.stringPointers[var.name] = alloc; // remember allocation
				}
				Parser.variables.stringIsPointer[var.name] = true;
			}
			else
			{
				if (Parser.variables.strings.ContainsKey(var.name))
					alloc = Parser.variables.strings[var.name]; // already allocated
				else
				{
					alloc = builder.CreateAlloca(i8p, zero, var.name); // new allocation
					Parser.variables.strings[var.name] = alloc; // remember allocation
				}
			}

			builder.CreateStore(stringVal, alloc);

			firstBlock = block;
			lastBlock = block;
			return block;
		}
	}
}
