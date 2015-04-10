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
		public static bool amDebug = false;
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
				if (arg == "-debug") amDebug = true;
			}

			LLVMContext context = new LLVMContext();
			Parser.context = context;
			List<Line> lines = Parser.parseFile(inputFile);

			if(dump) Console.WriteLine("Done Parsing");

	
			Module module = new Module(context, "SourceFile");
			Parser.module = module;
			LLVM.Type[] mainArgs = new LLVM.Type[] { LLVM.Type.GetInteger32Type(context), LLVM.PointerType.GetUnqualified(LLVM.Type.GetInteger8PointerType(context)) };
			FunctionType mainType = new FunctionType(LLVM.Type.GetInteger32Type(context),mainArgs);
			
			Function mainFunction = new Function(module,"main",mainType);

			Parser.function = mainFunction;
			

			BasicBlock newLine;

			for (int i = 0; i < lines.Count; i++)
			{
				newLine = lines[i].code(context, module, mainFunction);
				if(lines[i].lineNumber != -2) Parser.variables.lines.Add(lines[i].lineNumber, lines[i]);
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

		public static void debug(string info)
		{
			if (amDebug)
			{
				Console.WriteLine(info);
				Console.WriteLine();
			}			
		}
    }
}
