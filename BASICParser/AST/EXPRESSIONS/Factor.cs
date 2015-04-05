using System.Collections.Generic;
using LLVM;

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

		public Value code(LLVMContext context,IRBuilder builder)
		{
			if (primarys.Count == 1)
			{
				return primarys[0].code(context, builder);
			}
			else
			{
				// TODO POWERS
				return primarys[0].code(context,builder);
			}
		}
	}
}
