using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASICLLVM.AST
{
	class NumericExpression : Primary
	{
		public NumericConstant.Sign leadingSign = NumericConstant.Sign.PLUSSIGN;
		public List<Term> terms;
		public List<NumericConstant.Sign> subsequentSigns;

		public NumericExpression()
		{
			terms = new List<Term>();
			subsequentSigns = new List<NumericConstant.Sign>();
		}
		public void add(Term term, NumericConstant.Sign sign)
		{
			terms.Add(term);

			if (terms.Count == 0) leadingSign = sign;
			else subsequentSigns.Add(sign);
		}
		public void add(Term term)
		{
			terms.Add(term);
		}
		
	}
}
