﻿using System;
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
		public static VariableStore variables;
		public static Function function;
		public static List<Line> parseFile(string inputFile)
		{
			int counter = 0;
			string line;

			variables = new VariableStore();

			List<Line> parsedLines = new List<Line>();

			// Read the file and display it line by line.
			StreamReader file =  new System.IO.StreamReader(inputFile);
			while ((line = file.ReadLine()) != null)
			{
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
