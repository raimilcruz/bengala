#region Usings

using System.Collections.Generic;
using System.Reflection.Emit;
using Bengala.AST.CodeGenerationUtils;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    internal class NegExpressionAST : UnaryExpressionAST
    {
        public NegExpressionAST(ExpressionAST exp, int line, int col)
            : base(exp, line, col)
        {
            AlwaysReturn = true;
        }

        public override bool CheckSemantic(Scope scope, List<Message> listError)
        {
            CurrentScope = scope;
            if (Expression.CheckSemantic(scope, listError))
            {
                if (Expression.ReturnType == TigerType.GetType<IntType>())
                {
                    ReturnType = TigerType.GetType<IntType>();
                    return true;
                }
                listError.Add(new ErrorMessage(string.Format(Message.LoadMessage("NegExp"), Expression.ReturnType), Line,
                                               Columns));
            }
            ReturnType = TigerType.GetType<ErrorType>();
            return false;
        }

        public override void GenerateCode(ILCode code)
        {
            bool temp = code.PushOnStack;
            code.PushOnStack = true;
            ILGenerator il = code.Method.GetILGenerator();
            //cargando el entero para la pila
            il.Emit(OpCodes.Ldc_I4, -1);

            Expression.GenerateCode(code);

            il.Emit(OpCodes.Mul);

            code.PushOnStack = temp;

            if (!code.PushOnStack)
                il.Emit(OpCodes.Pop);
        }
    }
}