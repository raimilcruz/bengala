#region Usings

using System;
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
    public class NilLiteral : ExpressionAST
    {
        #region Constructors

        public NilLiteral(int line, int col) : base(line, col)
        {
            AlwaysReturn = true;
        }

        #endregion

     

        public override void GenerateCode(ILCode code)
        {
            ILGenerator il = code.Method.GetILGenerator();
            //cargando null para la pila
            il.Emit(OpCodes.Ldnull);
            if (!code.PushOnStack)
                il.Emit(OpCodes.Pop);
        }

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitNilLiteral(this);
        }
    }
}