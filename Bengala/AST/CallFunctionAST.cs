#region Usings

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Bengala.AST.CodeGenerationUtils;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Representa un llamado a funcion
    /// </summary>
    public class CallFunctionAST : ExpressionAST
    {
        #region Fields and Properties

        public string FunctionId { get; private set; }
        public List<ExpressionAST> RealParam { get; private set; }

        #endregion

        #region Constructors

        public CallFunctionAST(string functionId, List<ExpressionAST> realParam)
        {
            FunctionId = functionId;
            RealParam = realParam;
        }

        public CallFunctionAST(string functionId, List<ExpressionAST> realParam, int line, int col) : base(line, col)
        {
            FunctionId = functionId;
            RealParam = realParam;
        }

        #endregion

       

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitFunctionInvocation(this);
        }
    }
}