#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Representa la instan
    /// </summary>
    public class RecordInstantiationAST : ExpressionAst
    {
        #region Fields and Properties

        public string Id { get; private set; }
        public List<KeyValuePair<string, ExpressionAst>> ExpressionValue { get; private set; }
        public FieldInstanceList Fields { get; private set; }

        #endregion

        #region Constructors

        public RecordInstantiationAST(string id, FieldInstanceList fields)
        {
            Id = id;
            Fields = fields ?? new FieldInstanceList();
            ExpressionValue = Fields.Fields
                .Select(x=> new KeyValuePair<string,ExpressionAst>(x.Name,x.Value))
                .ToList();
        }

        #endregion



        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitRecordInstantiation(this);
        }
    }
}