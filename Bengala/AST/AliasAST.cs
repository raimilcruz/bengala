#region Usings

using System;
using System.Collections.Generic;
using Bengala.AST.CodeGenerationUtils;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Represents an alias declaration :
    /// type t = [TypeIdentifierAlreadyDefined]
    /// </summary>
    public class AliasAST : TypeDeclarationAST
    {
        #region Field and Properties

        /// <summary>
        /// Devuelve el typeId del tipo al cual se le hizo el alias.
        /// </summary>
        public string AliasToWho { get; private set; }

        #endregion

        #region  Constructors

        /// <summary>
        /// </summary>
        /// <param name="typeId">nombre del tipo q se crea</param>
        /// <param name="aliasToWho">nombre del tipo al cual se hace el Alias</param>
        /// <param name="line">Linea correspondiente en el codigo</param>
        /// <param name="col">Columna correspondiente en el codigo</param>
        public AliasAST(string typeId, string aliasToWho, int line, int col)
            : base(typeId, line, col)
        {
            AliasToWho = aliasToWho;
        }

        #endregion

        #region InstanceMethods

     
        public override void GenerateCode(ILCode code)
        {
            //el alias no genera codigo
        }

        #endregion

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitAlias(this);
        }
    }

}