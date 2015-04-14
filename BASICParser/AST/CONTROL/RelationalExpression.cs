using LLVM;
using System;

namespace BASICLLVM.AST
{
	class RelationalExpression
	{
		public enum EqualityRelation { EQUAL, NOTEQUAL }
		public enum Relation { EQUAL, NOTEQUAL, LESSTHAN, GREATERTHAN, NOTLESS, NOTGREATER }

		public virtual Value code(IRBuilder builder)
		{
			// abstract
			throw new NotImplementedException();
		}
	}
}
