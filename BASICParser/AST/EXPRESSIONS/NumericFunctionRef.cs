using System;
using System.Collections.Generic;
using LLVM;

namespace BASICLLVM.AST
{
	class NumericFunctionRef : Primary
	{
		public enum FunctionRefType { NUMERICDEFINEDFUNCTION, NUMERICSUPPLIEDFUNCTION };
		public enum NumericSuppliedFunction { ABS, ATN, COS, EXP, INT, LOG, RND, SGN, SIN, SQR, TAN };

		public static Dictionary<NumericSuppliedFunction,string> functionNames;
		public static Dictionary<NumericSuppliedFunction, FunctionType> functionTypes;

		public FunctionRefType refType;
		public string numericDefinedFunctionName;
		public NumericSuppliedFunction numericSuppliedFunctionName;
		public NumericExpression argument;

		public void setupFunctions(LLVMContext context)
		{
			functionNames = new Dictionary<NumericSuppliedFunction, string>();
			functionTypes = new Dictionary<NumericSuppliedFunction, FunctionType>();

			functionNames[NumericSuppliedFunction.ABS] = "abs";
			functionNames[NumericSuppliedFunction.ATN] = "atan";
			functionNames[NumericSuppliedFunction.COS] = "cos";
			functionNames[NumericSuppliedFunction.EXP] = "exp";
			functionNames[NumericSuppliedFunction.INT] = "floor";
			functionNames[NumericSuppliedFunction.LOG] = "log";
			functionNames[NumericSuppliedFunction.RND] = "rand";
			functionNames[NumericSuppliedFunction.SGN] = "copysign";
			functionNames[NumericSuppliedFunction.SIN] = "sin";
			functionNames[NumericSuppliedFunction.SQR] = "log";
			functionNames[NumericSuppliedFunction.TAN] = "tan";

			LLVM.Type[] argDouble = new LLVM.Type[] { LLVM.Type.GetDoubleType(context) };
			LLVM.Type[] argVoid = new LLVM.Type[] { };
			LLVM.Type[] argDoublePair = new LLVM.Type[] { LLVM.Type.GetDoubleType(context), LLVM.Type.GetDoubleType(context) };

			FunctionType doubleToDouble = new FunctionType(LLVM.Type.GetDoubleType(context), argDouble);
			FunctionType doublePairToDouble = new FunctionType(LLVM.Type.GetDoubleType(context), argDoublePair);
			FunctionType voidToDouble = new FunctionType(LLVM.Type.GetDoubleType(context), argVoid);

			functionTypes[NumericSuppliedFunction.ABS] = doubleToDouble;
			functionTypes[NumericSuppliedFunction.ATN] = doubleToDouble;
			functionTypes[NumericSuppliedFunction.COS] = doubleToDouble;
			functionTypes[NumericSuppliedFunction.EXP] = doubleToDouble;
			functionTypes[NumericSuppliedFunction.INT] = doubleToDouble;
			functionTypes[NumericSuppliedFunction.LOG] = doubleToDouble;
			functionTypes[NumericSuppliedFunction.RND] = voidToDouble;
			functionTypes[NumericSuppliedFunction.SGN] = doublePairToDouble;
			functionTypes[NumericSuppliedFunction.SIN] = doubleToDouble;
			functionTypes[NumericSuppliedFunction.SQR] = doubleToDouble;
			functionTypes[NumericSuppliedFunction.TAN] = doubleToDouble;
		}

		public Constant getSuppliedFunction(NumericSuppliedFunction fn, Module module)
		{
			string functionName = functionNames[fn];
			FunctionType type = functionTypes[fn];
			return module.GetOrInsertFunction(functionName, type);
		}

		public NumericFunctionRef(string _numericdefinedfunctionname)
		{
			numericDefinedFunctionName = _numericdefinedfunctionname;
			refType = FunctionRefType.NUMERICDEFINEDFUNCTION;
		}
		public NumericFunctionRef(NumericSuppliedFunction _suppliedFunction)
		{
			numericSuppliedFunctionName = _suppliedFunction;
			refType = FunctionRefType.NUMERICSUPPLIEDFUNCTION;
		}
		public NumericFunctionRef(string _numericdefinedfunctionname, NumericExpression arg)
		{
			numericDefinedFunctionName = _numericdefinedfunctionname;
			argument = arg;
			refType = FunctionRefType.NUMERICDEFINEDFUNCTION;
		}

		public NumericFunctionRef(NumericSuppliedFunction _suppliedFunction, NumericExpression arg)
		{
			numericSuppliedFunctionName = _suppliedFunction;
			argument = arg;
			refType = FunctionRefType.NUMERICSUPPLIEDFUNCTION;
		}

		public override Value code(LLVMContext context, Module module, IRBuilder builder)
		{
			setupFunctions(context);
			if (refType == FunctionRefType.NUMERICSUPPLIEDFUNCTION)
			{
				Constant suppliedFunction = getSuppliedFunction(numericSuppliedFunctionName,module);
				Value[] args = {};
				Value input;
				switch (numericSuppliedFunctionName)
				{
					case NumericSuppliedFunction.RND:
						// Do something
						break;
					case NumericSuppliedFunction.SGN:
						Value one = ConstantFP.Get(context, new APFloat(1.0));
						input = argument.code(context,module,builder);
						args = new Value[] {one, input};
						break;
					default:
						input = argument.code(context,module,builder);
						args = new Value[] {input};
						break;
				}

				return builder.CreateCall(suppliedFunction, args);
			}
			throw new NotImplementedException();
		}
	}
}
