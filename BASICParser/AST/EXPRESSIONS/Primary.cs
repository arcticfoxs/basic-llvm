using LLVM;
using System;
namespace BASICLLVM.AST
{
	class Primary
	{
		// override this!
		public virtual Value code(IRBuilder builder)
		{
			// abstract
			throw new NotImplementedException();
		}
	}
}
