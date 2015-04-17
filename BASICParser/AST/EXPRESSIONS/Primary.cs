using LLVM;
namespace BASICLLVM.AST
{
	class Primary
	{
		// override this!
		public virtual Value code(IRBuilder builder)
		{
			// abstract
			throw new CompileException("Can't code Primary directly");
		}
	}
}
