using System;
using System.Collections.Generic;
using System.IO;
using LLVM;
using Antlr4.Runtime;
using BASICLLVM.AST;
using System.Diagnostics;

namespace BASICLLVM
{
    class Program
    {
		public static bool debug = false;
		public static bool block = false;
		public enum outputFormats {LL,S,EXE};
		public static outputFormats outputFormat = outputFormats.LL;
		public static string outputFile;
        static void Main(string[] args)
        {
			if (args.Length < 1)
			{
				Console.WriteLine("Usage: BASICLLVM <inputfile> <flags>");
				Console.WriteLine("-debug enables compiler debugging output");
				Console.WriteLine("-block blocks on finished compile");
				Console.WriteLine("-o <outputfile> outputs to specified file");
				Console.WriteLine("-output LL/S/EXE specifies desired output format");
				Console.WriteLine("-output LL/S/EXE specifies desired output format");
				return;
			}
			string inputFile = args[0];
			string defaultOutputFile = inputFile.Substring(0,inputFile.LastIndexOf("."));
			outputFile = defaultOutputFile;


			bool outNext = false;
			bool formatNext = false;
			foreach (string arg in args)
			{
				if (outNext) {
					outputFile = arg;
					outNext = false;
				}
				if(formatNext) {
					outputFormat = (arg == "LL") ? outputFormats.LL : ((arg == "S") ? outputFormats.S : outputFormats.EXE);
					formatNext = false;
				}
				if (arg == "-debug") debug = true;
				if (arg == "-block") block = true;
				if (arg == "-o") outNext = true;
				if (arg == "-output") formatNext = true;
			}
			if (outputFile.Equals(defaultOutputFile))
			{
				if (outputFormat == outputFormats.LL) outputFile += ".ll";
				if (outputFormat == outputFormats.S) outputFile += ".s";
				if (outputFormat == outputFormats.EXE) outputFile += ".exe";
			}

			// Setup LLVM
			LLVMContext context = new LLVMContext();
			Parser.context = context;
			
			/*
			 * PARSE STAGE
			 */

			if (debug) Console.WriteLine("---");
			if (debug) Console.WriteLine();

			if (debug) Console.WriteLine("1 - Parsing");
			List<Line> lines = Parser.parseFile(inputFile);
			if (lines == null)
			{
				// parsing failed
				if (block) Console.ReadLine();
				return;
			}
			if(debug) Console.WriteLine("Done");

			if (debug) Console.WriteLine();
			if (debug) Console.WriteLine("---");
			if (debug) Console.WriteLine();

			/*
			 * COMPILE STAGE
			 */
			if (debug) Console.WriteLine("2 - Compiling");
			// Setup LLVM module
			
			Module module = new Module(context, "SourceFile");
			Parser.module = module;
			// Setup LLVM function
			LLVM.Type[] mainArgs = new LLVM.Type[] { Parser.i32, Parser.i8pp};
			FunctionType mainType = new FunctionType(Parser.i32,mainArgs);
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
			if(debug) Console.WriteLine("Done");
			if (debug)
			{
				Console.WriteLine();
				Console.WriteLine("---");
				Console.WriteLine();
				// Compile is complete
				ConsoleColor prevColor = Console.ForegroundColor;
				Console.ForegroundColor = ConsoleColor.Magenta;
				module.Dump();
				Console.ForegroundColor = prevColor;
			}


			if (debug) Console.WriteLine();
			if (debug) Console.WriteLine("---");
			if (debug) Console.WriteLine();
			if (debug) Console.WriteLine("3 - Output");
 			// Write out LLVM module
			try
			{
				module.WriteToFile("ll.tmp");
			}
			catch (Exception e)
			{
				CompileException.printColour("Failed to write to output file", ConsoleColor.Red);
				Console.WriteLine(e.Message);
			}
			

			// Call llc and gcc as well if we were asked to do that
			switch (outputFormat)
			{
				case outputFormats.LL:
					File.Delete(outputFile);
					File.Move("ll.tmp", outputFile);
					break;
				case outputFormats.S:
					do_llc();
					File.Delete(outputFile);
					File.Move("ll.tmp.s", outputFile);
					break;
				case outputFormats.EXE:
					do_llc();
					do_gcc();
					break;
			}

			if (debug) Console.WriteLine("Compile complete");
			if (block) Console.ReadLine();
        }

		static void do_llc()
		{
			if (debug) Console.Write("> LLC");
			ProcessStartInfo psi = new ProcessStartInfo("llc", "ll.tmp");
			psi.UseShellExecute = false;
			psi.RedirectStandardOutput = true;

			Process llcProcess = Process.Start(psi);
			string llcOutput = llcProcess.StandardOutput.ReadToEnd();
			llcProcess.WaitForExit();

			if (llcOutput.Length > 0) Console.WriteLine(llcOutput);
			if (debug) Console.WriteLine(" ...done");
			File.Delete("ll.tmp");
		}

		static void do_gcc()
		{
			do_fix_chkstk();
			if (debug) Console.Write("> GCC");
			ProcessStartInfo psi = new ProcessStartInfo("gcc", "ll.tmp.s -o "+outputFile);
			psi.UseShellExecute = false;
			psi.RedirectStandardOutput = true;

			Process gccProcess = Process.Start(psi);
			string gccOutput = gccProcess.StandardOutput.ReadToEnd();
			gccProcess.WaitForExit();

			if(gccOutput.Length > 0) Console.WriteLine(gccOutput);
			if (debug) Console.WriteLine(" ...done");
			File.Delete("ll.tmp.s");
		}

		static void do_fix_chkstk()
		{
			if (debug) Console.Write("> fix_chkstk");
			ProcessStartInfo psi = new ProcessStartInfo("fix_chkstk", "ll.tmp.s");
			psi.UseShellExecute = false;
			psi.RedirectStandardOutput = true;

			Process fixProcess = Process.Start(psi);
			string fixOutput = fixProcess.StandardOutput.ReadToEnd();
			fixProcess.WaitForExit();

			if (fixOutput.Length > 0) Console.WriteLine(fixOutput);
			if (debug) Console.WriteLine(" ...done");
		}
    }
}
