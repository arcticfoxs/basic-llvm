using LLVM;

namespace BASICLLVM.AST
{
	class StringRelationalExpression : RelationalExpression
	{
		public StringExpression LHS, RHS;
		public EqualityRelation relation;
		public StringRelationalExpression(StringExpression _lhs, StringExpression _rhs, EqualityRelation _rel)
		{
			LHS = _lhs;
			RHS = _rhs;
			relation = _rel;
		}
		public override Value code(IRBuilder builder)
		{
			Value L = LHS.code(builder);
			Value R = RHS.code(builder);

			// import strcmp function
			LLVM.Type[] argTypes = new LLVM.Type[] { Parser.i8p, Parser.i8p };
			FunctionType stringStringToInt = new FunctionType(Parser.i8, argTypes);
			Value strcmp = Parser.module.GetOrInsertFunction("strcmp", stringStringToInt);

			LLVM.Value[] args = new LLVM.Value[] { L, R };

			Value strCmpResult = builder.CreateCall(strcmp, args);

			Predicate comparison = (relation == EqualityRelation.EQUAL) ? Predicate.Equal : Predicate.NotEqual;

			Value output = builder.CreateFCmp(comparison, strCmpResult, Parser.zero, "tempStrEqExp");
			return output;
		}
	}
}
