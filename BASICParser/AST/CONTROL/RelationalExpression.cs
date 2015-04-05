using System;
using LLVM;

namespace BASICLLVM.AST
{
	class RelationalExpression
	{
		public enum EqualityRelation { EQUAL, NOTEQUAL }
		public enum Relation { EQUAL, NOTEQUAL, LESSTHAN, GREATERTHAN, NOTLESS, NOTGREATER }

		public virtual Value code(LLVMContext context, Module module, IRBuilder builder)
		{
			throw new NotImplementedException();
		}
	}
}
