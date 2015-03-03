using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASICLLVM.AST
{
	class SimpleNumericVariable : NumericVariable
	{
		string name;

		public SimpleNumericVariable(string _name)
		{
			name = _name;
		}
	}
}
