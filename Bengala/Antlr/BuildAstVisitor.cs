using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Bengala.AST;

namespace Bengala.Antlr
{
    /// <summary>
    /// Transform the ANTLR generated Context classes to AstNodes
    /// </summary>
    public class BuildAstVisitor : TigerBaseVisitor<AstNode>
    {
        public override AstNode VisitBreakInstr([NotNull] TigerParser.BreakInstrContext context)
        {
            return base.VisitBreakInstr(context);
        }

        public override AstNode VisitDecl([NotNull] TigerParser.DeclContext context)
        {
            return base.VisitDecl(context);
        }

        public override AstNode VisitExp([NotNull] TigerParser.ExpContext context)
        {
            return context.GetChild(0).Accept(this);
        }

        public override AstNode VisitExpCOMP([NotNull] TigerParser.ExpCOMPContext context)
        {
            //two cases:
            //1. It takes the first derivation
            if (context.ChildCount == 1)
                return context.GetChild(0).Accept(this);
            //2
            return new BinaryExpressionAst(
                (ExpressionAst)context.GetChild(0).Accept(this),
                (ExpressionAst)context.e2.Accept(this),context.s.Text);
        }

        public override AstNode VisitExpEQ([NotNull] TigerParser.ExpEQContext context)
        {
            //two cases:
            //1. It takes the first derivation
            if (context.ChildCount == 1)
                return context.GetChild(0).Accept(this);
            return new BinaryExpressionAst(
                (ExpressionAst)context.GetChild(0).Accept(this),
                (ExpressionAst)context.e2.Accept(this), context.s.Text);
        }

        public override AstNode VisitExpMod([NotNull] TigerParser.ExpModContext context)
        {
            if (context.ChildCount == 1)
                return context.GetChild(0).Accept(this);
            return new BinaryExpressionAst(
                (ExpressionAst) context.GetChild(0).Accept(this),
                (ExpressionAst) context.e2.Accept(this), context.s.Text);
        }

        public override AstNode VisitExpNE([NotNull] TigerParser.ExpNEContext context)
        {
            //two cases:
            //1. It takes the first derivation
            if (context.ChildCount == 1)
                return context.GetChild(0).Accept(this);
            return new BinaryExpressionAst(
                (ExpressionAst)context.GetChild(0).Accept(this),
                (ExpressionAst)context.e2.Accept(this), context.s.Text);
        }

        public override AstNode VisitExpOrAnd([NotNull] TigerParser.ExpOrAndContext context)
        {
            //two cases:
            //1. It takes the first derivation
            if (context.ChildCount == 1)
                return context.GetChild(0).Accept(this);
            return context.expOrAndList().Aggregate(context.e1.Accept(this),
                (acc, x) =>
                    new BinaryExpressionAst((ExpressionAst)acc,
                        (ExpressionAst)x.e2.Accept(this), x.s.Text));
        }

        public override AstNode VisitExpPorDiv([NotNull] TigerParser.ExpPorDivContext context)
        {
            if (context.ChildCount == 1)
                return context.GetChild(0).Accept(this);

            return context.expPorDivList().Aggregate(context.e1.Accept(this),
                (acc, x) =>
                    new BinaryExpressionAst((ExpressionAst)acc,
                        (ExpressionAst)x.e2.Accept(this), x.s.Text));
        }

        public override AstNode VisitExpSumRes([NotNull] TigerParser.ExpSumResContext context)
        {
            //two cases:
            //1. It takes the first derivation
            if (context.ChildCount == 1)
                return context.GetChild(0).Accept(this);

            return context.sumResList().Aggregate(context.e1.Accept(this), 
                (acc, x) => 
                    new BinaryExpressionAst((ExpressionAst)acc, 
                    (ExpressionAst)x.e2.Accept(this), x.s.Text));
        }

        public override AstNode VisitFactor([NotNull] TigerParser.FactorContext context)
        {
            return context.GetChild(0).Accept(this);
        }

        public override AstNode VisitNegExpr(TigerParser.NegExprContext context)
        {
            return new NegExpressionAST((ExpressionAst)context.fExp().Accept(this));
        }

        public override AstNode VisitFExp([NotNull] TigerParser.FExpContext context)
        {
            return base.VisitFExp(context);
        }

        public override AstNode VisitFieldList([NotNull] TigerParser.FieldListContext context)
        {
            return base.VisitFieldList(context);
        }

        public override AstNode VisitForExp([NotNull] TigerParser.ForExpContext context)
        {
            return base.VisitForExp(context);
        }

        public override AstNode VisitFunDecl([NotNull] TigerParser.FunDeclContext context)
        {
            return base.VisitFunDecl(context);
        }

        public override AstNode VisitIfExp([NotNull] TigerParser.IfExpContext context)
        {
            return base.VisitIfExp(context);
        }

        public override AstNode VisitInstructions([NotNull] TigerParser.InstructionsContext context)
        {
            return base.VisitInstructions(context);
        }

        public override AstNode VisitLetExp([NotNull] TigerParser.LetExpContext context)
        {
            return base.VisitLetExp(context);
        }

        public override AstNode VisitListExp([NotNull] TigerParser.ListExpContext context)
        {
            return new ArgumentList(context.exp().Select(x=> (ExpressionAst)x.Accept(this)).ToList());
        }

        public override AstNode VisitPrefixExpr(TigerParser.PrefixExprContext context)
        {       
            LHSExpressionAST res = new VarAST(context.id.Text);;
            if (context.prefixAccess() != null)
            {
                foreach (var prefixAccess in context.prefixAccess())
                {
                    if (prefixAccess.ID() == null)
                        res = new RecordAccessAST(prefixAccess.ID().GetText(),res);
                    else if(prefixAccess.exp() == null)
                        res = new ArrayAccessAST(res,(ExpressionAst)prefixAccess.exp().Accept(this));
                }
            }
            return res;
        }

        public override AstNode VisitProgram([NotNull] TigerParser.ProgramContext context)
        {
            return context.e.Accept(this);
        }

        public override AstNode VisitRecordInstance([NotNull] TigerParser.RecordInstanceContext context)
        {
            return base.VisitRecordInstance(context);
        }

        public override AstNode VisitSeqExp([NotNull] TigerParser.SeqExpContext context)
        {
            return new SequenceExpressionAST(context.exp().Select(x=> (ExpressionAst)x.Accept(this)).ToList());
        }

        public override AstNode VisitTypeDecl([NotNull] TigerParser.TypeDeclContext context)
        {
            return base.VisitTypeDecl(context);
        }

        public override AstNode VisitTypeFields([NotNull] TigerParser.TypeFieldsContext context)
        {
            return base.VisitTypeFields(context);
        }

        public override AstNode VisitTypeId([NotNull] TigerParser.TypeIdContext context)
        {
            return base.VisitTypeId(context);
        }

        public override AstNode VisitVarDecl([NotNull] TigerParser.VarDeclContext context)
        {
            return base.VisitVarDecl(context);
        }

        public override AstNode VisitWhileInstr([NotNull] TigerParser.WhileInstrContext context)
        {
            return base.VisitWhileInstr(context);
        }

        public override AstNode VisitIntRule(TigerParser.IntRuleContext context)
        {
            return new IntLiteral(int.Parse(context.i.Text));
        }

        public override AstNode VisitStrRule(TigerParser.StrRuleContext context)
        {
            return new StringLiteral(context.s.Text);
        }

        public override AstNode VisitNilRule(TigerParser.NilRuleContext context)
        {
            return new NilLiteral();
        }

        public override AstNode VisitFCall(TigerParser.FCallContext context)
        {
            var argumentList = (ArgumentList) context.argList?.Accept(this)?? new ArgumentList(new List<ExpressionAst>());
            return new CallFunctionAST(context.ID().GetText(), argumentList);
        }
        
    }

}