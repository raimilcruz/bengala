using System;
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

        public override bool VisitBinaryExpression(BinaryExpressionAST expr)
        {
            BinaryExpressionAST binExpr = _other as BinaryExpressionAST;
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
            throw new NotImplementedException();
        }

        public override bool VisitVarDeclaration(VarDeclarationAST expr)
        {
            throw new NotImplementedException();
        }

        public override bool VisitAlias(AliasAST alias)
        {
            throw new NotImplementedException();
        }

        public override bool VisitArrayAccess(ArrayAccessAST arrayAccess)
        {
            throw new NotImplementedException();
        }

        public override bool VisitArrayDeclaration(ArrayDeclarationAST arrayDeclaration)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public override bool VisitNegExpression(NegExpressionAST negExpression)
        {
            throw new NotImplementedException();
        }

        public override bool VisitNilLiteral(NilLiteral nil)
        {
            throw new NotImplementedException();
        }

        public override bool VisitSequence(SequenceExpressionAST sequenceExpression)
        {
            throw new NotImplementedException();
        }

        public override bool VisitTypeDeclaration(TypeDeclarationAST typeDeclaration)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public override bool VisitRecordAccess(RecordAccessAST recordAccess)
        {
            throw new NotImplementedException();
        }

        public override bool VisitRecordInstantiation(RecordInstantiationAST recordInstantiation)
        {
            throw new NotImplementedException();
        }

        public override bool VisitRecordDeclaration(RecordDeclarationAST recordDeclaration)
        {
            throw new NotImplementedException();
        }
    }
}
