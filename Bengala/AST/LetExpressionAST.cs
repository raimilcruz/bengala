#region Usings

using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Bengala.AST.CodeGenerationUtils;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    public class LetExpressionAST : ExpressionAST
    {
        #region Fields and Properties

        /// <summary>
        /// lista de declaraciones del let
        /// </summary>
        public List<Declaration> DeclarationList;

        /// <summary>
        /// cuerpo del let
        /// </summary>
        public SequenceExpressionAST SequenceExpressionList { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// </summary>
        /// <param name="declarationList">lista de declaraciones del let</param>
        /// <param name="seqExpList">cuerpo del let</param>
        public LetExpressionAST(List<Declaration> declarationList, SequenceExpressionAST seqExpList) : base(0, 0)
            //no problemas con pasar 0,0 let no produce errores propios
        {
            SequenceExpressionList = seqExpList;
            DeclarationList = declarationList ?? new List<Declaration>();
        }

        #endregion

     

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitLetExpression(this);
        }
    }
}