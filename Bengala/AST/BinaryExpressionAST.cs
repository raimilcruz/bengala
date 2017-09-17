#region Usings

using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Bengala.AST.CodeGenerationUtils;
using Bengala.AST.SemanticsUtils;
using Bengala.AST.Utils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Esta clase representa a las expresiones binarias.
    /// </summary>
    public class BinaryExpressionAST : ExpressionAST
    {
        #region Fields and Properties

        public ExpressionAST LeftExp { get; set; }

        public ExpressionAST RightExp { get; set; }

        public Operators Operator { get; protected set; }

        #endregion

        #region Constructors

        protected BinaryExpressionAST(ExpressionAST leftExp, ExpressionAST rightExp)
        {
            LeftExp = leftExp;
            RightExp = rightExp;
            AlwaysReturn = true;
        }

        protected BinaryExpressionAST(ExpressionAST leftExp, ExpressionAST rightExp, int line, int col)
            : base(line, col)
        {
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
        public static BinaryExpressionAST GetBinaryExpressionAST(ExpressionAST e1, ExpressionAST e2, string op, int line,
                                                                 int col)
        {
            var b = new BinaryExpressionAST(e1, e2, line, col);
            switch (op)
            {
                case "<=":
                    b.Operator = Operators.LessEqual;
                    break;
                case "<":
                    b.Operator = Operators.LessThan;
                    break;
                case ">":
                    b.Operator = Operators.GreaterThan;
                    break;
                case ">=":
                    b.Operator = Operators.GreaterEqual;
                    break;
                case "=":
                    b.Operator = Operators.Equal;
                    break;
                case "<>":
                    b.Operator = Operators.NotEqual;
                    break;
                case "-":
                    b.Operator = Operators.Minus;
                    break;
                case "+":
                    b.Operator = Operators.Plus;
                    break;
                case "/":
                    b.Operator = Operators.Div;
                    break;
                case "*":
                    b.Operator = Operators.Prod;
                    break;
                case "%":
                    b.Operator = Operators.Mod;
                    break;
                case "&":
                    return new AndExpressionAST(e1, e2, line, col);
                case "|":
                    return new AndExpressionAST(e1, e2, line, col);
                default:
                    throw new ArgumentException("Operacion no conocida");
            }
            return b;
        }

        #endregion

        #region Instance Methods

        public override bool CheckSemantic(Scope scope, List<Message> listError)
        {
            throw new NotImplementedException("Implementation has been move to StaticChecker.VisitBinaryExpression as part of refactoring");
        }
     

        /// <summary>
        /// Este metodo es redefinido en cada hijo para hacer cada uno la operacion correspondiente con los  operandos
        /// </summary>
        protected virtual void DoOperation(ILCode code)
        {
            GenOperation g = LeftExp.ReturnType.GetOperationGenerator(RightExp.ReturnType, Operator) ??
                             RightExp.ReturnType.GetOperationGenerator(LeftExp.ReturnType, Operator);
            if (g != null)
                g(code);
            else
                throw new InvalidOperationException("The operation is not supported");
        }

        public override void GenerateCode(ILCode code)
        {
            ILGenerator il = code.Method.GetILGenerator();

            //---> me quedo con el valor
            bool pushOnStack = code.PushOnStack;

            // cargar valor exp1 
            code.PushOnStack = true;
            LeftExp.GenerateCode(code);
            //cargar valor expr2
            code.PushOnStack = true;
            RightExp.GenerateCode(code);
            //aplicar el operador correspondient      
            DoOperation(code);

            if (!pushOnStack)
                il.Emit(OpCodes.Pop);

            //<--- pongo el valor 
            code.PushOnStack = pushOnStack;
        }

        #endregion

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitBinaryExpression(this);
        }
    }
}