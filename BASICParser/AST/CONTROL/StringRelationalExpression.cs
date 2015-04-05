using System;
using LLVM;

namespace BASICLLVM.AST
{
	class StringRelationalExpression : RelationalExpression
	{
		public StringExpression LHS, RHS;
		public EqualityRelation relation;
		public StringRelationalExpression(StringExpression _lhs, StringExpression _rhs, EqualityRelation _rel)
		{
			LHS = _lhs;
			RHS = _rhs;
			relation = _rel;
		}
		public Value code()
		{
			throw new NotImplementedException();
		}
	}
}
