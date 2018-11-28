﻿namespace Bengala.AST
{
    /// <summary>
    ///     Represents an alias declaration :
    ///     type t = [TypeIdentifierAlreadyDefined]
    /// </summary>
    public class TypeAliasAST : TypeDeclarationAST
    {
        #region  Constructors

        /// <summary>
        /// </summary>
        /// <param name="typeId">nombre del tipo q se crea</param>
        /// <param name="aliasToWho">nombre del tipo al cual se hace el Alias</param>
        /// <param name="line">Linea correspondiente en el codigo</param>
        /// <param name="col">Columna correspondiente en el codigo</param>
        public TypeAliasAST(string typeId, string aliasToWho)
            : base(typeId)
        {
            AliasToWho = aliasToWho;
        }

        #endregion

        #region Field and Properties

        /// <summary>
        ///     Devuelve el typeId del tipo al cual se le hizo el alias.
        /// </summary>
        public string AliasToWho { get; }

        #endregion


        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitAlias(this);
        }
    }
}