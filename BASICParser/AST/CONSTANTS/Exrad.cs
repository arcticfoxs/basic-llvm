using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASICLLVM.AST
{
	class Exrad
	{
		public NumericConstant.Sign sign;
		public int integer;

		public Exrad(int _integer)
		{
			sign = NumericConstant.Sign.PLUSSIGN;
			integer = _integer;
		}
		public Exrad(NumericConstant.Sign _sign, int _integer)
		{
			sign = _sign;
			integer = _integer;
		}
	}
}
