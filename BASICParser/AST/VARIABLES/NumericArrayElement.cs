using LLVM;

namespace BASICLLVM.AST
{
	class NumericArrayElement : NumericVariable
	{
		public string numericarrayname;
		public NumericExpression index;
		public NumericArrayElement(string _name, NumericExpression _index)
		{
			numericarrayname = _name;
			index = _index;
		}

		public override Value code(IRBuilder builder)
		{
			Value alloc = Parser.variables.arrayItem(builder, numericarrayname, index.code(builder));
			return builder.CreateLoad(alloc, "tempArrayLoad");
		}

	}
}
