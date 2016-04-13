#region Usings

using System.Reflection.Emit;
using Bengala.AST.CodeGenerationUtils;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Representa al operador logico leftExp | rightExp
    /// </summary>
    public class OrExpressionAST : LogicalExpressionAST
    {
        #region Constructors

        public OrExpressionAST(ExpressionAST leftExp, ExpressionAST rightExp, int line, int col)
            : base(leftExp, rightExp, line, col)
        {
            Operator = Operators.Or;
        }

        #endregion

        protected override void PushJump(ILCode code, Label end)
        {
            ILGenerator il = code.Method.GetILGenerator();
            il.Emit(OpCodes.Brtrue, end);
        }

        protected override void PushResult(ILCode code)
        {
            ILGenerator il = code.Method.GetILGenerator();
            il.Emit(OpCodes.Ldc_I4_1);
        }
    }
}