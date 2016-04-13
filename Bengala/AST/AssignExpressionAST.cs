#region Usings

using System.Collections.Generic;
using Bengala.AST.CodeGenerationUtils;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Representa una asignacion en el lenguaje tiger: lvalue ':=' exp
    /// </summary>
    public class AssignExpressionAST : ExpressionAST
    {
        #region Fields and Properties

        public LValueAST LeftExpression { get; private set; }
        public ExpressionAST RightExpression { get; private set; }

        #endregion

        #region Constructors

        public AssignExpressionAST(LValueAST leftExp, ExpressionAST rightExp)
        {
            LeftExpression = leftExp;
            RightExpression = rightExp;
            ReturnType = TigerType.GetType<NoType>();
        }

        public AssignExpressionAST(LValueAST leftExp, ExpressionAST rightExp, int line, int col)
            : base(line, col)
        {
            LeftExpression = leftExp;
            RightExpression = rightExp;
            ReturnType = TigerType.GetType<NoType>();
        }

        #endregion

        #region Instance Methods

        public override bool CheckSemantic(Scope scope, List<Message> listError)
        {
            CurrentScope = scope;

            ReturnType = TigerType.GetType<ErrorType>();
            if (!LeftExpression.CheckSemantic(scope, listError) || !RightExpression.CheckSemantic(scope, listError))
                return false;
            if (RightExpression.ReturnType.CanConvertTo(LeftExpression.ReturnType))
            {
                ReturnType = TigerType.GetType<NoType>();
                return true;
            }
            listError.Add(
                new ErrorMessage(
                    string.Format(Message.LoadMessage("Match"), LeftExpression.ReturnType, RightExpression.ReturnType),
                    Line, Columns));
            return false;
        }

        public override void GenerateCode(ILCode code)
        {
            //generar la asignacion.
            LeftExpression.GenerateCode(code, RightExpression);
        }

        #endregion
    }
}