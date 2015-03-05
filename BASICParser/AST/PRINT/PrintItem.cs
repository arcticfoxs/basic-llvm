using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASICLLVM.AST
{
	class PrintItem
	{
		Expression_String expr;
		bool is_tabcall;
		public PrintItem()
		{
			is_tabcall = true;
		}
		public PrintItem(Expression_String payload)
		{
			expr = payload;
			is_tabcall = false;
		}
	}
}
