using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASICLLVM.AST
{
	class PrintItem
	{
		public StringExpression stringExpr;
		public NumericExpression numExpr;
		public bool is_tabcall;
		public PrintItem()
		{
			is_tabcall = true;
		}
		public PrintItem(StringExpression payload)
		{
			stringExpr = payload;
			is_tabcall = false;
		}
		public PrintItem(NumericExpression payload)
		{
			numExpr = payload;
			is_tabcall = false;
		}
	}
}
