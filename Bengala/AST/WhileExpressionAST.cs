#region Usings

using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Bengala.AST.CodeGenerationUtils;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Representa una instruccion while de tiger
    /// </summary>
    public class WhileExpressionAST : LoopAST
    {
        #region Fields and Properties

        public ExpressionAST ExpressionConditional { get; private set; }

        #endregion

        #region Constructors

        public WhileExpressionAST(ExpressionAST expCondition, ExpressionAST expInstruction, int line, int col) :
            base(expInstruction, line, col)
        {
            ExpressionConditional = expCondition;
        }

        #endregion


        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitWhileExpression(this);
        }
    }
}