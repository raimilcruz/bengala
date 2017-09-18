using System;
using Bengala.Analysis;
using Bengala.AST;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bengala.Tests.Static
{
    [TestClass]
    public class FunctionDeclarationcs : CoreTest
    {
        [TestMethod]
        public void FunctionWithParameters()
        {
            var functionAst = Fun("add",
               Parameters(
                   Param("a", "int"),
                   Param("b", "int")
               ),
               BinExpr(Var("a"), Var("b"), "+"),
               "int");

            var let = Let(
                         Decls(functionAst),
                         Seq()
                      );

            var errorCollector = new PrinterErrorListener();
            var staticChecker = new StaticChecker(errorCollector);

            Assert.IsTrue(let.Accept(staticChecker));
            Assert.IsTrue(errorCollector.Count ==0);
        }
        [TestMethod]
        public void FunctionWithRepeatedParameters()
        {
            var functionAst = Fun("add",
               Parameters(
                   Param("a", "int"),
                   Param("a", "int")
               ),
               BinExpr(Var("a"), Var("a"), "+"),
               "int");

            var let = Let(
                         Decls(functionAst),
                         Seq()
                      );

            var errorCollector = new PrinterErrorListener();
            var staticChecker = new StaticChecker(errorCollector);

            Assert.IsFalse(let.Accept(staticChecker));
            Assert.IsFalse(errorCollector.Count == 0);
        }
    }
}
