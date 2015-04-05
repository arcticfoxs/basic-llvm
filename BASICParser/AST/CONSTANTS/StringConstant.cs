using LLVM;

namespace BASICLLVM.AST
{
	class StringConstant : StringExpression
	{
		public string value;
		public StringConstant(string _value)
		{
			value = _value;
		}
		public override Value code(LLVMContext context, Module module, IRBuilder builder)
		{
			Constant toPrint = new Constant(context, value);
			GlobalVariable global = new GlobalVariable(
				module,
			  toPrint.GetType(),
			  true, // constant
			  LinkageType.PrivateLinkage, // only visible in this module
			  toPrint,
			  ".str"); // the name of the global constant

			Constant zero = new Constant(context, 32, 0);
			Value[] args = new Value[] {
				// get the address of the string (two indices because the first one references the array and the second one references the first element in the array)
				ConstantExpr.GEP(global, zero, zero)
			};
			return ConstantExpr.GEP(global, zero, zero);
		}
	}
}
