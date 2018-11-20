using System;
using System.Reflection.Emit;
using Bengala.AST;
using Bengala.AST.SemanticsUtils;
using Bengala.Compilation.Helpers;

namespace Bengala.Compilation
{
    public class BinaryExpressionILGenerator
    {
        private BinaryExpressionAST _binaryExpression;
        private ILCodeGenerator _codeGenerator;

        public BinaryExpressionILGenerator(BinaryExpressionAST binExpr, ILCodeGenerator codeGenerator)
        {
            _binaryExpression = binExpr;
            _codeGenerator = codeGenerator;
        }

        public void GenerateCode(ILCode code)
        {
            if (IsLogicalOperator(_binaryExpression.Operator))
            {
                GenerateCodeForLogicalExpression(code);
                return;
            }
            ILGenerator il = code.Method.GetILGenerator();

            //---> me quedo con el valor
            bool pushOnStack = code.PushOnStack;

            // cargar valor exp1 
            code.PushOnStack = true;
            _binaryExpression.LeftExp.Accept(_codeGenerator);
            //cargar valor expr2
            code.PushOnStack = true;
            _binaryExpression.RightExp.Accept(_codeGenerator);
            //aplicar el operador correspondient
            PushResult(code);

            if (!pushOnStack)
                il.Emit(OpCodes.Pop);

            //<--- pongo el valor 
            code.PushOnStack = pushOnStack;
        }

        private bool IsLogicalOperator(Operators op)
        {
            return op == Operators.And || op == Operators.Or;
        }

        private void GenerateCodeForLogicalExpression(ILCode code)
        {
            ILGenerator il = code.Method.GetILGenerator();
            //hacer el corto circuito.
            //---> me quedo con el valor
            bool pushOnStack = code.PushOnStack;

            Label result = il.DefineLabel();
            Label end = il.DefineLabel();

            // cargar valor exp1 
            code.PushOnStack = true;
            _binaryExpression.LeftExp.Accept(_codeGenerator);

            //poner el salto
            PushJump(code, result);

            //cargar valor expr2
            code.PushOnStack = true;
            _binaryExpression.RightExp.Accept(_codeGenerator);

            il.Emit(OpCodes.Br, end);

            il.MarkLabel(result);
            PushResult(code);
            il.MarkLabel(end);


            if (!pushOnStack)
                il.Emit(OpCodes.Pop);

            //<--- pongo el valor il
            code.PushOnStack = pushOnStack;
        }

        /// <summary>
        /// Este metodo se encarga de dejar el valor final en la pila.
        /// </summary>
        /// <param name="code"></param>
        private void PushResult(ILCode code)
        {
            ILGenerator il = code.Method.GetILGenerator();
            switch (_binaryExpression.Operator)
            {
                case Operators.And:
                    il.Emit(OpCodes.Ldc_I4_0);
                    break;
                case Operators.Or:
                    il.Emit(OpCodes.Ldc_I4_1);
                    break;
                default:
                    throw new NotSupportedException("This kind of binary expression is not supported");
            }
        }

        /// <summary>
        /// Este metodo se encarga de saltar a una posicion. Cuando se llama el valor del primer operando esta en la pila
        /// </summary>
        /// <param name="code"></param>
        /// <param name="end"></param>
        private void PushJump(ILCode code, Label end)
        {
            ILGenerator il = code.Method.GetILGenerator();
            switch (_binaryExpression.Operator)
            {
                case Operators.And:
                    il.Emit(OpCodes.Brfalse, end);
                    break;
                case Operators.Or:
                    il.Emit(OpCodes.Brtrue, end);
                    break;
                default:
                    throw new NotSupportedException("This kind of binary expression is not supported");
            }
        }
    }
}