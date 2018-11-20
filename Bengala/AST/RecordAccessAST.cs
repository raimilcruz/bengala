#region Usings

using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Representa un accesso a record en el lenguaje Tiger.
    /// </summary>
    public class RecordAccessAST : LHSExpressionAST
    {
        #region Fields and Properties

        public ExpressionAst ExpressionRecord { get; private set; }
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
        public RecordAccessAST(string fieldId, ExpressionAst exp, int line, int col) : base(line, col)
        {
            FieldId = fieldId;
            ExpressionRecord = exp;
        }

        #endregion

      

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitRecordAccess(this);
        }
    }
}