#region Usings

#endregion

namespace Bengala.AST
{
    public abstract class LHSExpressionAST : ExpressionAst
    {
        #region Constructors

        protected LHSExpressionAST(int line, int col) : base(line, col)
        {
        }

        #endregion

    }
}