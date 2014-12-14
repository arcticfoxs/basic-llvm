using System;
using System.Collections.Generic;
using System.IO;

namespace BASICParser
{
    class Program
    {
        static List<Line> lines = new List<Line>();
        static void Main(string[] args)
        {
            int counter = 0;
            string line;

            // Read the file and display it line by line.
            System.IO.StreamReader file = new System.IO.StreamReader("D:\\basictest.txt");
            while ((line = file.ReadLine()) != null)
            {
                Console.WriteLine("(line " + counter.ToString()+")");
                lexLine(line);
                counter++;
            }

            file.Close();
            // Suspend the screen.
            Console.ReadLine();
        }

        static void lexLine(string line)
        {
            StringReader reader = new StringReader(line);

            Yylex lexer = new Yylex(reader);

			Symbol nextSymbol = lexer.next_token();
			while (nextSymbol != null)
			{
				if (nextSymbol.hasPayload)
				{
					if (nextSymbol.stringPayload != null) Console.WriteLine(nextSymbol.symType.ToString() + " " + nextSymbol.stringPayload.ToString());
					else Console.WriteLine(nextSymbol.symType.ToString() + " " + nextSymbol.intPayload);
				}
				else Console.WriteLine(nextSymbol.symType);
				nextSymbol = lexer.next_token();
			}
        }
    }
}
