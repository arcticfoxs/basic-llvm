using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASICLLVM.AST.VARIABLES
{
	class StringVariable : StringExpression
	{
		string name;
		public StringVariable(string _name)
		{
			name = _name;
		}
	}
}
