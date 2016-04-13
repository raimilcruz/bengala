#region Usings

using System.Collections.Generic;

#endregion

namespace Bengala.AST.SemanticsUtils
{
    /// <summary>
    /// Representa a un tipo Record en el lenguaje Tiger.
    /// </summary>
    public class RecordType : TigerType
    {
        #region Fields and Properties	

        private readonly Dictionary<string, TigerType> field;

        #endregion

        #region Constructors

        public RecordType(string typeName)
            : this(typeName, new Dictionary<string, TigerType>())
        {
        }

        public RecordType(string typeName, Dictionary<string, TigerType> field)
        {
            TypeID = typeName;
            this.field = field;

            #region Comparison Record-Record

            //operacion ==
            AddBinaryOperation(this, GetType<IntType>(), Operators.Equal, EqualOpGenerator, true);

            //operacion <>
            AddBinaryOperation(this, GetType<IntType>(), Operators.NotEqual, NeqOpGenerator, true);

            #endregion

            #region Comparison Record -Nil

            AddBinaryOperation(GetType<NilType>(), GetType<IntType>(), Operators.Equal, EqualOpGenerator, true);

            //operacion <>
            AddBinaryOperation(GetType<NilType>(), GetType<IntType>(), Operators.NotEqual, NeqOpGenerator, true);

            #endregion
        }

        protected override void AddPredefinedOperations()
        {
        }

        #endregion

        #region Instance Methods

        public TigerType this[string fieldId]
        {
            get
            {
                if (field.ContainsKey(fieldId))
                    return field[fieldId];
                return new ErrorType();
            }
        }

        public void AddField(string fieldId, TigerType t)
        {
            field.Add(fieldId, t);
        }

        public bool Contains(string fieldId)
        {
            return field.ContainsKey(fieldId);
        }


        public override string ToString()
        {
            return string.Format("record: {0}", TypeID);
        }

        #endregion
    }
}