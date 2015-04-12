//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.5
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from ../BASIC.g4 by ANTLR 4.5

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591

using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="BASICParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.5")]
[System.CLSCompliant(false)]
public interface IBASICListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.remarkstatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRemarkstatement([NotNull] BASICParser.RemarkstatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.remarkstatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRemarkstatement([NotNull] BASICParser.RemarkstatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.line"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLine([NotNull] BASICParser.LineContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.line"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLine([NotNull] BASICParser.LineContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.linenumber"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLinenumber([NotNull] BASICParser.LinenumberContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.linenumber"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLinenumber([NotNull] BASICParser.LinenumberContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.endline"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEndline([NotNull] BASICParser.EndlineContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.endline"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEndline([NotNull] BASICParser.EndlineContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.endstatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEndstatement([NotNull] BASICParser.EndstatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.endstatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEndstatement([NotNull] BASICParser.EndstatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStatement([NotNull] BASICParser.StatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStatement([NotNull] BASICParser.StatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.numericconstant"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNumericconstant([NotNull] BASICParser.NumericconstantContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.numericconstant"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNumericconstant([NotNull] BASICParser.NumericconstantContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.sign"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSign([NotNull] BASICParser.SignContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.sign"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSign([NotNull] BASICParser.SignContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.numericrep"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNumericrep([NotNull] BASICParser.NumericrepContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.numericrep"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNumericrep([NotNull] BASICParser.NumericrepContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.significand"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSignificand([NotNull] BASICParser.SignificandContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.significand"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSignificand([NotNull] BASICParser.SignificandContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.integer"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterInteger([NotNull] BASICParser.IntegerContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.integer"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitInteger([NotNull] BASICParser.IntegerContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.fraction"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFraction([NotNull] BASICParser.FractionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.fraction"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFraction([NotNull] BASICParser.FractionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.exrad"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExrad([NotNull] BASICParser.ExradContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.exrad"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExrad([NotNull] BASICParser.ExradContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.stringconstant"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStringconstant([NotNull] BASICParser.StringconstantContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.stringconstant"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStringconstant([NotNull] BASICParser.StringconstantContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.variable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterVariable([NotNull] BASICParser.VariableContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.variable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitVariable([NotNull] BASICParser.VariableContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.numericvariable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNumericvariable([NotNull] BASICParser.NumericvariableContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.numericvariable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNumericvariable([NotNull] BASICParser.NumericvariableContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.simplenumericvariable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSimplenumericvariable([NotNull] BASICParser.SimplenumericvariableContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.simplenumericvariable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSimplenumericvariable([NotNull] BASICParser.SimplenumericvariableContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.numericarrayelement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNumericarrayelement([NotNull] BASICParser.NumericarrayelementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.numericarrayelement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNumericarrayelement([NotNull] BASICParser.NumericarrayelementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.numericarrayname"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNumericarrayname([NotNull] BASICParser.NumericarraynameContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.numericarrayname"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNumericarrayname([NotNull] BASICParser.NumericarraynameContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.subscript"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSubscript([NotNull] BASICParser.SubscriptContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.subscript"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSubscript([NotNull] BASICParser.SubscriptContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.stringvariable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStringvariable([NotNull] BASICParser.StringvariableContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.stringvariable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStringvariable([NotNull] BASICParser.StringvariableContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression([NotNull] BASICParser.ExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression([NotNull] BASICParser.ExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.numericexpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNumericexpression([NotNull] BASICParser.NumericexpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.numericexpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNumericexpression([NotNull] BASICParser.NumericexpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTerm([NotNull] BASICParser.TermContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTerm([NotNull] BASICParser.TermContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFactor([NotNull] BASICParser.FactorContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFactor([NotNull] BASICParser.FactorContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.multiplier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMultiplier([NotNull] BASICParser.MultiplierContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.multiplier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMultiplier([NotNull] BASICParser.MultiplierContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.primary"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPrimary([NotNull] BASICParser.PrimaryContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.primary"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPrimary([NotNull] BASICParser.PrimaryContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.numericfunctionref"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNumericfunctionref([NotNull] BASICParser.NumericfunctionrefContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.numericfunctionref"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNumericfunctionref([NotNull] BASICParser.NumericfunctionrefContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.numericfunctionname"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNumericfunctionname([NotNull] BASICParser.NumericfunctionnameContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.numericfunctionname"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNumericfunctionname([NotNull] BASICParser.NumericfunctionnameContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.argumentlist"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterArgumentlist([NotNull] BASICParser.ArgumentlistContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.argumentlist"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitArgumentlist([NotNull] BASICParser.ArgumentlistContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.argument"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterArgument([NotNull] BASICParser.ArgumentContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.argument"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitArgument([NotNull] BASICParser.ArgumentContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.stringexpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStringexpression([NotNull] BASICParser.StringexpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.stringexpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStringexpression([NotNull] BASICParser.StringexpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.numericsuppliedfunction"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNumericsuppliedfunction([NotNull] BASICParser.NumericsuppliedfunctionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.numericsuppliedfunction"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNumericsuppliedfunction([NotNull] BASICParser.NumericsuppliedfunctionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.defstatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDefstatement([NotNull] BASICParser.DefstatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.defstatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDefstatement([NotNull] BASICParser.DefstatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.numericdefinedfunction"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNumericdefinedfunction([NotNull] BASICParser.NumericdefinedfunctionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.numericdefinedfunction"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNumericdefinedfunction([NotNull] BASICParser.NumericdefinedfunctionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.parameterlist"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParameterlist([NotNull] BASICParser.ParameterlistContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.parameterlist"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParameterlist([NotNull] BASICParser.ParameterlistContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.parameter"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParameter([NotNull] BASICParser.ParameterContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.parameter"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParameter([NotNull] BASICParser.ParameterContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.letstatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLetstatement([NotNull] BASICParser.LetstatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.letstatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLetstatement([NotNull] BASICParser.LetstatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.numericletstatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNumericletstatement([NotNull] BASICParser.NumericletstatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.numericletstatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNumericletstatement([NotNull] BASICParser.NumericletstatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.stringletstatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStringletstatement([NotNull] BASICParser.StringletstatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.stringletstatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStringletstatement([NotNull] BASICParser.StringletstatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.gotostatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterGotostatement([NotNull] BASICParser.GotostatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.gotostatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitGotostatement([NotNull] BASICParser.GotostatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.ifthenstatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIfthenstatement([NotNull] BASICParser.IfthenstatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.ifthenstatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIfthenstatement([NotNull] BASICParser.IfthenstatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.relationalexpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRelationalexpression([NotNull] BASICParser.RelationalexpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.relationalexpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRelationalexpression([NotNull] BASICParser.RelationalexpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.relation"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRelation([NotNull] BASICParser.RelationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.relation"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRelation([NotNull] BASICParser.RelationContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.equalityrelation"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEqualityrelation([NotNull] BASICParser.EqualityrelationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.equalityrelation"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEqualityrelation([NotNull] BASICParser.EqualityrelationContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.notless"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNotless([NotNull] BASICParser.NotlessContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.notless"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNotless([NotNull] BASICParser.NotlessContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.notgreater"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNotgreater([NotNull] BASICParser.NotgreaterContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.notgreater"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNotgreater([NotNull] BASICParser.NotgreaterContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.notequals"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNotequals([NotNull] BASICParser.NotequalsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.notequals"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNotequals([NotNull] BASICParser.NotequalsContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.gosubstatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterGosubstatement([NotNull] BASICParser.GosubstatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.gosubstatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitGosubstatement([NotNull] BASICParser.GosubstatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.returnstatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterReturnstatement([NotNull] BASICParser.ReturnstatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.returnstatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitReturnstatement([NotNull] BASICParser.ReturnstatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.ongotostatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOngotostatement([NotNull] BASICParser.OngotostatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.ongotostatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOngotostatement([NotNull] BASICParser.OngotostatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.stopstatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStopstatement([NotNull] BASICParser.StopstatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.stopstatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStopstatement([NotNull] BASICParser.StopstatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.forstatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterForstatement([NotNull] BASICParser.ForstatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.forstatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitForstatement([NotNull] BASICParser.ForstatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.controlvariable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterControlvariable([NotNull] BASICParser.ControlvariableContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.controlvariable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitControlvariable([NotNull] BASICParser.ControlvariableContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.initialvalue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterInitialvalue([NotNull] BASICParser.InitialvalueContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.initialvalue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitInitialvalue([NotNull] BASICParser.InitialvalueContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.limit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLimit([NotNull] BASICParser.LimitContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.limit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLimit([NotNull] BASICParser.LimitContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.increment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIncrement([NotNull] BASICParser.IncrementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.increment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIncrement([NotNull] BASICParser.IncrementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.nextstatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNextstatement([NotNull] BASICParser.NextstatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.nextstatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNextstatement([NotNull] BASICParser.NextstatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.printstatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPrintstatement([NotNull] BASICParser.PrintstatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.printstatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPrintstatement([NotNull] BASICParser.PrintstatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.printlist"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPrintlist([NotNull] BASICParser.PrintlistContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.printlist"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPrintlist([NotNull] BASICParser.PrintlistContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.printitem"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPrintitem([NotNull] BASICParser.PrintitemContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.printitem"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPrintitem([NotNull] BASICParser.PrintitemContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.printseparator"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPrintseparator([NotNull] BASICParser.PrintseparatorContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.printseparator"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPrintseparator([NotNull] BASICParser.PrintseparatorContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.inputstatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterInputstatement([NotNull] BASICParser.InputstatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.inputstatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitInputstatement([NotNull] BASICParser.InputstatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.dimensionstatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDimensionstatement([NotNull] BASICParser.DimensionstatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.dimensionstatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDimensionstatement([NotNull] BASICParser.DimensionstatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.bounds"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBounds([NotNull] BASICParser.BoundsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.bounds"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBounds([NotNull] BASICParser.BoundsContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.readstatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterReadstatement([NotNull] BASICParser.ReadstatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.readstatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitReadstatement([NotNull] BASICParser.ReadstatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.writestatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterWritestatement([NotNull] BASICParser.WritestatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.writestatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitWritestatement([NotNull] BASICParser.WritestatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.filename"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFilename([NotNull] BASICParser.FilenameContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.filename"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFilename([NotNull] BASICParser.FilenameContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.randomizestatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRandomizestatement([NotNull] BASICParser.RandomizestatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.randomizestatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRandomizestatement([NotNull] BASICParser.RandomizestatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.stringcharacter"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStringcharacter([NotNull] BASICParser.StringcharacterContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.stringcharacter"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStringcharacter([NotNull] BASICParser.StringcharacterContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.quotedstringcharacter"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterQuotedstringcharacter([NotNull] BASICParser.QuotedstringcharacterContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.quotedstringcharacter"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitQuotedstringcharacter([NotNull] BASICParser.QuotedstringcharacterContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.unquotedstringcharacter"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterUnquotedstringcharacter([NotNull] BASICParser.UnquotedstringcharacterContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.unquotedstringcharacter"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitUnquotedstringcharacter([NotNull] BASICParser.UnquotedstringcharacterContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.plainstringcharacter"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPlainstringcharacter([NotNull] BASICParser.PlainstringcharacterContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.plainstringcharacter"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPlainstringcharacter([NotNull] BASICParser.PlainstringcharacterContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.remarkstring"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRemarkstring([NotNull] BASICParser.RemarkstringContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.remarkstring"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRemarkstring([NotNull] BASICParser.RemarkstringContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.quotedstring"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterQuotedstring([NotNull] BASICParser.QuotedstringContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.quotedstring"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitQuotedstring([NotNull] BASICParser.QuotedstringContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BASICParser.unquotedstring"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterUnquotedstring([NotNull] BASICParser.UnquotedstringContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BASICParser.unquotedstring"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitUnquotedstring([NotNull] BASICParser.UnquotedstringContext context);
}
