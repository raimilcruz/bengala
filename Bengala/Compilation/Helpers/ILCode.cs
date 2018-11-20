#region Usings

using System;
using System.Collections.Generic;
using System.Reflection.Emit;

#endregion

namespace Bengala.Compilation.Helpers
{
    /// <summary>
    /// Esta clase contiene toda la informacion necesaria para la generacion de codigo IL.
    /// </summary>
    public class ILCode
    {
        #region Fields and Properties

        /// <summary>
        /// Contiene los metodos que se han definido en el codigo IL
        /// </summary>
        public Dictionary<string, MethodBuilder> DefinedMethod { get; set; }

        /// <summary>
        /// Contien los tipos que se han definido en el codigo Il
        /// </summary>
        public Dictionary<string, Type> DefinedType { get; set; }

        /// <summary>
        /// Contiene los tipos que se han definido en el codigo IL.
        /// </summary>
        public Dictionary<string, FieldBuilder> DefinedField { get; set; }

        /// <summary>
        /// Contiene las variables locales que han sido declaradas
        /// </summary>
        public Dictionary<string, LocalBuilder> DefinedLocal { get; set; }

        /// <summary>
        /// Contiene los tipos que se han annadido para mantener el concepto de Scope
        /// </summary>
        public Dictionary<string, TypeCodeInfo> FuntionWrapper { get; set; }

        /// <summary>
        /// Devuelve una referencia al modulo actual donde se esta generando el codigo.
        /// </summary>
        public ModuleBuilder Module { get; set; }

        /// <summary>
        /// Devuelve una referencia al tipo actual que se esta definiendo.
        /// </summary>
        public TypeBuilder Type { get; set; }

        /// <summary>
        /// Contiene una referencia al metodo actual que se esta definiendo.
        /// </summary>
        public MethodBuilder Method { get; set; }

        /// <summary>
        /// Devuelve la posicion a donde hay que saltar para romper un ciclo. En caso de que no halla ciclo devuelve null
        /// </summary>
        public Label EndCurrentLoop { get; set; }

        /// <summary>
        /// Devuelve true, si quiere que la instruccion no  ponga una valor en la pila.
        /// Ejemplo : 2 + 3
        /// </summary>
        public bool PushOnStack { get; set; }

        #endregion

        #region Constructors

        public ILCode()
        {
            DefinedMethod = new Dictionary<string, MethodBuilder>();
            DefinedField = new Dictionary<string, FieldBuilder>();
            DefinedType = new Dictionary<string, Type>();
            DefinedLocal = new Dictionary<string, LocalBuilder>();

            FuntionWrapper = new Dictionary<string, TypeCodeInfo>();
        }

        #endregion

        #region Instance Methods

        /// <summary>
        /// Devuelve un tipo dado su nombre. Si el tipo no esta definido se crea solamente con el nombre
        /// </summary>
        /// <param name="typeCodeName"></param>
        /// <returns></returns>
        public Type GetTypeBuilderMaybeNotCreated(string typeCodeName)
        {
            if (DefinedType.ContainsKey(typeCodeName))
                return DefinedType[typeCodeName];

            DefinedType.Add(typeCodeName, Module.DefineType(typeCodeName));
            return DefinedType[typeCodeName];
        }

        /// <summary>
        /// Adiciona un nuevo tipo a  la clase codigo
        /// </summary>
        /// <param name="typeCodeName"></param>
        /// <param name="type"></param>
        public void AddTypeBuilder(string typeCodeName, Type type)
        {
            if (DefinedType.ContainsKey(typeCodeName))
                throw new ArgumentException("Un tipo con ese nombre ya fue declarado en el codigo");
            DefinedType.Add(typeCodeName, type);
        }

        /// <summary>
        /// Adiciona un metodo builder a la clase codigo
        /// </summary>
        /// <param name="methodCodeName"></param>
        /// <param name="mBuilder"></param>
        public void AddMethodBuilder(string methodCodeName, MethodBuilder mBuilder)
        {
            if (DefinedMethod.ContainsKey(methodCodeName))
                throw new ArgumentException("Un metodo con ese nombre ya fue declarado en el codigo");
            DefinedMethod.Add(methodCodeName, mBuilder);
        }

        /// <summary>
        /// Retorna el metodo en cuestion
        /// </summary>
        /// <param name="methodCodeName"></param>
        /// <returns></returns>
        public MethodBuilder GetMethodBuilder(string methodCodeName)
        {
            if (!DefinedMethod.ContainsKey(methodCodeName))
                throw new ArgumentException("El metodo no se annadido a la clase codigo");
            return DefinedMethod[methodCodeName];
        }

        /// <summary>
        /// Adiciona un nuevo field a la clase
        /// </summary>
        /// <param name="fieldCodeName"></param>
        /// <param name="field"></param>       
        public void AddFieldBuilder(string fieldCodeName, FieldBuilder field)
        {
            if (DefinedField.ContainsKey(fieldCodeName))
                throw new ArgumentException("Un field con ese nombre ya se ha annadido");
            DefinedField.Add(fieldCodeName, field);
        }

        public FieldBuilder GetFieldBuilder(string fieldCodeName)
        {
            if (!DefinedField.ContainsKey(fieldCodeName))
                throw new ArgumentException("No existe ese field ");
            return DefinedField[fieldCodeName];
        }

        public void AddLocalBuilder(string localCodeName, LocalBuilder localBuilder)
        {
            if (DefinedLocal.ContainsKey(localCodeName))
                throw new ArgumentException("Ya existe un variable local annadida");
            DefinedLocal.Add(localCodeName, localBuilder);
        }

        public LocalBuilder GetLocalBuilder(string localCodeName)
        {
            if (!DefinedLocal.ContainsKey(localCodeName))
                throw new ArgumentException("No existe esta variable");
            return DefinedLocal[localCodeName];
        }

        /// <summary>
        /// Este metodo asocia una funcion con el wrapper que ella declara para sus funciones hijas
        /// </summary>
        /// <param name="functionId"></param>
        /// <returns></returns>
        internal TypeCodeInfo GetWrapperAsociatteTo(string functionId)
        {
            return FuntionWrapper[functionId];
        }

        /// <summary>
        /// Asocia una funcion con el wrapper que ella crea.
        /// </summary>
        /// <param name="functionCodeName"></param>
        /// <param name="typeWrapperCodeName"></param>
        public void AsociatteMethodToWrapper(string functionCodeName, string typeWrapperCodeName)
        {
            if (!DefinedMethod.ContainsKey(functionCodeName))
                throw new ArgumentException("La funcion debe ser annadida antes");
            if (!DefinedType.ContainsKey(typeWrapperCodeName))
                throw new ArgumentException("El tipo tiene que ser annadido antes");
            if (FuntionWrapper.ContainsKey(functionCodeName))
                throw new ArgumentException("Ya se ha asociado esa funcion a ese tipo");

            var typeBuilder = (TypeBuilder) DefinedType[typeWrapperCodeName];


            var typeCodeInfo = new TypeCodeInfo {TypeCodeName = typeWrapperCodeName, Type = typeBuilder};

            FuntionWrapper.Add(functionCodeName, typeCodeInfo);
        }

        #endregion

        #region Events

        public event GenerateCode OnBeginMethod;

        public void ThrowEventForFunction(string functionCodeName)
        {
            if (OnBeginMethod != null)
                OnBeginMethod(this, new BeginMethodEventArgs {FunctionCodeName = functionCodeName});
        }

        #endregion
    }

    public class BeginMethodEventArgs : EventArgs
    {
        public string FunctionCodeName { get; set; }
    }

    public delegate void GenerateCode(ILCode sender, BeginMethodEventArgs e);
}