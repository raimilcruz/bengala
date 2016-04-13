#region Usings

using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Bengala.AST.CodeGenerationUtils;
using Bengala.AST.Utils;

#endregion

namespace Bengala.AST.SemanticsUtils
{
    /// <summary>
    /// Es la clase base de todos los tipos de Tiger.
    /// </summary>
    public abstract class TigerType
    {
        #region Field and Properties

        private static readonly Dictionary<Type, TigerType> typeRef;

        /// <summary>
        /// Informacion de las operaciones que soporta el tipo
        /// </summary>
        protected Dictionary<string, OperationInfo> operatorTable;

        /// <summary>
        /// Se usa para saber si un tipo puede ser asignado a una variable.
        /// </summary>
        public bool IsLegalType { get; protected set; }

        /// <summary>
        /// Devuelve true si el tipo es un tipo predefinido en Tiger.
        /// </summary>
        public bool IsPredifined { get; protected set; }

        /// <summary>
        /// El identificador del tipo en codigo
        /// </summary>
        public string TypeID { get; protected set; }

        #endregion

        #region Constructor

        protected TigerType()
        {
            IsLegalType = true;
            operatorTable = new Dictionary<string, OperationInfo>();

            //add predefined operator equals
        }

        #endregion

        #region Static Method

        static TigerType()
        {
            typeRef = new Dictionary<Type, TigerType>();
        }

        #region Generacion de codigo

        /// <summary>
        /// Genera codigo para una operacion de not equal.Valido para todos excepto para string
        /// </summary>
        /// <param name="code"></param>
        internal static void NeqOpGenerator(ILCode code)
        {
            ILGenerator il = code.Method.GetILGenerator();
            //se asume que los operandos ya estan en la pilas.
            //los comparo
            il.Emit(OpCodes.Ceq);
            //cargo el valor 0
            il.Emit(OpCodes.Ldc_I4_0);
            //comparo el resultado de la igualdad con el cero.
            il.Emit(OpCodes.Ceq);
            //pregunto si debo dejar el valor en la pila
            if (!code.PushOnStack)
                il.Emit(OpCodes.Pop);
        }

        internal static void EqualOpGenerator(ILCode code)
        {
            ILGenerator il = code.Method.GetILGenerator();
            il.Emit(OpCodes.Ceq);
            //pregunto si debo dejar el resultado en la pila
            if (!code.PushOnStack)
                il.Emit(OpCodes.Pop);
        }

        #endregion

        public static TigerType GetType<T>() where T : TigerType, new()
        {
            TigerType t;
            typeRef.TryGetValue(typeof (T), out t);
            if (t != null) return t;
            var temp = new T();
            typeRef[typeof (T)] = temp;
            temp.AddPredefinedOperations();
            return temp;
        }

        #endregion

        #region Instance Method

        public static string Int = "int";
        public static string String = "string";

        public virtual bool IsValueType
        {
            get { return false; }
        }

        protected virtual void AddPredefinedOperations()
        {
        }

        /// <summary>
        /// Dice si el tipo machea con otro.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual bool CanConvertTo(TigerType type)
        {
            return (this == type);
        }

        /// <summary>
        /// Devuelve true si el operador puede ser aplicado a dos operandos del tipo de la clase.
        /// </summary>
        /// <param name="op">El operador en cuestion </param>
        /// <param name="other">Tipo del otro operando</param>
        /// <returns></returns>
        public virtual bool SupportsOperator(TigerType other, Operators op)
        {
            switch (op)
            {
                case Operators.Equal:
                case Operators.NotEqual:
                    {
                        if (this == other && !(other is NilType))
                            return true;
                        if (other is NilType)
                            return !IsValueType && !(this is NilType);
                        return !(other.IsValueType && this is NilType);
                    }
                default:
                    return operatorTable.ContainsKey(this + op.ToString() + other);
            }
        }

        /// <summary>
        /// Anade soporte para u operador binario
        /// </summary>
        /// <param name="other">Tipo del otro operando</param>
        /// <param name="resultType">Tipo del resultado de la operacion</param>
        /// <param name="op">Operador en cuestion</param>
        /// <param name="generator">Generador de codigo para la operacion</param>
        /// <param name="predefinedOp">Indica si es una operacion predefinida en el lenguaje</param>
        public void AddBinaryOperation(TigerType other, TigerType resultType, Operators op, GenOperation generator,
                                       bool predefinedOp)
        {
//            if ((int)op >= 10)
//                throw new InvalidOperationException("Operator " + op + " can not be defined for the given types");

            var nfo = new OperationInfo
                          {
                              ResultType = resultType,
                              Op1 = this,
                              Op2 = other,
                              CurrentOperator = op,
                              GenOperation = generator,
                              IsPredefinedOperation = predefinedOp
                          };
            string id = nfo.ToString();
            if (operatorTable.ContainsKey(id))
                throw new InvalidOperationException("Operations can not be redefined");
            operatorTable.Add(id, nfo);
        }

        /// <summary>
        /// Devuelve el delegate que genera el codigo para la operacion especificada
        /// </summary>
        /// <param name="other">Tipo del otro operando</param>
        /// <param name="op">Operador en cuestion</param>
        /// <returns>Generador de operacion si esta definida, null en otro caso</returns>
        public virtual GenOperation GetOperationGenerator(TigerType other, Operators op)
        {
            OperationInfo nfo;
            operatorTable.TryGetValue(this + op.ToString() + other, out nfo);
            return nfo != null ? nfo.GenOperation : null;
        }

        /// <summary>
        /// Devuelve el tipo de retorno de la operacion especificada
        /// </summary>
        /// <param name="other">Tipo del otro operando</param>
        /// <param name="op">Operador en cuestion</param>
        /// <returns>Generador de operacion si esta definida, null en otro caso</returns>
        public virtual TigerType GetOperationResult(TigerType other, Operators op)
        {
            OperationInfo nfo;
            operatorTable.TryGetValue(this + op.ToString() + other, out nfo);
            return nfo != null ? nfo.ResultType : null;
        }

        #endregion
    }
}