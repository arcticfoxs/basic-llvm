using System;
using LLVM;

namespace BASICLLVM.AST
{
	class StringExpression
	{
		// abstract
		public virtual Value code(LLVMContext context, Module module, IRBuilder builder)
		{
			throw new NotImplementedException();
		}
	}
}
