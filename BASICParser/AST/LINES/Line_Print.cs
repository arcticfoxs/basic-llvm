﻿using System.Collections.Generic;
using LLVM;
using BASICLLVM.AST;

namespace BASICLLVM
{
	class Line_Print : Line
	{

		public PrintList printList;

		LLVMContext context;
		Module module;
		Function mainFn;
		BasicBlock block;
		IRBuilder builder;
		Constant printf;

		public Line_Print(PrintList _printList)
		{
			printList = _printList;
		}

		public void compileString()
		{
			for (int i = 0; i < printList.items.Count; i++ )
			{
				PrintItem thisItem = printList.items[i];
				if (thisItem.is_tabcall)
				{
					// TODO: handle tabcall
				}
				else
				{
					if (thisItem.expr is StringConstant)
					{
						StringConstant thisConstant = (StringConstant)thisItem.expr;
						printConstant(thisConstant.value);
					}
					else
					{
						StringVariable var = (StringVariable)thisItem.expr;
						printVariable(var.name);
					}
				}

				if (i < printList.separators.Count)
				{
					PrintList.printseparator thisSeparator = printList.separators[i];
					if (thisSeparator == PrintList.printseparator.COMMA) printConstant(",");
					if (thisSeparator == PrintList.printseparator.SEMICOLON) printConstant(";");
				}
			}
			printConstant("\r\n");
		}

		public override BasicBlock code(LLVMContext _context, Module _module, Function _mainFn)
		{
			// set up vars
			context = _context;
			module = _module;
			mainFn = _mainFn;
			block = new BasicBlock(context, mainFn, "line"+lineNumber.ToString());
			builder = new IRBuilder(block);
			
			// Import printf function
			LLVM.Type[] argTypes = new LLVM.Type[] { LLVM.Type.GetInteger8PointerType(context) };
			FunctionType stringToVoid = new FunctionType(LLVM.Type.GetVoidType(context),argTypes);
			printf = module.GetOrInsertFunction("printf", stringToVoid);

			// do it
			this.compileString();
			

			firstBlock = block;
			lastBlock = block;

			return block;
			
		}

		public void printVariable(string variableName)
		{
			Type stringType = Type.GetInteger8PointerType(context);
			// stringType = PointerType.Get(stringType, 0);


			AllocaInstruction loadAlloc = Parser.variables.strings[variableName];
			Value loadValue = builder.CreateLoad(loadAlloc, "temp");

			Constant zero = new Constant(context, 32, 0);
			Value[] args = new Value[] {
				// get the address of the string (two indices because the first one references the array and the second one references the first element in the array)
				loadValue
			};
			// Call printf
			builder.CreateCall(printf, args);
		}

		public void printConstant(string strToPrint)
		{
			Constant toPrint = new Constant(context, strToPrint);
			GlobalVariable global = new GlobalVariable(
				module,
			  toPrint.GetType(),
			  true, // constant
			  LinkageType.PrivateLinkage, // only visible in this module
			  toPrint,
			  ".str"); // the name of the global constant

			Constant zero = new Constant(context, 32, 0);
			Value[] args = new Value[] {
				// get the address of the string (two indices because the first one references the array and the second one references the first element in the array)
				ConstantExpr.GEP(global, zero, zero)
			};

			// Call printf
			builder.CreateCall(printf, args);
		}
	}
}