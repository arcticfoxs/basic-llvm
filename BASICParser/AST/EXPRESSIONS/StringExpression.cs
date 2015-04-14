using LLVM;
using System;

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
