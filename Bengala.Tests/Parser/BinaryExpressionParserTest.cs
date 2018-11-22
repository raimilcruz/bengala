using Bengala.AST;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bengala.Tests.Parser
{
    [TestClass]
    public class BinaryExpressionParserTest:CoreParserTest
    {

        [TestMethod]
        public void AddTwoNumbers()
        {
            var program = "1 + 2";
            var ast = ParseText(program);

            var other = BinExpr(Num(1), Num(2), "+");

            Assert.IsTrue(AstComparer.EqualNodes(ast, other));
        }
        [TestMethod]
        public void AddThreeNumbers()
        {
            var program = "1 + 2 - 3";
            var ast = ParseText(program);

            var other = BinExpr(BinExpr(Num(1), Num(2),"+"), Num(3),"-");

            Assert.IsTrue(AstComparer.EqualNodes(ast, other),"Binary expression must be equal");
        }

        [TestMethod]
        public void AddTwoVars()
        {
            var program = "y + x";
            var ast = ParseText(program);

            var other = BinExpr(Var("y"), Var("x"), "+");

            Assert.IsTrue(AstComparer.EqualNodes(ast, other));
        }

        [TestMethod]
        public void GreaterThanExp()
        {
            var program = "2 > 1";
            var ast = ParseText(program);

            var other = BinExpr(Num(2), Num(1), ">");

            Assert.IsTrue(AstComparer.EqualNodes(ast, other));
        }
        [TestMethod]
        public void OrExp()
        {
            var program = "2 | 1";
            var ast = ParseText(program);

            var other = BinExpr(Num(2), Num(1), "|");

            Assert.IsTrue(AstComparer.EqualNodes(ast, other));
        }
        [TestMethod]
        public void EqExp()
        {
            var program = "2 = 1";
            var ast = ParseText(program);

            var other = BinExpr(Num(2), Num(1), "=");

            Assert.IsTrue(AstComparer.EqualNodes(ast, other));
        }
        [TestMethod]
        public void BinExprWithFunCall()
        {
            var program = "2 + f(2)";
            var ast = ParseText(program);

            var other = BinExpr(Num(2), FCall("f",Num(2)), "+");

            Assert.IsTrue(AstComparer.EqualNodes(ast, other));
        }
        [TestMethod]
        public void BinExprWithNegExpr()
        {
            var program = "-2 + 1";
            var ast = ParseText(program);

            var other = BinExpr(Neq(Num(2)), Num(1), "+");

            Assert.IsTrue(AstComparer.EqualNodes(ast, other));
        }

      
    }
}
