/*
 * NumericConstant is never referenced in the specification
 * This is probably a mistake
 * I have replaced NumericRep with NumericConstant in the list of non-terminals for Primary
 */

using LLVM;

namespace BASICLLVM.AST
{
	class NumericConstant : Primary
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

        public double value()
        {
            return (sign == Sign.PLUSSIGN) ? numericrep.value() : -numericrep.value();
        }

		public override Value code(IRBuilder builder)
		{
			return ConstantFP.Get(Parser.context, new APFloat(value()));
		}
	}
}
