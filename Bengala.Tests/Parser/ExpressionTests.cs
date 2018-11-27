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
    }
}
