using Bengala.Analysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bengala.Tests
{
    [TestClass]
    public class BinaryExpressionTests:CoreTest
    {
        [TestMethod]
        public void AddTwoNumbers()
        {
            var binAst = BinExpr(Num(1), Num(3),"+");

            var staticChecker = new StaticChecker(new PrinterErrorListener());

            Assert.IsTrue(binAst.Accept(staticChecker));
        }

        [TestMethod]
        public void SubtractNumberAndStringFails()
        {
            var binAst = BinExpr(Num(1), Str("3"), "-");

            var errorCollector = new PrinterErrorListener();
            var staticChecker = new StaticChecker(errorCollector);

            Assert.IsFalse(binAst.Accept(staticChecker) && (errorCollector.Count!=0));
        }

        [TestMethod]
        public void AddNumberAndString() {
            var binAst = BinExpr(Num(1), Str("3"), "/");

            var staticChecker = new StaticChecker(new PrinterErrorListener());

            Assert.IsFalse(binAst.Accept(staticChecker));
        }
    }
}
