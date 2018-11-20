namespace Bengala.AST.Errors
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