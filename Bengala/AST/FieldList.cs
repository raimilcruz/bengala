using System.Collections.Generic;

namespace Bengala.AST
{
    public class FieldInstanceList : AstNode
    {
        public List<FieldInstance> Fields { get; }

        public FieldInstanceList():this(new List<FieldInstance>())
        {
        }

        public FieldInstanceList(List<FieldInstance> fields)
        {
            Fields = fields;
        }

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitFieldInstanceList(this);
        }

        public override IEnumerable<AstNode> Children => Fields;
    }

    public class FieldInstance:AstNode
    {
        public string Name { get; set; }
        public ExpressionAst Value { get; set; }

        public FieldInstance(string name, ExpressionAst value)
        {
            Name = name;
            Value = Value;
        }

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitFieldInstance(this);
        }

        public override IEnumerable<AstNode> Children => new []{Value};
    }
}
