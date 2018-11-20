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

        private readonly ExpressionAST array;

        private readonly ExpressionAST indexer;

        public ExpressionAST Array
        {
            get { return array; }
        }

        public ExpressionAST Indexer
        {
            get { return indexer; }
        }

        #endregion

        #region Constructors

        public ArrayAccessAST(ExpressionAST array, ExpressionAST indexer, int line, int col) : base(line, col)
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