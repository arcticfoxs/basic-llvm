using LLVM;
using System.Collections.Generic;

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
			if (terms.Count == 0) leadingSign = sign;
			else subsequentSigns.Add(sign);

			terms.Add(term);
		}
		public void add(Term term)
		{
			terms.Add(term);
		}

		public override LLVM.Value code(IRBuilder builder)
		{
			List<Term> tempTerms = new List<Term>();
			List<NumericConstant.Sign> tempSigns = new List<NumericConstant.Sign>();

			// perform shallow copy
			foreach (Term item in terms)
				tempTerms.Add(item);

			foreach (NumericConstant.Sign item in subsequentSigns)
				tempSigns.Add(item);

			Value L = tempTerms[0].code(builder);
			if (leadingSign == NumericConstant.Sign.MINUSSIGN) L = builder.CreateFSub(Parser.zeroFP, L, "tempNeg");
			tempTerms.RemoveAt(0);
			while (tempTerms.Count > 0)
			{
				Value R = tempTerms[0].code(builder);
				if (tempSigns[0] == NumericConstant.Sign.PLUSSIGN) L = builder.CreateFAdd(L, R, "addtmp");
				else L = builder.CreateFSub(L, R, "subtmp");
				tempTerms.RemoveAt(0);
				tempSigns.RemoveAt(0);
			}
			return L;
		}

	}
}
