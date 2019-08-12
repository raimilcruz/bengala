namespace Bengala.AST
{
    /// <summary>
    /// Representa una variable : "var" + 3 .Tambien puede ser usado para representa la asignacion a una variable
    /// </summary>
    public class VarAST : LHSExpressionAST
    {
        #region Fields and Properties

        public string VarId { get; }

        #endregion

        #region Constructors

        public VarAST(string varId) 
        {
            VarId = varId;
            AlwaysReturn = true;
        }

        #endregion

      
        public bool IsForeignVar { get; set; }

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitVar(this);
        }
    }
}