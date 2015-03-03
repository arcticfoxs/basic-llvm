using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASICLLVM
{
	class Expression_String
	{
		bool isConstant;
		string stringconstant,stringvariable;

		public Expression_String(bool constant, string payload)
		{
			isConstant = constant;
			if (isConstant) stringconstant = payload;
			else stringvariable = payload;
		}
	}
}
