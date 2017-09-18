using Antlr.Runtime;
using Bengala.AST;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bengala.Tests.Parser
{
    public class CoreParserTest:CoreTest
    {
        //public AstNode parseText(string text)
        //{
        //    var stm = new StreamReader(GenerateStreamFromString(text));
        //    var reader = new ANTLRReaderStream(stm);
        //    var lexer = new BengalaLexer(reader);
        //    var cm = new CommonTokenStream(lexer);
        //    var parser = new BengalaParser(lexer.Errors, cm);

        //    return parser.program();
        //}
        //public Stream GenerateStreamFromString(string s)
        //{
        //    MemoryStream stream = new MemoryStream();
        //    StreamWriter writer = new StreamWriter(stream);
        //    writer.Write(s);
        //    writer.Flush();
        //    stream.Position = 0;
        //    return stream;
        //}        
    }
}
