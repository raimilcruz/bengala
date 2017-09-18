using Bengala.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bengala.Tests
{
    public class CoreTest
    {
        protected IntLiteral Num(int n) {
            return new IntLiteral(n);
        }
        protected StringLiteral Str(string s) {
            return new StringLiteral(s, 1, 1);
        }

        protected VarAST Var(String s)
        {
            return new VarAST(s, 1, 1);
        }

        protected List<Declaration> Decls(params Declaration[] declarations)
        {
            return declarations.ToList();
        }

        protected VarDeclarationAST VarDecl(String s, ExpressionAST expr)
        {
            return new VarDeclarationAST(s,expr);
        }
        protected SequenceExpressionAST Seq(params ExpressionAST[] expressions )
        {
            return new SequenceExpressionAST(expressions.ToList());
        }
        protected LetExpressionAST Let(List<Declaration> declarations, SequenceExpressionAST body)
        {
            return new LetExpressionAST(declarations,body);
        }


        protected FunctionDeclarationAST Fun(string name, List<KeyValuePair<string, string>> parameters,ExpressionAST body,String retType)
        {
            return new FunctionDeclarationAST(name,parameters, body, retType);
        }

        protected List<KeyValuePair<string, string>> Parameters(params KeyValuePair<string, string>[] parameters)
        {
            return parameters.ToList();
        }

        protected KeyValuePair<string, string> Param(string name, string type)
        {
            return new KeyValuePair<string, string>(name, type);
        }

        protected BinaryExpressionAST BinExpr(ExpressionAST l,ExpressionAST r,string op) {
            return new BinaryExpressionAST(l,r,op);
        }

        protected IfExpressionAST If(ExpressionAST c, ExpressionAST l, ExpressionAST r)
        {
            return new IfExpressionAST(c, l, r);
        }
    }
}
