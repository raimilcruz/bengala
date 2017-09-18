#region Usings

using System;
using System.Reflection.Emit;
using Bengala.AST.CodeGenerationUtils;

#endregion

namespace Bengala.AST
{
    public abstract class LogicalExpressionAST : BinaryExpressionAST
    {
        protected LogicalExpressionAST(ExpressionAST leftExp, ExpressionAST rightExp,string op, int line, int col)
            : base(leftExp, rightExp,op, line, col)
        {
        }

        public static LogicalExpressionAST GetLogicalExpressionAST(ExpressionAST leftExp, ExpressionAST rightExp,
                                                                   string op, int line, int col)
        {
            switch (op)
            {
                case "|":
                    return new OrExpressionAST(leftExp, rightExp, line, col);
                case "&":
                    return new AndExpressionAST(leftExp, rightExp, line, col);
                default:
                    throw new ArgumentException("no logical exp with op " + op);
            }
        }

        public override void GenerateCode(ILCode code)
        {
            ILGenerator il = code.Method.GetILGenerator();
            //hacer el corto circuito.
            //---> me quedo con el valor
            bool pushOnStack = code.PushOnStack;

            Label result = il.DefineLabel();
            Label end = il.DefineLabel();

            // cargar valor exp1 
            code.PushOnStack = true;
            LeftExp.GenerateCode(code);

            //poner el salto
            PushJump(code, result);

            //cargar valor expr2
            code.PushOnStack = true;
            RightExp.GenerateCode(code);

            il.Emit(OpCodes.Br, end);

            il.MarkLabel(result);
            PushResult(code);
            il.MarkLabel(end);


            if (!pushOnStack)
                il.Emit(OpCodes.Pop);

            //<--- pongo el valor il
            code.PushOnStack = pushOnStack;
        }

        protected override void DoOperation(ILCode code)
        {
            PushResult(code);
        }

        /// <summary>
        /// Este metodo se encarga de dejar el valor final en la pila.
        /// </summary>
        /// <param name="code"></param>
        protected abstract void PushResult(ILCode code);

        /// <summary>
        /// Este metodo se encarga de saltar a una posicion. Cuando se llama el valor del primer operando esta en la pila
        /// </summary>
        /// <param name="code"></param>
        protected abstract void PushJump(ILCode code, Label end);
    }
}