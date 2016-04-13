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
    /// Representa un llamado a funcion
    /// </summary>
    public class CallFunctionAST : ExpressionAST
    {
        #region Fields and Properties

        public string FunctionId { get; private set; }
        public List<ExpressionAST> RealParam { get; private set; }

        #endregion

        #region Constructors

        public CallFunctionAST(string functionId, List<ExpressionAST> realParam)
        {
            FunctionId = functionId;
            RealParam = realParam;
        }

        public CallFunctionAST(string functionId, List<ExpressionAST> realParam, int line, int col) : base(line, col)
        {
            FunctionId = functionId;
            RealParam = realParam;
        }

        #endregion

        #region Instance Method

        public override bool CheckSemantic(Scope scope, List<Message> listError)
        {
            //probablemente se cambie
            CurrentScope = scope;

            bool ok = true;

            FunctionInfo functionInfo;
            string message;
            //verificar que la funcion existe.
            if (scope.HasFunction(FunctionId, out functionInfo) != ScopeLocation.NotDeclared)
            {
                //coger su tipo de retorno
                if (functionInfo.ParameterList.Count == RealParam.Count)
                {
                    for (int i = 0; i < functionInfo.ParameterList.Count; i++)
                    {
                        //no es necesario chequear q el chequeo de los parametros sea true o no
                        RealParam[i].CheckSemantic(scope, listError);
                        ok = ok && (RealParam[i].ReturnType.CanConvertTo(functionInfo.ParameterList[i].Value));
                    }
                    if (ok)
                    {
                        AlwaysReturn = !(functionInfo.FunctionReturnType is NoType);
                        ReturnType = functionInfo.FunctionReturnType;
                        return true;
                    }
                    message = string.Format(Message.LoadMessage("FuncParams"), FunctionId);
                }
                else
                    message = string.Format(Message.LoadMessage("FuncParamsCount"), FunctionId, RealParam.Count);
            }
            else
                message = string.Format(Message.LoadMessage("FuncUndecl"), FunctionId);
            listError.Add(new ErrorMessage(message, Line, Columns));
            ReturnType = TigerType.GetType<ErrorType>();
            return false;
        }

        #region Generacion de codigo

        public override void GenerateCode(ILCode code)
        {
            //<--- quedarme con el valor
            bool pushOnStack = code.PushOnStack;

            ILGenerator il = code.Method.GetILGenerator();
            FunctionInfo funInfo = CurrentScope.GetFunction(FunctionId);
            bool returnAValue = !(funInfo.FunctionReturnType is NoType);


            //para permitir sobrecargas
            MethodBuilder methodToCall = GetMethodToCall(code);


            CallToMethod(methodToCall, code);

            //si la funcion retorna una valor y no se espera que ponga este en la pila ,QUITALO
            if (returnAValue && !pushOnStack)
                il.Emit(OpCodes.Pop);

            //--->
            code.PushOnStack = pushOnStack;
        }

        private void CallToMethod(MethodBuilder methodToCall, ILCode code)
        {
            FunctionInfo funInfo = CurrentScope.GetFunction(FunctionId);
            ILGenerator il = code.Method.GetILGenerator();
            //esto significa que la funcion a llamar es estatica. 
            //TODO :Ver como hacer extensible esto para el dia en quiera tener funciones predefinidas de instancia
            if (funInfo.IsPredifined)
            {
                code.PushOnStack = true;
                foreach (var item in RealParam)
                    item.GenerateCode(code);
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
                foreach (var item in RealParam)
                    item.GenerateCode(code);
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
            string funCodeName = CurrentScope.CurrentFunction.CodeName;
            TypeCodeInfo typeCodeInfo = code.GetWrapperAsociatteTo(funCodeName);
            return typeCodeInfo.ContainMethodInLevel1(FunctionCodeName());
        }

        private void GenerateCodeForCallMethodParent(ILCode code)
        {
            ILGenerator il = code.Method.GetILGenerator();

            string parentOfCurrentFunction = CurrentScope.CurrentFunction.FunctionParent.CodeName;
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
            string funParentCodeName = CurrentScope.CurrentFunction.FunctionParent.CodeName;
            TypeCodeInfo typeCodeInfo = code.GetWrapperAsociatteTo(funParentCodeName);
            return typeCodeInfo.ContainMethodInLevel1(FunctionCodeName());
        }

        /// <summary>
        /// Devuelve el metodo que hay que llamar .Este metodo puede no estar definido en IL a la hora 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private MethodBuilder GetMethodToCall(ILCode code)
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
            var container = (TypeBuilder) code.DefinedType[parentWrapper];

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
            string currentFunctionParent = CurrentScope.CurrentFunction.FunctionParent.CodeName;
            TypeCodeInfo typeCodeInfo = code.GetWrapperAsociatteTo(currentFunctionParent);
            typeCodeInfo.AddMethod(mBuilder.Name, mBuilder);
            //typcodeindo

            return mBuilder;
        }

        private List<KeyValuePair<string, string>> GetParameterList()
        {
            FunctionInfo funInfo = CurrentScope.GetFunction(FunctionId);
            var parameters = new List<KeyValuePair<string, string>>();
            foreach (var item in funInfo.ParameterList)
            {
                parameters.Add(new KeyValuePair<string, string>(item.Key, item.Value.TypeID));
            }
            return parameters;
        }

        private string ReturnTypeId()
        {
            return CurrentScope.GetFunction(FunctionId).FunctionReturnType.TypeID;
        }

        private void CreateParams(ILCode code, bool isFunction, out Type returnType, out Type[] parameterTypes)
        {
            returnType = null;
            if (isFunction)
            {
                string typeCodeName = CurrentScope.GetTypeInfo(ReturnTypeId()).CodeName;
                returnType = code.DefinedType[typeCodeName];
            }

            parameterTypes = null;
            List<KeyValuePair<string, string>> ParameterList = GetParameterList();
            if (ParameterList != null)
            {
                parameterTypes = new Type[ParameterList.Count];
                for (int i = 0; i < ParameterList.Count; i++)
                {
                    string typeCodeName = CurrentScope.GetTypeInfo(ParameterList[i].Value).CodeName;
                    parameterTypes[i] = code.DefinedType[typeCodeName];
                }
            }
        }

        private string FunctionParentCodeName()
        {
            return CurrentScope.GetFunction(FunctionId).FunctionParent.CodeName;
        }

        private string FunctionCodeName()
        {
            return CurrentScope.GetFunction(FunctionId).CodeName;
        }

        #endregion

        #endregion
    }
}