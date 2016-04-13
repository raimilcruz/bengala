#region Usings

using System.Reflection.Emit;
using Bengala.AST.CodeGenerationUtils;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Representa al operador logico leftExp '&' rightExp
    /// </summary>   
    public class AndExpressionAST : LogicalExpressionAST
    {
        #region Constructors

        public AndExpressionAST(ExpressionAST leftExp, ExpressionAST rightExp, int line, int col)
            : base(leftExp, rightExp, line, col)
        {
            Operator = Operators.And;
        }

        #endregion

        protected override void PushJump(ILCode code, Label end)
        {
            ILGenerator il = code.Method.GetILGenerator();
            il.Emit(OpCodes.Brfalse, end);
        }

        protected override void PushResult(ILCode code)
        {
            ILGenerator il = code.Method.GetILGenerator();
            il.Emit(OpCodes.Ldc_I4_0);
        }
    }
}