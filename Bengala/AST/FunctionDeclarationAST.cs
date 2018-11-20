#region Usings

using System;
using System.Collections.Generic;
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
    public class FunctionDeclarationAST : Declaration
    {
        #region Fields and Properties

        public string FunctionId { get; private set; }
        public string ReturnTypeId { get; private set; }
        public ExpressionAST ExprInstructions { get; set; }
        public List<KeyValuePair<string, string>> ParameterList { get; private set; }

        #endregion

        #region Constructors

        public FunctionDeclarationAST(string id, List<KeyValuePair<string, string>> parameterList,
                                      ExpressionAST exprInstructions,
                                      string retType)
            : this(id, parameterList, exprInstructions)
        {
            ReturnTypeId = retType;
        }

        public FunctionDeclarationAST(string id, List<KeyValuePair<string, string>> parameterList,
                                      ExpressionAST exprInstructions,
                                      string retType, int line, int col)
            : this(id, parameterList, exprInstructions)
        {
            Line = line;
            Columns = col;
            ReturnTypeId = retType;
        }

        private FunctionDeclarationAST(string id, List<KeyValuePair<string, string>> parameterList,
                                      ExpressionAST exprInstructions)
        {
            FunctionId = id;
            ParameterList = parameterList??new List<KeyValuePair<string, string>>();
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