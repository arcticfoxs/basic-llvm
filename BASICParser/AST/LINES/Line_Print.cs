using System.Collections.Generic;
using LLVM;
using BASICLLVM.AST;

namespace BASICLLVM
{
	class Line_Print : Line
	{

		public PrintList printList;
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
						printNumericExpression(thisItem.numExpr);
					else
						printStringExpression(thisItem.stringExpr);

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


		public void printStringExpression(StringExpression thisExpression)
		{
			Value expressionValue = thisExpression.code(builder);
			Value[] args = new Value[] { expressionValue };
			// Call printf
			builder.CreateCall(printf, args);
		}
		public override BasicBlock code()
		{
			block = bb();
			builder = new IRBuilder(block);
			
			// Import printf function
			LLVM.Type[] argTypes = new LLVM.Type[] { Parser.i8p };
			FunctionType stringToVoid = new FunctionType(Parser.vd, argTypes);
			printf = Parser.module.GetOrInsertFunction("printf", stringToVoid);

			// Import doubleprintf function
			argTypes = new LLVM.Type[] { Parser.i8p, Parser.dbl };
			FunctionType stringAndDouble = new FunctionType(Parser.vd, argTypes);
			doubleprintf = Parser.module.GetOrInsertFunction("printf", stringAndDouble);

			// do it
			this.compileString();
			

			firstBlock = block;
			lastBlock = block;

			return block;
			
		}

		public void printLiteral(string strToPrint)
		{
			StringConstant tmp = new StringConstant(strToPrint);
			printStringExpression(tmp);
		}

		public void printNumericExpression(NumericExpression expr)
		{
			Value expressionValue = expr.code(builder);
			Constant stringFormat = new Constant(Parser.context, "%f");

			GlobalVariable global = new GlobalVariable(
				Parser.module,
			  stringFormat.GetType(),
			  true, // constant
			  LinkageType.PrivateLinkage, // only visible in this module
			  stringFormat,
			  ".str"); // the name of the global constant

			Value[] args = new Value[] {
							ConstantExpr.GEP(global,Parser.zero,Parser.zero),
							expressionValue
						};
			// Call printf
			builder.CreateCall(doubleprintf, args);
		}
	}
}