namespace Bengala.AST.SemanticsUtils
{
    public class ErrorMessage : Message
    {
        public ErrorMessage()
        {
        }

        public ErrorMessage(string innerMessage, int line, int column) : base(innerMessage, line, column)
        {
        }
    }
}