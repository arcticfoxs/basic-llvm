﻿using System;
using System.Collections.Generic;
using System.IO;

namespace BASICParser
{
    class Program
    {
        static void Main(string[] args)
        {
			string inputFile = "D:\\basictest.txt"; // TODO: Read from parameter

			List<Line> lines = Lexer.lex(inputFile,true);
			Console.ReadLine();
			for (int i = 0; i < lines.Count; i++)
			{
				Line parsedLine = lines[i].parse();
				lines[i] = parsedLine;
			}
        }

    }
}
