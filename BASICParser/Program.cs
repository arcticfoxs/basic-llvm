using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Console.WriteLine("Reading line " + counter.ToString());
                parseLine(line);
                counter++;
                Console.ReadLine();
            }

            file.Close();
            Console.WriteLine("done");
            // Suspend the screen.
            Console.ReadLine();
        }

        static void parseLine(string line)
        {
            char[] characters = line.ToArray();
            string[] words = line.Split(' ');
            Line thisLine = new Line();

            bool lineNumber;
            // find line number
            lineNumber = int.TryParse(words[0], out thisLine.lineNumber);

            // find first keyword
            string firstKeyword = (lineNumber ? words[1] : words[0]);

            switch (firstKeyword)
            {

                  
            }

            lines.Add(thisLine);
            Console.WriteLine(line);
        }
    }
}
