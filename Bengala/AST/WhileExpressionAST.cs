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
    /// Representa una instruccion while de tiger
    /// </summary>
    public class WhileExpressionAST : LoopAST
    {
        #region Fields and Properties

        public ExpressionAST ExpressionConditional { get; private set; }

        #endregion

        #region Constructors

        public WhileExpressionAST(ExpressionAST expCondition, ExpressionAST expInstruction, int line, int col) :
            base(expInstruction, line, col)
        {
            ExpressionConditional = expCondition;
        }

        #endregion

        #region Instance Method

   

        #region Generacion de Codigo

        public override void GenerateCode(ILCode code)
        {
            ILGenerator il = code.Method.GetILGenerator();

            //declaracion de las etiquetas de salto
            Label evaluarCond = il.DefineLabel();
            Label bodyInstr = il.DefineLabel();

            //--->
            Label loopAboveEnd = code.EndCurrentLoop;
            code.EndCurrentLoop = il.DefineLabel();
            //salto a la comparacion
            il.Emit(OpCodes.Br, evaluarCond);
            //body
            il.MarkLabel(bodyInstr);
            code.PushOnStack = false;
            BodyExpressions.GenerateCode(code);
            //condicion
            il.MarkLabel(evaluarCond);
            code.PushOnStack = true;
            ExpressionConditional.GenerateCode(code);
            il.Emit(OpCodes.Brtrue, bodyInstr);
            //lo que viene detras del while.
            il.MarkLabel(code.EndCurrentLoop);

            //<--- reponiendo la marca del posible ciclo sobre mi.
            code.EndCurrentLoop = loopAboveEnd;
        }

        #endregion

        #endregion

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitWhileExpression(this);
        }
    }
}