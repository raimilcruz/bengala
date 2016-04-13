#region Usings

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

        public override bool CheckSemantic(Scope scope, List<Message> listError)
        {
            ExpressionConditional.CheckSemantic(scope, listError);
            ReturnType = TigerType.GetType<ErrorType>();
            if (ExpressionConditional.ReturnType != TigerType.GetType<IntType>())
                listError.Add(new ErrorMessage(Message.LoadMessage("IfCond"), Line, Columns));
            else
            {
                //guardo, si hay, el ciclo previo
                LoopAST prevLoop = scope.ContainerLoop;
                scope.ContainerLoop = this;
                if (BodyExpressions.CheckSemantic(scope, listError))
                {
                    //repongo el ciclo q habia
                    scope.ContainerLoop = prevLoop;
                    ReturnType = TigerType.GetType<NoType>();
                    return true;
                }
            }
            return false;
        }

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
    }
}