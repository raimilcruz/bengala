#region Usings

using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST.CodeGenerationUtils
{
    internal interface IExpressionILGenerator
    {
        void GenerateCode(ExpressionAST exp, Scope currentScope, ILCode code);
    }
}