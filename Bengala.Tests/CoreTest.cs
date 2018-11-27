using Bengala.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bengala.AST.SemanticsUtils;
using Bengala.AST.Utils;

namespace Bengala.Tests
{
    public class CoreTest
    {
        protected Scope InitialScope()
        {
            var scope = new Scope(null);
            var scopeInitializator = new ScopeInitializator();
            scopeInitializator.InitScope(scope);
            return scope;
        }

        protected IntLiteral Num(int n) {
            return new IntLiteral(n);
        }
        protected ExpressionAst Neq(ExpressionAst expression)
        {
            return new NegExpressionAST(expression);
        }
        protected StringLiteral Str(string s) {
            return new StringLiteral(s);
        }

        protected VarAST Var(string s)
        {
            return new VarAST(s);
        }

        protected List<Declaration> Decls(params Declaration[] declarations)
        {
            return declarations.ToList();
        }

        protected VarDeclarationAST VarDecl(String s, ExpressionAst expr)
        {
            return new VarDeclarationAST(s,expr);
        }
        protected AliasAST Alias(string id, string referTo)
        {
            return new AliasAST(id, referTo);
        }
        protected SequenceExpressionAST Seq(params ExpressionAst[] expressions )
        {
            return new SequenceExpressionAST(expressions.ToList());
        }
        protected LetExpressionAST Let(List<Declaration> declarations, SequenceExpressionAST body)
        {
            return new LetExpressionAST(declarations,body);
        }

        protected ArrayDeclarationAST ArrayDecl(string arrayTypeId, string baseTypeId)
        {
            return new ArrayDeclarationAST(arrayTypeId, baseTypeId);
        }
        protected RecordDeclarationAST RecordType(string recordType, params FormalParameter[] parameters)
        {
            return new RecordDeclarationAST(recordType, 
                new FormalParameterList(parameters.ToList()));
        }
        protected FormalParameter Fp(string name,string typeId)
        {
            return new FormalParameter(name,typeId);
        }
        protected RecordInstantiationAST Record(string recordType, params FieldInstance[] fields)
        {
            return new RecordInstantiationAST(recordType,
                new FieldInstanceList(fields.ToList()));
        }
        protected FieldInstance FieldIns(string field, ExpressionAst value)
        {
            return new FieldInstance(field,value);
        }

        protected FunctionDeclarationAST FDecl(string functionId, ExpressionAst body, string returnType,  
            params FormalParameter [] parameters)
        {
            return new FunctionDeclarationAST(functionId, new FormalParameterList(parameters.ToList()), body,returnType);
        }


        protected ArrayInstatiationAST ArrayInst(string arrayTypeId, ExpressionAst sizeExpr,ExpressionAst initExpr)
        {
            return new ArrayInstatiationAST(arrayTypeId, sizeExpr, initExpr, 0, 0);
        }

        protected ArrayAccessAST ArrayAccess(ExpressionAst arrayExpr, ExpressionAst indexExpr)
        {
            return new ArrayAccessAST(arrayExpr, indexExpr);
        }

        protected FunctionDeclarationAST Fun(string name, List<KeyValuePair<string, string>> parameters,ExpressionAst body,
            string retType)
        {
            return new FunctionDeclarationAST(name,
                new FormalParameterList(parameters.Select(x=> new FormalParameter(x.Key,x.Value)).ToList()), body, retType);
        }
        protected ExpressionAst FCall(string function, params ExpressionAst[] args)
        {
            return new CallFunctionAST(function,new ArgumentList(args.ToList()));
        }

        protected List<KeyValuePair<string, string>> Parameters(params KeyValuePair<string, string>[] parameters)
        {
            return parameters.ToList();
        }

        protected KeyValuePair<string, string> Param(string name, string type)
        {
            return new KeyValuePair<string, string>(name, type);
        }

        protected BinaryExpressionAst BinExpr(ExpressionAst l,ExpressionAst r,string op) {
            return new BinaryExpressionAst(l,r,op);
        }

        protected IfExpressionAST If(ExpressionAst c, ExpressionAst l, ExpressionAst r)
        {
            return new IfExpressionAST(c, l, r);
        }
    }
}
