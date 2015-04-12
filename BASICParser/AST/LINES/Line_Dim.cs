using LLVM;

namespace BASICLLVM.AST
{
	class Line_Dim : Line
	{
		public string arrayName;
		public NumericExpression bounds;
		public override BasicBlock code()
		{
			BasicBlock block = bb();
			IRBuilder builder = new IRBuilder(block);

			Parser.variables.intialiseArray(builder, bounds.code(builder), arrayName);

			firstBlock = block;
			lastBlock = block;
			return block;
		}
	}
}
