using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASICLLVM.AST
{
	class NumericFunctionRef : Primary
	{
		public enum FunctionRefType { NUMERICDEFINEDFUNCTION, NUMERICSUPPLIEDFUNCTION };
		public enum NumericSuppliedFunction { ABS, ATN, COS, EXP, INT, LOG, RND, SGN, SIN, SQR, TAN };

		public FunctionRefType refType;
		public string numericDefinedFunctionName;
		public NumericSuppliedFunction numericSuppliedFunctionName;
		public NumericExpression argument;
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
	}
}
