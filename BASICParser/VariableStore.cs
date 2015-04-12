using System.Collections.Generic;
using LLVM;

namespace BASICLLVM
{
	class VariableStore
	{
		public Dictionary<string, AllocaInstruction> strings;
		public Dictionary<string, AllocaInstruction> stringPointers;
		public Dictionary<string, bool> stringIsPointer;
		public Dictionary<string, AllocaInstruction> numbers;

		public Dictionary<string, AllocaInstruction> limits;
		public Dictionary<string, AllocaInstruction> increments;
		public Stack<AllocaInstruction> returnAddresses;
		public Dictionary<string, AST.Line_For> forLines;
		public Dictionary<string, AST.Line_GoSub> gosubLines;
		public Dictionary<int, AST.Line> lines;
		public Dictionary<int, int> codeLineNumbers;
		public List<BasicBlock> returnBlocks;
		public Dictionary<string, Value> stringLiterals;
		public Dictionary<string, Value> definedConstants;

		public Dictionary<string, AllocaInstruction> arrays;
		public Dictionary<string,Value> arraySizes;

		public VariableStore()
		{
			strings = new Dictionary<string, AllocaInstruction>();
			stringPointers = new Dictionary<string, AllocaInstruction>();
			stringIsPointer = new Dictionary<string, bool>();
			numbers = new Dictionary<string, AllocaInstruction>();
			limits = new Dictionary<string, AllocaInstruction>();
			increments = new Dictionary<string, AllocaInstruction>();
			forLines = new Dictionary<string, AST.Line_For>();
			gosubLines = new Dictionary<string, AST.Line_GoSub>();
			lines = new Dictionary<int, AST.Line>();
			returnAddresses = new Stack<AllocaInstruction>();
			returnBlocks = new List<BasicBlock>();
			stringLiterals = new Dictionary<string, Value>();
			codeLineNumbers = new Dictionary<int, int>();
			arrays = new Dictionary<string, AllocaInstruction>();
			arraySizes = new Dictionary<string,Value>();
			fillConstants();
		}

		public void fillConstants()
		{
			definedConstants = new Dictionary<string, Value>();
			double xd = 3.141592653589793115997963468544185161590576171875;
			definedConstants["CONST_PI"] = ConstantFP.Get(Parser.context, new APFloat(xd));

			long xl = 2147483647;
			definedConstants["CONST_RANDMAX"] = ConstantFP.Get(Parser.context, new APFloat(xl));

		}

		public void intialiseArray(IRBuilder builder,Value arraySize,string name)
		{
			// size must be cast to int
			Value index = builder.CreateFPToUI(arraySize, Parser.i8, "arrayIndex");
			arrays[name] = builder.CreateAlloca(Parser.dbl, index, name);
			arraySizes[name] = arraySize;
		}
		


		public Value arrayItem(IRBuilder builder, string name, Value position)
		{
			// array implicitly defined
			if (!arrays.ContainsKey(name))
			{
				this.intialiseArray(builder, new Constant(Parser.context, 8, 11), name);
				this.arraySizes[name] = new Constant(Parser.context, 8, 11);
			}

			// position must be cast to int
			Value index = builder.CreateFPToUI(position, Parser.i8, "arrayIndex");
			return builder.CreateGEP(arrays[name], index, "tempArrayItem");
		}

		public void fillInitBlock(BasicBlock initBlock, BasicBlock nextBlock)
		{
			IRBuilder builder = new IRBuilder(initBlock);
			foreach(KeyValuePair<string,AllocaInstruction> kv in numbers) {
				builder.CreateAlloca(Parser.dbl, kv.Key);
			}
			builder.CreateBranch(nextBlock);
		}

	}
}
