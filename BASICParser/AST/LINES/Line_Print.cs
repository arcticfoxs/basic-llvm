﻿using System.Collections.Generic;
using LLVM;
using BASICLLVM.AST;

namespace BASICLLVM
{
	class Line_Print : Line
	{

		public PrintList printList;

		public Line_Print(PrintList _printList)
		{
			printList = _printList;
		}

	}
}

		// BELOW THIS IS ALL OLD!

		/*
		public override BasicBlock code(LLVMContext context, Module module, Function mainFn)
		{
			BasicBlock block = new BasicBlock(context, mainFn, "line"+lineNumber.ToString());
			IRBuilder builder = new IRBuilder(block);
			
			// Import printf function
			LLVM.Type[] argTypes = new LLVM.Type[] { LLVM.Type.GetInteger8PointerType(context) };
			
			FunctionType stringToVoid = new FunctionType(LLVM.Type.GetVoidType(context),argTypes);
			Constant printf = module.GetOrInsertFunction("printf", stringToVoid);


			// create a global constant with the value of the string
			Constant arg = new Constant(context, printTokens[0].stringPayload);
			GlobalVariable global = new GlobalVariable(
				module,
				arg.GetType(),
				true, // constant
				LinkageType.PrivateLinkage, // only visible in this module
				arg,
				".str"); // the name of the global constant

			Constant zero = new Constant(context, 32, 0);
			Value[] args = new Value[] {
			// get the address of the string (two indices because the first one references the array and the second one references the first element in the array)
				ConstantExpr.GEP(global, zero, zero)
			};

			// Call printf
			builder.CreateCall(printf, args);


			firstBlock = block;
			lastBlock = block;

			return block;
			
		}
	}
}
		*/