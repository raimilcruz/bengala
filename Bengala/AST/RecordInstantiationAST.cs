#region Usings

using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Bengala.AST.CodeGenerationUtils;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Representa la instan
    /// </summary>
    public class RecordInstantiationAST : ExpressionAST
    {
        #region Fields and Properties

        public string Id { get; private set; }
        public List<KeyValuePair<string, ExpressionAST>> ExpressionValue { get; private set; }

        #endregion

        #region Constructors

        public RecordInstantiationAST(string id, List<KeyValuePair<string, ExpressionAST>> exp, int line, int col)
            : base(line, col)
        {
            Id = id;
            ExpressionValue = exp ?? new List<KeyValuePair<string, ExpressionAST>>();
        }

        #endregion



        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitRecordInstantiation(this);
        }
    }
}