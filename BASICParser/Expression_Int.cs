using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASICParser
{
	class Expression_Int
	{
		public enum Operators { PLUS, MINUS, LITERAL, VAR }
		Operators op;

		Expression_Int LHS, RHS;
		int literal;
		string varName;

		public Expression_Int(int intLiteral)
		{
			op = Operators.LITERAL;
			literal = intLiteral;

			varName = null;
			LHS = null;
			RHS = null;
		}

		public Expression_Int(string var)
		{
			op = Operators.VAR;
			varName = var;

			literal = -1;
			LHS = null;
			RHS = null;
		}

		public Expression_Int(Operators thisOp, Expression_Int left, Expression_Int right)
		{
			op = thisOp;
			LHS = left;
			RHS = right;

			varName = null;
			literal = -1;
		}

		public static Expression_Int parse(List<Symbol> tokens)
		{
			switch (tokens.Count)
			{
				case 1:
					if (tokens[0].symType == Symbol.sym.INTLITERAL) return new Expression_Int(tokens[0].intPayload);
					else if (tokens[0].symType == Symbol.sym.INTVAR) return new Expression_Int(tokens[0].stringPayload);
					else { /* error */ }
					break;
				case 2:
					// impossible expression!
					// throw an error
					break;
			}

			// Now we have to do complicated actual parsing bracket analysis stuff :(

			int bracketLevel = 0;
			for (int i = 0; i < tokens.Count; i++)
			{
				switch (tokens[i].symType)
				{
					case Symbol.sym.LPAREN:
						bracketLevel++;
						break;
					case Symbol.sym.RPAREN:
						bracketLevel--;
						break;
					case Symbol.sym.PLUS:
						if (bracketLevel == 0)	return buildExpression(Operators.PLUS, tokens, i); // parse this!
						break;
					case Symbol.sym.MINUS:
						if (bracketLevel == 0)	return buildExpression(Operators.MINUS, tokens, i); // parse this!
						break;
				}
				// TODO: some syntax checking
				// () and )( not allowed
				// )3 and 3( not allowed
				// (+ and +) not allowed
			}
			// OK, we haven't managed to parse this yet, maybe it's all wrapped in a big bracket?
			if (tokens[0].symType == Symbol.sym.LPAREN && tokens[tokens.Count - 1].symType == Symbol.sym.RPAREN)
			{
				// hooray! strip the brackets
				return parse(tokens.GetRange(1, tokens.Count - 2));
			}
			
			// Everything's gone wrong - this expression doesn't parse
			// Throw an exception

			return new Expression_Int(-1); // TODO: This isn't the right thing to do at all!

		}
		static Expression_Int buildExpression(Operators op, List<Symbol> tokens, int opPosition)
		{
			// helper function to keep above code tidy and clarify parsing steps
			List<Symbol> left = tokens.GetRange(0, opPosition);
			List<Symbol> right = tokens.GetRange(opPosition + 1, tokens.Count - opPosition - 1);
			Expression_Int leftExpression = parse(left);
			Expression_Int rightExpression = parse(right);
			return new Expression_Int(op, leftExpression, rightExpression);
		}
	}
	
}
