using System.Collections;
using System.Collections.Generic;

namespace Bengala.AST
{
    public class FormalParameterList : AstNode, IEnumerable<FormalParameter>
    {
        public List<FormalParameter> Parameters { get; private set; }

        public FormalParameterList(List<FormalParameter> parameters)
        {
            Parameters = parameters;
        }

        public FormalParameterList() : this(new List<FormalParameter>())
        {

        }

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitFormalParameterList(this);
        }

        public override IEnumerable<AstNode> Children => Parameters;
        public IEnumerator<FormalParameter> GetEnumerator()
        {
            return Parameters.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class FormalParameter : AstNode
    {
        public string Name { get; private set; }
        public string TypeIdentifier { get; private set; }

        public FormalParameter(string name, string typeIdentifier)
        {
            Name = name;
            TypeIdentifier = typeIdentifier;
        }

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitFormalParameter(this);
        }

        public override IEnumerable<AstNode> Children => new AstNode[] { };
    }
}
