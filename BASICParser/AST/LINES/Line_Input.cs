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

		public override BasicBlock code(LLVMContext context, Module module, Function mainFn)
		{
			BasicBlock block = new BasicBlock(context, mainFn, "line" + lineNumber.ToString());
			IRBuilder builder = new IRBuilder(block);

			AllocaInstruction alloc;

			LLVM.Type i8p = LLVM.Type.GetInteger8PointerType(context);
			LLVM.Type i8Type = LLVM.Type.GetInteger8Type(context);
			LLVM.Type i32Type = LLVM.Type.GetInteger32Type(context);
			LLVM.Type doubleType = LLVM.Type.GetDoubleType(context);
			LLVM.Type dp = LLVM.Type.GetDoublePointerType(context);
			Constant valLength = new Constant(context, 8, 50L);

			bool isNumeric = !(vars[0] is StringVariable);

			if (!isNumeric)
			{
				StringVariable var = (StringVariable)vars[0];

				if (Parser.variables.strings.ContainsKey(var.name))
					alloc = Parser.variables.strings[var.name]; // already allocated
				else
				{
					// new allocation
					alloc = builder.CreateAlloca(i8Type, valLength, var.name);
					// remember allocation
					Parser.variables.strings[var.name] = alloc;
				}
				Parser.variables.stringIsPointer[var.name] = false;
			}
			else
			{
				SimpleNumericVariable var = (SimpleNumericVariable)vars[0];

				if (Parser.variables.numbers.ContainsKey(var.name)) alloc = Parser.variables.numbers[var.name];
				else
				{
					alloc = builder.CreateAlloca(doubleType, var.name);
					Parser.variables.numbers[var.name] = alloc;
				}
			}


			// Import scanf function
			PointerType i8pp = PointerType.GetUnqualified(i8p);
			

			LLVM.Type[] argTypes = isNumeric ? new LLVM.Type[] { i8p, dp } : new LLVM.Type[] { i8p, i8p };

			FunctionType stringToInt = new FunctionType(LLVM.Type.GetVoidType(context), argTypes);
			Constant scanf = module.GetOrInsertFunction("scanf", stringToInt);

			string formatString = isNumeric ? "%lf" : "%79s";
			StringConstant format = new StringConstant(formatString);
			Value formatValue = format.code(context, module, builder);

			Constant zero = new Constant(context, 8, 0);

			Value gep = builder.CreateGEP(alloc, zero, "gepTest");
			
			Value[] args = {formatValue,gep};

			Value hop = (Value)alloc;

			scanf.Dump();
			formatValue.Dump();
			gep.Dump();

			Value outputs = builder.CreateCall(scanf, args);

			outputs.Dump();

			firstBlock = block;
			lastBlock = block;
			return block;
		}
	}
}
