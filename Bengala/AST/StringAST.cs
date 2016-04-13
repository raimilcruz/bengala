#region Usings

using System.Collections.Generic;
using System.Reflection.Emit;
using Bengala.AST.CodeGenerationUtils;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Representa una constante string :   "bengala"   
    /// </summary>
    public class StringAST : ExpressionAST
    {
        #region Fields and Properties

        /// <summary>
        /// Devuelve o asigna el valor del "string"
        /// </summary>
        public string Value { get; set; }

        #endregion

        #region Constructors

        public StringAST(string value, int line, int col) : base(line, col)
        {
            //se eliminan las comillas
            Value = value.Substring(1, value.Length - 2);
            Value = Value.Replace("\\n", "\n");
            Value = Value.Replace("\\t", "\t");
            Value = Value.Replace("\\\\", "\\");
            Value = Value.Replace("\\\"", "\"");
            Value = Value.Replace("\\^", "^");

            AlwaysReturn = true;
        }

        #endregion

        #region Instance Methods

        public override bool CheckSemantic(Scope scope, List<Message> listError)
        {
            ReturnType = TigerType.GetType<StringType>();
            return true;
        }

        public override void GenerateCode(ILCode code)
        {
            ILGenerator il = code.Method.GetILGenerator();
            il.Emit(OpCodes.Ldstr, Value);
            if (!code.PushOnStack)
                il.Emit(OpCodes.Pop);
        }

        #endregion
    }
}