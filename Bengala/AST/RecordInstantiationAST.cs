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
    /// Representa la instan
    /// </summary>
    public class RecordInstantiationAST : ExpressionAST
    {
        #region Fields and Properties

        public string Id { get; private set; }
        public List<KeyValuePair<string, ExpressionAST>> ExpressionValue { get; private set; }

        #endregion

        #region Constructors

        public RecordInstantiationAST(string id, List<KeyValuePair<string, ExpressionAST>> exp, int line, int col)
            : base(line, col)
        {
            Id = id;
            ExpressionValue = exp ?? new List<KeyValuePair<string, ExpressionAST>>();
        }

        #endregion

        #region Instance Methods


        public override void GenerateCode(ILCode code)
        {
            //--->
            bool pushOnStack = code.PushOnStack;

            //crear un instancia de la clase que representa al record
            string recordCodeName = CurrentScope.GetTypeInfo(Id).CodeName;
            Type type = code.DefinedType[recordCodeName];

            ILGenerator il = code.Method.GetILGenerator();
            //crear la instancia del objeto
            il.Emit(OpCodes.Newobj, type.GetConstructor(Type.EmptyTypes));

            //Guardar localmente una referencia al record un LocalBuilder dentro del metodo
            LocalBuilder local = il.DeclareLocal(type);
            //guardar la instancia en la variable local.
            il.Emit(OpCodes.Stloc, local.LocalIndex);

            var rt = (RecordType) CurrentScope.GetType(Id);
            foreach (var item in ExpressionValue)
            {
                //cargar la instancia de la clase.
                il.Emit(OpCodes.Ldloc, local.LocalIndex);
                pushOnStack = true;
                //generar el codigo para inicializar el campo
                item.Value.GenerateCode(code);
                //asignarle  el valor al field

                il.Emit(OpCodes.Stfld, type.GetField(item.Key));
            }
            //dejar la instancia del tipo en la pila
            il.Emit(OpCodes.Ldloc, local.LocalIndex);

            if (!pushOnStack)
                il.Emit(OpCodes.Pop);
            //<---
            code.PushOnStack = pushOnStack;
        }

        #endregion

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitRecordInstantiation(this);
        }
    }
}