namespace BASICLLVM.AST
{
	class NumericRelationalExpression : RelationalExpression
	{
		public NumericExpression LHS, RHS;
		public Relation rel;

		public NumericRelationalExpression(NumericExpression _lhs, NumericExpression _rhs, Relation _rel)
		{
			LHS = _lhs;
			RHS = _rhs;
			rel = _rel;
		}
	}
}
