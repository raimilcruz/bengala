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
    public abstract class ExpressionAst : AstNode
    {
        #region Fields and Properties

        /// <summary>
        /// Devuelve true si la expresion en question siempre retorna un valor
        /// </summary>
        public bool AlwaysReturn { get; set; }

        //TODO: Move this property to an Element hierarchy
        /// <summary>
        /// Devuelve el tipo de retorna de la expresion.Esto permite verificar que las asignaciones y operaciones 
        /// tienen sentido
        /// </summary>
        public virtual TigerType ReturnType { get; set; }

        //TODO: Move this property to an Element hierarchy
        /// <summary>
        /// Devuelve una referencia al scope que pasa como parametro en el metodo CheckSemantics
        /// </summary>
        public Scope CurrentScope { get; set; }
       

        #endregion

        #region Constructors

        public ExpressionAst()
        {
            AlwaysReturn = false;
        }

        protected ExpressionAst(int line, int col)
        {
            Line = line;
            Columns = col + 1;
            AlwaysReturn = false;
        }

        #endregion


        public override IEnumerable<AstNode> Children => throw new NotImplementedException();
    }

    public abstract class Declaration : ExpressionAst
    {
        public abstract string Id { get; }

        protected Declaration()
        {
            
        }

        protected Declaration(int line, int col):base(line,col)
        {
            
        }
    }
}