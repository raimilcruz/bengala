#region Usings

using System;
using System.Collections.Generic;
using System.Reflection.Emit;

#endregion

namespace Bengala.Compilation.Helpers
{
    public class MethodCodeInfo
    {
        private readonly Dictionary<string, LocalBuilder> locals;

        public MethodCodeInfo(string methodName)
        {
            MethodName = methodName;
            locals = new Dictionary<string, LocalBuilder>();
        }

        public string MethodName { get; private set; }

        public void AddLocal(string localCodeName, LocalBuilder localBuilder)
        {
            if (locals.ContainsKey(localCodeName))
                throw new ArgumentException("Una variable con el mismo nombre ya fue annadida");
            locals.Add(localCodeName, localBuilder);
        }

        public LocalBuilder GetLocal(string localCodeName)
        {
            if (!locals.ContainsKey(localCodeName))
                throw new ArgumentException("La variable no existe");
            return locals[localCodeName];
        }
    }
}