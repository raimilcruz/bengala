#region Usings

using System;
using System.Collections.Generic;
using Bengala.AST.CodeGenerationUtils;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// clase base de todas las declaraciones de tipos: arrayDec, recordDec, aliasDec
    /// </summary>
    public abstract class TypeDeclarationAST : ExpressionAST
    {
        /// <summary>
        /// Nombre del tipo q se declara
        /// </summary>
        public string TypeId { get; set; }

        #region Constructors

        /// <summary>
        /// </summary>
        /// <param name="typeId"> Nombre del tipo q se declara</param>
        /// <param name="line">Linea correspondiente en el codigo</param>
        /// <param name="col">Columna correspondiente en el codigo</param>
        protected TypeDeclarationAST(string typeId, int line, int col) : base(line, col)
        {
            TypeId = typeId;
        }

        #endregion

     

        /// <summary>
        /// Este metodo es usado para crear un tipo al cual se hace referencia y es posible que no haya sido previamente creado en el codigo IL
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        protected Type CreateTypeNotFounded(string typeId, ILCode code)
        {
            TypeInfo type = CurrentScope.GetTypeInfo(typeId);
            if (code.DefinedType.ContainsKey(type.CodeName))
                return code.DefinedType[type.CodeName];
            TigerType t = type.Type;
            if (t is ArrayType)
            {
                Type baseType = CreateTypeNotFounded(((ArrayType) t).BaseType.TypeID, code);
                Type arrayType = baseType.MakeArrayType();
                code.DefinedType.Add(type.CodeName, arrayType);
                return arrayType;
            }
            if (t is RecordType)
            {
                Type temp = code.Module.DefineType(type.CodeName);
                code.DefinedType.Add(type.CodeName, temp);
                return temp;
            }
            throw new NotImplementedException("Los restantes tipos no estan soportados en tiger");
        }

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitTypeDeclaration(this);
        }
    }
}