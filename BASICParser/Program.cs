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
		public static bool dump = false;
		public static bool block = false;
        static void Main(string[] args)
        {
			string inputFile = args[0];
			string outputFile = inputFile.Substring(0, inputFile.LastIndexOf("."))+".ll";
			bool outNext = false;
			foreach (string arg in args)
			{
				if (outNext) outputFile = arg;
				if (arg == "-dump") dump = true;
				if (arg == "-block") block = true;
				if (arg == "-o") outNext = true;
			}

			LLVMContext context = new LLVMContext();
			Parser.context = context;
			List<Line> lines = Parser.parseFile(inputFile);
			if (lines == null)
			{
				// there was a syntax error
				if (block) Console.ReadLine();
				return;
			}

			if(dump) Console.WriteLine("Done Parsing");

	
			Module module = new Module(context, "SourceFile");
			Parser.module = module;
			LLVM.Type[] mainArgs = new LLVM.Type[] { Parser.i32, Parser.i8pp};
			FunctionType mainType = new FunctionType(Parser.i32,mainArgs);
			
			Function mainFunction = new Function(module,"main",mainType);

			Parser.function = mainFunction;
			

			BasicBlock newLine;

			for (int i = 0; i < lines.Count; i++)
			{
				Parser.counter = i;

				try {
					newLine = lines[i].code();
				}
				catch (CompileException ex)
				{
					ex.print("COMPILE ERROR");
					if (block) Console.ReadLine();
					return;
				}

				if(lines[i].hasLineNumber) Parser.variables.lines.Add(lines[i].lineNumber, lines[i]);
			}

			for (int i = 0; i < lines.Count - 1; i++)
				lines[i].jumpToNext(lines[i + 1]);

			for (int i = 0; i < lines.Count; i++)
				lines[i].processGoto();

			if(dump) module.Dump();
			module.WriteToFile(outputFile);
			if(dump) Console.WriteLine("Compile Successful");
			if(block) Console.ReadLine();
        }
    }
}
