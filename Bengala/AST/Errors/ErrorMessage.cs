using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Bengala.AST.Types;

namespace Bengala.AST.Errors
{
    public class ErrorMessage 
    {
        //TODO: Make protected after refactoring
        public ErrorMessage(string innerMessage, int line, int column)
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
    
        //TODO: Specify ErrorCode as Warning
        public static ErrorMessage VariableHidesAnotherOne(Declaration node) =>
            new ErrorMessage(string.Format(LoadMessage("Hide"), node.Id), node.Line, node.Columns);

        public static ErrorMessage VariableAlreadyDeclared(AstNode node,string variable) =>
            new ErrorMessage(string.Format(LoadMessage("VarDecl"), variable), node.Line, node.Columns);

        public static ErrorMessage VariableIsNotDefined(AstNode node, string variable) =>
            new ErrorMessage(string.Format(LoadMessage("VarUndecl"), variable), node.Line, node.Columns);

        public static ErrorMessage TypesAreNotCompatible(AstNode node,string expectedType, string actualType) =>
            new ErrorMessage(
                string.Format(LoadMessage("Match"), expectedType, actualType),
                node.Line, node.Columns);

        public static ErrorMessage TypeIsNotDefined(AstNode expr, string exprTypeId) =>
            new ErrorMessage(string.Format(LoadMessage("TypeUndecl"), exprTypeId), expr.Line, expr.Columns);

        public static ErrorMessage TypeCannotBeInferred(AstNode expr,string variableId) =>
            new ErrorMessage(string.Format(LoadMessage("InferType"), variableId), expr.Line, expr.Columns);

        public static ErrorMessage FunctionAlreadyDeclared(AstNode node, string functionName) =>
            new ErrorMessage(string.Format(LoadMessage("FuncDecl"), functionName), node.Line,
                node.Columns);

        public static ErrorMessage FunctionNotDeclared(FunctionDeclarationAST fDecl)
        {
            return new ErrorMessage(
                $"It is expected that the function {fDecl.FunctionId} is at scope at this point",fDecl.Line,fDecl.Columns);
        }

        public static ErrorMessage FunctionParameterAlreadyExists(FunctionDeclarationAST fDecl, KeyValuePair<string, TigerType> parameter)
        {
            return new ErrorMessage(
                string.Format(ErrorMessage.LoadMessage("FuncDeclParams"), parameter.Key,fDecl.FunctionId), fDecl.Line,
                fDecl.Columns);
        }
    }
}