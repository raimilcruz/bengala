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
            return new BreakAST();
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
                (ExpressionAst)context.e2.Accept(this), context.s.Text);
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
                (ExpressionAst)context.GetChild(0).Accept(this),
                (ExpressionAst)context.e2.Accept(this), context.s.Text);
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
            return context.GetChild(0).Accept(this);
        }

        public override AstNode VisitFieldList([NotNull] TigerParser.FieldListContext context)
        {
            return new FieldInstanceList(context.fieldInstance()
                .Select(x => (FieldInstance)x.Accept(this)).ToList());
        }

        public override AstNode VisitFieldInstance(TigerParser.FieldInstanceContext context)
        {
            return new FieldInstance(context.id.Text, (ExpressionAst)context.e.Accept(this));
        }

        public override AstNode VisitForExp([NotNull] TigerParser.ForExpContext context)
        {
            return new ForExpressionAST(context.id.Text,
                (ExpressionAst)context.e1.Accept(this),
                (ExpressionAst)context.e2.Accept(this),
                (ExpressionAst)context.e3.Accept(this));
        }

        public override AstNode VisitFunDecl([NotNull] TigerParser.FunDeclContext context)
        {
            return new FunctionDeclarationAST(
                context.fId.Text,
                (FormalParameterList)context.pList.Accept(this),
                (ExpressionAst)context.body.Accept(this),
                context.ret.GetText());
        }

        public override AstNode VisitIfExp([NotNull] TigerParser.IfExpContext context)
        {
            return new IfExpressionAST((ExpressionAst)context.cond.Accept(this),
                (ExpressionAst)context.e1.Accept(this),
                (ExpressionAst) context.e2?.Accept(this));
        }

        public override AstNode VisitInstructions([NotNull] TigerParser.InstructionsContext context)
        {
            return new SequenceExpressionAST(context.exp().Select(x => (ExpressionAst)x.Accept(this)).ToList());
        }

        public override AstNode VisitLetExp([NotNull] TigerParser.LetExpContext context)
        {
            var body = context.insts == null
                ? new SequenceExpressionAST(new List<ExpressionAst>())
                : (SequenceExpressionAST)context.insts.Accept(this);
            return new LetExpressionAST(context.decl().Select(x => x.Accept(this)).Cast<Declaration>().ToList(), body);
        }

        public override AstNode VisitListExp([NotNull] TigerParser.ListExpContext context)
        {
            return new ArgumentList(context.exp().Select(x => (ExpressionAst)x.Accept(this)).ToList());
        }

        public override AstNode VisitPrefixExpr(TigerParser.PrefixExprContext context)
        {
            LHSExpressionAST res = new VarAST(context.id.Text); ;
            if (context.prefixAccess() != null)
            {
                foreach (var prefixAccess in context.prefixAccess())
                {
                    if (prefixAccess.ID() != null)
                        res = new RecordAccessAST(prefixAccess.ID().GetText(), res);
                    else if (prefixAccess.exp() != null)
                        res = new ArrayAccessAST(res, (ExpressionAst)prefixAccess.exp().Accept(this));
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
            return new RecordInstantiationAST(context.id.Text, (FieldInstanceList)context.fields.Accept(this));
        }

        public override AstNode VisitSeqExp([NotNull] TigerParser.SeqExpContext context)
        {
            return new SequenceExpressionAST(context.exp().Select(x => (ExpressionAst)x.Accept(this)).ToList());
        }

        public override AstNode VisitTypeDecl([NotNull] TigerParser.TypeDeclContext context)
        {
            var definition = (TypeDeclarationAST)context.typeDefinition().Accept(this);
            definition.TypeId = context.id.Text;
            return definition;
        }

        public override AstNode VisitTypeDefinition(TigerParser.TypeDefinitionContext context)
        {
            return context.GetChild(0).Accept(this);
        }

        public override AstNode VisitAliasType(TigerParser.AliasTypeContext context)
        {
            //TODO: refactor AST nodes for type definitions. They should not include the id.
            return new AliasAST("it will be renamed", context.type_id.GetText());
        }

        public override AstNode VisitArrayType(TigerParser.ArrayTypeContext context)
        {
            //TODO: refactor AST nodes for type definitions. They should not include the id.
            return new ArrayDeclarationAST("it will be renamed", context.typeOfArray.GetText());
        }

        public override AstNode VisitRecordDef(TigerParser.RecordDefContext context)
        {
            //TODO: refactor AST nodes for type definitions. They should not include the id.
            return new RecordDeclarationAST("it will be renamed", (FormalParameterList)context.typeList.Accept(this));
        }

        public override AstNode VisitTypeFields([NotNull] TigerParser.TypeFieldsContext context)
        {
            return new FormalParameterList(context.formalParameter().Select(x => (FormalParameter)x.Accept(this)).ToList());
        }

        public override AstNode VisitFormalParameter(TigerParser.FormalParameterContext context)
        {
            return new FormalParameter(context.id.Text, context.type.GetText());
        }

        public override AstNode VisitTypeId([NotNull] TigerParser.TypeIdContext context)
        {
            return base.VisitTypeId(context);
        }

        public override AstNode VisitVarDecl([NotNull] TigerParser.VarDeclContext context)
        {
            if (context.type_Id == null)
                return new VarDeclarationAST(context.id.Text, (ExpressionAst)context.value.Accept(this));
            return new VarDeclarationAST(context.id.Text, context.type_Id.GetText(), (ExpressionAst)context.value.Accept(this));
        }

        public override AstNode VisitWhileInstr([NotNull] TigerParser.WhileInstrContext context)
        {
            return new WhileExpressionAST((ExpressionAst)context.cond.Accept(this),
                (ExpressionAst)context.body.Accept(this));
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
            var argumentList = (ArgumentList)context.argList?.Accept(this) ?? new ArgumentList(new List<ExpressionAst>());
            return new CallFunctionAST(context.ID().GetText(), argumentList);
        }

        public override AstNode VisitAssignment(TigerParser.AssignmentContext context)
        {
            return new AssignExpressionAST((LHSExpressionAST)context.prefixExpr().Accept(this),
                (ExpressionAst)context.exp().Accept(this));
        }

        public override AstNode VisitArrayInstance(TigerParser.ArrayInstanceContext context)
        {
            return new ArrayInstatiationAST(context.id.Text,
                (ExpressionAst)context.sizeExp.Accept(this),
                (ExpressionAst)context.initExp.Accept(this));
        }
    }

}