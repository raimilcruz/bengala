
using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using Bengala.Antlr;
using Bengala.AST.Errors;

namespace Bengala
{
    public class BengalaParser : TigerParser
    {
        private BengalaErrorListener _errorListener;
        public BengalaParser(ITokenStream input) : base(input)
        {
            _errorListener = new BengalaErrorListener();
        }

        public void ConfigErrorListeners()
        {
            RemoveErrorListeners(); // remove ConsoleErrorListener
            AddErrorListener(_errorListener);
        }

        public IEnumerable<ErrorMessage> Errors
        {
            get { return _errorListener.Errors.Select(x=> new ErrorMessage(x,0,0)).ToList(); }
        }

        // public override string GetErrorMessage(RecognitionException e, string[] tokenNames)
        // {
        //     string msg = e.Message;
        //     Cuando no existe ninguna produccion que comienza con ese token
        //     if (e is UnwantedTokenException)
        //     {
        //         var ute = (UnwantedTokenException)e;
        //         string tokenName = "<unknown>";
        //         if (ute.Expecting == Token.EOF)
        //         {
        //             tokenName = "EOF";
        //             msg = string.Format(ErrorMessage.LoadMessage("AntlrNoMatch_1"),
        //                                 GetTokenErrorDisplay(ute.UnexpectedToken));
        //         }
        //         else
        //         {
        //             tokenName = tokenNames[ute.Expecting];
        //             msg = string.Format(ErrorMessage.LoadMessage("AntlrNoMatch_2"),
        //                                 GetTokenErrorDisplay(ute.UnexpectedToken),
        //                                 tokenName);
        //         }
        //     }
        //     Cuando falta algun simbolo(creo que esta excepcion la lanza el Lexer)
        //     else if (e is MissingTokenException)
        //     {
        //         var mte = (MissingTokenException)e;
        //         string tokenName = "<unknown>";
        //         if (mte.Expecting == Token.EOF)
        //         {
        //             tokenName = "EOF";
        //             msg = string.Format(ErrorMessage.LoadMessage("AntlrUnexpect_1"),
        //                                 GetTokenErrorDisplay(e.Token));
        //         }
        //         else
        //         {
        //             tokenName = tokenNames[mte.Expecting];
        //             msg = string.Format(ErrorMessage.LoadMessage("AntlrUnexpect_2"),
        //                                 tokenName, GetTokenErrorDisplay(e.Token));
        //         }
        //     }
        //     Cuando se encuentra un token que no corresponde, (creo que esta excepcion la lanza el Parser)
        //     else if (e is MismatchedTokenException)
        //     {
        //         var mte = (MismatchedTokenException)e;
        //         string tokenName = "<unknown>";
        //         if (mte.Expecting == Token.EOF)
        //         {
        //             tokenName = "EOF";
        //             msg = string.Format(ErrorMessage.LoadMessage("AntlrNoMatch_1"),
        //                                 GetTokenErrorDisplay(e.Token));
        //         }
        //         else if (e.Token.TokenIndex != Token.EOF)
        //         {
        //             tokenName = tokenNames[mte.Expecting];
        //             msg = string.Format(ErrorMessage.LoadMessage("AntlrNoMatch_2"),
        //                                 GetTokenErrorDisplay(e.Token), tokenName);
        //         }
        //         else
        //         {
        //             tokenName = tokenNames[mte.Expecting];
        //             msg = string.Format(ErrorMessage.LoadMessage("AntlrNoMatch_3"),
        //                                 tokenName);
        //             e.CharPositionInLine = 0;
        //             e.Line = 1;
        //         }
        //     }
        //     Desconozco cuando se lanza.
        //     else if (e is MismatchedTreeNodeException)
        //     {
        //         var mtne = (MismatchedTreeNodeException)e;
        //         string tokenName = "<unknown>";
        //         if (mtne.expecting == Token.EOF)
        //             tokenName = "EOF";
        //         else
        //             tokenName = tokenNames[mtne.expecting];
        //         The ternary operator is only necessary because of a bug in the.NET framework
        //         msg = string.Format(ErrorMessage.LoadMessage("AntlrDesconocida_1"),
        //                             ((mtne.Node != null && mtne.Node.ToString() != null)
        //                                  ?
        //                                      mtne.Node
        //                                  : string.Empty),
        //                             tokenName);
        //     }
        //     Cuando no hay alternativa posible.
        //     else if (e is NoViableAltException)
        //     {
        //         var nvae = (NoViableAltException)e;
        //         for development, can add "decision=<<" + nvae.grammarDecisionDescription + ">>"

        //         and "(decision=" + nvae.decisionNumber + ") and

        //         "state " + nvae.stateNumber
        //         if (e.Token.TokenIndex == Token.EOF)
        //             {
        //                 e.CharPositionInLine = 0;
        //                 e.Line = 1;
        //                 msg = Message.LoadMessage("AntlrMissing");
        //             }
        //             else
        //                 msg = string.Format(ErrorMessage.LoadMessage("AntlrNoMatch_1")
        //                                     , GetTokenErrorDisplay(e.Token));
        //     }
        //     Cuando se tiene la seccion de decl del let vacia.
        //     else if (e is EarlyExitException)
        //         EarlyExitException eee = (EarlyExitException)e;
        //     for development, can add "(decision=" + eee.decisionNumber + ")"

        //    msg = Message.LoadMessage("AntlrLetDeclEmpty");
        //Desconozco...
        //     else if (e is MismatchedSetException)
        //         {
        //             var mse = (MismatchedSetException)e;
        //             msg = string.Format(ErrorMessage.LoadMessage("AntlrDesconocida_2"), GetTokenErrorDisplay(e.Token),
        //                                 mse.expecting);
        //         }
        //     Desconozco..
        //     else if (e is MismatchedNotSetException)
        //     {
        //         var mse = (MismatchedNotSetException)e;
        //         msg = string.Format(ErrorMessage.LoadMessage("AntlrDesconocida_2"), GetTokenErrorDisplay(e.Token),
        //                             mse.expecting);
        //     }
        //     Desconozco...
        //     else if (e is FailedPredicateException)
        //     {
        //         var fpe = (FailedPredicateException)e;
        //         msg = string.Format(ErrorMessage.LoadMessage("AntlrDesconocida_3"), fpe.ruleName, fpe.predicateText);
        //     }
        //     return msg;
        // }

        // public override void DisplayRecognitionError(string[] tokenNames, RecognitionException e)
        // {
        //     string msg = GetErrorMessage(e, tokenNames);

        //     Errors.Add(new ErrorMessage(msg, e.Line, e.CharPositionInLine));
        // }
    }

    public class BengalaErrorListener : BaseErrorListener
    {
        public List<string> Errors { get; private set; }

        public BengalaErrorListener()
        {
            Errors = new List<string>();
        }

        public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, 
            int line, int charPositionInLine, string msg,
            RecognitionException e)
        {
            Errors.Add(msg);
            var parser = ((Parser) recognizer);

            Console.WriteLine($"Context: {parser}");
            var stack = ((Parser)recognizer).GetRuleInvocationStack();
            var reverseStack = stack.Reverse();
            Console.WriteLine("rule stack: " );
            foreach (var s in reverseStack)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("line " + line + ":" + charPositionInLine + " at " +
                               offendingSymbol + ": " + msg);
        }
    }
}