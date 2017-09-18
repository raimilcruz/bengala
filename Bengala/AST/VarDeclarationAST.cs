#region Usings

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Bengala.AST.CodeGenerationUtils;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Representa la declaracion de una variable en el lenguaje Tiger/
    ///Ejemplos
    ///   var i  := 8
    /// </summary>
    public class VarDeclarationAST : ExpressionAST
    {
        #region Fields and Properties

        /// <summary>
        /// Devuelve la expresion que representa al valor de la variable
        /// </summary>
        public ExpressionAST ExpressionValue { get; private set; }

        /// <summary>
        /// El identificador de la variable
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// El nombre del tipo de la variable
        /// </summary>
        public string TypeId { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// </summary>
        /// <param name="id">El id de la variable</param>
        /// <param name="typeId">El nombre del tipo de la variable</param>
        /// <param name="expValue">La expresion que define el valor de la variable</param>
        /// <param name="line">Linea correspondiente en el codigo</param>
        /// <param name="col">Columna correspondiente en el codigo</param>
        public VarDeclarationAST(string id, string typeId, ExpressionAST expValue, int line, int col) : base(line, col)
        {
            TypeId = typeId;
            Id = id;
            ExpressionValue = expValue;
        }

        /// <summary>
        /// Cdo el tipo es inferido 
        /// </summary>
        /// <param name="id">El id de la variable</param>
        /// <param name="expValue">La expresion que define el valor de la variable</param>
        public VarDeclarationAST(string id, ExpressionAST expValue)
        {
            Id = id;
            ExpressionValue = expValue;
        }

        #endregion

        #region Instance Methods

        public override void GenerateCode(ILCode code)
        {
            VarInfo varInfo = CurrentScope.GetVarInfo(Id);
            //nombre de la variable generado
            string varCodeName = varInfo.CodeName;
            // necesito el tipo de la variable.
            string typeCodeName = varInfo.TypeInfo.CodeName;
            Type varType = code.DefinedType[typeCodeName];

            //aca hay que tener en cuenta que las variable que son usadas por otras funciones se declaran en la declaracion de
            //la funcion a la que pertenecen

            if (!varInfo.IsUsedForAnotherFunction)
            {
                if (!varInfo.IsParameterFunction)
                    //no tengo en cuenta los parametro de funcion porque esos los declara la funcion
                {
                    //generar la variable como local a la funcion
                    ILGenerator il = code.Method.GetILGenerator();
                    LocalBuilder local = il.DeclareLocal(varType);
                    // local.SetLocalSymInfo(varCodeName);
                    //adicionarla a las varibles locales
                    code.DefinedLocal.Add(varCodeName, local);
                }
            }
            else
            {
                if (!varInfo.IsParameterFunction)
                {
                    string currentFunction = CurrentScope.CurrentFunction.CodeName;
                    TypeCodeInfo wrapper = code.GetWrapperAsociatteTo(currentFunction);

                    FieldBuilder field = wrapper.Type.DefineField(varInfo.CodeName, varType, FieldAttributes.Public);
                    //añadir el field a la clase ILCode
                    code.DefinedField.Add(varInfo.CodeName, field);
                    //añadida al wrapper
                    wrapper.AddField(varCodeName, field);
                }
            }
            //generar la inicializacion de la variable
            code.OnBeginMethod += code_OnBeginMethod;
        }

        private void code_OnBeginMethod(ILCode code, BeginMethodEventArgs e)
        {
            VarInfo varInfo = CurrentScope.GetVarInfo(Id);

            if (IsMyFunction(varInfo, e.FunctionCodeName))
            {
                ILGenerator il = code.Method.GetILGenerator();
                //--->
                bool pushOnStack = code.PushOnStack;
                code.PushOnStack = true;

                string varCodeName = varInfo.CodeName;
                if (!varInfo.IsUsedForAnotherFunction)
                {
                    if (!varInfo.IsParameterFunction) //significa que es una variable local
                    {
                        ExpressionValue.GenerateCode(code);
                        il.Emit(OpCodes.Stloc, code.DefinedLocal[varCodeName].LocalIndex);
                    }
                }
                else
                {
                    if (!varInfo.IsParameterFunction) //significa que es un campo de la clase 
                    {
                        //cargar la instancia de la clase contenedora que tengo como variable local
                        il.Emit(OpCodes.Ldloc_0);
                        ExpressionValue.GenerateCode(code);
                        il.Emit(OpCodes.Stfld, code.DefinedField[varCodeName]);
                    }
                }
                //<---
                code.PushOnStack = pushOnStack;
            }
        }

        /// <summary>
        /// Este metodo devuelve true si la funcion que me pasan es la misma que la funcion donde se declaro la variable
        /// </summary>
        /// <param name="varInfo"></param>
        /// <param name="funCodeName"></param>
        /// <returns></returns>
        private bool IsMyFunction(VarInfo varInfo, string funCodeName)
        {
            return CurrentScope.GetFunction(varInfo.FunctionNameParent).CodeName == funCodeName;
        }

        #endregion

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitVarDeclaration(this);
        }
    }
}