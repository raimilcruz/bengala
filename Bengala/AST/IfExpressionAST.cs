#region Usings

using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Representa las expresiones: 'if' expCond 'then' expThen 'else' expElse 
    ///                             'if' expCond 'then' expThen
    /// </summary>
    public class IfExpressionAST : ExpressionAst
    {
        #region Fields and Properties

        public ExpressionAst ExpConditional { get; }
        public ExpressionAst ExpressionElse { get; }
        public ExpressionAst ExpressionThen { get; }

        #endregion

        #region Constructors

        public IfExpressionAST(ExpressionAst expCondition, ExpressionAst expThen)
        {
            ExpConditional = expCondition;
            ExpressionThen = expThen;
        }

        public IfExpressionAST(ExpressionAst expCondition, ExpressionAst expThen,
                               ExpressionAst expElse)
            : this(expCondition, expThen)
        {
            ExpressionElse = expElse;
            ExpConditional = expCondition;
            ExpressionThen = expThen;
        }
        

        #endregion

     

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitIfExpression(this);
        }
    }
}