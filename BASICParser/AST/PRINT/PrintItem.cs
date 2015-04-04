using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASICLLVM.AST
{
	class PrintItem
	{
		public StringExpression expr;
		public bool is_tabcall;
		public PrintItem()
		{
			is_tabcall = true;
		}
		public PrintItem(StringExpression payload)
		{
			expr = payload;
			is_tabcall = false;
		}
	}
}
