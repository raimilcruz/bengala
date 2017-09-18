using System;
using Bengala.Analysis;
using Bengala.AST;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bengala.Tests.Static
{
    [TestClass]
    public class LetTests : CoreTest
    {
        [TestMethod]
        public void EmptyLet()
        {
            var let = Let(
                Decls(
                ),
                Seq());

            var errors = new PrinterErrorListener();
            var staticChecker = new StaticChecker(errors);

            Assert.IsTrue(let.Accept(staticChecker));
            Assert.IsTrue(errors.Count == 0);
        }

        [TestMethod]
        public void Variables1()
        {
            var let = Let(
                Decls(
                     VarDecl("x", Num(1))
                ), 
                Seq(Var("x")));

            var errors = new PrinterErrorListener();
            var staticChecker = new StaticChecker(errors);

            Assert.IsTrue(let.Accept(staticChecker) && errors.Count==0);
        }
        [TestMethod]
        public void Variables2()
        {
            var let = Let(
                Decls(
                     VarDecl("x", Num(1))
                ),
                Seq(Var("y")));

            var errors = new PrinterErrorListener();
            var staticChecker = new StaticChecker(errors);

            Assert.IsFalse(let.Accept(staticChecker));
            Assert.IsFalse(errors.Count == 0);
        }


    }
}
