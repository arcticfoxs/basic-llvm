using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASICLLVM.AST
{
	class Line_Let_String : Line
	{
		StringVariable var;
		StringExpression expr;

		public Line_Let_String(StringVariable _var, StringExpression _expr)
		{
			var = _var;
			expr = _expr;
		}
	}
}
