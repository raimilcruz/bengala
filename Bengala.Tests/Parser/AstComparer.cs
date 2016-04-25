using Bengala.AST;
using Bengala.AST.SemanticsUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bengala.Tests.Parser
{
    /// <summary>
    /// Ast visitor to compare two AST tree (based on the implementation of the
    /// ASTComparer from the Dart Analyzer code
    /// </summary>
    class AstComparer : AstVisitor<bool>
    {
        //The ast node to be compare
        AstNode _other;

        bool isEqualNodes(AstNode first, AstNode second)
        { 
            if (first == null)
            {
                return second == null;
            }
            else if (second == null)
            {
                return false;
            }
            else if (first.GetType() != second.GetType())
            {
                return false;
            }
            _other = second;
            return first.Accept(this);
        }        

        public override bool VisitBinaryExpression(BinaryExpressionAST expr)
        {
            BinaryExpressionAST binExpr = _other as BinaryExpressionAST;

            return isEqualNodes(binExpr.LeftExp, expr.LeftExp) &&
                    isEqualNodes(binExpr.RightExp, expr.RightExp)
                    && (binExpr.Operator == expr.Operator);
        }

        public override bool VisitIntAst(IntAST ast)
        {
            IntAST intAst = _other as IntAST;
            return intAst.Value == ast.Value;
        }


        public override bool VisitVar(VarAST ast)
        {
            VarAST varAst = _other as VarAST;
            return varAst.VarId == ast.VarId;
        }

        public override bool VisitNode(AstNode node)
        {
            throw new NotImplementedException();
        }

        public override bool VisitStringAst(StringAST ast)
        {
            StringAST stringAst = _other as StringAST;
            return stringAst.Value == ast.Value;
        }
        
        public override bool VisitIfExpression(IfExpressionAST ast)
        {
            IfExpressionAST ifExpr = _other as IfExpressionAST;
            return isEqualNodes(ifExpr.ExpConditional, ast.ExpConditional)
                && isEqualNodes(ifExpr.ExpressionThen, ast.ExpressionThen)
                && isEqualNodes(ifExpr.ExpressionElse, ast.ExpressionElse);
        }


        public static bool EqualNodes(AstNode first, AstNode second)
        {
            AstComparer comparator = new AstComparer();
            return comparator.isEqualNodes(first, second);
        }

        public override bool VisitLetExpression(LetExpressionAST expr)
        {
            throw new NotImplementedException();
        }

        public override bool VisitVarDeclaration(VarDeclarationAST expr)
        {
            throw new NotImplementedException();
        }
    }
}
