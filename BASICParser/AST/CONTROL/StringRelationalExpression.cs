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
