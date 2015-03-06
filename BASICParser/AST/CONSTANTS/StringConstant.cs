using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASICLLVM.AST
{
	class StringConstant : StringExpression
	{
		public string value;
		public StringConstant(string _value)
		{
			value = _value;
		}
	}
}
