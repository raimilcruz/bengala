#region Usings

using System.Collections.Generic;
using System.Reflection.Emit;
using Bengala.AST.CodeGenerationUtils;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Esta clase representa al valor nil o null de los lenguajes de programacion
    /// </summary>
    public class NilAST : ExpressionAST
    {
        #region Constructors

        public NilAST(int line, int col) : base(line, col)
        {
            AlwaysReturn = true;
        }

        #endregion

        public override bool CheckSemantic(Scope scope, List<Message> listError)
        {
            ReturnType = TigerType.GetType<NilType>();
            return true;
        }

        public override void GenerateCode(ILCode code)
        {
            ILGenerator il = code.Method.GetILGenerator();
            //cargando null para la pila
            il.Emit(OpCodes.Ldnull);
            if (!code.PushOnStack)
                il.Emit(OpCodes.Pop);
        }
    }
}