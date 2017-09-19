﻿using Bengala.AST.SemanticsUtils;
using System;

namespace Bengala.AST
{
    public abstract class ErrorListener {
        public abstract void Add(ErrorMessage msg);
        public abstract void Add(WarningMessage msg);

        public abstract void Insert(int pos, ErrorMessage msg);

        public abstract int Count { get;}

    }
    public abstract class AstVisitor<T>
    {
        public abstract T VisitNode(AstNode node);

        public abstract T VisitIntLiteral(IntLiteral literal);

        public abstract T VisitStringLiteral(StringLiteral literal);

        //TODO: Check if VarAst is used in both side: left and right
        public abstract T VisitVar(VarAST ast);

        public abstract T VisitIfExpression(IfExpressionAST ast);

        public abstract T VisitBinaryExpression(BinaryExpressionAST expr);

        public abstract T VisitLetExpression(LetExpressionAST expr);

        public abstract T VisitVarDeclaration(VarDeclarationAST expr);

        public abstract T VisitAlias(AliasAST alias);

        public abstract T VisitArrayAccess(ArrayAccessAST arrayAccess);

        public abstract T VisitArrayDeclaration(ArrayDeclarationAST arrayDeclaration);

        public abstract T VisitArrayInstantiation(ArrayInstatiationAST arrayInstatiation);

        public abstract T VisitAssignExpression(AssignExpressionAST assignExpression);

        public abstract T VisitForExpression(ForExpressionAST forExpressionAst);

        public abstract T VisitFunctionDeclaration(FunctionDeclarationAST functionDeclaration);

        public abstract T VisitNegExpression(NegExpressionAST negExpression);

        public abstract T VisitNilLiteral(NilLiteral nil);

        public abstract T VisitSequence(SequenceExpressionAST sequenceExpression);

        public abstract T VisitWhileExpression(WhileExpressionAST whileExpression);

        public abstract T VisitBreakStatement(BreakAST breakStm);

        public abstract T VisitFunctionInvocation(CallFunctionAST functionInvocation);

        public abstract T VisitRecordAccess(RecordAccessAST recordAccess);

        public abstract T VisitRecordInstantiation(RecordInstantiationAST recordInstantiation);

        public abstract T VisitRecordDeclaration(RecordDeclarationAST recordDeclaration);
    }
}