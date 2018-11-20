#region Usings

using System.Collections.Generic;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Abstract class to factorize bucle statements. Used to solve the "break" problem.
    /// </summary>
    public abstract class LoopAST : ExpressionAst
    {
        #region Fields and Propertied

        public List<int> BreakPos { get; protected set; }

        public ExpressionAst BodyExpressions { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Asigna lo que es comun para todos. 
        /// </summary>
        /// <param name="bodyExpressions">La expression del cuerpo del ciclo</param>
        /// <param name="line">Linea correspondiente en el codigo</param>
        /// <param name="col">Columna correspondiente en el codigo</param>
        protected LoopAST(ExpressionAst bodyExpressions, int line, int col) : base(line, col)
        {
            BreakPos = new List<int>();
            BodyExpressions = bodyExpressions;
            AlwaysReturn = false;
        }

        #endregion
    }
}