using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASICLLVM
{
	class Line_Let_Int : Line
	{
		public string varName;

		public Expression_Int value;

		public Line_Let_Int(Line parentLine,string intVar,List<Symbol> intExpression)
		{
			lineNumber = parentLine.lineNumber;
			tokens = parentLine.tokens;

			varName = intVar;
			value = Expression_Int.parse(intExpression);
			parsed = true;
		}
	}
}
