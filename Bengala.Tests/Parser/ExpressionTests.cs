using Bengala.AST;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bengala.Tests.Parser
{
    [TestClass]
    public class ExpressionTests:CoreParserTest
    {

        [TestMethod]
        public void RecordInstance()
        {
            var program = "let type person = { name : string , age : int } " +
                          "in person{name = 1, age=2} end";
            var ast = ParseText(program);

            var other = Let(
                Decls(RecordType("person",
                    Fp("name", "string"),
                    Fp("age", "int"))),
                Seq(Record("person",
                        FieldIns("name",Num(1)),
                        FieldIns("age", Num(2)))));

            Assert.IsTrue(AstComparer.EqualNodes(ast, other));
        }

        [TestMethod]
        public void FunctionCall()
        {
            var program = "f(3)";
            var ast = ParseText(program);

            var other = FCall("f",Num(3));

            Assert.IsTrue(AstComparer.EqualNodes(ast, other));
        }

        [TestMethod]
        public void RecordAccess()
        {
            var program = "p.age";
            var ast = ParseText(program);

            var other = RecordAccess(Var("p"),"age");

            Assert.IsTrue(AstComparer.EqualNodes(ast, other));
        }
        [TestMethod]
        public void SequenceExpression()
        {
            var program = "(p;2)";
            var ast = ParseText(program);

            var other = Seq(Var("p"), Num(2));

            Assert.IsTrue(AstComparer.EqualNodes(ast, other));
        }
    }
}
