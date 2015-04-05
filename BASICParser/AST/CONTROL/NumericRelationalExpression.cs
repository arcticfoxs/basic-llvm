using System;
using LLVM;

namespace BASICLLVM.AST
{
	class NumericRelationalExpression : RelationalExpression
	{
		public NumericExpression LHS, RHS;
		public Relation rel;

		public NumericRelationalExpression(NumericExpression _rhs, NumericExpression _lhs, Relation _rel)
		{
			// NOTE LHS AND RHS SWAPPED IN CONSTRUCTOR!
			LHS = _lhs;
			RHS = _rhs;
			rel = _rel;
		}

		public override Value code(LLVMContext context, Module module, IRBuilder builder)
		{
			Value L = LHS.code(context,module,builder);
			Value R = RHS.code(context,module,builder);
			Predicate fcmpPredicate;

			switch (rel)
			{
				case Relation.EQUAL:
					fcmpPredicate = Predicate.OrderedEqual;
					break;
				case Relation.NOTEQUAL:
					fcmpPredicate = Predicate.OrderedNotEqual;
					break;
				case Relation.GREATERTHAN:
					fcmpPredicate = Predicate.OrderedGreaterThan;
					break;
				case Relation.LESSTHAN:
					fcmpPredicate = Predicate.OrderedLessThan;
					break;
				case Relation.NOTGREATER:
					fcmpPredicate = Predicate.OrderedLessOrEqual;
					break;
				case Relation.NOTLESS:
					fcmpPredicate = Predicate.OrderedGreaterOrEqual;
					break;
				default:
					fcmpPredicate = Predicate.OrderedEqual;
					break;
			}
			return builder.CreateFCmp(fcmpPredicate, L, R, "tempNumEqExp");
			throw new NotImplementedException();
		}
	}
}
