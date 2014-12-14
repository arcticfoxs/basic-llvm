using System;
using System.Collections.Generic;
using System.IO;

namespace BASICParser
{
    class Program
    {
        static void Main(string[] args)
        {
			string inputFile = "D:\\basictest.txt"; // TODO: Read from parameter

			Lexer.lex(inputFile);
        }

    }
}
