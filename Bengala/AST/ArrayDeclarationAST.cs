#region Usings

using System;
using System.Collections.Generic;
using Bengala.AST.CodeGenerationUtils;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Representa la declaracion de un array 'type' typeId 'array of' type
    /// </summary>
    public class ArrayDeclarationAST : TypeDeclarationAST
    {
        #region Fields and Properties

        public string BaseTypeID { get; set; }

        #endregion

        #region Constructors

        public ArrayDeclarationAST(string typeId, string baseTypeID, int line, int col)
            : base(typeId, line, col)
        {
            BaseTypeID = baseTypeID;
            TypeId = typeId;
        }

        #endregion

        #region Instance Methods

        public override bool CheckSemantic(Scope scope, List<Message> listError)
        {
           throw new NotImplementedException("The implementation has been moved to StaticChecker.VisitArrayDeclaration");
        }

        public override void GenerateCode(ILCode code)
        {
            //quedandome con el TypeInfo de tipo base del array.
            TypeInfo t = CurrentScope.GetTypeInfo(BaseTypeID);

            //en este momento es posible que el tipo del array no halla sido creado en el codigo il.
            //este tipo puede ser un array ,un alias o un record ,incluso una clase si se quiere extender esto un poco mas

            //creando el tipo del array
            Type arrayType = CreateTypeNotFounded(t.TypeId, code).MakeArrayType();

            //annadir el tipo si es necesario.
            string arrayCodeName = CurrentScope.GetTypeInfo(TypeId).CodeName;
            if (!code.DefinedType.ContainsKey(arrayCodeName))
                code.DefinedType.Add(arrayCodeName, arrayType);
        }

        #endregion

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitArrayDeclaration(this);
        }
    }
}