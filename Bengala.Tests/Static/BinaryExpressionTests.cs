using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bengala.AST;

namespace Bengala.Tests
{
    [TestClass]
    public class BinaryExpressionTests:CoreTest
    {
        [TestMethod]
        public void AddTwoNumbers()
        {
            var binAst = BinExpr(Num(1), Num(3),"+");

            StaticSemanticsChecker staticChecker = new StaticSemanticsChecker(new PrinterErrorListener());

            Assert.IsTrue(binAst.Accept(staticChecker));
        }
        [TestMethod]
        public void AddNumberAndString() {
            var binAst = BinExpr(Num(1), Str("3"), "/");

            StaticSemanticsChecker staticChecker = new StaticSemanticsChecker(new PrinterErrorListener());

            Assert.IsFalse(binAst.Accept(staticChecker));
        }
    }
}
