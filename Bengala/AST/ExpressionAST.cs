#region Usings

using System.Collections.Generic;
using Bengala.AST.CodeGenerationUtils;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Representa a una expresion en el lenguaje tiger.Es la clase base de todas las posibles expresiones
    /// </summary>
    public abstract class ExpressionAST
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
        public virtual TigerType ReturnType { get; protected set; }

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

        #region Instance Methods

        /// <summary>
        /// Metodo que permite comprobar la semantica de una expresion
        /// </summary>
        /// <param name="scope">El scope donde se usa esta expresion</param>
        /// <param name="listError">Una lista para poner los errores encontrados</param>
        /// <returns>El retorno no es todavia confiable</returns>
        public abstract bool CheckSemantic(Scope scope, List<Message> listError);

        public abstract void GenerateCode(ILCode code);

        #endregion
    }
}