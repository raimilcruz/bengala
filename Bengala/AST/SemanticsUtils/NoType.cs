namespace Bengala.AST.SemanticsUtils
{
    /// <summary>
    /// Esta clase se usa en la implementacion de un compilador para las expression que no retornan un valor.
    /// Por ejemplo las funciones.
    /// </summary>
    public class NoType : TigerType
    {
        #region Constructor

        public NoType()
        {
            IsLegalType = false;
        }

        #endregion

        #region Instance Methods

        public override string ToString()
        {
            return "void";
        }

        #endregion
    }
}