﻿#region Usings

using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Bengala.AST.CodeGenerationUtils;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Representa la instruction :   arrayType '[' SizeExp ']' 'of' initiaExp
    /// </summary>
    public class ArrayInstatiationAST : ExpressionAST
    {
        #region Fields and Properties

        public readonly string ArrayTypeIdentifier;
        public readonly ExpressionAST InitializationExp;
        public readonly ExpressionAST SizeExp;

        #endregion

        #region Constructors

        public ArrayInstatiationAST(string arrayType, ExpressionAST sizeExp, ExpressionAST initializationExp)
        {
            ArrayTypeIdentifier = arrayType;
            this.SizeExp = sizeExp;
            this.InitializationExp = initializationExp;
        }

        public ArrayInstatiationAST(string arrayType, ExpressionAST sizeExp, ExpressionAST initializationExp, int line, int col)
            : base(line, col)
        {
            this.ArrayTypeIdentifier = arrayType;
            this.SizeExp = sizeExp;
            this.InitializationExp = initializationExp;
        }

        #endregion

        #region Instance Methods

        public override bool CheckSemantic(Scope scope, List<Message> listError)
        {
            throw new NotImplementedException("The implementation has been moved to StaticChecker.VisitArrayInstantiation");
        }

        public override void GenerateCode(ILCode code)
        {
            //tengo que declarar un metodo que sea el que inicialize este array. es decir que le asigne el valor de sizeexp;
            TypeInfo tI = CurrentScope.GetTypeInfo(ArrayTypeIdentifier);
            Type array = code.DefinedType[tI.CodeName];


            ILGenerator il = code.Method.GetILGenerator();

            SizeExp.GenerateCode(code);
            il.Emit(OpCodes.Newarr, array.GetElementType());

            //guardar el array .
            LocalBuilder localArray = il.DeclareLocal(array);
            il.Emit(OpCodes.Stloc, localArray.LocalIndex);

            //inicializar correctamente el array 
            // for (i =0 to SizeExp ) array[i] = InitializationExp
            Initialize(code, localArray, array);

            il.Emit(OpCodes.Ldloc, localArray.LocalIndex);


            if (!code.PushOnStack)
                il.Emit(OpCodes.Pop);
        }

        private void Initialize(ILCode code, LocalBuilder localArray, Type arrayType)
        {
            ILGenerator il = code.Method.GetILGenerator();
            //--->
            bool pushOnStack = code.PushOnStack;

            //crear la variable del for.
            LocalBuilder varFor = il.DeclareLocal(typeof (int));

            //generar codigo para cargar el cero.
            il.Emit(OpCodes.Ldc_I4_0);
            // i = 0
            il.Emit(OpCodes.Stloc, varFor.LocalIndex);


            //declaracion de las etiquetas de salto
            Label evaluarCond = il.DefineLabel();
            Label endLoop = il.DefineLabel();

            //condicion
            il.MarkLabel(evaluarCond);
            //cargas la i
            code.PushOnStack = true;
            il.Emit(OpCodes.Ldloc, varFor.LocalIndex);
            //carga la expresion from
            code.PushOnStack = true;
            SizeExp.GenerateCode(code);
            // tengo que ver si i<exp 
            il.Emit(OpCodes.Clt);

            //salto al final si no se cumplio la condicion
            il.Emit(OpCodes.Brfalse, endLoop);

            // a[i] = InitializationExp
            code.PushOnStack = pushOnStack;
            //cargar el array
            il.Emit(OpCodes.Ldloc, localArray.LocalIndex);
            //cargar el indexer
            il.Emit(OpCodes.Ldloc, varFor.LocalIndex);
            //cargar la exp
            InitializationExp.GenerateCode(code);
            //hacer la asignacion
            il.Emit(OpCodes.Stelem, arrayType.IsArray ? arrayType.GetElementType() : arrayType);

            //incrementar el valor de la variable de iteracion
            il.Emit(OpCodes.Ldloc, varFor.LocalIndex);
            il.Emit(OpCodes.Ldc_I4, 1);
            il.Emit(OpCodes.Add);
            il.Emit(OpCodes.Stloc, varFor.LocalIndex);
            //salta a la condicion
            il.Emit(OpCodes.Br, evaluarCond);
            //lo que viene detras del ciclo.

            il.MarkLabel(endLoop);
            //<---
            code.PushOnStack = pushOnStack;
        }

        #endregion

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitArrayInstantiation(this);
        }
    }
}