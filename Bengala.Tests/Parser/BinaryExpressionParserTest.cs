using Antlr.Runtime;
using Bengala.AST;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bengala.Tests.Parser
{
    [TestClass]
    public class BinaryExpressionParserTest:CoreParserTest
    {

        //[TestMethod]
        //public void AddTwoNumbers() {
        //    var program = "1 + 2";
        //    var ast = parseText(program);

        //    var other = BinExpr(Num(1), Num(2), "+");

        //    Assert.IsTrue(AstComparer.EqualNodes(ast,other));            
        //}

        //[TestMethod]
        //public void AddTwoVars()
        //{
        //    var program = "y + x";
        //    var ast = parseText(program);

        //    var other = BinExpr(Var("y"), Var("x"), "+");

        //    Assert.IsTrue(AstComparer.EqualNodes(ast, other));
        //}

        //[TestMethod]
        //public void GreaterThanExp()
        //{
        //    var program = "2 > 1";
        //    var ast = parseText(program);

        //    var other = BinExpr(Num(2), Num(1), ">");

        //    Assert.IsTrue(AstComparer.EqualNodes(ast, other));
        //}
    }
}
