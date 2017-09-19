using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using Bengala.AST;
using Bengala.AST.CodeGenerationUtils;
using Bengala.AST.SemanticsUtils;

namespace Bengala.Compilation
{
    class FunctionDeclarationILGeneratorHelper
    {
        private FunctionDeclarationAST _functionDecl;
        public FunctionDeclarationILGeneratorHelper(FunctionDeclarationAST functionDeclaration)
        {
            _functionDecl = functionDeclaration;
        }
        /// <summary>
        /// Esta metodo crea un metodo en Il correspondiente a la funcion. Lo crea dentro de una clase.En esta clase se encuentra
        /// las variables de la funcion padre que son usadas por esta funcion. Esta clase tambien contiene una instancia a su clase
        /// padre donde se pueden encontrar otras variables que puede usar esta funcion
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public MethodBuilder GenerateInstanceFunction(ILCode code)
        {
            //este es el nombre de la clase que contiene a este funcion
            string parentWrapper = string.Format("Tg_{0}", FunctionParentCodeName());
            //obtengo el tipo de la clase
            var container = (TypeBuilder)code.DefinedType[parentWrapper];

            Type returnType;
            Type[] parameterTypes;
            //creo los paremtros de la funcion
            bool isFunction = !string.IsNullOrEmpty(_functionDecl.ReturnTypeId);
            //creando los parametros y el tipo de retorno
            CreateParams(code, isFunction, out returnType, out parameterTypes);

            //creo la funcion como un metodo de instancia.
            MethodBuilder mBuilder = container.DefineMethod(FunctionCodeName(), MethodAttributes.Public, returnType,
                                                            parameterTypes);

            //adiciono la nueva funcion que he declarado 
            code.DefinedMethod.Add(mBuilder.Name, mBuilder);

            return mBuilder;
        }

        /// <summary>
        /// Este metodo devuelve un metodo builder . Si detecta que ya esta creado(por que se realizo un llamado a el antes
        /// de que fuese declardo en Il) no hace nada, sino lo crea.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private MethodBuilder GetMethod(ILCode code)
        {
            if (code.DefinedMethod.ContainsKey(FunctionCodeName()))
                return code.DefinedMethod[FunctionCodeName()];
            return GenerateInstanceFunction(code);
        }

        public MethodBuilder GenerateInstanceMethodWithClassWrapper(ILCode code)
        {
            MethodBuilder mBuilder = GetMethod(code);

            //este es el nombre de la clase que contiene a este funcion
            string parentWrapper = string.Format("Tg_{0}", FunctionParentCodeName());
            //obtengo el tipo de la clase que contiene a esta funcion
            var container = (TypeBuilder)code.DefinedType[parentWrapper];

            //generar la clase wrapper a mis variables
            string currentWrapper = string.Format("Tg_{0}", FunctionCodeName());
            TypeBuilder typeNested = container.DefineNestedType(currentWrapper,
                                                                TypeAttributes.NestedPublic);

            //annadir a la clase code el tipo.
            code.DefinedType.Add(typeNested.Name, typeNested);
            //asociar el wrapper con la funcion que lo creo
            code.AsociatteMethodToWrapper(mBuilder.Name, currentWrapper);

            //tengo que asociar este tipo con su padre.
            TypeCodeInfo typeCodeInfo = code.GetWrapperAsociatteTo(FunctionCodeName());
            typeCodeInfo.Parent = code.GetWrapperAsociatteTo(FunctionParentCodeName());

            //annadir esta funcion al padre.
            //Tener en cuenta cuando se llame la funcion antes de ser declarada
            if (!typeCodeInfo.Parent.ContainMethodInLevel1(mBuilder.Name))
                typeCodeInfo.Parent.AddMethod(mBuilder.Name, mBuilder);

            PopulateNestedTypeWithVar(code, typeNested, typeCodeInfo, container);

            //crear la instancia de esta clase dentro de mi codigo.
            ILGenerator il = mBuilder.GetILGenerator();
            il.DeclareLocal(typeNested);
            //No annado la variable local al ILCode porque siempre sera la primera de todas.            

            return mBuilder;
        }

        /// <summary>
        /// Esta metodo annade todas los campos necesarios a la clase nested. Annade tambien un campo que es una referencia 
        /// padre.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="nested">El tipo nested</param>
        /// <param name="typeCodeInfo">la informacion asociada  al tipo nested</param>
        /// <param name="container">El padre del tipo nested</param>
        private void PopulateNestedTypeWithVar(ILCode code, TypeBuilder nested, TypeCodeInfo typeCodeInfo,
                                               TypeBuilder container)
        {
            //annadir un field del tipo de mi padre
            typeCodeInfo.FieldNameOfParent = "parent";
            FieldBuilder fieldRefPadreClass = nested.DefineField(typeCodeInfo.FieldNameOfParent, container,
                                                                 FieldAttributes.Public);
            //le annado a la clase TypeCodeInfo que ahora tiene un nuevo campo(la referencia al padre)
            typeCodeInfo.AddField(typeCodeInfo.FieldNameOfParent, fieldRefPadreClass);

            FunctionInfo funInfo = _functionDecl.CurrentScope.GetFunction(_functionDecl.FunctionId);
            foreach (var item in funInfo.VarsUsedForAnotherFunction)
            {
                if (item.IsParameterFunction)
                {
                    Type fieldType = code.DefinedType[item.TypeInfo.CodeName];
                    FieldBuilder field = nested.DefineField(item.CodeName, fieldType, FieldAttributes.Public);
                    code.DefinedField.Add(item.CodeName, field);

                    //annadir el nuevo campo a la clase que me controlo los campos de un tipo nested
                    typeCodeInfo.AddField(item.CodeName, field);
                }
            }
        }

        private string FunctionParentCodeName()
        {
            return _functionDecl.CurrentScope.GetFunction(_functionDecl.FunctionId).FunctionParent.CodeName;
        }

        /// <summary>
        /// Crea los parametros y el tipo de retorno de la funcion
        /// </summary>
        /// <param name="code"></param>
        /// <param name="isFunction"></param>
        /// <param name="returnType"></param>
        /// <param name="parameterTypes"></param>
        private void CreateParams(ILCode code, bool isFunction, out Type returnType, out Type[] parameterTypes)
        {
            returnType = null;
            if (isFunction)
            {
                string typeCodeName = _functionDecl.CurrentScope.GetTypeInfo(_functionDecl.ReturnTypeId).CodeName;
                returnType = code.DefinedType[typeCodeName];
            }

            parameterTypes = null;
            if (_functionDecl.ParameterList != null)
            {
                parameterTypes = new Type[_functionDecl.ParameterList.Count];
                for (int i = 0; i < _functionDecl.ParameterList.Count; i++)
                {
                    string typeCodeName = _functionDecl.CurrentScope.GetTypeInfo(_functionDecl.ParameterList[i].Value).CodeName;
                    parameterTypes[i] = code.DefinedType[typeCodeName];
                }
            }
        }

        public string FunctionCodeName()
        {
            return _functionDecl.CurrentScope.GetFunction(_functionDecl.FunctionId).CodeName;
        }
    }
}
