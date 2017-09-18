using System;
using Bengala.Analysis;
using Bengala.AST.SemanticsUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bengala.Tests.Static
{
    [TestClass]
    public class ArrayTests:CoreTest
    {
        [TestMethod]
        public void BasicArrayDeclaration()
        {
            var arrayDecl = ArrayDecl("intArray", "int");

            var let = Let(
                         Decls(arrayDecl),
                         Seq()
                      );

            var errorCollector = new PrinterErrorListener();
            var staticChecker = new StaticChecker(errorCollector);

            Assert.IsTrue(let.Accept(staticChecker));
            Assert.IsTrue(errorCollector.Count == 0);
        }
        [TestMethod]
        public void BasicArrayDeclarationFails()
        {
            var arrayDecl = ArrayDecl("intArray", "unknowntype");

            var let = Let(
                         Decls(arrayDecl),
                         Seq()
                      );

            var errorCollector = new PrinterErrorListener();
            var staticChecker = new StaticChecker(errorCollector);

            Assert.IsFalse(let.Accept(staticChecker));
            Assert.IsFalse(errorCollector.Count == 0);
        }
        [TestMethod]
        public void BasicArrayInstantiation()
        {
            var arrayDecl = ArrayDecl("intArray", "int");
            var arrayInstDecl = VarDecl("a", ArrayInst("intArray", Num(1), Num(5)));

            var let = Let(
                         Decls(arrayDecl, arrayInstDecl),
                         Seq()
                      );

            var errorCollector = new PrinterErrorListener();
            var staticChecker = new StaticChecker(errorCollector);

            Assert.IsTrue(let.Accept(staticChecker));
            Assert.IsTrue(errorCollector.Count == 0);
        }
        [TestMethod]
        public void BasicArrayInstantiationFail()
        {
            var arrayInstDecl = VarDecl("a", ArrayInst("intArray", Num(1), Num(5)));

            var let = Let(
                         Decls(arrayInstDecl),
                         Seq()
                      );

            var errorCollector = new PrinterErrorListener();
            var staticChecker = new StaticChecker(errorCollector);

            Assert.IsFalse(let.Accept(staticChecker));
            Assert.IsFalse(errorCollector.Count == 0);
        }

        [TestMethod]
        public void BasicArrayAccess()
        {
            var arrayDecl = ArrayDecl("intArray", "int");
            var arrayInstDecl = VarDecl("a", ArrayInst("intArray", Num(1), Num(5)));

            var let = Let(
                         Decls(arrayDecl, arrayInstDecl),
                         Seq(ArrayAccess(Var("a"), Num(0)))
                      );

            var errorCollector = new PrinterErrorListener();
            var staticChecker = new StaticChecker(errorCollector);

            Assert.IsTrue(let.Accept(staticChecker));
            Assert.IsTrue(errorCollector.Count == 0);
            Assert.IsTrue(let.ReturnType == TigerType.GetType<IntType>());
        }
        
    }
}
