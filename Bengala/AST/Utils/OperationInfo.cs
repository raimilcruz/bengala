#region Usings

using System;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST.Utils
{
    public class OperationInfo
    {
        public TigerType ResultType { get; set; }
        public GenOperation GenOperation { get; set; }
        public TigerType Op1 { get; set; }
        public TigerType Op2 { get; set; }
        public bool IsPredefinedOperation { get; set; }
        public Operators CurrentOperator { get; set; }

        public override string ToString()
        {
            return String.Format("{0}{1}{2}", Op1, CurrentOperator, Op2);
        }
    }
}