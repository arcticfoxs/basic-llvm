using System;
using LLVM;
namespace BASICLLVM.AST
{
	class Primary
	{
		// override this!
		public virtual Value code(LLVMContext context, IRBuilder builder)
		{
			throw new NotImplementedException();
		}
	}
}
