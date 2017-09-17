using Bengala.AST.SemanticsUtils;
using System;

namespace Bengala.AST
{
    public abstract class ErrorListener {
        public abstract void Add(ErrorMessage msg);
        public abstract void Add(WarningMessage msg);

        public abstract void Insert(int pos, ErrorMessage msg);

        public int Count { get; protected set; }

    }
    public abstract class AstVisitor<T>
    {
        public abstract T VisitNode(AstNode node);

        public abstract T VisitIntAst(IntAST ast);

        public abstract T VisitStringAst(StringAST ast);

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
    }
}