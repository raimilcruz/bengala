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
    /// Representa el acceso a un array: exp'['indexer']'
    /// </summary>
    public class ArrayAccessAST : LValueAST
    {
        #region Fields and Properties

        private readonly ExpressionAST array;

        private readonly ExpressionAST indexer;

        public ExpressionAST Array
        {
            get { return array; }
        }

        public ExpressionAST Indexer
        {
            get { return indexer; }
        }

        #endregion

        #region Constructors

        public ArrayAccessAST(ExpressionAST array, ExpressionAST indexer, int line, int col) : base(line, col)
        {
            this.array = array;
            this.indexer = indexer;
        }

        #endregion

        #region Instance Methods

        #region Check Semantics

        public override bool CheckSemantic(Scope scope, List<Message> listError)
        {
           throw new NotImplementedException("The implementation has been moved to StatiChecker.VisitArrayAccess");
        }

        #endregion

        #region Code Generation

        public override void GenerateCode(ILCode code)
        {
            //--->
            bool pushOnStack = code.PushOnStack;

            ILGenerator il = code.Method.GetILGenerator();
            //cargar el array
            if (pushOnStack)
            {
                code.PushOnStack = true;
                array.GenerateCode(code);
                //cargar el indexer
                code.PushOnStack = true;
                indexer.GenerateCode(code);

                //aca tengo que pedir el tipo del array , y luego el type asociado a el.
                string typeCodeName = CurrentScope.GetTypeInfo(array.ReturnType.TypeID).CodeName;
                Type t = code.DefinedType[typeCodeName];
                il.Emit(OpCodes.Ldelem, t.IsArray ? t.GetElementType() : t);
            }
            //<---
            code.PushOnStack = pushOnStack;
        }

        /// <summary>
        /// Genera codigo para la asignacion a un array
        /// </summary>
        /// <param name="code"></param>
        /// <param name="exp"></param>
        public override void GenerateCode(ILCode code, ExpressionAST exp)
        {
            //--->
            bool pushOnStack = code.PushOnStack;

            //cargo primero el array.
            code.PushOnStack = true;
            array.GenerateCode(code);
            //cargo el indice al cual voy a acceder
            code.PushOnStack = true;
            Indexer.GenerateCode(code);
            //cargo el valor que le voy a asignar
            code.PushOnStack = true;
            //generar el codigo de la expresion que quiero asignar.
            exp.GenerateCode(code);

            ILGenerator il = code.Method.GetILGenerator();

            //aca tengo que pedir el tipo del array , y luego el type asociado a el.
            string typeCodeName = CurrentScope.GetTypeInfo(array.ReturnType.TypeID).CodeName;
            Type t = code.DefinedType[typeCodeName];
            il.Emit(OpCodes.Stelem, t.IsArray ? t.GetElementType() : t);

            //<----
            code.PushOnStack = pushOnStack;
        }

        #endregion

        #endregion

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitArrayAccess(this);
        }
    }
}