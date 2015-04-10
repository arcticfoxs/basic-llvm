using System;
using System.Collections.Generic;
using System.IO;
using Antlr4.Runtime;
using LLVM;
using BASICLLVM.AST;

namespace BASICLLVM
{
	class Parser
	{
		public static LLVMContext context;
		public static Module module;
		public static Function function;
		public static VariableStore variables;
		public static LLVM.Type i8, i8p, i8pp, i32, dbl,dblp,vd;
		public static Constant zero;
		public static List<Line> parseFile(string inputFile)
		{
			i8 = LLVM.Type.GetInteger8Type(context);
			i8p = LLVM.Type.GetInteger8PointerType(context);
			i8pp = LLVM.PointerType.GetUnqualified(i8p);
			i32 = LLVM.Type.GetInteger32Type(context);
			dbl = LLVM.Type.GetDoubleType(context);
			dblp = LLVM.Type.GetDoublePointerType(context);
			vd = LLVM.Type.GetVoidType(context);
			zero = new Constant(context, 8, 0);

			int counter = 0;
			string line;

			variables = new VariableStore();

			List<Line> parsedLines = new List<Line>();

			// Read the file and display it line by line.
			StreamReader file =  new System.IO.StreamReader(inputFile);
			while ((line = file.ReadLine()) != null)
			{
				Program.debug("Parsing line " + counter.ToString());
				Program.debug(line);
				AntlrInputStream stream = new AntlrInputStream(line);
				ITokenSource lexer = new BASICLexer(stream);
				ITokenStream tokens = new CommonTokenStream(lexer);
				BASICParser parser = new BASICParser(tokens);
				Listener lis = new Listener();
				parser.BuildParseTree = true;
				parser.AddParseListener(lis);
				RuleContext tree = parser.line();
				parsedLines.Add(lis.finishedLine);
				counter++;
			}

			file.Close();

			return parsedLines;
						
		}
	}
}
