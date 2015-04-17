
namespace BASICLLVM.AST
{
	class PrintItem
	{
		public StringExpression stringExpr;
		public NumericExpression numExpr;

		public PrintItem(StringExpression payload)
		{
			stringExpr = payload;
		}
		public PrintItem(NumericExpression payload)
		{
			numExpr = payload;
		}
	}
}
