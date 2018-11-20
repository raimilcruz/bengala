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

        public ExpressionAst ExpConditional { get; private set; }
        public ExpressionAst ExpressionElse { get; private set; }
        public ExpressionAst ExpressionThen { get; private set; }

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
        }

        public IfExpressionAST(ExpressionAst expCondition, ExpressionAst expThen, int line, int col) : base(line, col)
        {
            ExpConditional = expCondition;
            ExpressionThen = expThen;
        }

        public IfExpressionAST(ExpressionAst expCondition, ExpressionAst expThen,
                               ExpressionAst expElse, int line, int col)
            : this(expCondition, expThen)
        {
            Line = line;
            Columns = col + 1;
            ExpressionElse = expElse;
        }

        #endregion

     

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitIfExpression(this);
        }
    }
}