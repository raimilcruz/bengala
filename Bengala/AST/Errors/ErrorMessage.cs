﻿using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using Bengala.AST.Types;

namespace Bengala.AST.Errors
{
    public class AnalysisError 
    {
        //TODO: Make protected after refactoring
        public AnalysisError(string innerMessage, int line, int column)
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
        public static AnalysisError VariableHidesAnotherOne(Declaration node) =>
            new AnalysisError(string.Format(LoadMessage("Hide"), node.Id), node.Line, node.Columns);

        public static AnalysisError VariableAlreadyDeclared(AstNode node,string variable) =>
            new AnalysisError(string.Format(LoadMessage("VarDecl"), variable), node.Line, node.Columns);

        public static AnalysisError VariableIsNotDefined(AstNode node, string variable) =>
            new AnalysisError(string.Format(LoadMessage("VarUndecl"), variable), node.Line, node.Columns);

        public static AnalysisError TypesAreNotCompatible(AstNode node,string expectedType, string actualType) =>
            new AnalysisError(
                string.Format(LoadMessage("Match"), expectedType, actualType),
                node.Line, node.Columns);

        public static AnalysisError TypeIsNotDefined(AstNode expr, string exprTypeId) =>
            new AnalysisError(string.Format(LoadMessage("TypeUndecl"), exprTypeId), expr.Line, expr.Columns);

        public static AnalysisError TypeCannotBeInferred(AstNode expr,string variableId) =>
            new AnalysisError(string.Format(LoadMessage("InferType"), variableId), expr.Line, expr.Columns);

        public static AnalysisError FunctionAlreadyDeclared(AstNode node, string functionName) =>
            new AnalysisError(string.Format(LoadMessage("FuncDecl"), functionName), node.Line,
                node.Columns);

        public static AnalysisError FunctionNotDeclared(FunctionDeclarationAST fDecl)
        {
            return new AnalysisError(
                $"It is expected that the function {fDecl.FunctionId} is at scope at this point",fDecl.Line,fDecl.Columns);
        }

        public static AnalysisError FunctionParameterAlreadyExists(FunctionDeclarationAST fDecl, KeyValuePair<string, TigerType> parameter)
        {
            return new AnalysisError(
                string.Format(AnalysisError.LoadMessage("FuncDeclParams"), parameter.Key,fDecl.FunctionId), fDecl.Line,
                fDecl.Columns);
        }
    }
}