#region Usings

using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Bengala.AST.CodeGenerationUtils;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    public class NegExpressionAST : UnaryExpressionAST
    {
        public NegExpressionAST(ExpressionAST exp, int line, int col)
            : base(exp, line, col)
        {
            AlwaysReturn = true;
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

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitNegExpression(this);
        }
    }
}