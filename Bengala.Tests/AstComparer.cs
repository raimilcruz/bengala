using System;
using System.Collections.Generic;
using System.Linq;
using Bengala.AST;

namespace Bengala.Tests
{
    /// <summary>
    /// Ast visitor to compare two AST tree (based on the implementation of the
    /// ASTComparer from the Dart Analyzer code
    /// </summary>
    class AstComparer : AstVisitor<bool>
    {
        //The ast node to be compare
        AstNode _other;
        public static bool EqualNodes(AstNode first, AstNode second)
        {
            AstComparer comparator = new AstComparer();
            return comparator.IsEqualNodes(first, second);
        }


        bool IsEqualNodes(AstNode first, AstNode second)
        {
            if (first == null)
                return second == null;

            if (second == null)
                return false;

            if (first.GetType() != second.GetType())
                return false;

            _other = second;
            return first.Accept(this);
        }

        public override bool VisitBinaryExpression(BinaryExpressionAst expr)
        {
            BinaryExpressionAst binExpr = _other as BinaryExpressionAst;
            if (binExpr == null)
                return false;

            return IsEqualNodes(binExpr.LeftExp, expr.LeftExp) &&
                    IsEqualNodes(binExpr.RightExp, expr.RightExp)
                    && (binExpr.Operator == expr.Operator);
        }

        public override bool VisitIntLiteral(IntLiteral ast)
        {
            IntLiteral intAst = _other as IntLiteral;
            return intAst?.Value == ast.Value;
        }


        public override bool VisitVar(VarAST ast)
        {
            VarAST varAst = _other as VarAST;
            return varAst?.VarId == ast.VarId;
        }

        public override bool VisitNode(AstNode node)
        {
            throw new NotImplementedException();
        }

        public override bool VisitStringLiteral(StringLiteral ast)
        {
            StringLiteral stringAst = _other as StringLiteral;
            return stringAst?.Value == ast.Value;
        }

        public override bool VisitIfExpression(IfExpressionAST ast)
        {
            IfExpressionAST ifExpr = _other as IfExpressionAST;
            if (ifExpr == null)
                return false;
            return IsEqualNodes(ifExpr.ExpConditional, ast.ExpConditional)
                && IsEqualNodes(ifExpr.ExpressionThen, ast.ExpressionThen)
                && IsEqualNodes(ifExpr.ExpressionElse, ast.ExpressionElse);
        }


        public override bool VisitLetExpression(LetExpressionAST expr)
        {
            var other = _other as LetExpressionAST;
            if (other == null)
                return false;

            return IsEqualNodes(other.SequenceExpressionList, expr.SequenceExpressionList) &&
                   other.DeclarationList.Count == expr.DeclarationList.Count &&
                   other.DeclarationList.Zip(expr.DeclarationList,IsEqualNodes).All(x=>x);
        }

        public override bool VisitVarDeclaration(VarDeclarationAST expr)
        {
            var other = _other as VarDeclarationAST;
            if (other == null)
                return false;

            return IsEqualNodes(other.ExpressionValue, expr.ExpressionValue) &&
                   other.TypeId == expr.TypeId &&
                   other.Id == expr.Id;
        }

        public override bool VisitAlias(AliasAST alias)
        {
            var other = _other as AliasAST;
            if (other == null)
                return false;

            return other.TypeId == alias.TypeId &&
                   other.AliasToWho == alias.AliasToWho;
        }

        public override bool VisitArrayAccess(ArrayAccessAST arrayAccess)
        {
            throw new NotImplementedException();
        }

        public override bool VisitArrayDeclaration(ArrayDeclarationAST arrayDeclaration)
        {
            var other = _other as ArrayDeclarationAST;
            if (other == null)
                return false;

            return other.TypeId == arrayDeclaration.TypeId &&
                   other.BaseTypeID == arrayDeclaration.BaseTypeID;
        }

        public override bool VisitArrayInstantiation(ArrayInstatiationAST arrayInstatiation)
        {
            throw new NotImplementedException();
        }

        public override bool VisitAssignExpression(AssignExpressionAST assignExpression)
        {
            throw new NotImplementedException();
        }

        public override bool VisitForExpression(ForExpressionAST forExpressionAst)
        {
            throw new NotImplementedException();
        }

        public override bool VisitFunctionDeclaration(FunctionDeclarationAST functionDeclaration)
        {
            var other = _other as FunctionDeclarationAST;
            if (other == null)
                return false;

            return IsEqualNodes(other.FormalParameterList, functionDeclaration.FormalParameterList) &&
                   other.FunctionId == functionDeclaration.FunctionId &&
                    other.ReturnTypeId == functionDeclaration.ReturnTypeId &&
                    IsEqualNodes(other.ExprInstructions,functionDeclaration.ExprInstructions);
        }

        public override bool VisitNegExpression(NegExpressionAST negExpression)
        {
            var other = _other as NegExpressionAST;
            if (other == null)
                return false;

            return IsEqualNodes(other.Expression, negExpression.Expression);
        }

        public override bool VisitNilLiteral(NilLiteral nil)
        {
            throw new NotImplementedException();
        }

        public override bool VisitSequence(SequenceExpressionAST sequenceExpression)
        {
            var other = _other as SequenceExpressionAST;
            if (other == null)
                return false;

            return other.ExpressionList.Count == sequenceExpression.ExpressionList.Count &&
                   other.ExpressionList.Zip(sequenceExpression.ExpressionList, IsEqualNodes).All(x => x);
        }
     

        public override bool VisitWhileExpression(WhileExpressionAST whileExpression)
        {
            throw new NotImplementedException();
        }

        public override bool VisitBreakStatement(BreakAST breakStm)
        {
            throw new NotImplementedException();
        }

        public override bool VisitFunctionInvocation(CallFunctionAST functionInvocation)
        {
            var  fCall = _other as CallFunctionAST;
            if (fCall == null)
                return false;

            return fCall.RealParam.Count == functionInvocation.RealParam.Count &&
                   fCall.RealParam.Zip(functionInvocation.RealParam,IsEqualNodes).All(x=>x)
                   && (fCall.FunctionId == functionInvocation.FunctionId);
        }

        public override bool VisitRecordAccess(RecordAccessAST recordAccess)
        {
            throw new NotImplementedException();
        }

        public override bool VisitRecordInstantiation(RecordInstantiationAST recordInstantiation)
        {
            var other = _other as RecordInstantiationAST;
            if (other == null)
                return false;

            return other.Id == recordInstantiation.Id &&
                   IsEqualNodes(other.Fields,recordInstantiation.Fields);
        }

        public override bool VisitRecordDeclaration(RecordDeclarationAST recordDeclaration)
        {
            var other = _other as RecordDeclarationAST;
            if (other == null)
                return false;

            return other.Fields.Count == recordDeclaration.Fields.Count &&
                   other.Fields.Zip(recordDeclaration.Fields, (x,y)=> x.Key == y.Key && x.Value == y.Value).All(x => x)
                   && (other.TypeId == recordDeclaration.TypeId);
        }

        public override bool VisitArgumentList(ArgumentList argumentList)
        {
            throw new NotImplementedException();
        }
        public override bool VisitFormalParameterList(FormalParameterList formalParameterList)
        {
            var other = _other as FormalParameterList;
            if (other == null)
                return false;

            return other.Parameters.Count == formalParameterList.Parameters.Count &&
                   other.Parameters.Zip(formalParameterList.Parameters,IsEqualNodes).All(x=>x);
        }

        public override bool VisitFormalParameter(FormalParameter formalParameter)
        {
            var other = _other as FormalParameter;
            if (other == null)
                return false;

            return other.Name == formalParameter.Name && other.TypeIdentifier == formalParameter.TypeIdentifier;
        }

        public override bool VisitFieldInstanceList(FieldInstanceList fieldList)
        {
            var other = _other as FieldInstanceList;
            if (other == null)
                return false;

            return other.Fields.Count == fieldList.Fields.Count &&
                   other.Fields.Zip(fieldList.Fields, IsEqualNodes).All(x => x);
        }

        public override bool VisitFieldInstance(FieldInstance fieldInstance)
        {
            var other = _other as FieldInstance;
            if (other == null)
                return false;

            return other.Name == fieldInstance.Name && 
                   IsEqualNodes(other.Value,fieldInstance.Value);
        }
    }
}
