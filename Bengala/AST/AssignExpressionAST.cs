#region Usings

using System;
using System.Collections.Generic;
using Bengala.AST.SemanticsUtils;
using Bengala.AST.Types;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Represents an assigment instruction in the language.
    /// Example. lvalue ':=' exp
    /// </summary>
    public class AssignExpressionAST : ExpressionAst
    {
        #region Fields and Properties

        public LHSExpressionAST LeftExpression { get; private set; }
        public ExpressionAst RightExpression { get; private set; }

        #endregion

        #region Constructors

        public AssignExpressionAST(LHSExpressionAST leftExp, ExpressionAst rightExp)
        {
            LeftExpression = leftExp;
            RightExpression = rightExp;
            ReturnType = TigerType.GetType<NoType>();
        }

        public AssignExpressionAST(LHSExpressionAST leftExp, ExpressionAst rightExp, int line, int col)
            : base(line, col)
        {
            LeftExpression = leftExp;
            RightExpression = rightExp;
            ReturnType = TigerType.GetType<NoType>();
        }

        #endregion


        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitAssignExpression(this);
        }
    }
}