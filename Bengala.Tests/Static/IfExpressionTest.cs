using Bengala.AST;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bengala.Analysis;

namespace Bengala.Tests.Static
{
    [TestClass]
    public class IfExpressionTest : CoreTest
    {
        [TestMethod]
        public void SimpleIf()
        {
            var ast = If(BinExpr(Num(1),Num(1),">"), Num(2), Num(3));

            var errorListener = new PrinterErrorListener();

            StaticChecker staticChecker = new StaticChecker(errorListener);

            Assert.IsTrue(ast.Accept(staticChecker) && errorListener.Count == 0);
        }
    }
}
