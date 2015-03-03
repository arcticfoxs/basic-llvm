using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASICLLVM.AST
{
	class NumericConstant
	{
		public enum Sign { PLUSSIGN, MINUSSIGN };

		public NumericRep numericrep;
		public Sign sign;

		public NumericConstant(NumericRep rep)
		{
			numericrep = rep;
			sign = Sign.PLUSSIGN;
		}
		public NumericConstant(Sign _sign, NumericRep rep)
		{
			numericrep = rep;
			sign = _sign;
		}
	}
}
