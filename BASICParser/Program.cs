using System;
using System.Collections.Generic;
using System.IO;
using LLVM;

namespace BASICParser
{
    class Program
    {
        static void Main(string[] args)
        {
			string inputFile = "D:\\Project\\basictest.txt"; // TODO: Read from parameter
			
			List<Line> lines = Lexer.lex(inputFile,true);
			Console.WriteLine("Done Lexing");
			Console.ReadLine();
			for (int i = 0; i < lines.Count; i++)
			{
				Line parsedLine = lines[i].parse();
				lines[i] = parsedLine;
			}
			Console.WriteLine("Done Parsing");
			Console.ReadLine();

			LLVMContext context = new LLVMContext();
			Module module = new Module(context, "SourceFile");

			LLVM.Type[] mainArgs = new LLVM.Type[] { LLVM.Type.GetInteger32Type(context), LLVM.PointerType.GetUnqualified(LLVM.Type.GetInteger8PointerType(context)) };
			FunctionType mainType = new FunctionType(LLVM.Type.GetInteger32Type(context),mainArgs);
			
			Function mainFunction = new Function(module,"main",new FunctionType(mainType));

			for (int i = 0; i < lines.Count; i++)
			{
				lines[i].code(context, module, mainFunction);
			}

			for (int i = 0; i < lines.Count - 1; i++)
			{
				lines[i].addJump(lines[i + 1]);
			}
			
			module.Dump();
			module.WriteToFile("D:\\Project\\out.ll");
			Console.WriteLine("Compile Successful");
			Console.ReadLine();
        }
    }
}
