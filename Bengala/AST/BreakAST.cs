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
    /// Representa la instruccion "break".
    /// </summary>
    public class BreakAST : ExpressionAST
    {
        #region Fields and Properties

        public LoopAST BreakeableLoop { get; set; }

        #endregion

        #region Constructors

        public BreakAST(int line, int col) : base(line, col)
        {
        }

        #endregion

        #region Instance Methods


        public override void GenerateCode(ILCode code)
        {
            ILGenerator il = code.Method.GetILGenerator();
            //saltando a donde me dice mi padre.
            il.Emit(OpCodes.Br, code.EndCurrentLoop);
        }

        #endregion

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitBreakStatement(this);
        }
    }
}