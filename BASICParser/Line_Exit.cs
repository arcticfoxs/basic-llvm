using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASICParser
{
	class Line_Exit : Line
	{
		public Line_Exit(Line parentLine)
		{
			lineNumber = parentLine.lineNumber;
			tokens = parentLine.tokens;
			parsed = true;
			// done! Exit is so easy to parse
		}
	}
}
