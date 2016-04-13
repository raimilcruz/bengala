#region Usings

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
    internal class BreakAST : ExpressionAST
    {
        #region Fields and Properties

        public LoopAST BreakeableLoop { get; private set; }

        #endregion

        #region Constructors

        public BreakAST(int line, int col) : base(line, col)
        {
        }

        #endregion

        #region Instance Methods

        public override bool CheckSemantic(Scope scope, List<Message> listError)
        {
            CurrentScope = scope;

            if (!scope.IsInLoop)
            {
                listError.Add(new ErrorMessage(Message.LoadMessage("Break"), Line, Columns));
                ReturnType = TigerType.GetType<ErrorType>();
                return false;
            }
            BreakeableLoop = scope.ContainerLoop;
            ReturnType = TigerType.GetType<NoType>();
            return true;
        }

        public override void GenerateCode(ILCode code)
        {
            ILGenerator il = code.Method.GetILGenerator();
            //saltando a donde me dice mi padre.
            il.Emit(OpCodes.Br, code.EndCurrentLoop);
        }

        #endregion
    }
}