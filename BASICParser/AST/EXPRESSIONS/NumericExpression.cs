using System;
using System.Collections.Generic;
using LLVM;

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

		public override LLVM.Value code(LLVM.LLVMContext context, IRBuilder builder)
		{
			Value L = terms[0].code(context, builder);
			terms.RemoveAt(0);
			while (terms.Count > 0)
			{
				Value R = terms[0].code(context, builder);
				if (subsequentSigns[0] == NumericConstant.Sign.PLUSSIGN) L = builder.CreateFAdd(L, R, "multmp");
				else L = builder.CreateFSub(L, R, "subtmp");
				terms.RemoveAt(0);
				subsequentSigns.RemoveAt(0);
			}
			return L;
		}
	}
}
