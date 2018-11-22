#region Usings

using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Representa una constante string :   "bengala"   
    /// </summary>
    public class StringLiteral : ExpressionAst
    {
        #region Fields and Properties

        /// <summary>
        /// Devuelve o asigna el valor del "string"
        /// </summary>
        public string Value { get; set; }

        #endregion

        #region Constructors

        public StringLiteral(string value) 
        {
            //se eliminan las comillas
            //TODO: We do not need to assume that value start and end with qoutes
            Value = RemoveBeginAndEndQuote(value);
            Value = Value.Replace("\\n", "\n");
            Value = Value.Replace("\\t", "\t");
            Value = Value.Replace("\\\\", "\\");
            Value = Value.Replace("\\\"", "\"");
            Value = Value.Replace("\\^", "^");

            AlwaysReturn = true;
        }
        string RemoveBeginAndEndQuote(string s) {
            var result = s;
            if (result.Length > 0 && result[0] == '\"')
                result = result.Substring(1, result.Length - 1);
            if(result.Length>0 && result[result.Length-1] == '\"')
                result = result.Substring(0, s.Length - 1);
            return result;
        }

        #endregion

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitStringLiteral(this);
        }
    }
}