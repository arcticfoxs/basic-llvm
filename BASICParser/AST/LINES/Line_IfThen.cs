namespace BASICLLVM.AST
{
	class Line_IfThen : Line
	{
		public RelationalExpression expression;
		public int target;

		public Line_IfThen(RelationalExpression _expr, int _goto)
		{
			expression = _expr;
			target = _goto;
		}
	}
}
