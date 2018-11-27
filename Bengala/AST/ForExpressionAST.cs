#region Usings

using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Representa una expresion for en el lenguaje Tiger:
    /// for :   'for' id ':=' expFrom 'to' expTo 'do' expInstruction.
    /// </summary>
    public class ForExpressionAST : LoopAST
    {
        #region Fields and Properties

        public ExpressionAst ExpressionFrom { get; private set; }
        public ExpressionAst ExpressionTo { get; private set; }
        public string VarId { get; private set; }

        #endregion

        #region Constructors

        public ForExpressionAST(string id, ExpressionAst expFrom, ExpressionAst expTo, ExpressionAst expInstruction)
            : base(expInstruction)
        {
            VarId = id;
            ExpressionFrom = expFrom;
            ExpressionTo = expTo;

            AlwaysReturn = false;
        }

        #endregion

   

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitForExpression(this);
        }
    }
}