using LLVM;
using System;

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
			if (Parser.variables.stringLiterals.ContainsKey(value))
				return Parser.variables.stringLiterals[value];

			Constant toPrint = new Constant(context, value);
			GlobalVariable global = new GlobalVariable(
				module,
			  toPrint.GetType(),
			  true, // constant
			  LinkageType.PrivateLinkage, // only visible in this module
			  toPrint,
			  ".str"); // the name of the global constant

			Constant zero = new Constant(context, 32, 0);
			Value output = ConstantExpr.GEP(global, zero, zero);
			Parser.variables.stringLiterals[value] = output;
			return output;
		}
	}
}
