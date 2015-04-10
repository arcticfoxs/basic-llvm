using System.Collections.Generic;
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
		Constant printf,doubleprintf;

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
					if (thisItem.stringExpr == null)
					{
						// print NumericExpression
						NumericExpression thisExpression = thisItem.numExpr;
						Value expressionValue = thisExpression.code(context,module,builder);
						Constant stringFormat = new Constant(context,"%f");

						GlobalVariable global = new GlobalVariable(
							module,
						  stringFormat.GetType(),
						  true, // constant
						  LinkageType.PrivateLinkage, // only visible in this module
						  stringFormat,
						  ".str"); // the name of the global constant

						Constant zero = new Constant(context, 32, 0);


						Value[] args = new Value[] {
							ConstantExpr.GEP(global,zero,zero),
							expressionValue
						};
						// Call printf
						builder.CreateCall(doubleprintf, args);
					}
					else
					{
						if (thisItem.stringExpr is StringConstant)
						{
							StringConstant thisConstant = (StringConstant)thisItem.stringExpr;
							printLiteral(thisConstant.value);
						}
						else
						{
							StringVariable var = (StringVariable)thisItem.stringExpr;
							printVariable(var.name);
						}
					}

				}

				if (i < printList.separators.Count)
				{
					PrintList.printseparator thisSeparator = printList.separators[i];
					if (thisSeparator == PrintList.printseparator.COMMA) printLiteral(",");
					if (thisSeparator == PrintList.printseparator.SEMICOLON) printLiteral(";");
				}
			}
			printLiteral("\r\n");
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

			// Import doubleprintf function
			argTypes = new LLVM.Type[] { LLVM.Type.GetInteger8PointerType(context),LLVM.Type.GetDoubleType(context) };
			FunctionType stringAndDouble = new FunctionType(LLVM.Type.GetVoidType(context), argTypes);
			doubleprintf = module.GetOrInsertFunction("printf", stringAndDouble);

			// do it
			this.compileString();
			

			firstBlock = block;
			lastBlock = block;

			return block;
			
		}

		public void printVariable(string variableName)
		{
			Type stringType = Type.GetInteger8PointerType(context);

			AllocaInstruction loadAlloc;
			if (Parser.variables.stringIsPointer[variableName])
				loadAlloc = Parser.variables.stringPointers[variableName];
			else
				loadAlloc = Parser.variables.strings[variableName];

			Value loadValue = builder.CreateLoad(loadAlloc, "temp");
			Constant zero = new Constant(context, 32, 0L);

			if (!Parser.variables.stringIsPointer[variableName])
			{
				loadValue = builder.CreateGEP(loadAlloc, zero, "tempStringVariable");
			}
			
			Value[] args = new Value[] {
				// get the address of the string (two indices because the first one references the array and the second one references the first element in the array)
				loadValue
			};

			// Call printf
			builder.CreateCall(printf, args);
		}

		public void printLiteral(string strToPrint)
		{
			StringConstant tmp = new StringConstant(strToPrint);
			printConstant(tmp);
		}
		public void printConstant(StringConstant toPrint)
		{
			Value[] args = new Value[] {
				toPrint.code(context,module,builder)
			};

			// Call printf
			builder.CreateCall(printf, args);
		}
	}
}