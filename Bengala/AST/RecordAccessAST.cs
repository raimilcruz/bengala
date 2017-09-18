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
    /// Representa un accesso a record en el lenguaje Tiger.
    /// </summary>
    public class RecordAccessAST : LValueAST
    {
        #region Fields and Properties

        public ExpressionAST ExpressionRecord { get; private set; }
        public string FieldId { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldId">El campo al cual se accede</param>
        /// <param name="exp">La expresion que representa a la instancia del record</param>
        /// <param name="line">Linea correspondiente en el codigo</param>
        /// <param name="col">Columna correspondiente en el codigo</param>
        public RecordAccessAST(string fieldId, ExpressionAST exp, int line, int col) : base(line, col)
        {
            FieldId = fieldId;
            ExpressionRecord = exp;
        }

        #endregion

        #region Instance Methods

    

        #endregion

        #region Code Generation

        /// <summary>
        /// Este metodo se encarga de generar el codigo para el acceso a un record
        /// </summary>
        /// <param name="code"></param>
        public override void GenerateCode(ILCode code)
        {
            ILGenerator il = code.Method.GetILGenerator();
            //--->
            bool pushOnStack = code.PushOnStack;

            //cargando el valor del campo del record
            code.PushOnStack = true;
            ExpressionRecord.GenerateCode(code);

            string typeCodeName = CurrentScope.GetTypeInfo(ExpressionRecord.ReturnType.TypeID).CodeName;
            Type recordType = code.DefinedType[typeCodeName];
            il.Emit(OpCodes.Ldfld, recordType.GetField(FieldId));

            //<---
            if (!pushOnStack)
                il.Emit(OpCodes.Pop);
            code.PushOnStack = pushOnStack;
        }

        public override void GenerateCode(ILCode code, ExpressionAST exp)
        {
        }

        #endregion

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitRecordAccess(this);
        }
    }
}