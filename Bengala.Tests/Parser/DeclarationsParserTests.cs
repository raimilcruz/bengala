using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bengala.Tests.Parser
{
    [TestClass]
    public class DeclarationsParserTests : CoreParserTest
    {
        [TestMethod]
        public void LetWithOneVarDeclaration()
        {
            var program = "let var i := 5 in 2 end";
            var ast = ParseText(program);

            var other = Let(
                            Decls(VarDecl("i",Num(5))),
                            Seq(Num(2)));

            Assert.IsTrue(AstComparer.EqualNodes(ast, other));
        }
        [TestMethod]
        public void LetWithOneAliasTypeDeclaration()
        {
            var program = "let type myInt = int in 2 end";
            var ast = ParseText(program);

            var other = Let(
                Decls(Alias("myInt", "int")),
                Seq(Num(2)));

            Assert.IsTrue(AstComparer.EqualNodes(ast, other));
        }
        [TestMethod]
        public void LetWithOneArrayTypeDeclaration()
        {
            var program = "let type myArray = array of int in 2 end";
            var ast = ParseText(program);

            var other = Let(
                Decls(ArrayDecl("myArray", "int")),
                Seq(Num(2)));

            Assert.IsTrue(AstComparer.EqualNodes(ast, other));
        }
        [TestMethod]
        public void LetWithOneRecordTypeDeclaration()
        {
            var program = "let type person = { name : string , age : int } in 2 end";
            var ast = ParseText(program);

            var other = Let(
                Decls(RecordType("person", 
                        Fp("name","string"),
                        Fp("age","int"))),
                Seq(Num(2)));

            Assert.IsTrue(AstComparer.EqualNodes(ast, other));
        }
        [TestMethod]
        public void LetWithOneFunctionDeclaration()
        {
            var program = "let " +
                            "function fakeSum(a: int, b:int) : int = 1" +
                          "in 2 " +
                          "end";
            var ast = ParseText(program);

            var other = Let(
                Decls(FDecl("fakeSum",
                       Num(1),
                       "int",
                        Fp("a", "int"),
                        Fp("b", "int"))),
                Seq(Num(2)));

            Assert.IsTrue(AstComparer.EqualNodes(ast, other));
        }
    }
}
