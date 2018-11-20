#region Usings

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Representa la declaracion de una variable en el lenguaje Tiger/
    ///Ejemplos
    ///   var i  := 8
    /// </summary>
    public class VarDeclarationAST : Declaration
    {
        #region Fields and Properties

        /// <summary>
        /// Devuelve la expresion que representa al valor de la variable
        /// </summary>
        public ExpressionAST ExpressionValue { get; private set; }

        /// <summary>
        /// El identificador de la variable
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// El nombre del tipo de la variable
        /// </summary>
        public string TypeId { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// </summary>
        /// <param name="id">El id de la variable</param>
        /// <param name="typeId">El nombre del tipo de la variable</param>
        /// <param name="expValue">La expresion que define el valor de la variable</param>
        /// <param name="line">Linea correspondiente en el codigo</param>
        /// <param name="col">Columna correspondiente en el codigo</param>
        public VarDeclarationAST(string id, string typeId, ExpressionAST expValue, int line, int col) : base(line, col)
        {
            TypeId = typeId;
            Id = id;
            ExpressionValue = expValue;
        }

        /// <summary>
        /// Cdo el tipo es inferido 
        /// </summary>
        /// <param name="id">El id de la variable</param>
        /// <param name="expValue">La expresion que define el valor de la variable</param>
        public VarDeclarationAST(string id, ExpressionAST expValue)
        {
            Id = id;
            ExpressionValue = expValue;
        }

        #endregion

  

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitVarDeclaration(this);
        }
    }
}