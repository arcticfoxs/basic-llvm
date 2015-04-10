using System;
using System.Collections.Generic;
using BASICLLVM.AST;
using LLVM;

namespace BASICLLVM
{
	class Listener : IBASICListener
	{
		public Line finishedLine; // write to this

		// temporary variables
		int currentInteger, currentLineNumber;
		int thisLineNumber;
		StringConstant currentStringConstant;
		Stack<StringExpression> currentStringExpression = new Stack<StringExpression>();
		bool isTabCall = false;
		bool isInt = false;
		bool wasArray = false;
		PrintItem currentPrintItem;
		PrintList currentPrintList;
		PrintList.printseparator currentPrintSeparator = PrintList.printseparator.NULL;

		SimpleNumericVariable currentSimpleNumericVariable;
		NumericVariable currentNumericVariable;
		NumericArrayElement currentNumericArrayElement;

		Stack<StringVariable> currentStringVariable = new Stack<StringVariable>();
		Stack<NumericExpression> currentNumericExpression = new Stack<NumericExpression>();
		Stack<Term> currentTerm = new Stack<Term>();
		Stack<Factor> currentFactor = new Stack<Factor>();
		Stack<Primary> currentPrimary = new Stack<Primary>();
		NumericRep currentNumericRep;
		NumericConstant currentNumericConstant;
		Significand currentSignificand;
		Fraction currentFraction;
		Exrad currentExrad;
		NumericConstant.Sign currentSign;
		NumericConstant.Sign currentNumericConstantSign;
		Term.Multiplier currentMultiplier;
		Line_Let_Int currentLineLetInt;

		SimpleNumericVariable currentParameter;
		string currentNumericDefinedFunction;

		RelationalExpression currentRelationalExpression;
		RelationalExpression.Relation currentRelation;
		RelationalExpression.EqualityRelation currentEqualityRelation;

		SimpleNumericVariable currentControlVariable;
		NumericExpression currentInitialValue, currentLimit, currentIncrement;

		NumericFunctionRef.NumericSuppliedFunction currentNumericSuppliedFunction;
		NumericExpression currentArgument;
		NumericFunctionRef currentNumericFunctionRef;
		enum PrimaryOptions {VAR,CONST,FN,EXP};
		PrimaryOptions primaryOp;

		Line_Input currentInputLine;
		bool haveSign;
		bool seekingSign;

		public void EnterLine(BASICParser.LineContext context)
		{
			currentLineNumber = -2;
			thisLineNumber = -2;
		}

		public void ExitLine(BASICParser.LineContext context)
		{
			if (thisLineNumber > -2)
			{
				finishedLine.lineNumber = thisLineNumber;
				finishedLine.hasLineNumber = true;
			}
			else
			{
				finishedLine.lineNumber = Parser.unlabeledLines;
				finishedLine.hasLineNumber = false;
				Parser.unlabeledLines++;
			}
		}

		public void EnterLinenumber(BASICParser.LinenumberContext context){}

		public void ExitLinenumber(BASICParser.LinenumberContext context)
		{
			currentLineNumber = Convert.ToInt32(context.GetText());
		}

		public void EnterEndline(BASICParser.EndlineContext context){}

		public void ExitEndline(BASICParser.EndlineContext context)
		{
			finishedLine = new Line_End();
		}

		public void EnterEndstatement(BASICParser.EndstatementContext context)
		{
			thisLineNumber = currentLineNumber;
		}

		public void ExitEndstatement(BASICParser.EndstatementContext context) {}

		public void EnterStatement(BASICParser.StatementContext context)
		{
			thisLineNumber = currentLineNumber;
		}

		public void ExitStatement(BASICParser.StatementContext context) {}

		public void EnterNumericconstant(BASICParser.NumericconstantContext context) {
			haveSign = false;
			seekingSign = true;
		}
		public void ExitNumericconstant(BASICParser.NumericconstantContext context) {
			currentNumericConstant = haveSign ? new NumericConstant(currentSign,currentNumericRep) : new NumericConstant(currentNumericRep);
			primaryOp = PrimaryOptions.CONST;		
		}

		public void EnterSign(BASICParser.SignContext context) {}

		public void ExitSign(BASICParser.SignContext context)
		{
			currentSign = context.GetText().Equals("-") ? NumericConstant.Sign.MINUSSIGN : NumericConstant.Sign.PLUSSIGN;
			if (seekingSign)
			{
				currentNumericConstantSign = currentSign;
				haveSign = true;
				seekingSign = false;
			}
		}

		public void EnterNumericrep(BASICParser.NumericrepContext context)
		{
			if (seekingSign) haveSign = false;
			seekingSign = false;
		}

		public void ExitNumericrep(BASICParser.NumericrepContext context)
		{
			if (currentExrad == null) currentNumericRep = new NumericRep(currentSignificand);
			else
			{
				currentNumericRep = new NumericRep(currentSignificand, currentExrad);
				currentExrad = null;
			}
		}

		public void EnterSignificand(BASICParser.SignificandContext context)
		{
			isInt = false;
			
		}

		public void ExitSignificand(BASICParser.SignificandContext context)
		{
			if (currentFraction == null) currentSignificand = new Significand(currentInteger);
			else
			{
				currentSignificand =  isInt ? new Significand(currentInteger, currentFraction) : new Significand(currentFraction);
				currentFraction = null;
			}
		}

		public void EnterInteger(BASICParser.IntegerContext context)
		{
			isInt = true;
		}

		public void ExitInteger(BASICParser.IntegerContext context)
		{
			int payload = Convert.ToInt32(context.GetText());
			currentInteger = payload;			
		}

		public void EnterFraction(BASICParser.FractionContext context){}

		public void ExitFraction(BASICParser.FractionContext context)
		{
			currentFraction = new Fraction(context.GetText());
		}

		public void EnterExrad(BASICParser.ExradContext context){}

		public void ExitExrad(BASICParser.ExradContext context)
		{
			currentExrad = new Exrad(currentInteger);
		}

		public void EnterStringconstant(BASICParser.StringconstantContext context)
		{
			// throw new NotImplementedException();
		}

		public void ExitStringconstant(BASICParser.StringconstantContext context)
		{
			string theString = context.GetText();
			// strip quotes
			theString = theString.Substring(1, theString.Length - 2);
			currentStringConstant = new StringConstant(theString);
		}

		public void EnterVariable(BASICParser.VariableContext context) {}

		public void ExitVariable(BASICParser.VariableContext context) {}

		public void EnterNumericvariable(BASICParser.NumericvariableContext context) {}

		public void ExitNumericvariable(BASICParser.NumericvariableContext context)
		{
			currentNumericVariable = wasArray ? (NumericVariable)currentNumericArrayElement : (NumericVariable)currentSimpleNumericVariable;

			if (currentLineLetInt != null && currentLineLetInt.varName == null)
				currentLineLetInt.varName = context.GetText();

			primaryOp = PrimaryOptions.VAR;
			if (currentInputLine != null) currentInputLine.vars.Add(currentNumericVariable);
		}

		public void EnterSimplenumericvariable(BASICParser.SimplenumericvariableContext context)
		{
		
		}

		public void ExitSimplenumericvariable(BASICParser.SimplenumericvariableContext context)
		{
			currentSimpleNumericVariable = new SimpleNumericVariable(context.GetText());
			wasArray = false;
		}

		public void EnterNumericarrayelement(BASICParser.NumericarrayelementContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitNumericarrayelement(BASICParser.NumericarrayelementContext context)
		{
			wasArray = true;
			throw new NotImplementedException();
		}

		public void EnterNumericarrayname(BASICParser.NumericarraynameContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitNumericarrayname(BASICParser.NumericarraynameContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterSubscript(BASICParser.SubscriptContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitSubscript(BASICParser.SubscriptContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterStringvariable(BASICParser.StringvariableContext context)
		{
			
		}

		public void ExitStringvariable(BASICParser.StringvariableContext context)
		{
			currentStringVariable.Push(new StringVariable(context.GetText()));
			if(currentInputLine != null) currentInputLine.vars.Add(currentStringVariable.Pop());
		}

		public void EnterExpression(BASICParser.ExpressionContext context)
		{
			// throw new NotImplementedException();
		}

		public void ExitExpression(BASICParser.ExpressionContext context)
		{
			// throw new NotImplementedException();
		}

		public void EnterNumericexpression(BASICParser.NumericexpressionContext context)
		{
			currentNumericExpression.Push(new NumericExpression());
		}

		public void ExitNumericexpression(BASICParser.NumericexpressionContext context)
		{
			primaryOp = PrimaryOptions.EXP;
		}

		public void EnterTerm(BASICParser.TermContext context)
		{
			currentTerm.Push(new Term());
			currentTerm.Peek().precedingSign = currentSign;
			currentSign = NumericConstant.Sign.PLUSSIGN;
		}

		public void ExitTerm(BASICParser.TermContext context)
		{
			// TODO SIGNS!!!!!
			Term thisTerm = currentTerm.Pop();
			currentNumericExpression.Peek().add(thisTerm,thisTerm.precedingSign);
		}

		public void EnterFactor(BASICParser.FactorContext context)
		{
			currentFactor.Push(new Factor());
		}

		public void ExitFactor(BASICParser.FactorContext context)
		{
			currentTerm.Peek().add(currentFactor.Pop(),currentMultiplier);
		}

		public void EnterMultiplier(BASICParser.MultiplierContext context)
		{
			
		}

		public void ExitMultiplier(BASICParser.MultiplierContext context)
		{
			
			currentMultiplier = context.GetText().Equals("*") ? Term.Multiplier.ASTERISK : Term.Multiplier.SOLIDUS;
		}

		public void EnterPrimary(BASICParser.PrimaryContext context)
		{
			
		}

		public void ExitPrimary(BASICParser.PrimaryContext context)
		{
			switch(primaryOp) {
				case PrimaryOptions.EXP:
					currentPrimary.Push(currentNumericExpression.Pop());
					break;
				case PrimaryOptions.FN:
					currentPrimary.Push(currentNumericFunctionRef);
					break;
				case PrimaryOptions.CONST:
					currentPrimary.Push(currentNumericConstant);
					break;
				case PrimaryOptions.VAR:
					currentPrimary.Push(currentNumericVariable);
					break;
			}
			currentFactor.Peek().add(currentPrimary.Pop());
		}

		public void EnterNumericfunctionref(BASICParser.NumericfunctionrefContext context)
		{
			
		}

		public void ExitNumericfunctionref(BASICParser.NumericfunctionrefContext context)
		{
			if (currentNumericDefinedFunction == null)
			{
				if (currentArgument == null)
					currentNumericFunctionRef = new NumericFunctionRef(currentNumericSuppliedFunction);
				else
					currentNumericFunctionRef = new NumericFunctionRef(currentNumericSuppliedFunction, currentArgument);
			}
			else
			{
				if (currentArgument == null)
					currentNumericFunctionRef = new NumericFunctionRef(currentNumericDefinedFunction);
				else
					currentNumericFunctionRef = new NumericFunctionRef(currentNumericDefinedFunction, currentArgument);
			}

			currentNumericDefinedFunction = null;
			currentArgument = null;

			primaryOp = PrimaryOptions.FN;
		}

		public void EnterNumericfunctionname(BASICParser.NumericfunctionnameContext context)
		{
			
		}

		public void ExitNumericfunctionname(BASICParser.NumericfunctionnameContext context)
		{
			
		}

		public void EnterArgumentlist(BASICParser.ArgumentlistContext context)
		{
			
		}

		public void ExitArgumentlist(BASICParser.ArgumentlistContext context)
		{
			
		}

		public void EnterArgument(BASICParser.ArgumentContext context)
		{
			
		}

		public void ExitArgument(BASICParser.ArgumentContext context)
		{
			currentArgument = currentNumericExpression.Pop();
		}

		public void EnterStringexpression(BASICParser.StringexpressionContext context)
		{
			if (currentStringExpression == null) currentStringExpression = new Stack<StringExpression>();
		}

		public void ExitStringexpression(BASICParser.StringexpressionContext context)
		{
			if (currentStringConstant != null)
			{
				currentStringExpression.Push(currentStringConstant);
				currentStringConstant = null;
			} 
			else
			{
				currentStringExpression.Push(currentStringVariable.Pop());
			}
			
		}

		public void EnterNumericsuppliedfunction(BASICParser.NumericsuppliedfunctionContext context)
		{
			
		}

		public void ExitNumericsuppliedfunction(BASICParser.NumericsuppliedfunctionContext context)
		{
			Enum.TryParse<NumericFunctionRef.NumericSuppliedFunction>(context.GetText(), out currentNumericSuppliedFunction);
		}

		public void EnterDefstatement(BASICParser.DefstatementContext context)
		{
			
		}

		public void ExitDefstatement(BASICParser.DefstatementContext context)
		{
			if (currentParameter == null)
				finishedLine = new Line_Def(currentNumericDefinedFunction, currentNumericExpression.Pop());
			else
				finishedLine = new Line_Def(currentNumericDefinedFunction, currentParameter, currentNumericExpression.Pop());
			currentParameter = null;
		}

		public void EnterNumericdefinedfunction(BASICParser.NumericdefinedfunctionContext context)
		{
			
		}

		public void ExitNumericdefinedfunction(BASICParser.NumericdefinedfunctionContext context)
		{
			currentNumericDefinedFunction = context.GetText().Substring(context.GetText().Length - 1);
		}

		public void EnterParameterlist(BASICParser.ParameterlistContext context)
		{
			
		}

		public void ExitParameterlist(BASICParser.ParameterlistContext context)
		{
			
		}

		public void EnterParameter(BASICParser.ParameterContext context)
		{
			
		}

		public void ExitParameter(BASICParser.ParameterContext context)
		{
			currentParameter = currentSimpleNumericVariable;
		}

		public void EnterLetstatement(BASICParser.LetstatementContext context)
		{
			// throw new NotImplementedException();
		}

		public void ExitLetstatement(BASICParser.LetstatementContext context)
		{
			
		}

		public void EnterNumericletstatement(BASICParser.NumericletstatementContext context)
		{
			currentLineLetInt = new Line_Let_Int();
		}

		public void ExitNumericletstatement(BASICParser.NumericletstatementContext context)
		{
			currentLineLetInt.value = currentNumericExpression.Pop();
			finishedLine = currentLineLetInt;
			finishedLine.lineNumber = currentLineNumber;
		}

		public void EnterStringletstatement(BASICParser.StringletstatementContext context)
		{
			
		}

		public void ExitStringletstatement(BASICParser.StringletstatementContext context)
		{
			finishedLine = new Line_Let_String(currentStringVariable.Pop(),currentStringExpression.Pop());
		}

		public void EnterGotostatement(BASICParser.GotostatementContext context)
		{
			// throw new NotImplementedException();
		}

		public void ExitGotostatement(BASICParser.GotostatementContext context)
		{
			finishedLine = new Line_Goto(currentLineNumber, currentInteger);
			// throw new NotImplementedException();
		}

		public void EnterIfthenstatement(BASICParser.IfthenstatementContext context)
		{
			
		}

		public void ExitIfthenstatement(BASICParser.IfthenstatementContext context)
		{
			finishedLine = new Line_IfThen(currentRelationalExpression, currentInteger);
		}

		public void EnterRelationalexpression(BASICParser.RelationalexpressionContext context)
		{
			
		}

		public void ExitRelationalexpression(BASICParser.RelationalexpressionContext context)
		{
			if (currentNumericExpression.Count == 0)
				currentRelationalExpression = new StringRelationalExpression(currentStringExpression.Pop(), currentStringExpression.Pop(), currentEqualityRelation);
			else
				currentRelationalExpression = new NumericRelationalExpression(currentNumericExpression.Pop(), currentNumericExpression.Pop(), currentRelation);
		}

		public void EnterRelation(BASICParser.RelationContext context)
		{
			
		}

		public void ExitRelation(BASICParser.RelationContext context)
		{
			switch (context.GetText())
			{
				case "=":
					currentRelation = RelationalExpression.Relation.EQUAL;
					break;
				case "!=":
					currentRelation = RelationalExpression.Relation.NOTEQUAL;
					break;
				case ">":
					currentRelation = RelationalExpression.Relation.GREATERTHAN;
					break;
				case "<":
					currentRelation = RelationalExpression.Relation.LESSTHAN;
					break;
				case "<=":
					currentRelation = RelationalExpression.Relation.NOTGREATER;
					break;
				case ">=":
					currentRelation = RelationalExpression.Relation.NOTLESS;
					break;
			}
		}

		public void EnterEqualityrelation(BASICParser.EqualityrelationContext context)
		{
			
		}

		public void ExitEqualityrelation(BASICParser.EqualityrelationContext context)
		{
			switch (context.GetText())
			{
				case "=":
					currentRelation = RelationalExpression.Relation.EQUAL;
					currentEqualityRelation = RelationalExpression.EqualityRelation.EQUAL;
					break;
				case "!=":
					currentRelation = RelationalExpression.Relation.NOTEQUAL;
					currentEqualityRelation = RelationalExpression.EqualityRelation.EQUAL;
					break;
			}
		}

		public void EnterNotless(BASICParser.NotlessContext context)
		{
			
		}

		public void ExitNotless(BASICParser.NotlessContext context)
		{
			
		}

		public void EnterNotgreater(BASICParser.NotgreaterContext context)
		{
			
		}

		public void ExitNotgreater(BASICParser.NotgreaterContext context)
		{
			
		}

		public void EnterNotequals(BASICParser.NotequalsContext context)
		{
			
		}

		public void ExitNotequals(BASICParser.NotequalsContext context)
		{
			
		}

		public void EnterGosubstatement(BASICParser.GosubstatementContext context)
		{
			
		}

		public void ExitGosubstatement(BASICParser.GosubstatementContext context)
		{
			finishedLine = new Line_GoSub(currentInteger);
		}

		public void EnterReturnstatement(BASICParser.ReturnstatementContext context)
		{
			
		}

		public void ExitReturnstatement(BASICParser.ReturnstatementContext context)
		{
			finishedLine = new Line_Return();
		}

		public void EnterOngotostatement(BASICParser.OngotostatementContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitOngotostatement(BASICParser.OngotostatementContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterStopstatement(BASICParser.StopstatementContext context)
		{
			
		}

		public void ExitStopstatement(BASICParser.StopstatementContext context)
		{
			finishedLine = new Line_End();
		}

		public void EnterForstatement(BASICParser.ForstatementContext context)
		{
			
		}

		public void ExitForstatement(BASICParser.ForstatementContext context)
		{
			finishedLine = new Line_For(currentControlVariable, currentInitialValue, currentLimit, currentIncrement);
		}

		public void EnterControlvariable(BASICParser.ControlvariableContext context)
		{
			
		}

		public void ExitControlvariable(BASICParser.ControlvariableContext context)
		{
			currentControlVariable = currentSimpleNumericVariable;
		}

		public void EnterInitialvalue(BASICParser.InitialvalueContext context)
		{
			
		}

		public void ExitInitialvalue(BASICParser.InitialvalueContext context)
		{
			currentInitialValue = currentNumericExpression.Pop();
		}

		public void EnterLimit(BASICParser.LimitContext context)
		{
			
		}

		public void ExitLimit(BASICParser.LimitContext context)
		{
			currentLimit = currentNumericExpression.Pop();
		}

		public void EnterIncrement(BASICParser.IncrementContext context)
		{
			
		}

		public void ExitIncrement(BASICParser.IncrementContext context)
		{
			currentIncrement = currentNumericExpression.Pop();
		}

		public void EnterNextstatement(BASICParser.NextstatementContext context)
		{
			
		}

		public void ExitNextstatement(BASICParser.NextstatementContext context)
		{
			finishedLine = new Line_Next(currentControlVariable);
		}

		public void EnterPrintstatement(BASICParser.PrintstatementContext context)
		{
			// throw new NotImplementedException();
		}

		public void ExitPrintstatement(BASICParser.PrintstatementContext context)
		{
			if (currentPrintList.items.Count != currentPrintList.separators.Count + 1)
			{
				// error! should be one more item than separator
				throw new NotImplementedException();
			}
			finishedLine = new Line_Print(currentPrintList);
		}

		public void EnterPrintlist(BASICParser.PrintlistContext context)
		{
			currentPrintList = new PrintList();
		}

		public void ExitPrintlist(BASICParser.PrintlistContext context)
		{
			currentPrintSeparator = PrintList.printseparator.NULL;
		}

		public void EnterPrintitem(BASICParser.PrintitemContext context)
		{
			currentStringExpression = null;
			currentNumericExpression = new Stack<NumericExpression>();
		}

		public void ExitPrintitem(BASICParser.PrintitemContext context)
		{
			if (currentStringExpression == null)
				currentPrintItem = isTabCall ? new PrintItem() : new PrintItem(currentNumericExpression.Pop());
			else
				currentPrintItem = isTabCall ? new PrintItem() : new PrintItem(currentStringExpression.Pop());

			if (currentPrintSeparator == PrintList.printseparator.NULL)
			{
				currentPrintList.add(currentPrintItem);
			}
			else
			{
				currentPrintList.add(currentPrintItem, currentPrintSeparator);
				currentPrintSeparator = PrintList.printseparator.NULL;
			}
		}

		public void EnterTabcall(BASICParser.TabcallContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitTabcall(BASICParser.TabcallContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterPrintseparator(BASICParser.PrintseparatorContext context)
		{
			// throw new NotImplementedException();
		}

		public void ExitPrintseparator(BASICParser.PrintseparatorContext context)
		{
			currentPrintSeparator = context.GetText().Equals(",") ? PrintList.printseparator.COMMA : PrintList.printseparator.SEMICOLON;
		}

		public void EnterInputstatement(BASICParser.InputstatementContext context)
		{
			currentInputLine = new Line_Input();
		}

		public void ExitInputstatement(BASICParser.InputstatementContext context)
		{
			finishedLine = currentInputLine;
		}

		public void EnterVariablelist(BASICParser.VariablelistContext context) {}

		public void ExitVariablelist(BASICParser.VariablelistContext context) {}

		public void EnterInputprompt(BASICParser.InputpromptContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitInputprompt(BASICParser.InputpromptContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterInputreply(BASICParser.InputreplyContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitInputreply(BASICParser.InputreplyContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterInputlist(BASICParser.InputlistContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitInputlist(BASICParser.InputlistContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterPaddeddatum(BASICParser.PaddeddatumContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitPaddeddatum(BASICParser.PaddeddatumContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterDatum(BASICParser.DatumContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitDatum(BASICParser.DatumContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterReadstatement(BASICParser.ReadstatementContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitReadstatement(BASICParser.ReadstatementContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterRestorestatement(BASICParser.RestorestatementContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitRestorestatement(BASICParser.RestorestatementContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterDatastatement(BASICParser.DatastatementContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitDatastatement(BASICParser.DatastatementContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterDatalist(BASICParser.DatalistContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitDatalist(BASICParser.DatalistContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterDimensionstatement(BASICParser.DimensionstatementContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitDimensionstatement(BASICParser.DimensionstatementContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterArraydeclaration(BASICParser.ArraydeclarationContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitArraydeclaration(BASICParser.ArraydeclarationContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterBounds(BASICParser.BoundsContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitBounds(BASICParser.BoundsContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterRemarkstatement(BASICParser.RemarkstatementContext context) { }

		public void ExitRemarkstatement(BASICParser.RemarkstatementContext context)
		{
			finishedLine = new Line();
		}

		public void EnterRandomizestatement(BASICParser.RandomizestatementContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitRandomizestatement(BASICParser.RandomizestatementContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterStringcharacter(BASICParser.StringcharacterContext context) {}

		public void ExitStringcharacter(BASICParser.StringcharacterContext context) {}

		public void EnterQuotedstringcharacter(BASICParser.QuotedstringcharacterContext context){}

		public void ExitQuotedstringcharacter(BASICParser.QuotedstringcharacterContext context) {}

		public void EnterUnquotedstringcharacter(BASICParser.UnquotedstringcharacterContext context) {}

		public void ExitUnquotedstringcharacter(BASICParser.UnquotedstringcharacterContext context) {}

		public void EnterPlainstringcharacter(BASICParser.PlainstringcharacterContext context) {}

		public void ExitPlainstringcharacter(BASICParser.PlainstringcharacterContext context) {}

		public void EnterRemarkstring(BASICParser.RemarkstringContext context) {}

		public void ExitRemarkstring(BASICParser.RemarkstringContext context) {}

		public void EnterQuotedstring(BASICParser.QuotedstringContext context) {}

		public void ExitQuotedstring(BASICParser.QuotedstringContext context) {}

		public void EnterUnquotedstring(BASICParser.UnquotedstringContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitUnquotedstring(BASICParser.UnquotedstringContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterEveryRule(Antlr4.Runtime.ParserRuleContext ctx){}

		public void ExitEveryRule(Antlr4.Runtime.ParserRuleContext ctx){}

		public void VisitErrorNode(Antlr4.Runtime.Tree.IErrorNode node)
		{
			if (thisLineNumber > 0)
				throw new SyntaxErrorException(Parser.counter, node.GetText());
			else
				throw new SyntaxErrorException(Parser.counter, thisLineNumber, node.GetText());

		}

		public void VisitTerminal(Antlr4.Runtime.Tree.ITerminalNode node) {}
	}
}
