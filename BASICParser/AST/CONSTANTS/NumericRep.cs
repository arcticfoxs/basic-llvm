using System;
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
		}

        public double value()
        {
            if (exrad == null) {
                return significand.value();
            }
            else
            {
                string sign = exrad.sign == NumericConstant.Sign.PLUSSIGN ? "" : "-";
                string val = significand.value().ToString() + "E" + sign + exrad.integer;
                return Convert.ToDouble(val);
            }
        }

		public bool isInt()
		{
			int intVersion;
			return Int32.TryParse(this.value().ToString(),out intVersion);
		}

		public override Value code(LLVMContext context, Module module, IRBuilder builder)
		{
			return ConstantFP.Get(context, new APFloat(value()));
		}

	}
}
