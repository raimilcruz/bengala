#region Usings

using System;
using System.Collections.Generic;
using Bengala.AST.CodeGenerationUtils;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Represents an assigment instruction in the language.
    /// Example. lvalue ':=' exp
    /// </summary>
    public class AssignExpressionAST : ExpressionAST
    {
        #region Fields and Properties

        public LHSExpressionAST LeftExpression { get; private set; }
        public ExpressionAST RightExpression { get; private set; }

        #endregion

        #region Constructors

        public AssignExpressionAST(LHSExpressionAST leftExp, ExpressionAST rightExp)
        {
            LeftExpression = leftExp;
            RightExpression = rightExp;
            ReturnType = TigerType.GetType<NoType>();
        }

        public AssignExpressionAST(LHSExpressionAST leftExp, ExpressionAST rightExp, int line, int col)
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