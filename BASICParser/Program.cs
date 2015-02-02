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
			Console.ReadLine();
			for (int i = 0; i < lines.Count; i++)
			{
				Line parsedLine = lines[i].parse();
				lines[i] = parsedLine;
			}
			Console.ReadLine();
			LLVMContext context = new LLVMContext();
			Module module = new Module(context, "SourceFile");
			FunctionType fnType = new FunctionType(LLVM.Type.GetVoidType(context));
			Function printFunction = new LLVM.Function(module,"PRINT",fnType);
			Function mallocFunction = new LLVM.Function(module,"PRINT",fnType);
			BasicBlock block = new LLVM.BasicBlock(context, printFunction, "print1");
			IRBuilder builder = new LLVM.IRBuilder(block);
			Constant string_to_print = new LLVM.Constant(context, "Hello");
			UInt64 totalMemory = 512;
			Constant valMemory = new Constant(context, 32, totalMemory);
			BasicBlock bb = builder.GetInsertBlock();
			LLVM.Type intType = LLVM.Type.GetInteger32Type(context);
			LLVM.Type byteType = LLVM.Type.GetInteger8Type(context);
			Constant allocsize = new Constant(byteType);
			allocsize.TruncOrBitCast(intType);
			Instruction pointerArray = CallInstruction.CreateMalloc(bb, intType, byteType, allocsize, valMemory, mallocFunction, "arr");
			Value currentHead = builder.CreateGEP(pointerArray, new Constant(context, 32, totalMemory / 2), "head");
			LoadInstruction out0 = builder.CreateLoad(currentHead, "tape");
			Value tape1 = builder.CreateSignExtend(out0, LLVM.Type.GetInteger32Type(context), "tape");
			CallInstruction putcharCall = builder.CreateCall(printFunction, tape1);
			putcharCall.TailCall = false;
			module.Dump();
        }
    }
}
