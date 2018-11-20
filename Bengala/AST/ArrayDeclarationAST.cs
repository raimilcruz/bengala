#region Usings

using System;
using System.Collections.Generic;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Representa la declaracion de un array 'type' typeId 'array of' type
    /// </summary>
    public class ArrayDeclarationAST : TypeDeclarationAST
    {
        #region Fields and Properties

        public string BaseTypeID { get; set; }

        #endregion

        #region Constructors

        public ArrayDeclarationAST(string typeId, string baseTypeID, int line, int col)
            : base(typeId, line, col)
        {
            BaseTypeID = baseTypeID;
            TypeId = typeId;
        }

        #endregion


        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitArrayDeclaration(this);
        }
    }
}