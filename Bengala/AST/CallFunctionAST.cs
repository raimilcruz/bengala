﻿#region Usings

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Representa un llamado a funcion
    /// </summary>
    public class CallFunctionAST : ExpressionAst
    {
        #region Fields and Properties

        public string FunctionId { get; private set; }
        public List<ExpressionAst> RealParam { get; private set; }

        #endregion

        #region Constructors

        public CallFunctionAST(string functionId, ArgumentList realParam)
        {
            FunctionId = functionId;
            RealParam = realParam.Arguments;
        }

        public CallFunctionAST(string functionId, List<ExpressionAst> realParam, int line, int col) : base(line, col)
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