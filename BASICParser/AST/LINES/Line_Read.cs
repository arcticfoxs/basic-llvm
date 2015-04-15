using LLVM;

namespace BASICLLVM.AST
{
	class Line_Read : Line
	{
		public string arrayName;
		public StringExpression fileName;
		public override BasicBlock code()
		{
			block = bb();
			IRBuilder builder = new IRBuilder(block);
			LLVM.Type[] argTypes = new LLVM.Type[] { Parser.i8p, Parser.dblp };

			FunctionType stringToInt = new FunctionType(Parser.vd, argTypes);
			Constant readFileToArray = Parser.module.GetOrInsertFunction("readFileToArray", stringToInt);

			Value arrayPointer = Parser.variables.arrayItem(builder, arrayName, Parser.zero);
			Value fileNamePointer = fileName.code(builder);

			Value[] args = {fileNamePointer,arrayPointer};
			builder.CreateCall(readFileToArray, args);

			return block;
		}
	}
}
