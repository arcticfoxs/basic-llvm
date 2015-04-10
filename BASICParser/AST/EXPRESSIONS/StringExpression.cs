using System;
using LLVM;

namespace BASICLLVM.AST
{
	class StringExpression
	{
		// abstract
		public virtual Value code(IRBuilder builder)
		{
			throw new NotImplementedException();
		}
	}
}
