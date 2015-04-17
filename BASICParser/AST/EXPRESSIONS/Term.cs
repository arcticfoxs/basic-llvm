using LLVM;
using System;
using System.Collections.Generic;

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

		public Value code(IRBuilder builder)
		{
			List<Factor> tempFactors = new List<Factor>();
			List<Multiplier> tempMultipliers = new List<Multiplier>();

			// do shallow copy
			foreach (Factor item in factors)
				tempFactors.Add(item);

			foreach (Multiplier item in multipliers)
				tempMultipliers.Add(item);


			Value L = tempFactors[0].code(builder);
			tempFactors.RemoveAt(0);
			while (tempFactors.Count > 0)
			{
				Value R = tempFactors[0].code(builder);
				if (tempMultipliers[0] == Multiplier.ASTERISK) L = builder.CreateFMul(L, R, "multmp");
				else L = builder.CreateFDiv(L, R, "divtmp");
				tempFactors.RemoveAt(0);
				tempMultipliers.RemoveAt(0);
			}
			return L;
		}
	}
}
