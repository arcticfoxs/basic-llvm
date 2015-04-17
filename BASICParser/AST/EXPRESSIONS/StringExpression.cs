using LLVM;

namespace BASICLLVM.AST
{
	class StringExpression
	{
		// abstract
		public virtual Value code(IRBuilder builder)
		{
			throw new CompileException("Can't code StringExpression directly");
		}
	}
}
