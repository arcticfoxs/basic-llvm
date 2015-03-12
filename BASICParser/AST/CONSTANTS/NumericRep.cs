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

	}
}
