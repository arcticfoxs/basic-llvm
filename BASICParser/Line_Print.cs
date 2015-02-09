﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LLVM;

namespace BASICParser
{
	class Line_Print : Line
	{
		public List<Symbol> printTokens = new List<Symbol>();

		public Line_Print(Line parentLine,List<Symbol> printExpression)
		{
			lineNumber = parentLine.lineNumber;
			tokens = parentLine.tokens;
			parsed = parentLine.parsed;

			printTokens = printExpression;
		}

		public void parse()
		{
			// parse the print tokens into a valid string expression
			if (printTokens.Count == 1)
			{
				// we need a single string literal
				if (printTokens[0].symType == Symbol.sym.STRINGLITERAL)
				{
					// great! now form a simple expression

					parsed = true;
				}
				else
				{
					// throw an exception
				}
			}
			else
			{
				// do some clever parsing
			}
		}

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
			/*
			FunctionType fnType = new FunctionType(LLVM.Type.GetVoidType(context));
			Function printFunction = new LLVM.Function(module, "PRINT", fnType);
			Function mallocFunction = new LLVM.Function(module, "PRINT", fnType);
			
			
			Constant string_to_print = new LLVM.Constant(context, printTokens[0].stringPayload);
			UInt64 totalMemory = 512;
			Constant valMemory = new Constant(context, 32, totalMemory);
			BasicBlock bb = builder.GetInsertBlock();
			LLVM.Type intType = LLVM.Type.GetInteger32Type(context);
			LLVM.Type byteType = LLVM.Type.GetInteger8Type(context);
			Constant allocsize = new Constant(byteType);
			allocsize.TruncOrBitCast(intType);
			Instruction pointerArray = CallInstruction.CreateMalloc(bb, intType, byteType, allocsize, valMemory, mallocFunction, "arr");
			Value currentHead = builder.CreateGEP(pointerArray, new Constant(context, 32, totalMemory / 2), "head");
			LoadInstruction out0 = builder.CreateLoad(currentHead, "tape");
			Value tape1 = builder.CreateSignExtend(out0, LLVM.Type.GetInteger32Type(context), "tape");
			CallInstruction putcharCall = builder.CreateCall(printFunction, tape1);
			putcharCall.TailCall = false;
			 */
		}
	}
}
