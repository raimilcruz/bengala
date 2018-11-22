#region Usings

using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Representa el acceso a un array: exp'['indexer']'
    /// </summary>
    public class ArrayAccessAST : LHSExpressionAST
    {
        #region Fields and Properties

        private readonly ExpressionAst array;

        private readonly ExpressionAst indexer;

        public ExpressionAst Array
        {
            get { return array; }
        }

        public ExpressionAst Indexer
        {
            get { return indexer; }
        }

        #endregion

        #region Constructors

        public ArrayAccessAST(ExpressionAst array, ExpressionAst indexer)
        {
            this.array = array;
            this.indexer = indexer;
        }

        #endregion

     

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitArrayAccess(this);
        }
    }
}