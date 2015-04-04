using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASICLLVM.AST
{
	class StringVariable : StringExpression
	{
		public string name;
		public StringVariable(string _name)
		{
			name = _name;
		}
	}
}
