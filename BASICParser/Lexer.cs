using System;
using System.Collections.Generic;
using System.IO;

namespace BASICLLVM
{
	class Lexer
	{
		static List<Line> lines = new List<Line>();

		static StreamWriter writer;
		public static List<Line> lex(string inputFile, bool debug=false)
		{
			writer = new StreamWriter(new FileStream("lex.txt", FileMode.Create));
			int counter = 0;
			string line;

			// Read the file and display it line by line.
			System.IO.StreamReader file = new System.IO.StreamReader(inputFile);
			while ((line = file.ReadLine()) != null)
			{
				writer.WriteLine("<line" + counter.ToString() + ">");
				lines.Add(lexLine(line));
				counter++;
			}

			file.Close();

			writer.Close();

			if (debug)
			{
				string lexOutput = File.ReadAllText("lex.txt");
				Console.WriteLine(lexOutput);
			}
			return lines;
		}

		static Line lexLine(string line)
		{
			List<Symbol> lineTokens = new List<Symbol>();

			StringReader reader = new StringReader(line);

			Yylex lexer = new Yylex(reader);

			Symbol nextSymbol = lexer.next_token();
			while (nextSymbol != null)
			{
				if (nextSymbol.hasPayload)
				{
					if (nextSymbol.stringPayload != null) writer.WriteLine(nextSymbol.symType.ToString() + " " + nextSymbol.stringPayload.ToString());
					else writer.WriteLine(nextSymbol.symType.ToString() + " " + nextSymbol.intPayload);
				}
				else writer.WriteLine(nextSymbol.symType);

				lineTokens.Add(nextSymbol);

				nextSymbol = lexer.next_token();
			}

			return new Line(lineTokens);

		}
	}
}
