#region Usings

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

#endregion

namespace Bengala.AST.CodeGenerationUtils
{
    /// <summary>
    /// Esta clase se utiliza para guardar la informacion de un tipo en la clase ILCode
    /// </summary>
    public class TypeCodeInfo
    {
        #region Fields and Properties

        //campos del tipo
        private readonly Dictionary<string, FieldBuilder> fields;
        //metodos del tipo
        private readonly Dictionary<string, MethodBuilder> methods;
        //contructor pot defecto del tipo
        private ConstructorBuilder ctorBuilder;

        public string FieldNameOfParent { get; set; }
        public string TypeCodeName { get; set; }
        public TypeBuilder Type { get; set; }
        public TypeCodeInfo Parent { get; set; }

        #endregion

        #region Constructors

        public TypeCodeInfo()
        {
            fields = new Dictionary<string, FieldBuilder>();
            methods = new Dictionary<string, MethodBuilder>();
        }

        #endregion

        #region Instance Methods

        /// <summary>
        /// Devuelve el consctructor por defecto para el tipo.
        /// </summary>
        /// <returns></returns>
        public ConstructorBuilder DefaultConstructor()
        {
            if (ctorBuilder != null)
                return ctorBuilder;
            ConstructorBuilder ctor = Type.DefineConstructor(MethodAttributes.Public, CallingConventions.HasThis,
                                                             new Type[0]);
            ILGenerator il = ctor.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Callvirt, typeof (object).GetConstructor(new Type[0]));
            il.Emit(OpCodes.Ret);

            ctorBuilder = ctor;

            return ctor;
        }

        /// <summary>
        /// Devuelve true si el tipo contiene ese campo.
        /// </summary>
        /// <param name="fieldName">Nombre del campo</param>
        /// <returns></returns>
        public bool ContainFieldInLevel1(string fieldName)
        {
            return fields.ContainsKey(fieldName);
        }

        /// <summary>
        /// Devuelve el field cuyo nombre es fieldName
        /// </summary>
        /// <param name="fieldName">Nombre del campo</param>
        /// <returns></returns>
        public FieldBuilder GetField(string fieldName)
        {
            if (!ContainFieldInLevel1(fieldName))
                throw new ArgumentException("No se contiene ese varible a primer nivel" + fieldName);
            return fields[fieldName];
        }

        /// <summary>
        /// Adiciona un nuevo field al tipo
        /// </summary>
        /// <param name="fieldCodeName"></param>
        /// <param name="fieldBuilder"></param>
        public void AddField(string fieldCodeName, FieldBuilder fieldBuilder)
        {
            if (fields.ContainsKey(fieldCodeName))
                throw new ArgumentException("ya se ha annadido el campo");
            fields.Add(fieldCodeName, fieldBuilder);
        }

        /// <summary>
        /// Devuelve true si el tipo contiene ese metodo
        /// </summary>
        /// <param name="funcCodeName">Nombre del metodo</param>
        /// <returns></returns>
        public bool ContainMethodInLevel1(string funcCodeName)
        {
            return methods.ContainsKey(funcCodeName);
        }

        /// <summary>
        /// Deuelve el metodo cuyo nombre es funCodeName
        /// </summary>
        /// <param name="funcCodeName"></param>
        /// <returns></returns>
        public MethodBuilder GetMethod(string funcCodeName)
        {
            if (!ContainMethodInLevel1(funcCodeName))
                throw new ArgumentException("No se contiene ese varible a primer nivel" + funcCodeName);
            return methods[funcCodeName];
        }

        /// <summary>
        /// Adiciona un metodo al tipo
        /// </summary>
        /// <param name="functionCodeName"></param>
        /// <param name="mBuilder"></param>
        public void AddMethod(string functionCodeName, MethodBuilder mBuilder)
        {
            if (methods.ContainsKey(functionCodeName))
                throw new ArgumentException("Ya se annadido la funcion");
            methods.Add(functionCodeName, mBuilder);
        }

        #endregion
    }
}