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

			Constant zero = new Constant(context, 32, 0);

			// import strcmp function
			LLVM.Type[] argTypes = new LLVM.Type[] { LLVM.Type.GetInteger8PointerType(context), LLVM.Type.GetInteger8PointerType(context) };
			FunctionType stringStringToInt = new FunctionType(LLVM.Type.GetInteger8Type(context), argTypes);
			Value strcmp = module.GetOrInsertFunction("strcmp", stringStringToInt);

			L.Dump();
			R.Dump();
			strcmp.Dump();

			LLVM.Value[] args = new LLVM.Value[] { L, R };

			builder.CreateCall(strcmp, args);

			Predicate fcmpPredicate;

			switch (relation)
			{
				case EqualityRelation.EQUAL:
					fcmpPredicate = Predicate.OrderedEqual;
					break;
				case EqualityRelation.NOTEQUAL:
					fcmpPredicate = Predicate.OrderedNotEqual;
					break;
				default:
					fcmpPredicate = Predicate.OrderedEqual;
					break;
			}
			return builder.CreateFCmp(fcmpPredicate, L, R, "tempStrEqExp");
		}
	}
}
