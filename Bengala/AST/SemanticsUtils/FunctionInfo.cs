#region Usings

using System.Collections.Generic;

#endregion

namespace Bengala.AST.SemanticsUtils
{
    public class FunctionInfo
    {
        public FunctionInfo(List<KeyValuePair<string, TigerType>> parameterList)
        {
            ParameterList = parameterList ?? new List<KeyValuePair<string, TigerType>>();
            FunctionReturnType = TigerType.GetType<NoType>();
            VarsUsedForAnotherFunction = new List<VarInfo>();
        }

        public FunctionInfo(List<KeyValuePair<string, TigerType>> parameterList, TigerType retType)
            : this(parameterList)
        {
            FunctionReturnType = retType;
        }

        public FunctionInfo()
        {
        }

        public List<KeyValuePair<string, TigerType>> ParameterList { get; private set; }
        public TigerType FunctionReturnType { get; private set; }

        public string CodeName { get; set; }
        public string FunctionName { get; set; }
        public FunctionInfo FunctionParent { get; set; }
        public bool ContainVarsUsedForAnotherFunction { get; set; }
        public List<VarInfo> VarsUsedForAnotherFunction { get; set; }
        public bool IsPredifined { get; set; }
        public int Deep { get; set; }
    }
}