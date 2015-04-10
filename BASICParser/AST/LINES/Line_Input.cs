using System;
using System.Collections.Generic;
using LLVM;

namespace BASICLLVM.AST
{
	class Line_Input : Line
	{
		public List<Variable> vars;
		public Line_Input()
		{
			vars = new List<Variable>();
		}

		public override BasicBlock code()
		{
			BasicBlock block = bb();
			IRBuilder builder = new IRBuilder(block);

			AllocaInstruction alloc;

			Constant valLength = new Constant(Parser.context, 8, 50L);

			if (vars.Count == 0)
			{
				CompileException ex = new CompileException("Expected input variable");
				ex.message = "INPUT statements require at least one input variable";
				throw ex;
			}

			bool isNumeric = !(vars[0] is StringVariable);

			if (!isNumeric)
			{
				StringVariable var = (StringVariable)vars[0];

				if (Parser.variables.strings.ContainsKey(var.name))
					alloc = Parser.variables.strings[var.name]; // already allocated
				else
				{
					alloc = builder.CreateAlloca(Parser.i8, valLength, var.name); // new allocation
					Parser.variables.strings[var.name] = alloc; // remember allocation
				}
				Parser.variables.stringIsPointer[var.name] = false;
			}
			else
			{
				SimpleNumericVariable var = (SimpleNumericVariable)vars[0];

				if (Parser.variables.numbers.ContainsKey(var.name)) alloc = Parser.variables.numbers[var.name];
				else
				{
					alloc = builder.CreateAlloca(Parser.dbl, var.name);
					Parser.variables.numbers[var.name] = alloc;
				}
			}


			// Import scanf function
			LLVM.Type[] argTypes = isNumeric ? new LLVM.Type[] { Parser.i8p, Parser.dblp } : new LLVM.Type[] { Parser.i8p, Parser.i8p };

			FunctionType stringToInt = new FunctionType(Parser.vd, argTypes);
			Constant scanf = Parser.module.GetOrInsertFunction("scanf", stringToInt);

			string formatString = isNumeric ? "%lf" : "%79s";
			StringConstant format = new StringConstant(formatString);
			Value formatValue = format.code(builder);

			Value gep = builder.CreateGEP(alloc, Parser.zero, "gepTest");
			
			Value[] args = {formatValue,gep};

			Value hop = (Value)alloc;


			Value outputs = builder.CreateCall(scanf, args);


			firstBlock = block;
			lastBlock = block;
			return block;
		}
	}
}
