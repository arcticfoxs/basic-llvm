using System;
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
		public override Value code(LLVMContext context, Module module, IRBuilder builder)
		{
			Value L = LHS.code(context, module, builder);
			Value R = RHS.code(context, module, builder);

			Constant zero = new Constant(context, 8, 0);

			// import strcmp function
			LLVM.Type[] argTypes = new LLVM.Type[] { LLVM.Type.GetInteger8PointerType(context), LLVM.Type.GetInteger8PointerType(context) };
			FunctionType stringStringToInt = new FunctionType(LLVM.Type.GetInteger8Type(context), argTypes);
			Value strcmp = module.GetOrInsertFunction("strcmp", stringStringToInt);

			LLVM.Value[] args = new LLVM.Value[] { L, R };

			Value strCmpResult = builder.CreateCall(strcmp, args);

			Value output = builder.CreateFCmp(Predicate.Equal, strCmpResult, zero, "tempStrEqExp");
			output.Dump();
			return output;
		}
	}
}
