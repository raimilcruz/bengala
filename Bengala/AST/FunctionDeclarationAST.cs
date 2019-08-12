#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Bengala.AST.SemanticsUtils;
using Bengala.AST.Types;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Esta clase representa la declaracion tanto de una funcion ,como de un procedimiento.
    /// procedimientos  :   'function' functionId '(' paramsList ')' = exp
    /// funciones       :   'function' functionId '(' paramsList ')' : typeId = exp
    /// </summary>
    public class FunctionDeclarationAST :Declaration
    {
        #region Fields and Properties

        public override string Id => FunctionId;

        public string FunctionId { get; private set; }
        public string ReturnTypeId { get; private set; }
        public ExpressionAst ExprInstructions { get; set; }
        public List<KeyValuePair<string, string>> ParameterList { get; private set; }

        public FormalParameterList FormalParameterList { get; private set; }

        #endregion

        #region Constructors

        public FunctionDeclarationAST(string id, FormalParameterList parameterList,
                                      ExpressionAst exprInstructions,
                                      string retType)
            : this(id, parameterList, exprInstructions)
        {
            ReturnTypeId = retType;
        }

        private FunctionDeclarationAST(string id, FormalParameterList parameterList,
                                      ExpressionAst exprInstructions)
        {
            FunctionId = id;
            FormalParameterList = parameterList;
            ParameterList = (parameterList??new FormalParameterList())
                            .Parameters
                            .Select(x=> new KeyValuePair<string,string>(x.Name,x.TypeIdentifier)).ToList();
            ExprInstructions = exprInstructions;
            ReturnType = TigerType.GetType<NoType>();
        }

        #endregion



        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitFunctionDeclaration(this);
        }
    }
}