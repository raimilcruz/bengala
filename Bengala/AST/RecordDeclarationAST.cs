#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <param name="parameterList"></param>
        public RecordDeclarationAST(string typeID, FormalParameterList parameterList)
            : base(typeID)
        {
            Fields = (parameterList ?? new FormalParameterList()).Parameters
                .Select(x=>new KeyValuePair<string,string>(x.Name,x.TypeIdentifier)).ToList();
        }

        #endregion


        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitRecordDeclaration(this);
        }
    }
}