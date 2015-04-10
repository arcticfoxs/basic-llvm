﻿using System;
using System.Collections.Generic;
using System.IO;
using LLVM;
using Antlr4.Runtime;
using BASICLLVM.AST;

namespace BASICLLVM
{
    class Program
    {
        static void Main(string[] args)
        {
			string inputFile = "D:\\Project\\basictest.txt"; // TODO: Read from parameter

			LLVMContext context = new LLVMContext();
			Parser.context = context;
			// VariableStore.init();
			List<Line> lines = Parser.parseFile(inputFile);

			Console.WriteLine("Done Parsing");

	
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

			Console.WriteLine("");
			Console.WriteLine("-----");
			Console.WriteLine("");
			module.Dump();
			module.WriteToFile("D:\\Project\\out.ll");
			Console.WriteLine("Compile Successful");
			Console.ReadLine();
        }
    }
}
