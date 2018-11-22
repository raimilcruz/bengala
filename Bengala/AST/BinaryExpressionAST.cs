#region Usings

using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Bengala.AST.SemanticsUtils;
using Bengala.AST.Utils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Esta clase representa a las expresiones binarias.
    /// </summary>
    public class BinaryExpressionAst : ExpressionAst
    {
        #region Fields and Properties

        public ExpressionAst LeftExp { get; set; }

        public ExpressionAst RightExp { get; set; }

        public Operators Operator { get; protected set; }

        #endregion

        #region Constructors

        

        public BinaryExpressionAst(ExpressionAst leftExp, ExpressionAst rightExp, string op)
        {
            Operator = GetOperator(op);
            LeftExp = leftExp;
            RightExp = rightExp;
            AlwaysReturn = true;
        }

        #endregion

        #region Static Methods

        //TODO : Move to the parser namespace
        /// <summary>
        /// Metodo para crear alguna de la expresiones que siempre tienen todos los lenguajes
        /// </summary>
        /// <param name="e1"></param>
        /// <param name="e2"></param>
        /// <param name="op"></param>
        /// <param name="line"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        private static Operators GetOperator(string op)
        {
            switch (op)
            {
                case "<=":
                    return Operators.LessEqual;
                case "<":
                    return Operators.LessThan;
                case ">":
                    return Operators.GreaterThan;
                case ">=":
                    return Operators.GreaterEqual;
                case "=":
                    return Operators.Equal;
                case "<>":
                    return Operators.NotEqual;
                case "-":
                    return Operators.Minus;
                case "+":
                    return Operators.Plus;
                case "/":
                    return Operators.Div;
                case "*":
                    return Operators.Prod;
                case "%":
                    return Operators.Mod;
                case "&":
                    return Operators.And;
                case "|":
                    return Operators.Or;
                default:
                    throw new ArgumentException("Unknown operator");
            }
        }

        #endregion


        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitBinaryExpression(this);
        }
    }
}