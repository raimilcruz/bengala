#region Usings

using System;
using System.Collections.Generic;
using Bengala.AST.SemanticsUtils;
using Bengala.AST.Types;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Representa a una expresion en el lenguaje tiger.Es la clase base de todas las posibles expresiones
    /// </summary>
    public abstract class ExpressionAST : AstNode
    {
        #region Fields and Properties

        /// <summary>
        /// Devuelve true si la expresion en question siempre retorna un valor
        /// </summary>
        public bool AlwaysReturn { get; set; }

        /// <summary>
        /// Devuelve el tipo de retorna de la expresion.Esto permite verificar que las asignaciones y operaciones 
        /// tienen sentido
        /// </summary>
        public virtual TigerType ReturnType { get; set; }

        /// <summary>
        /// Devuelve una referencia al scope que pasa como parametro en el metodo CheckSemantics
        /// </summary>
        public Scope CurrentScope { get; set; }

        /// <summary>
        /// Devuelve la posicion de la expresion en el fichero de codigo
        /// </summary>
        public int Line { get; set; }

        /// <summary>
        /// Devuelve la columna donde la comienza la expresion en el fichero de codigo
        /// </summary>
        public int Columns { get; set; }

        #endregion

        #region Constructors

        public ExpressionAST()
        {
            AlwaysReturn = false;
        }

        protected ExpressionAST(int line, int col)
        {
            Line = line;
            Columns = col + 1;
            AlwaysReturn = false;
        }

        #endregion



    }

    public abstract class Declaration : ExpressionAST
    {
        protected Declaration()
        {
            
        }

        protected Declaration(int line, int col):base(line,col)
        {
            
        }
    }
}