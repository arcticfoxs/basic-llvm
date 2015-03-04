using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASICLLVM.AST
{
	class Term
	{
		public enum Multiplier { ASTERISK, SOLIDUS };
		
		public List<Factor> factors;
		public List<Multiplier> multipliers;

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
			factors.Add(factor);
			multipliers.Add(multiplier);
		}
	}
}
