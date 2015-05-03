using Antlr4.Runtime;
using BASICLLVM.AST;
using LLVM;
using System;
using System.Collections.Generic;
using System.IO;

namespace BASICLLVM
{
	class Parser
	{
		public static LLVMContext context;
		public static Module module;
		public static Function function;
		public static VariableStore variables;
		public static LLVM.Type i8, i8p, i8pp, i32, dbl, dblp, vd;
		public static Constant zero, zero32;
		public static ConstantFP zeroFP;
		public static int currentLine;
		public static List<Statement> parseFile(string inputFile)
		{
			try
			{
				// setup global static variables
				i8 = LLVM.Type.GetInteger8Type(context);
				i8p = LLVM.Type.GetInteger8PointerType(context);
				i8pp = LLVM.PointerType.GetUnqualified(i8p);
				i32 = LLVM.Type.GetInteger32Type(context);
				dbl = LLVM.Type.GetDoubleType(context);
				dblp = LLVM.Type.GetDoublePointerType(context);
				vd = LLVM.Type.GetVoidType(context);
				zero = new Constant(context, 8, 0);
				zero32 = new Constant(context, 32, 0);
				zeroFP = ConstantFP.Get(context, new APFloat((double)0));
				variables = new VariableStore();
				currentLine = 0;

				string line;

				List<Statement> parsedLines = new List<Statement>();

				// Read the file and parse it line by line
				StreamReader file = new System.IO.StreamReader(inputFile);
				while ((line = file.ReadLine()) != null)
				{
					// Do ANTLR stuff
					AntlrInputStream stream = new AntlrInputStream(line);
					ITokenSource lexer = new BASICLexer(stream);
					ITokenStream tokens = new CommonTokenStream(lexer);
					BASICParser parser = new BASICParser(tokens);

					parser.RemoveErrorListeners();
					parser.AddErrorListener(new AntlrErrorListener());
					Listener lis = new Listener();
					parser.BuildParseTree = true;
					parser.AddParseListener(lis);

					try
					{
						RuleContext tree = parser.line();
					}
					catch (CompileException ex)
					{
						ex.print("INNER PARSE ERROR");
						file.Close();
						return null;
					}
					parsedLines.Add(lis.finishedLine);
					currentLine++;
				}

				file.Close();
				currentLine = -1;
				if (!Statement_End.existsEnd)
				{
					CompileException ex = new CompileException("No END line");
					ex.message = "An END line is required";
					throw ex;
				}

				return parsedLines;
			}
			catch (CompileException ex)
			{
				ex.print("OUTER PARSE ERROR");
				return null;
			}
			catch (Exception ex)
			{
				CompileException.printColour("UNHANDLED PARSE ERROR", ConsoleColor.Red);
				Console.WriteLine(ex.Message);
				return null;
			}
		}
	}
}
