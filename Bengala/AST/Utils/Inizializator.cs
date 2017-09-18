#region Usings

using System.Collections.Generic;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST.Utils
{
    /// <summary>
    /// Esta clase se usaria para inicializar el Scope,El parametro T representa la informacion asociada al codigo que se va a generar.
    /// </summary>
    public class Inizializator<T>
    {
        private readonly List<FunctionPredifined<T>> functions;
        private readonly List<PrefinedType<T>> types;

        public Inizializator(Scope scope, T codeInfo)
        {
            Scope = scope;
            CodeInfo = codeInfo;
            functions = new List<FunctionPredifined<T>>();
            types = new List<PrefinedType<T>>();
        }

        /// <summary>
        /// Esta sobrecarga del constructor es para cuando quieres annadir luego la informacion sobre codigo.
        /// </summary>
        /// <param name="scope"></param>
        public Inizializator(Scope scope) : this(scope, default(T))
        {
        }

        public T CodeInfo { get; set; }
        public Scope Scope { get; private set; }

        public void AddPredifinedFunction(FunctionPredifined<T> predifinedFunction)
        {
            functions.Add(predifinedFunction);
        }

        public void AddPredifinedTypes(PrefinedType<T> predifinedType)
        {
            types.Add(predifinedType);
        }

        public void InitializeScope()
        {
            foreach (var item in functions)
            {
                Scope.AddFunction(item.FunctionInfo.FunctionName, item.FunctionInfo);
            }
            foreach (var item in types)
            {
                Scope.AddType(item.TigerType.TypeID, item.TigerType);
            }
        }

        /// <summary>
        /// Se asume que se ha llamado previamente al metodo InitializaScope
        /// </summary>
        public void GeneratePredifinedCode()
        {
            foreach (var item in functions)
            {
                item.FunctionGenerator(item.FunctionInfo.CodeName, CodeInfo);
            }
            foreach (var item in types)
            {
                string typeCodeName = Scope.GetTypeInfo(item.TigerType.TypeID).CodeName;
                item.TypeGenerator(typeCodeName, CodeInfo);
            }
        }
    }

    public class ScopeInitializator
    {
        public void InitScope(Scope scope)
        {
            //predifined types
            scope.AddType("int",TigerType.GetType<IntType>());
            scope.AddType("string",TigerType.GetType<StringType>());
            //We add ErrorType to scope, because when a variable is not defined
            //we say that it has ErrorType, so ErrorType must be in the 
            //Scope.
            scope.AddType(TigerType.GetType<ErrorType>().TypeID, TigerType.GetType<StringType>());
        }
    }
}