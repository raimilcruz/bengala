﻿#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Representa una sequencia de expresiones del lenguaje Tiger.
    /// sequence expression :   '(' exp (',' exp)* ')'
    /// </summary>
    public class SequenceExpressionAST : ExpressionAst
    {
        #region Fields and Properties

        /// <summary>
        /// Devuelve la lista de expresiones que forman la secuencia
        /// </summary>
        public List<ExpressionAst> ExpressionList { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Lista de expresiones de la secuencia
        /// </summary>
        /// <param name="expList"></param>
        public SequenceExpressionAST(List<ExpressionAst> expList) : base(0, 0)
            //no hay problemas con pasar 0,0 pq esta instruccion no emite errores
        {
            ExpressionList = expList ?? new List<ExpressionAst>();
        }

        #endregion


        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitSequence(this);
        }
    }
}