#region Usings

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Representa la declaracion de un record: type REC = {uncampo:int, otrocampo:REC}
    /// </summary>
    public class RecordDeclarationAST : TypeDeclarationAST
    {
        #region Fields and Properties

        /// <summary>
        /// Devuelve una lista con los campos que contiene el record
        /// </summary>
        public List<KeyValuePair<string, string>> Fields { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Inicializa una nueva instancia de record declaration
        /// </summary>
        /// <param name="typeID">Nombre del record que se declara</param>
        /// <param name="fields">lista de campos de la forma [field:type]</param>
        /// <param name="line">Linea correspondiente en el codigo</param>
        /// <param name="col">Columna correspondiente en el codigo</param>
        public RecordDeclarationAST(string typeID, List<KeyValuePair<string, string>> fields, int line, int col)
            : base(typeID, line, col)
        {
            Fields = fields ?? new List<KeyValuePair<string, string>>();
        }

        #endregion


        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitRecordDeclaration(this);
        }
    }
}