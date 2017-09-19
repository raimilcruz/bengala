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
    /// Representa una expresion for en el lenguaje Tiger:
    /// for :   'for' id ':=' expFrom 'to' expTo 'do' expInstruction.
    /// </summary>
    public class ForExpressionAST : LoopAST
    {
        #region Fields and Properties

        public ExpressionAST ExpressionFrom { get; private set; }
        public ExpressionAST ExpressionTo { get; private set; }
        public string VarId { get; private set; }

        #endregion

        #region Constructors

        public ForExpressionAST(string id, ExpressionAST expFrom, ExpressionAST expTo, ExpressionAST expInstruction,
                                int line, int col)
            : base(expInstruction, line, col)
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