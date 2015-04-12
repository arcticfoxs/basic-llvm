using LLVM;

namespace BASICLLVM.AST
{
	class Line_Write : Line
	{
		public string arrayName;
		public StringExpression fileName;
		public override BasicBlock code()
		{
			BasicBlock block = bb();
			IRBuilder builder = new IRBuilder(block);
			LLVM.Type[] argTypes = new LLVM.Type[] { Parser.i8p, Parser.dblp, Parser.i8 };

			FunctionType stringToInt = new FunctionType(Parser.vd, argTypes);
			Constant writeArrayToFile = Parser.module.GetOrInsertFunction("writeArrayToFile", stringToInt);

			Value arrayPointer = Parser.variables.arrayItem(builder, arrayName, Parser.zero);
			Value fileNamePointer = fileName.code(builder);

			Value[] args = {fileNamePointer,arrayPointer,Parser.variables.arraySizes[arrayName]};
			builder.CreateCall(writeArrayToFile, args);

			firstBlock = block;
			lastBlock = block;
			return block;
		}
	}
}
