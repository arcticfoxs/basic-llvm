using System;
using LLVM;

namespace BASICLLVM.AST
{
	class NumericArrayElement : NumericVariable
	{
		public string numericarrayname;
		public Subscript subscript;
		public NumericArrayElement(string _name, Subscript _subscript)
		{
			numericarrayname = _name;
			subscript = _subscript;
		}

		public override Value code(LLVMContext context, IRBuilder builder)
		{
			throw new NotImplementedException();
		}
	}
}
