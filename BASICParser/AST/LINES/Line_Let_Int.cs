using BASICLLVM.AST;
using LLVM;

namespace BASICLLVM
{
	class Line_Let_Int : Line
	{
		public string varName;

		public NumericExpression value;

		public Line_Let_Int()
		{

		}

		public override BasicBlock code(LLVMContext context, Module module, Function mainFn)
		{
			// let's just do a really simple one
			BasicBlock block = new BasicBlock(context, mainFn, "line" + lineNumber.ToString());
			IRBuilder builder = new IRBuilder(block);

			if (value.leadingSign == NumericConstant.Sign.PLUSSIGN && value.terms.Count == 1)
			{
				Term thisTerm = value.terms[0];
				if (thisTerm.precedingSign == NumericConstant.Sign.PLUSSIGN && thisTerm.factors.Count == 1)
				{
					Factor thisFactor = thisTerm.factors[0];
					if (thisFactor.primarys.Count == 1)
					{
						Primary thisPrimary = thisFactor.primarys[0];
						if (thisPrimary is NumericRep && ((NumericRep)thisPrimary).isInt())
						{
							double constDouble = ((NumericRep)thisPrimary).value();
							Type i32Type = Type.GetInteger32Type(context);
							AllocaInstruction alloc = builder.CreateAlloca(i32Type, varName);
							Constant constValue = new Constant(context, 32, (ulong)System.Convert.ToInt32(constDouble));
							builder.CreateStore(constValue, alloc);
						}
						else if (thisPrimary is SimpleNumericVariable)
						{
							SimpleNumericVariable loadVar = (SimpleNumericVariable)thisPrimary;
							Type i32Type = Type.GetInteger32Type(context);
							AllocaInstruction loadAlloc = builder.CreateAlloca(i32Type, loadVar.name);
							Value loadValue = builder.CreateLoad(loadAlloc,"temp");

							AllocaInstruction alloc = builder.CreateAlloca(i32Type, varName);
							builder.CreateStore(loadValue, alloc);							
						}
					}
				}
				
			}
			firstBlock = block;
			lastBlock = block;
			return block;
		}

	}
}
