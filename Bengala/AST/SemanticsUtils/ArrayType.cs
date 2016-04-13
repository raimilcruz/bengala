namespace Bengala.AST.SemanticsUtils
{
    public class ArrayType : TigerType
    {
        #region Constructores

        public ArrayType(TigerType baseType, string typeId)
        {
            BaseType = baseType;
            TypeID = typeId;

            #region Comparison Array-Array

            //operacion ==
            AddBinaryOperation(this, GetType<IntType>(), Operators.Equal, EqualOpGenerator, true);

            //operacion <>
            AddBinaryOperation(this, GetType<IntType>(), Operators.NotEqual, NeqOpGenerator, true);

            #endregion

            #region Comparison Array -Nil

            //operacion ==
            AddBinaryOperation(GetType<NilType>(), GetType<IntType>(), Operators.Equal, EqualOpGenerator, true);

            //operacion <>
            AddBinaryOperation(GetType<NilType>(), GetType<IntType>(), Operators.NotEqual, NeqOpGenerator, true);

            #endregion
        }

        #endregion

        #region Fields and Properties

        /// <summary>
        /// Devuelve el tipo de los elementos del arreglo.
        /// </summary>
        public TigerType BaseType { get; set; }

        #endregion

        #region Instance Method

        public override string ToString()
        {
            return string.Format("array of {0}", BaseType);
        }

        #endregion
    }
}