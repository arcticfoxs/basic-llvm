using LLVM;

namespace BASICLLVM.AST
{
	class NumericRep : Primary
	{
		Significand significand;
		Exrad exrad;
		public NumericRep(Significand _significand, Exrad _exrad)
		{
			significand = _significand;
			exrad = _exrad;
		}
		public NumericRep(Significand _significand)
		{
			significand = _significand;
			Value sigVal = significand.val();
		}

		public Value val()
		{
			if (exrad == null) return significand.val();
			else
			{
				string output = significand.value();
				output += "E";
				output += ((exrad.sign == NumericConstant.Sign.MINUSSIGN) ? "-" : "");
				output += exrad.integer.ToString();
				return new Constant(Parser.context, output);
			}
		}

	}
}
