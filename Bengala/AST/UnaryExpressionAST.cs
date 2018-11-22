namespace Bengala.AST
{
    public abstract class UnaryExpressionAST : ExpressionAst
    {
        public ExpressionAst Expression { get; }

        protected UnaryExpressionAST(ExpressionAst exp) 
        {
            Expression = exp;
        }
    }
}