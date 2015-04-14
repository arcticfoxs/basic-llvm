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
		public override Value code(IRBuilder builder)
		{
			if (Parser.variables.stringLiterals.ContainsKey(value))
				return Parser.variables.stringLiterals[value];

			Constant toPrint = new Constant(Parser.context, value);
			GlobalVariable global = new GlobalVariable(
				Parser.module,
			  toPrint.GetType(),
			  true, // constant
			  LinkageType.PrivateLinkage, // only visible in this module
			  toPrint,
			  ".str"); // the name of the global constant

			Value output = ConstantExpr.GEP(global, Parser.zero, Parser.zero);
			Parser.variables.stringLiterals[value] = output;
			return output;
		}
	}
}
