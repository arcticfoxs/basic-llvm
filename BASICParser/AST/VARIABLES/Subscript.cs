using System.Collections.Generic;

namespace BASICLLVM.AST
{
	class Subscript
	{
		public List<NumericExpression> expressions;
		public Subscript(List<NumericExpression> _expressions)
		{
			expressions = _expressions;
		}
	}
}
