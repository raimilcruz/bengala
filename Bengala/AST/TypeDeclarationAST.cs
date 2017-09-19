#region Usings

using System;
using System.Collections.Generic;
using Bengala.AST.CodeGenerationUtils;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// clase base de todas las declaraciones de tipos: arrayDec, recordDec, aliasDec
    /// </summary>
    public abstract class TypeDeclarationAST : Declaration
    {
        /// <summary>
        /// Nombre del tipo q se declara
        /// </summary>
        public string TypeId { get; set; }

        #region Constructors

        /// <summary>
        /// </summary>
        /// <param name="typeId"> Nombre del tipo q se declara</param>
        /// <param name="line">Linea correspondiente en el codigo</param>
        /// <param name="col">Columna correspondiente en el codigo</param>
        protected TypeDeclarationAST(string typeId, int line, int col) : base(line, col)
        {
            TypeId = typeId;
        }

        #endregion

    }
}