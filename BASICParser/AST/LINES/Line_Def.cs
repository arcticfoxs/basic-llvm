namespace BASICLLVM.AST
{
	class Line_Def : Line
	{
		string numericDefinedFunction;
		SimpleNumericVariable parameter;
		NumericExpression expr;

		public Line_Def(string _fn, SimpleNumericVariable _param, NumericExpression _expr)
		{
			numericDefinedFunction = _fn;
			parameter = _param;
			expr = _expr;
		}
		public Line_Def(string _fn, NumericExpression _expr)
		{
			numericDefinedFunction = _fn;
			expr = _expr;
		}
	}
}
