#region Usings

using System;
using System.Collections.Generic;

#endregion

namespace Bengala.AST.SemanticsUtils
{
    public class Scope
    {
        #region Fields and Properties

        private readonly int deep;
        private readonly Dictionary<string, FunctionInfo> functions;
        private readonly Dictionary<string, TypeInfo> types;


        //variables que cada funcion usa y que estan en scope superiores.
        private readonly Dictionary<string, List<VarInfo>> usedInChildFunctions;
        private readonly Dictionary<string, VarInfo> vars;

        private FunctionInfo currentFunction;

        public FunctionInfo CurrentFunction
        {
            get { return currentFunction ?? (Parent != null ? Parent.CurrentFunction : null); }
            set { currentFunction = value; }
        }


        public Scope Parent { get; set; }

        public bool IsInLoop
        {
            get { return ContainerLoop != null; }
        }

        public LoopAST ContainerLoop { get; set; }

        public bool ThereIsUsedVarsForFunction(string functionName)
        {
            bool ok;
            try
            {
                ok = GetVarsUsedInFunction(functionName).Count > 0;
            }
            catch
            {
                ok = false;
            }
            return ok;
        }

        //la profundidad del scope.

        #endregion

        #region Constructors

        public Scope(Scope parent)
        {
            vars = new Dictionary<string, VarInfo>();
            types = new Dictionary<string, TypeInfo>();
            functions = new Dictionary<string, FunctionInfo>();
            Parent = parent;

            usedInChildFunctions = new Dictionary<string, List<VarInfo>>();
            deep = parent == null ? 0 : parent.deep + 1;
        }

        public Scope(Scope scope, LoopAST loopAST)
            : this(scope)
        {
            ContainerLoop = loopAST;
        }

        #endregion

        #region Instance Methods

        /// <summary>
        /// Se encarga de annadir una variable al scope
        /// </summary>
        /// <param name="varId">El id de la variable en el fichero de codigo</param>
        /// <param name="typeId">El id del tipo en el ficheo de codigo</param>
        /// <param name="varInfo">La informacion asociada a  la variable</param>
        private void AddVar(string varId, string typeId, VarInfo varInfo)
        {
            if (HasType(typeId) == ScopeLocation.NotDeclared)
                throw new ArgumentException("El tipo no esta en el scope");
            varInfo.VarName = varId;
            varInfo.TypeInfo = GetTypeInfo(typeId);
            //asociarle  la funcion en la cual se encuentra declarada esta variable.
            varInfo.FunctionNameParent = varInfo.FunctionNameParent ??
                                         (CurrentFunction != null ? CurrentFunction.FunctionName : null);
            if (HasVar(varId) == ScopeLocation.NotDeclared)
                varInfo.CodeName = varInfo.CodeName ?? varInfo.VarName;
                //es dejar el mismo nombre en el CodeNameGenereted que en VarID
            else
            {
                //la variable oculta a otra con el mismo nombre
                varInfo.Hides = true;
                varInfo.CodeName = string.Format("{0}_{1}", varId, Utils.GetUID());
            }
            vars.Add(varId, varInfo);
        }

        /// <summary>
        /// Adiciona una variable al scope
        /// </summary>
        /// <param name="varId">El identificador de la variable en el codigo del programa</param>
        /// <param name="typeId">El identificador del tipo en el codigo del programa</param>
        public void AddVar(string varId, string typeId)
        {
            var varInfo = new VarInfo
                              {
                                  IsLocalVariable = true,
                                  //Esto esta OK,se asume que la variable es local ,si no lo es se le cambia luego              
                                  CodeName = string.Format("{0}_{1}", varId, Utils.GetUID()),
                              };
            AddVar(varId, typeId, varInfo);
        }

        /// <summary>
        /// Adiciona al scope una variable que representa un parametro de funcion
        /// </summary>
        /// <param name="varId"></param>
        /// <param name="typeId"></param>
        /// <param name="posParams"></param>
        /// <param name="functionName"></param>
        public void AddVarParameter(string varId, string typeId, int posParams, string functionName)
        {
            var varInfo = new VarInfo
                              {
                                  IsParameterFunction = true,
                                  ParameterNumber = posParams,
                                  ParamInFunction = functionName,
                                  FunctionNameParent = functionName,
                                  CodeName = string.Format("{0}_{1}", varId, Utils.GetUID())
                              };
            AddVar(varId, typeId, varInfo);
        }

        /// <summary>
        /// Adiciona al scope una variable que representa una variable de un for
        /// </summary>
        /// <param name="varId"></param>
        /// <param name="typeId"></param>
        public void AddVarFor(string varId, string typeId)
        {
            var varInfo = new VarInfo
                              {
                                  IsLocalVariable = true,
                                  CodeName = string.Format("{0}_{1}", varId, Utils.GetUID())
                              };
            AddVar(varId, typeId, varInfo);
        }


        /// <summary>
        /// Este metodo es usado para asociar a la funcion una variable que es de ella y que se usa en sus hijas
        /// </summary>
        /// <param name="var"></param>
        public void AsociatteVarToFunctionAsUsedForChildFunction(string var)
        {
            VarInfo varInfo = GetVarInfo(var);
            FunctionInfo funInfo = GetFunction(varInfo.FunctionNameParent);
            funInfo.VarsUsedForAnotherFunction.Add(varInfo);
        }

        public void AddFunction(string functionId, FunctionInfo functionInfo)
        {
            functionInfo.Deep = deep;
            functionInfo.CodeName = functionInfo.CodeName ?? string.Format("{0}_{1}", functionId, Utils.GetUID());
            functions.Add(functionId, functionInfo);
        }

        public void AddType(string typeId, TigerType type)
        {
            var t = new TypeInfo
                        {CodeName = string.Format("{0}_{1}", typeId, Utils.GetUID()), Type = type, TypeId = typeId};
            types.Add(typeId, t);
            OnTypeAdded(typeId, type);
        }

        public void AddAlias(string aliasId, string typeId)
        {
            if (HasType(typeId) == ScopeLocation.NotDeclared)
                throw new ArgumentException(string.Format("El tipo {0} no esta definido", typeId));
            if (HasType(aliasId) == ScopeLocation.DeclaredLocal)
                throw new ArgumentException(string.Format("El tipo {0} ya esta declarado en este scope", aliasId));
            types.Add(aliasId, GetTypeInfo(typeId));
        }

        public ScopeLocation HasVar(string varId)
        {
            TigerType noNeeded;
            return HasVar(varId, out noNeeded);
        }

        public ScopeLocation HasVar(string varId, out TigerType varType)
        {
            VarInfo var;
            vars.TryGetValue(varId, out var);
            varType = var == null ? null : var.TypeInfo.Type;
            if (varType != null)
                return ScopeLocation.DeclaredLocal;
            return (Parent != null)
                       ? (Parent.HasVar(varId, out varType) == ScopeLocation.NotDeclared
                              ? ScopeLocation.NotDeclared
                              :
                                  ScopeLocation.DeclaredInParent)
                       : ScopeLocation.NotDeclared;
        }

        public ScopeLocation HasFunction(string functionId)
        {
            FunctionInfo noNeeded;
            return HasFunction(functionId, out noNeeded);
        }

        public ScopeLocation HasFunction(string functionId, out FunctionInfo info)
        {
            functions.TryGetValue(functionId, out info);
            if (info != null)
                return ScopeLocation.DeclaredLocal;
            return Parent != null ? Parent.HasFunction(functionId, out info) : ScopeLocation.NotDeclared;
        }

        /// <summary>
        /// Devuelve una instancia de ScopeLocation dando informacion si el tipo ya esta declarado
        /// </summary>
        /// <param name="typeID">El identificador del tipo en el codigo Tiger </param>
        /// <returns></returns>
        public ScopeLocation HasType(string typeID)
        {
            if (string.IsNullOrEmpty(typeID))
                return ScopeLocation.NotDeclared;
            TigerType noNeeded;
            return HasType(typeID, out noNeeded);
        }

        public ScopeLocation HasType(string typeID, out TigerType type)
        {
            TypeInfo t;
            types.TryGetValue(typeID, out t);
            type = (t != null) ? t.Type : null;
            if (type != null)
                return ScopeLocation.DeclaredLocal;
            return (Parent != null)
                       ? (Parent.HasType(typeID, out type) == ScopeLocation.NotDeclared
                              ? ScopeLocation.NotDeclared
                              :
                                  ScopeLocation.DeclaredInParent)
                       : ScopeLocation.NotDeclared;
        }

        public TigerType GetVarType(string varId)
        {
            TigerType tt;
            HasVar(varId, out tt);
            if (tt == null)
                throw new InvalidOperationException("no existe esa variable");
            return tt;
        }

        public string GetVarNameCode(string varId)
        {
            return GetVarInfo(varId).CodeName;
        }

        /// <summary>
        /// Devuelve una lista de las variables que se declararon en esta funcion y son usadas por funciones hijas
        /// </summary>
        /// <param name="functionId">El nombre de la funcion </param>
        /// <returns></returns>
        public List<VarInfo> GetVarsUsedInFunction(string functionId)
        {
            if (HasFunction(functionId) != ScopeLocation.NotDeclared)
            {
                if (usedInChildFunctions.ContainsKey(functionId))
                    return usedInChildFunctions[functionId];
                List<VarInfo> varsInfo = Parent.GetVarsUsedInFunction(functionId);
                if (varsInfo != null)
                    return varsInfo;
            }
            throw new ArgumentException(string.Format("El paremetro {0} no exite  {1}", functionId, deep));
        }

        /// <summary>
        /// Devuelve informacion asociada a la variable
        /// </summary>
        /// <param name="varId"></param>
        /// <returns></returns>
        public VarInfo GetVarInfo(string varId)
        {
            if (vars.ContainsKey(varId))
                return vars[varId];
            return Parent.GetVarInfo(varId);
        }

        /// <summary>
        /// Devuelve informacion asociada a la funcion
        /// </summary>
        /// <param name="functionId"></param>
        /// <returns></returns>
        public FunctionInfo GetFunction(string functionId)
        {
            FunctionInfo info;
            HasFunction(functionId, out info);
            if (info == null)
                throw new InvalidOperationException("no existe esa funcion");
            return info;
        }

        public TigerType GetType(string typeId)
        {
            TigerType tt;
            HasType(typeId, out tt);
            if (tt == null)
                throw new InvalidOperationException("no existe ese tipo");
            return tt;
        }

        /// <summary>
        /// Devuelve informacion asociada al tipo
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public TypeInfo GetTypeInfo(string typeId)
        {
            ScopeLocation sl = HasType(typeId);
            if (sl == ScopeLocation.NotDeclared)
                throw new ArgumentException("El tipo no esta declarado");
            if (sl == ScopeLocation.DeclaredLocal)
                return types[typeId];
            return Parent.GetTypeInfo(typeId);
        }

        #endregion

        #region Events and Method'Handlers

        #region Delegates

        /// <summary>
        /// Accion a ejecutar ante la ocurrencia del evento FinalizeScope
        /// </summary>
        /// <param name="sender">instacia que origina el evento</param>
        /// <param name="args">informacion sobre el evento</param>
        public delegate void FinalizeScopeEventHandler(Scope sender, EventArgs args);

        /// <summary>
        /// Accion a ejecutar ante la ocurrencia del evento TypeAdded
        /// </summary>
        /// <param name="sender">instacia que origina el evento</param>
        /// <param name="args">informacion sobre el evento</param>
        public delegate void TypeAddedEventHandler(Scope sender, TypeAddedEventArgs args);

        #endregion
        /// <summary>
        /// Manejador del evento que ocurre al añadir un tipo
        /// </summary>
        public event TypeAddedEventHandler TypeAdded;

        /// <summary>
        /// disparador del evento que ocurre cuando se añade un tipo
        /// </summary>
        protected virtual void OnTypeAdded(string typeName, TigerType type)
        {
            if (TypeAdded != null)
            {
                var args = new TypeAddedEventArgs(typeName, type);
                TypeAdded(this, args);
            }
        }

        /// <summary>
        /// Manejador del evento que ocurre al cerrar un scope
        /// </summary>
        public event FinalizeScopeEventHandler FinalizeScope;
        
        /// <summary>
        /// Disparador del evento cerrar scope
        /// </summary>
        protected virtual void OnFinalize()
        {
            if (FinalizeScope != null)
                FinalizeScope(this, new EventArgs());
        }

        /// <summary>
        /// Indica que no se anadiran mas tipos al scope
        /// </summary>
        public virtual void Close()
        {
            OnFinalize();
        }

        /// <summary>
        /// informacion que se envia cuando ocurre el evento de añadir un tipo
        /// </summary>
        public class TypeAddedEventArgs : EventArgs
        {
            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="typeName">Nombre del tipo que se añade</param>
            /// <param name="newType">Tipo que se añade</param>
            public TypeAddedEventArgs(string typeName, TigerType newType)
            {
                TypeName = typeName;
                NewType = newType;
            }

            public string TypeName { get; private set; }
            public TigerType NewType { get; private set; }
        }

        #endregion
    }

    /// <summary>
    /// ScopeLocation es usada por la clase Scope para informar donde se ha declarado un determinado elemento.
    /// </summary>
    public enum ScopeLocation
    {
        DeclaredInParent = 2,
        DeclaredLocal = 4,
        NotDeclared = 8
    }

    /// <summary>
    /// Esta clase se usa para generar Ids para las variables, tipos y funciones.
    /// </summary>
    internal static class Utils
    {
        private static int i;

        public static string GetUID()
        {
            i++;
            return "tiger" + i;
        }
    }
}