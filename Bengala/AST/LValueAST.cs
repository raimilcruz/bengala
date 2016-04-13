#region Usings

using Bengala.AST.CodeGenerationUtils;

#endregion

namespace Bengala.AST
{
    public abstract class LValueAST : ExpressionAST
    {
        #region Constructors

        protected LValueAST(int line, int col) : base(line, col)
        {
        }

        #endregion

        /// <summary>
        /// Genera codigo para una asignacion.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="exp"></param>
        public abstract void GenerateCode(ILCode code, ExpressionAST exp);
    }
}