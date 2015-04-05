using System;
using System.Collections.Generic;
using LLVM;

namespace BASICLLVM.AST
{
	class Term
	{
		public enum Multiplier { ASTERISK, SOLIDUS };
		
		public List<Factor> factors;
		public List<Multiplier> multipliers;

		public NumericConstant.Sign precedingSign;

		public Term()
		{
			factors = new List<Factor>();
			multipliers = new List<Multiplier>();
		}
		public Term(List<Factor> _factors, List<Multiplier> _multipliers)
		{
			factors = _factors;
			multipliers = _multipliers;
			if (factors.Count != multipliers.Count + 1)
			{
				throw new InvalidProgramException();
			}
		}

		public void add(Factor factor) {
			factors.Add(factor);
		}

		public void add(Factor factor, Multiplier multiplier) {
			if(factors.Count > 0) multipliers.Add(multiplier);
			factors.Add(factor);
		}

		public Value code(LLVMContext context,IRBuilder builder)
		{
			Value L = factors[0].code(context,builder);
			factors.RemoveAt(0);
			while (factors.Count > 0)
			{
				Value R = factors[0].code(context, builder);
				if (multipliers[0] == Multiplier.ASTERISK) L = builder.CreateFMul(L, R, "multmp");
				else L = builder.CreateFDiv(L, R, "divtmp");
				factors.RemoveAt(0);
				multipliers.RemoveAt(0);
			}
			return L;
		}
	}
}
