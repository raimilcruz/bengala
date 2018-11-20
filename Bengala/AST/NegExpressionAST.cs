#region Usings

using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    public class NegExpressionAST : UnaryExpressionAST
    {
        public NegExpressionAST(ExpressionAST exp, int line, int col)
            : base(exp, line, col)
        {
            AlwaysReturn = true;
        }

   

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitNegExpression(this);
        }
    }
}