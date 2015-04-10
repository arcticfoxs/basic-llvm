using System;
using System.Collections.Generic;
using System.IO;
using LLVM;
using Antlr4.Runtime;
using BASICLLVM.AST;

namespace BASICLLVM
{
    class Program
    {
		public static bool debug = false;
		public static bool block = false;
        static void Main(string[] args)
        {
			string inputFile = args[0];
			string outputFile = inputFile.Substring(0, inputFile.LastIndexOf("."))+".ll";
			bool outNext = false;
			foreach (string arg in args)
			{
				if (outNext) outputFile = arg;
				if (arg == "-debug") debug = true;
				if (arg == "-block") block = true;
				if (arg == "-o") outNext = true;
			}

			// Setup LLVM
			LLVMContext context = new LLVMContext();
			Parser.context = context;
			
			/*
			 * PARSE STAGE
			 */

			List<Line> lines = Parser.parseFile(inputFile);
			if (lines == null)
			{
				// parsing failed
				if (block) Console.ReadLine();
				return;
			}
			if(debug) Console.WriteLine("Done Parsing");



			/*
			 * COMPILE STAGE
			 */

			// Setup LLVM module
			Module module = new Module(context, "SourceFile");
			Parser.module = module;
			// Setup LLVM function
			LLVM.Type[] mainArgs = new LLVM.Type[] { Parser.i8, Parser.i8pp};
			FunctionType mainType = new FunctionType(Parser.i8,mainArgs);
			Function mainFunction = new Function(module,"main",mainType);
			Parser.function = mainFunction;
			
			try {
				for (int i = 0; i < lines.Count; i++)
				{
					Parser.counter = i;
					// Compile Line
					lines[i].code();
					
					if (lines[i].hasLineNumber)
					{
						// add code line number to dictionary
						if(!Parser.variables.lines.ContainsKey(lines[i].lineNumber))
							Parser.variables.lines.Add(lines[i].lineNumber, lines[i]);
						else
						{
							CompileException ex = new CompileException("Duplicate line number");
							ex.message = "Line numbers must be unique";
							throw ex;
						}
					}
				}

				// add jumps between adjacent lines
				for (int i = 0; i < lines.Count - 1; i++)
					lines[i].jumpToNext(lines[i + 1]);

				// process control flow statements
				for (int i = 0; i < lines.Count; i++)
					lines[i].processGoto();

				}
			catch (CompileException ex)
			{
				ex.print("COMPILE ERROR");
				if (block) Console.ReadLine();
				return;
			}

			// Compile is complete
			if(debug) module.Dump();

			try
			{
				module.WriteToFile(outputFile);
			}
			catch (Exception e)
			{
				CompileException.printColour("Failed to write to output file", ConsoleColor.Red);
				Console.WriteLine(e.Message);
			}
			
			if(debug) Console.WriteLine("Compile Successful");
			if(block) Console.ReadLine();
        }
    }
}
