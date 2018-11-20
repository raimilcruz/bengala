namespace Bengala.AST.Errors
{
    /// <summary>
    /// Esta clase es usada para representar Warning en la compilacion
    /// </summary>
    public class WarningMessage : Message
    {
        public WarningMessage(string innerMessage, int line, int col) : base(innerMessage, line, col)
        {
        }
    }
}