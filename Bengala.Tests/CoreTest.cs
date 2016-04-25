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
        protected IntAST Num(int n) {
            return new IntAST(n);
        }
        protected StringAST Str(string s) {
            return new StringAST(s, 1, 1);
        }

        protected VarAST Var(String s)
        {
            return new VarAST(s, 1, 1);
        }

        protected BinaryExpressionAST BinExpr(ExpressionAST l,ExpressionAST r,string op) {
            return BinaryExpressionAST.GetBinaryExpressionAST(l, r, op, 1, 1);
        }

        protected IfExpressionAST If(ExpressionAST c, ExpressionAST l, ExpressionAST r)
        {
            return new IfExpressionAST(c, l, r);
        }
    }
}
