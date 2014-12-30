using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASICParser
{
	class Line_Goto : Line
	{
		public int gotoTarget;
		public Line_Goto(Line parentLine, int gotoLineNumber)
		{
			lineNumber = parentLine.lineNumber;
			tokens = parentLine.tokens;
			gotoTarget = gotoLineNumber;
			parsed = true;
		}
	}
}
