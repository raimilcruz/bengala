using System.Collections.Generic;

namespace Bengala.AST
{
    public class ArgumentList :AstNode
    {
        public List<ExpressionAst> Arguments { get; }

        public ArgumentList(List<ExpressionAst> arguments)
        {
            Arguments = arguments;
        }

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitArgumentList(this);
        }
    }
}