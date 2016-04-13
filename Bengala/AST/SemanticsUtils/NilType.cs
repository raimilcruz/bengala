namespace Bengala.AST.SemanticsUtils
{
    /// <summary>
    /// Representa al tipo nil en el lenguaje Tiger.
    /// </summary>
    public class NilType : TigerType
    {
        #region Constructors

        public NilType()
        {
            IsLegalType = false;
        }

        #endregion

        #region Instance Methods

        public override bool CanConvertTo(TigerType type)
        {
            //Nil Type se puede convertir a todas los tipos ,excepto a los tipos por valor a NIL
            return !type.IsValueType && !(type is NilType);
        }


        public override string ToString()
        {
            return "nil";
        }

        #endregion
    }
}