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
    /// Representa una expresion for en el lenguaje Tiger:
    /// for :   'for' id ':=' expFrom 'to' expTo 'do' expInstruction.
    /// </summary>
    public class ForExpressionAST : LoopAST
    {
        #region Fields and Properties

        public ExpressionAST ExpressionFrom { get; private set; }
        public ExpressionAST ExpressionTo { get; private set; }
        public string VarId { get; private set; }

        #endregion

        #region Constructors

        public ForExpressionAST(string id, ExpressionAST expFrom, ExpressionAST expTo, ExpressionAST expInstruction,
                                int line, int col)
            : base(expInstruction, line, col)
        {
            VarId = id;
            ExpressionFrom = expFrom;
            ExpressionTo = expTo;

            AlwaysReturn = false;
        }

        #endregion

        #region Instance Methods

 
        public override void GenerateCode(ILCode code)
        {
            //--->
            bool pushOnStack = code.PushOnStack;

            ILGenerator il = code.Method.GetILGenerator();


            //crear la variable del for.
            LocalBuilder varFor = il.DeclareLocal(typeof (int));
            string varCodeName = CurrentScope.GetVarNameCode(VarId);
            code.DefinedLocal.Add(varCodeName, varFor);

            code.PushOnStack = true;
            ExpressionFrom.GenerateCode(code);
            il.Emit(OpCodes.Stloc, varFor.LocalIndex);

            //declarar un variable que es el resultado del for
            LocalBuilder result = null;
            if (!(BodyExpressions.ReturnType is NoType))
            {
                string typeCodeName = CurrentScope.GetTypeInfo(BodyExpressions.ReturnType.TypeID).CodeName;
                result = il.DeclareLocal(code.DefinedType[typeCodeName]);
            }

            //declaracion de las etiquetas de salto
            Label evaluarCond = il.DefineLabel();

            //--->
            Label loopAboveEnd = code.EndCurrentLoop;

            code.EndCurrentLoop = il.DefineLabel();

            //condicion
            il.MarkLabel(evaluarCond);
            //cargas la i
            code.PushOnStack = true;
            il.Emit(OpCodes.Ldloc, varFor.LocalIndex);
            //carga la expresion from
            code.PushOnStack = true;
            ExpressionTo.GenerateCode(code);
            // tengo que ver si i<=exp ,pero lo que hago es !(i>exp)
            il.Emit(OpCodes.Cgt);

            //salto al final si no se cumplio la condicion
            il.Emit(OpCodes.Brtrue, code.EndCurrentLoop);

            //body
            code.PushOnStack = pushOnStack;
            BodyExpressions.GenerateCode(code);

            if (!(BodyExpressions.ReturnType is NoType) && pushOnStack)
            {
                il.Emit(OpCodes.Stloc, result.LocalIndex);
            }

            //incrementar el valor de la variable de iteracion
            il.Emit(OpCodes.Ldloc, varFor.LocalIndex);
            il.Emit(OpCodes.Ldc_I4, 1);
            il.Emit(OpCodes.Add);
            il.Emit(OpCodes.Stloc, varFor.LocalIndex);
            //salta a la condicion
            il.Emit(OpCodes.Br, evaluarCond);
            //lo que viene detras del ciclo.

            il.MarkLabel(code.EndCurrentLoop);
            //<--- reponiendo la marca del posible ciclo sobre mi.
            code.EndCurrentLoop = loopAboveEnd;

            if (!(BodyExpressions.ReturnType is NoType) && pushOnStack)
            {
                il.Emit(OpCodes.Ldloc, result.LocalIndex);
            }

            //<---
            code.PushOnStack = pushOnStack;
        }

        #endregion

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitForExpression(this);
        }
    }
}