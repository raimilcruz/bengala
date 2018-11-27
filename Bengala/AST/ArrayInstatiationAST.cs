#region Usings

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Representa la instruction :   arrayType '[' SizeExp ']' 'of' initiaExp
    /// </summary>
    public class ArrayInstatiationAST : ExpressionAst
    {
        #region Fields and Properties

        public readonly string ArrayTypeIdentifier;
        public readonly ExpressionAst InitializationExp;
        public readonly ExpressionAst SizeExp;

        #endregion

        #region Constructors

        public ArrayInstatiationAST(string arrayType, ExpressionAst sizeExp, ExpressionAst initializationExp)
        {
            ArrayTypeIdentifier = arrayType;
            SizeExp = sizeExp;
            InitializationExp = initializationExp;
        }
        #endregion


        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitArrayInstantiation(this);
        }
    }
}