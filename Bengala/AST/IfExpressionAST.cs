#region Usings

using System.Collections.Generic;
using System.Reflection.Emit;
using Bengala.AST.CodeGenerationUtils;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Representa las expresiones: 'if' expCond 'then' expThen 'else' expElse 
    ///                             'if' expCond 'then' expThen
    /// </summary>
    public class IfExpressionAST : ExpressionAST
    {
        #region Fields and Properties

        public ExpressionAST ExpConditional { get; private set; }
        public ExpressionAST ExpressionElse { get; private set; }
        public ExpressionAST ExpressionThen { get; private set; }

        #endregion

        #region Constructors

        public IfExpressionAST(ExpressionAST expCondition, ExpressionAST expThen)
        {
            ExpConditional = expCondition;
            ExpressionThen = expThen;
        }

        public IfExpressionAST(ExpressionAST expCondition, ExpressionAST expThen,
                               ExpressionAST expElse)
            : this(expCondition, expThen)
        {
            ExpressionElse = expElse;
        }

        public IfExpressionAST(ExpressionAST expCondition, ExpressionAST expThen, int line, int col) : base(line, col)
        {
            ExpConditional = expCondition;
            ExpressionThen = expThen;
        }

        public IfExpressionAST(ExpressionAST expCondition, ExpressionAST expThen,
                               ExpressionAST expElse, int line, int col)
            : this(expCondition, expThen)
        {
            Line = line;
            Columns = col + 1;
            ExpressionElse = expElse;
        }

        #endregion

        #region Instance Methods

        public override bool CheckSemantic(Scope scope, List<Message> listError)
        {
            ExpConditional.CheckSemantic(scope, listError);
            ReturnType = TigerType.GetType<ErrorType>();
            if (ExpConditional.ReturnType != TigerType.GetType<IntType>())
                listError.Add(new ErrorMessage(Message.LoadMessage("IfCond"), Line, Columns));
            else if (ExpressionThen.CheckSemantic(scope, listError))
            {
                if (ExpressionElse != null)
                {
                    ExpressionElse.CheckSemantic(scope, listError);
                    ReturnType = ExpressionElse.ReturnType.CanConvertTo(ExpressionThen.ReturnType)
                                     ?
                                         ExpressionThen.ReturnType
                                     :
                                         ExpressionThen.ReturnType.CanConvertTo(ExpressionElse.ReturnType)
                                             ?
                                                 ExpressionElse.ReturnType
                                             : TigerType.GetType<ErrorType>();
                    if (!(ReturnType is ErrorType))
                        return AlwaysReturn = true;

                    listError.Add(
                        new ErrorMessage(
                            string.Format(Message.LoadMessage("Match"), ExpressionThen.ReturnType,
                                          ExpressionElse.ReturnType), Line, Columns));
                }
                AlwaysReturn = false;
                ReturnType = TigerType.GetType<NoType>();
                return true;
            }
            return false;
        }

        #region Generacion  de Codigo

        public override void GenerateCode(ILCode code)
        {
            //---> quedandome con el valor .
            bool pushOnStack = code.PushOnStack;

            ILGenerator il = code.Method.GetILGenerator();

            //generar el codigo de la condicional
            code.PushOnStack = true;
            ExpConditional.GenerateCode(code);

            //definiendo donde esta la marca del else
            Label parteElse = il.DefineLabel();
            //definiendo el final del if
            Label endIf = il.DefineLabel();

            //si la comparacion fue false salta a la parteElse
            il.Emit(OpCodes.Brfalse, parteElse);

            //sino ejecuta la parte del then
            code.PushOnStack = true;
            ExpressionThen.GenerateCode(code);
            //saltar al final del if
            il.Emit(OpCodes.Br, endIf);
            //marca la donde empieza el else
            il.MarkLabel(parteElse);
            //generar el else si lo tengo
            if (ExpressionElse != null)
            {
                code.PushOnStack = true;
                ExpressionElse.GenerateCode(code);
            }
            //marcando el final de if
            il.MarkLabel(endIf);

            //quitar lo que hay en la pila "si hay algo" y no debo ponerlo
            if (!(ReturnType is NoType) && !pushOnStack)
                il.Emit(OpCodes.Pop);
            //<--- poniendo el valor
            code.PushOnStack = pushOnStack;
        }

        #endregion

        #endregion

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitIfExpression(this);
        }
    }
}