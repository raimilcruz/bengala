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
    class ILCodeGeneratorFuntionInvocationHelper
    {
        private readonly CallFunctionAST _functionInvocation;
        private readonly ILCodeGenerator _codeGenerator;
      
        public ILCodeGeneratorFuntionInvocationHelper(CallFunctionAST functionInvocation,ILCodeGenerator codeGenerator)
        {
            _functionInvocation = functionInvocation;
            _codeGenerator = codeGenerator;
        }

        public void CallToMethod(MethodBuilder methodToCall, ILCode code)
        {
            FunctionInfo funInfo = _functionInvocation.CurrentScope.GetFunction(_functionInvocation.FunctionId);
            ILGenerator il = code.Method.GetILGenerator();
            //esto significa que la funcion a llamar es estatica. 
            //TODO :Ver como hacer extensible esto para el dia en quiera tener funciones predefinidas de instancia
            if (funInfo.IsPredifined)
            {
                code.PushOnStack = true;
                foreach (var item in _functionInvocation.RealParam)
                    item.Accept(_codeGenerator);
                il.Emit(OpCodes.Call, methodToCall);
            }
            else //este es el caso en que llamo a un metodo de instancia
            {
                if (IsCallToMethodChild(code)) //funciones que de cierta forma son mis hijas.Las funciones globales
                    il.Emit(OpCodes.Ldloc_0); //son hijas del main              
                else if (IsCallRecursiveOrMethodBrother(code))
                    //es el caso en que llamo funciones que estan a mi mismo nivel               
                    il.Emit(OpCodes.Ldarg_0);
                else if (IsCallToMethodParent(code))
                    GenerateCodeForCallMethodParent(code);
                else
                    throw new NotImplementedException("No se en que caso real puediese ser aca");
                code.PushOnStack = true;
                foreach (var item in _functionInvocation.RealParam)
                    item.Accept(_codeGenerator);
                il.Emit(OpCodes.Callvirt, methodToCall);
            }
        }

        /// <summary>
        /// Devuelve true si la funcion es una llamado a una funcion padre.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private bool IsCallToMethodParent(ILCode code)
        {
            return !IsCallToMethodChild(code) && !IsCallRecursiveOrMethodBrother(code);
        }

        /// <summary>
        /// Devuelve true si el llamado es a una funcion hija.Es decir definidas a partir de la funcion actual
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private bool IsCallToMethodChild(ILCode code)
        {
            string funCodeName = _functionInvocation.CurrentScope.CurrentFunction.CodeName;
            TypeCodeInfo typeCodeInfo = code.GetWrapperAsociatteTo(funCodeName);
            return typeCodeInfo.ContainMethodInLevel1(FunctionCodeName());
        }

        private void GenerateCodeForCallMethodParent(ILCode code)
        {
            ILGenerator il = code.Method.GetILGenerator();

            string parentOfCurrentFunction = _functionInvocation.CurrentScope.CurrentFunction.FunctionParent.CodeName;
            TypeCodeInfo parentWrapper = code.GetWrapperAsociatteTo(parentOfCurrentFunction);

            il.Emit(OpCodes.Ldarg_0);
            while (parentWrapper != null && !parentWrapper.ContainMethodInLevel1(FunctionCodeName()))
            {
                il.Emit(OpCodes.Ldfld, parentWrapper.GetField(parentWrapper.FieldNameOfParent));
                parentWrapper = parentWrapper.Parent;
            }
            //lo proximo seria llamar al metodo
        }

        /// <summary>
        /// Devuelve true si el llamado es a una funcion definida al mismo nivel del la funcion actual
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private bool IsCallRecursiveOrMethodBrother(ILCode code)
        {
            string funParentCodeName = _functionInvocation.CurrentScope.CurrentFunction.FunctionParent.CodeName;
            TypeCodeInfo typeCodeInfo = code.GetWrapperAsociatteTo(funParentCodeName);
            return typeCodeInfo.ContainMethodInLevel1(FunctionCodeName());
        }

        /// <summary>
        /// Devuelve el metodo que hay que llamar .Este metodo puede no estar definido en IL a la hora 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public MethodBuilder GetMethodToCall(ILCode code)
        {
            return GetMethod(FunctionCodeName(), code);
        }


        private MethodBuilder GetMethod(string funCodeName, ILCode code)
        {
            //si esta definida la uso
            if (code.DefinedMethod.ContainsKey(funCodeName))
                return code.DefinedMethod[funCodeName];

            //sino no  esta definida creo su signatura.
            //este es el nombre de la clase que contiene a este funcion
            string parentWrapper = string.Format("Tg_{0}", FunctionParentCodeName());
            //obtengo el tipo de la clase
            var container = (TypeBuilder)code.DefinedType[parentWrapper];

            Type returnType;
            Type[] parameterTypes;
            //creo los paremtros de la funcion
            bool isFunction = !string.IsNullOrEmpty(ReturnTypeId());
            //creando los parametros y el tipo de retorno
            CreateParams(code, isFunction, out returnType, out parameterTypes);

            //creo la funcion como un metodo de instancia.
            MethodBuilder mBuilder = container.DefineMethod(FunctionCodeName(), MethodAttributes.Public, returnType,
                                                            parameterTypes);

            //adiciono la nueva funcion que he declarado 
            code.DefinedMethod.Add(mBuilder.Name, mBuilder);

            //Tener en cuenta cuando se llame la funcion antes de ser declarada ,hay que annadir el metodo a su padre
            string currentFunctionParent = _functionInvocation.CurrentScope.CurrentFunction.FunctionParent.CodeName;
            TypeCodeInfo typeCodeInfo = code.GetWrapperAsociatteTo(currentFunctionParent);
            typeCodeInfo.AddMethod(mBuilder.Name, mBuilder);
            //typcodeindo

            return mBuilder;
        }

        private List<KeyValuePair<string, string>> GetParameterList()
        {
            FunctionInfo funInfo = _functionInvocation.CurrentScope.GetFunction(_functionInvocation.FunctionId);
            var parameters = new List<KeyValuePair<string, string>>();
            foreach (var item in funInfo.ParameterList)
            {
                parameters.Add(new KeyValuePair<string, string>(item.Key, item.Value.TypeID));
            }
            return parameters;
        }

        private string ReturnTypeId()
        {
            return _functionInvocation.CurrentScope.GetFunction(_functionInvocation.FunctionId).FunctionReturnType.TypeID;
        }

        private void CreateParams(ILCode code, bool isFunction, out Type returnType, out Type[] parameterTypes)
        {
            returnType = null;
            if (isFunction)
            {
                string typeCodeName = _functionInvocation.CurrentScope.GetTypeInfo(ReturnTypeId()).CodeName;
                returnType = code.DefinedType[typeCodeName];
            }

            parameterTypes = null;
            List<KeyValuePair<string, string>> ParameterList = GetParameterList();
            if (ParameterList != null)
            {
                parameterTypes = new Type[ParameterList.Count];
                for (int i = 0; i < ParameterList.Count; i++)
                {
                    string typeCodeName = _functionInvocation.CurrentScope.GetTypeInfo(ParameterList[i].Value).CodeName;
                    parameterTypes[i] = code.DefinedType[typeCodeName];
                }
            }
        }

        private string FunctionParentCodeName()
        {
            return _functionInvocation.CurrentScope.GetFunction(_functionInvocation.FunctionId).FunctionParent.CodeName;
        }

        private string FunctionCodeName()
        {
            return _functionInvocation.CurrentScope.GetFunction(_functionInvocation.FunctionId).CodeName;
        }

    }
}
