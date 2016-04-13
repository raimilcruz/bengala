namespace Bengala.AST
{
    public abstract class UnaryExpressionAST : ExpressionAST
    {
        public ExpressionAST Expression { get; private set; }

        #region Constructors

        public UnaryExpressionAST(ExpressionAST exp, int line, int col) : base(line, col)
        {
            Expression = exp;
        }

        #endregion
    }
}