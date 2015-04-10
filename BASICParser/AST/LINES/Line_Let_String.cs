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

		public override BasicBlock code()
		{
			BasicBlock block = new BasicBlock(Parser.context, Parser.function, "line" + lineNumber.ToString());
			IRBuilder builder = new IRBuilder(block);

			AllocaInstruction alloc;
			Value stringVal = expr.code(builder); // get value of RHS


			if (expr is StringConstant)
			{
				if (Parser.variables.stringPointers.ContainsKey(var.name))
					alloc = Parser.variables.stringPointers[var.name]; // already allocated
				else
				{
					alloc = builder.CreateAlloca(Parser.i8p, Parser.zero, var.name); // new allocation
					Parser.variables.stringPointers[var.name] = alloc; // remember allocation
				}
				Parser.variables.stringIsPointer[var.name] = true;
			}
			else
			{
				if (Parser.variables.stringPointers.ContainsKey(var.name))
					alloc = Parser.variables.stringPointers[var.name]; // already allocated
				else
				{
					alloc = builder.CreateAlloca(Parser.i8p, Parser.zero, var.name); // new allocation
					Parser.variables.stringPointers[var.name] = alloc; // remember allocation
				}
				Parser.variables.stringIsPointer[var.name] = true;
			}

			builder.CreateStore(stringVal, alloc);

			firstBlock = block;
			lastBlock = block;
			return block;
		}
	}
}
