using Bengala.AST;
using System.IO;
using Antlr4.Runtime;
using Bengala.Antlr;

namespace Bengala.Tests.Parser
{
    public class CoreParserTest : CoreTest
    {
        protected AstNode ParseText(string text)
        {
            var s = new StreamReader(GenerateStreamFromString(text));
            var stm = new AntlrInputStream(s); ;
            var lexer = new TigerLexer(stm);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new TigerParser(tokenStream);
            var expContext = parser.program();

            var contextVisitor = new BuildAstVisitor();
            return expContext.Accept(contextVisitor);
        }
        private static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
