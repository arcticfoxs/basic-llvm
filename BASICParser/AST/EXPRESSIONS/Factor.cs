using LLVM;
using System.Collections.Generic;

namespace BASICLLVM.AST
{
	class Factor
	{
		public List<Primary> primarys;

		public Factor()
		{
			primarys = new List<Primary>();
		}
		public Factor(List<Primary> _primarys)
		{
			primarys = _primarys;
		}
		public void add(Primary primary)
		{
			primarys.Add(primary);
		}

		public Value code(IRBuilder builder)
		{
			if (primarys.Count == 1)
				return primarys[0].code(builder);

			List<Primary> tempPrimaries = new List<Primary>();

			// do shallow copy
			foreach (Primary item in primarys)
				tempPrimaries.Add(item);

			// Import pow function
			LLVM.Type[] argTypes = new LLVM.Type[] {Parser.dbl, Parser.dbl };
			FunctionType powType = new FunctionType(Parser.dbl, argTypes);
			Constant pow = Parser.module.GetOrInsertFunction("pow", powType);
			

			Value L = tempPrimaries[0].code(builder);
			tempPrimaries.RemoveAt(0);
			while (tempPrimaries.Count > 0)
			{
				Value R = tempPrimaries[0].code(builder);
				Value[] args = {L,R};
				L = builder.CreateCall(pow, args);
				tempPrimaries.RemoveAt(0);
			}
			return L;
		}
	}
}
