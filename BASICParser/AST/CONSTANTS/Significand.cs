using System;

namespace BASICLLVM.AST
{
	class Significand
	{
		enum SignificandType { INTEGER, INTEGERFRACTION, FRACTION };

		public int integer;
		public Fraction fraction;
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

		public double value()
		{
			string val;
            if (type == SignificandType.INTEGER) val = integer.ToString();
            else if (type == SignificandType.FRACTION) val = fraction.digits;
            else val = integer.ToString() + fraction.digits;
            return Convert.ToDouble(val);
		}
		
	}
}
