using Bengala.AST;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bengala.Tests.Parser
{
    [TestClass]
    public class InstructionTest:CoreParserTest
    {

        [TestMethod]
        public void For()
        {
            var program = "for i := 1 to n do 2";
            var ast = ParseText(program);

            var other = For("i",
                    Num(1),
                    Var("n"),
                    Num(2));

            Assert.IsTrue(AstComparer.EqualNodes(ast, other));
        }

        [TestMethod]
        public void While()
        {
            var program = "while n do 2";
            var ast = ParseText(program);

            var other = While(
                            Var("n"),
                            Num(2));

            Assert.IsTrue(AstComparer.EqualNodes(ast, other));
        }
        [TestMethod]
        public void BreakInstruction()
        {
            var program = "break";
            var ast = ParseText(program);

            var other = Break();

            Assert.IsTrue(AstComparer.EqualNodes(ast, other));
        }
        [TestMethod]
        public void Assignment()
        {
            var program = "i := 1";
            var ast = ParseText(program);

            var other = Assign(Var("i"),Num(1));

            Assert.IsTrue(AstComparer.EqualNodes(ast, other));
        }

        [TestMethod]
        public void ArrayInstance()
        {
            //arrays are second-citizens
            var program = "nums[4] of 0";
            var ast = ParseText(program);

            var other = ArrayInst("nums",Num(4),Num(0));

            Assert.IsTrue(AstComparer.EqualNodes(ast, other));
        }
    }
}
