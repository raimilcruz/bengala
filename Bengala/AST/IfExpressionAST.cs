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
    public class IfExpressionAST : ExpressionAST
    {
        #region Fields and Properties

        public ExpressionAST ExpConditional { get; private set; }
        public ExpressionAST ExpressionElse { get; private set; }
        public ExpressionAST ExpressionThen { get; private set; }

        #endregion

        #region Constructors

        public IfExpressionAST(ExpressionAST expCondition, ExpressionAST expThen)
        {
            ExpConditional = expCondition;
            ExpressionThen = expThen;
        }

        public IfExpressionAST(ExpressionAST expCondition, ExpressionAST expThen,
                               ExpressionAST expElse)
            : this(expCondition, expThen)
        {
            ExpressionElse = expElse;
        }

        public IfExpressionAST(ExpressionAST expCondition, ExpressionAST expThen, int line, int col) : base(line, col)
        {
            ExpConditional = expCondition;
            ExpressionThen = expThen;
        }

        public IfExpressionAST(ExpressionAST expCondition, ExpressionAST expThen,
                               ExpressionAST expElse, int line, int col)
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