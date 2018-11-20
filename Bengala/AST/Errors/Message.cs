#region Usings

using System.Reflection;
using System.Resources;

#endregion

namespace Bengala.AST.Errors
{
    public abstract class Message
    {
        protected Message()
        {
        }

        protected Message(string innerMessage, int line, int column)
        {
            InnerMessage = innerMessage;
            Line = line;
            Column = column + 1;
        }

        public string InnerMessage { get; set; }
        public int Line { get; set; }
        public int Column { get; set; }

        internal static string LoadMessage(string messageName)
        {
            var rr = new ResourceManager("Bengala.Properties.Resources", Assembly.GetExecutingAssembly());
            return rr.GetString(messageName);
        }

        public override string ToString()
        {
            return InnerMessage;
        }
    }
}