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

		public Value code(LLVMContext context,Module module,IRBuilder builder)
		{
			if (primarys.Count == 1)
				return primarys[0].code(context, module, builder);

			// Import pow function
			LLVM.Type[] argTypes = new LLVM.Type[] { LLVM.Type.GetDoubleType(context), LLVM.Type.GetDoubleType(context) };
			FunctionType powType = new FunctionType(LLVM.Type.GetDoubleType(context), argTypes);
			Constant pow = module.GetOrInsertFunction("pow", powType);


			Value L = primarys[0].code(context, module, builder);
			primarys.RemoveAt(0);
			while (primarys.Count > 0)
			{
				Value R = primarys[0].code(context, module, builder);
				Value[] args = {L,R};
				L = builder.CreateCall(pow, args);
				primarys.RemoveAt(0);
			}
			return L;
		}
	}
}
