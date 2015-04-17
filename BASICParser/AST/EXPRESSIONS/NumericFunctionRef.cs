using LLVM;
using System.Collections.Generic;

namespace BASICLLVM.AST
{
	class NumericFunctionRef : Primary
	{
		public enum FunctionRefType { NUMERICDEFINEDFUNCTION, NUMERICSUPPLIEDFUNCTION };
		public enum NumericSuppliedFunction { ABS, ATN, COS, EXP, INT, LOG, RND, SGN, SIN, SQR, TAN, PI, MOD2 };

		public static Dictionary<NumericSuppliedFunction, string> functionNames;
		public static Dictionary<NumericSuppliedFunction, FunctionType> functionTypes;

		public FunctionRefType refType;
		public string numericDefinedFunctionName;
		public NumericSuppliedFunction numericSuppliedFunctionName;
		public NumericExpression argument;

		public void setupFunctions()
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
			functionNames[NumericSuppliedFunction.SQR] = "sqrt";
			functionNames[NumericSuppliedFunction.TAN] = "tan";

			LLVM.Type[] argDouble = new LLVM.Type[] { Parser.dbl };
			LLVM.Type[] argVoid = new LLVM.Type[] { };
			LLVM.Type[] argDoublePair = new LLVM.Type[] { Parser.dbl, Parser.dbl };

			FunctionType doubleToDouble = new FunctionType(Parser.dbl, argDouble);
			FunctionType doublePairToDouble = new FunctionType(Parser.dbl, argDoublePair);
			FunctionType voidToDouble = new FunctionType(Parser.dbl, argVoid);

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

		public Constant getSuppliedFunction(NumericSuppliedFunction fn)
		{
			string functionName = functionNames[fn];
			FunctionType type = functionTypes[fn];
			return Parser.module.GetOrInsertFunction(functionName, type);
		}

		public Constant getConstantAbs()
		{
			LLVM.Type[] args = new LLVM.Type[] { Parser.i8p };
			FunctionType type = new FunctionType(Parser.dbl, args);
			return Parser.module.GetOrInsertFunction("abs", type);
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

		public override Value code(IRBuilder builder)
		{

			setupFunctions();

			if (refType == FunctionRefType.NUMERICSUPPLIEDFUNCTION)
			{
				Constant suppliedFunction = getSuppliedFunction(numericSuppliedFunctionName);
				Value[] args = { };
				Value input;
				switch (numericSuppliedFunctionName)
				{
					case NumericSuppliedFunction.RND:
						// Do something
						break;
					case NumericSuppliedFunction.SGN:
						Value one = ConstantFP.Get(Parser.context, new APFloat(1.0));
						input = argument.code(builder);
						args = new Value[] { one, input };
						break;
					case NumericSuppliedFunction.MOD2:
						Value two = ConstantFP.Get(Parser.context, new APFloat(2.0));
						input = argument.code(builder);
						args = new Value[] { input, two };
						break;
					default:
						input = argument.code(builder);
						args = new Value[] { input };
						break;
				}

				return builder.CreateCall(suppliedFunction, args);
			}
			else
			{
				// Arbitrary function;
				LLVM.Type[] argDouble = new LLVM.Type[] { Parser.dbl };
				LLVM.Type[] argVoid = new LLVM.Type[] { };

				LLVM.Type[] argType = (argument == null) ? argVoid : argDouble;
				LLVM.Type returnType = Parser.dbl;

				FunctionType fnType = new FunctionType(returnType, argType);
				Constant fn = Parser.module.GetOrInsertFunction(numericDefinedFunctionName, fnType);

				Value[] args = (argument == null) ? new Value[] { } : new Value[] { argument.code(builder) };
				return builder.CreateCall(fn, args);
			}
		}
	}
}
