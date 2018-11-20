namespace Bengala.AST
{
    public abstract class UnaryExpressionAST : ExpressionAst
    {
        public ExpressionAst Expression { get; private set; }

        #region Constructors

        public UnaryExpressionAST(ExpressionAst exp, int line, int col) : base(line, col)
        {
            Expression = exp;
        }

        #endregion
    }
}