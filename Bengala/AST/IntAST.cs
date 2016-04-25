#region Usings

using System.Collections.Generic;
using System.Reflection.Emit;
using Bengala.AST.CodeGenerationUtils;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Clase que representa a un entero
    /// </summary>
    public class IntAST : ExpressionAST
    {
        #region Fields and Properties

        public int Value { get; set; }

        #endregion

        #region Constructors

        public IntAST(int value)
        {
            Value = value;
            AlwaysReturn = true;
        }

        public IntAST(int value, int line, int col) : base(line, col)
        {
            Value = value;
            AlwaysReturn = true;
        }

        #endregion

        #region Instance Methods

        public override bool CheckSemantic(Scope scope, List<Message> listError)
        {
            ReturnType = TigerType.GetType<IntType>();
            return true;
        }

        public override void GenerateCode(ILCode code)
        {
            if(code.PushOnStack)
                //cargando el entero para la pila
                code.Method.GetILGenerator().Emit(OpCodes.Ldc_I4, Value);
        }

        #endregion

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitIntAst(this);
        }
    }
}