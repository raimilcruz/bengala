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
            return new ArrayDeclarationAST(arrayTypeId, baseTypeId,0,0);
        }

        protected ArrayInstatiationAST ArrayInst(string arrayTypeId, ExpressionAst sizeExpr,ExpressionAst initExpr)
        {
            return new ArrayInstatiationAST(arrayTypeId, sizeExpr, initExpr, 0, 0);
        }

        protected ArrayAccessAST ArrayAccess(ExpressionAst arrayExpr, ExpressionAst indexExpr)
        {
            return new ArrayAccessAST(arrayExpr, indexExpr);
        }

        protected FunctionDeclarationAST Fun(string name, List<KeyValuePair<string, string>> parameters,ExpressionAst body,String retType)
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

        protected BinaryExpressionAst BinExpr(ExpressionAst l,ExpressionAst r,string op) {
            return new BinaryExpressionAst(l,r,op);
        }

        protected IfExpressionAST If(ExpressionAst c, ExpressionAst l, ExpressionAst r)
        {
            return new IfExpressionAST(c, l, r);
        }
    }
}
