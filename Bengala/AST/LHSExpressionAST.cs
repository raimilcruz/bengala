#region Usings

using Bengala.AST.CodeGenerationUtils;

#endregion

namespace Bengala.AST
{
    public abstract class LHSExpressionAST : ExpressionAST
    {
        #region Constructors

        protected LHSExpressionAST(int line, int col) : base(line, col)
        {
        }

        #endregion

    }
}