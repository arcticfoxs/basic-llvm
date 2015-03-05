using System;
using System.Collections.Generic;
using BASICLLVM.AST;

namespace BASICLLVM
{
	class Listener : IBASICListener
	{
		public Line finishedLine; // write to this

		// temporary variables
		int currentInteger, currentLineNumber;
		String currentStringConstant;
		Expression_String currentStringExpression;
		bool isTabCall = false;
		bool isInt = false;
		PrintItem currentPrintItem;
		PrintList currentPrintList;
		PrintList.printseparator currentPrintSeparator = PrintList.printseparator.NULL;
		string currentIntVariable;
		NumericExpression currentNumericExpression;
		Term currentTerm;
		Factor currentFactor;
		Primary currentPrimary;
		NumericRep currentNumericRep;
		Significand currentSignificand;
		Fraction currentFraction;

		public void EnterLine(BASICParser.LineContext context)
		{
			// throw new NotImplementedException();
		}

		public void ExitLine(BASICParser.LineContext context)
		{
			// throw new NotImplementedException();
		}

		public void EnterLinenumber(BASICParser.LinenumberContext context)
		{
			// throw new NotImplementedException();
		}

		public void ExitLinenumber(BASICParser.LinenumberContext context)
		{
			currentLineNumber = Convert.ToInt32(context.GetText());
		}

		public void EnterEndline(BASICParser.EndlineContext context)
		{
			// throw new NotImplementedException();
		}

		public void ExitEndline(BASICParser.EndlineContext context)
		{
			finishedLine = new Line_End(currentLineNumber);
			// throw new NotImplementedException();
		}

		public void EnterEndstatement(BASICParser.EndstatementContext context)
		{
			// throw new NotImplementedException();
		}

		public void ExitEndstatement(BASICParser.EndstatementContext context)
		{
			// throw new NotImplementedException();
		}

		public void EnterStatement(BASICParser.StatementContext context)
		{
			// throw new NotImplementedException();
		}

		public void ExitStatement(BASICParser.StatementContext context)
		{
			// throw new NotImplementedException();
		}

		public void EnterNumericconstant(BASICParser.NumericconstantContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitNumericconstant(BASICParser.NumericconstantContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterSign(BASICParser.SignContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitSign(BASICParser.SignContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterNumericrep(BASICParser.NumericrepContext context)
		{
			// dfdf
		}

		public void ExitNumericrep(BASICParser.NumericrepContext context)
		{
			throw new NotImplementedException();
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
			// throw new NotImplementedException();
			
		}

		public void EnterFraction(BASICParser.FractionContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitFraction(BASICParser.FractionContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterExrad(BASICParser.ExradContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitExrad(BASICParser.ExradContext context)
		{
			throw new NotImplementedException();
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
			currentStringConstant = theString;
		}

		public void EnterVariable(BASICParser.VariableContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitVariable(BASICParser.VariableContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterNumericvariable(BASICParser.NumericvariableContext context)
		{
			// throw new NotImplementedException();
		}

		public void ExitNumericvariable(BASICParser.NumericvariableContext context)
		{
			// throw new NotImplementedException();
		}

		public void EnterSimplenumericvariable(BASICParser.SimplenumericvariableContext context)
		{
			// throw new NotImplementedException();
		}

		public void ExitSimplenumericvariable(BASICParser.SimplenumericvariableContext context)
		{
			currentIntVariable = context.GetText();
		}

		public void EnterNumericarrayelement(BASICParser.NumericarrayelementContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitNumericarrayelement(BASICParser.NumericarrayelementContext context)
		{
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
			throw new NotImplementedException();
		}

		public void ExitStringvariable(BASICParser.StringvariableContext context)
		{
			throw new NotImplementedException();
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
			currentNumericExpression = new NumericExpression();
		}

		public void ExitNumericexpression(BASICParser.NumericexpressionContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterTerm(BASICParser.TermContext context)
		{
			currentTerm = new Term();
		}

		public void ExitTerm(BASICParser.TermContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterFactor(BASICParser.FactorContext context)
		{
			currentFactor = new Factor();
		}

		public void ExitFactor(BASICParser.FactorContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterMultiplier(BASICParser.MultiplierContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitMultiplier(BASICParser.MultiplierContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterPrimary(BASICParser.PrimaryContext context)
		{
			currentPrimary = new Primary();
		}

		public void ExitPrimary(BASICParser.PrimaryContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterNumericfunctionref(BASICParser.NumericfunctionrefContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitNumericfunctionref(BASICParser.NumericfunctionrefContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterNumericfunctionname(BASICParser.NumericfunctionnameContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitNumericfunctionname(BASICParser.NumericfunctionnameContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterArgumentlist(BASICParser.ArgumentlistContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitArgumentlist(BASICParser.ArgumentlistContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterArgument(BASICParser.ArgumentContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitArgument(BASICParser.ArgumentContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterStringexpression(BASICParser.StringexpressionContext context)
		{
			// throw new NotImplementedException();
		}

		public void ExitStringexpression(BASICParser.StringexpressionContext context)
		{
			if (currentStringConstant != null)
			{
				currentStringExpression = new Expression_String(true, currentStringConstant);
				currentStringConstant = null;
			} 
			else
			{
				// handle variable
			}
			
		}

		public void EnterNumericsuppliedfunction(BASICParser.NumericsuppliedfunctionContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitNumericsuppliedfunction(BASICParser.NumericsuppliedfunctionContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterDefstatement(BASICParser.DefstatementContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitDefstatement(BASICParser.DefstatementContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterNumericdefinedfunction(BASICParser.NumericdefinedfunctionContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitNumericdefinedfunction(BASICParser.NumericdefinedfunctionContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterParameterlist(BASICParser.ParameterlistContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitParameterlist(BASICParser.ParameterlistContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterParameter(BASICParser.ParameterContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitParameter(BASICParser.ParameterContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterLetstatement(BASICParser.LetstatementContext context)
		{
			// throw new NotImplementedException();
		}

		public void ExitLetstatement(BASICParser.LetstatementContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterNumericletstatement(BASICParser.NumericletstatementContext context)
		{
			// throw new NotImplementedException();
		}

		public void ExitNumericletstatement(BASICParser.NumericletstatementContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterStringletstatement(BASICParser.StringletstatementContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitStringletstatement(BASICParser.StringletstatementContext context)
		{
			throw new NotImplementedException();
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
			throw new NotImplementedException();
		}

		public void ExitIfthenstatement(BASICParser.IfthenstatementContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterRelationalexpression(BASICParser.RelationalexpressionContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitRelationalexpression(BASICParser.RelationalexpressionContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterRelation(BASICParser.RelationContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitRelation(BASICParser.RelationContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterEqualityrelation(BASICParser.EqualityrelationContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitEqualityrelation(BASICParser.EqualityrelationContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterNotless(BASICParser.NotlessContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitNotless(BASICParser.NotlessContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterNotgreater(BASICParser.NotgreaterContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitNotgreater(BASICParser.NotgreaterContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterNotequals(BASICParser.NotequalsContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitNotequals(BASICParser.NotequalsContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterGosubstatement(BASICParser.GosubstatementContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitGosubstatement(BASICParser.GosubstatementContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterReturnstatement(BASICParser.ReturnstatementContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitReturnstatement(BASICParser.ReturnstatementContext context)
		{
			throw new NotImplementedException();
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
			throw new NotImplementedException();
		}

		public void ExitStopstatement(BASICParser.StopstatementContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterForline(BASICParser.ForlineContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitForline(BASICParser.ForlineContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterNextline(BASICParser.NextlineContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitNextline(BASICParser.NextlineContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterForstatement(BASICParser.ForstatementContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitForstatement(BASICParser.ForstatementContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterControlvariable(BASICParser.ControlvariableContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitControlvariable(BASICParser.ControlvariableContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterInitialvalue(BASICParser.InitialvalueContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitInitialvalue(BASICParser.InitialvalueContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterLimit(BASICParser.LimitContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitLimit(BASICParser.LimitContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterIncrement(BASICParser.IncrementContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitIncrement(BASICParser.IncrementContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterNextstatement(BASICParser.NextstatementContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitNextstatement(BASICParser.NextstatementContext context)
		{
			throw new NotImplementedException();
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
			// throw new NotImplementedException();
		}

		public void ExitPrintitem(BASICParser.PrintitemContext context)
		{
			currentPrintItem = isTabCall ? new PrintItem() : new PrintItem(currentStringExpression);
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
			throw new NotImplementedException();
		}

		public void ExitInputstatement(BASICParser.InputstatementContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterVariablelist(BASICParser.VariablelistContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitVariablelist(BASICParser.VariablelistContext context)
		{
			throw new NotImplementedException();
		}

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

		public void EnterRemarkstatement(BASICParser.RemarkstatementContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitRemarkstatement(BASICParser.RemarkstatementContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterRandomizestatement(BASICParser.RandomizestatementContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitRandomizestatement(BASICParser.RandomizestatementContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterStringcharacter(BASICParser.StringcharacterContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitStringcharacter(BASICParser.StringcharacterContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterQuotedstringcharacter(BASICParser.QuotedstringcharacterContext context)
		{
			// throw new NotImplementedException();
		}

		public void ExitQuotedstringcharacter(BASICParser.QuotedstringcharacterContext context)
		{
			// throw new NotImplementedException();
		}

		public void EnterUnquotedstringcharacter(BASICParser.UnquotedstringcharacterContext context)
		{
			// throw new NotImplementedException();
		}

		public void ExitUnquotedstringcharacter(BASICParser.UnquotedstringcharacterContext context)
		{
			// throw new NotImplementedException();
		}

		public void EnterPlainstringcharacter(BASICParser.PlainstringcharacterContext context)
		{
			// throw new NotImplementedException();
		}

		public void ExitPlainstringcharacter(BASICParser.PlainstringcharacterContext context)
		{
			// throw new NotImplementedException();
		}

		public void EnterRemarkstring(BASICParser.RemarkstringContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitRemarkstring(BASICParser.RemarkstringContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterQuotedstring(BASICParser.QuotedstringContext context)
		{
			// throw new NotImplementedException();
		}

		public void ExitQuotedstring(BASICParser.QuotedstringContext context)
		{

			// throw new NotImplementedException();
		}

		public void EnterUnquotedstring(BASICParser.UnquotedstringContext context)
		{
			throw new NotImplementedException();
		}

		public void ExitUnquotedstring(BASICParser.UnquotedstringContext context)
		{
			throw new NotImplementedException();
		}

		public void EnterEveryRule(Antlr4.Runtime.ParserRuleContext ctx)
		{
			// throw new NotImplementedException();
		}

		public void ExitEveryRule(Antlr4.Runtime.ParserRuleContext ctx)
		{
			// throw new NotImplementedException();
		}

		public void VisitErrorNode(Antlr4.Runtime.Tree.IErrorNode node)
		{
			throw new NotImplementedException();
		}

		public void VisitTerminal(Antlr4.Runtime.Tree.ITerminalNode node)
		{
			// throw new NotImplementedException();
		}
	}
}
