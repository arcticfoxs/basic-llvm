using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASICLLVM.AST
{
	class Significand
	{
		enum SignificandType { INTEGER, INTEGERFRACTION, FRACTION };

		int integer;
		Fraction fraction;
		SignificandType type;

		public Significand(int _integer)
		{
			integer = _integer;
			type = SignificandType.INTEGER;
		}

		public Significand(int _integer, Fraction _fraction)
		{
			integer = _integer;
			fraction = _fraction;
			type = SignificandType.INTEGERFRACTION;
		}

		public Significand(Fraction _fraction)
		{
			fraction = _fraction;
			type = SignificandType.FRACTION;
		}
		
	}
}
