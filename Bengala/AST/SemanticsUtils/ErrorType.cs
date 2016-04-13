#region Usings

using Bengala.AST.Utils;

#endregion

namespace Bengala.AST.SemanticsUtils
{
    /// <summary>
    /// Esta clase es utilizada cuando una expresion da error. Se le asigna entonces a esa expresion
    /// una instancia de esta clase. Por defecto el metodo Match de esta clase conforma con todos los tipos
    /// Este se hace con el objetivo de poder capturar mas errores de compilacion
    /// </summary>
    public class ErrorType : TigerType
    {
        #region Constructor

        public ErrorType()
        {
            IsLegalType = false;
            TypeID = "1error";
        }

        #endregion

        #region Instance Methods

        public override bool CanConvertTo(TigerType type)
        {
            return true;
        }

        public override bool SupportsOperator(TigerType tt, Operators op)
        {
            return true;
        }

        public override TigerType GetOperationResult(TigerType other, Operators op)
        {
            return other;
        }

        public override GenOperation GetOperationGenerator(TigerType other, Operators op)
        {
            return x => { };
        }

        public override string ToString()
        {
            return "error type";
        }

        #endregion
    }
}