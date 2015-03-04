using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASICLLVM.AST
{
	class NumericArrayElement : NumericVariable
	{
		public string numericarrayname;
		public Subscript subscript;
		public NumericArrayElement(string _name, Subscript _subscript)
		{
			numericarrayname = _name;
			subscript = _subscript;
		}
	}
}
