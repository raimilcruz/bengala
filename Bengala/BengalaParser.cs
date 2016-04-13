#region Usings

using System.Collections.Generic;
using Antlr.Runtime;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala
{
    internal class BengalaParser : tigerParser
    {
        public BengalaParser(List<Message> errorsLexer, ITokenStream input) : base(input)
        {
            Errors = errorsLexer;
        }

        public List<Message> Errors { get; private set; }

        public override string GetErrorMessage(RecognitionException e, string[] tokenNames)
        {
            string msg = e.Message;
            //Cuando no existe ninguna produccion que comienza con ese token
            if (e is UnwantedTokenException)
            {
                var ute = (UnwantedTokenException) e;
                string tokenName = "<unknown>";
                if (ute.Expecting == Token.EOF)
                {
                    tokenName = "EOF";
                    msg = string.Format(Message.LoadMessage("AntlrNoMatch_1"),
                                        GetTokenErrorDisplay(ute.UnexpectedToken));
                }
                else
                {
                    tokenName = tokenNames[ute.Expecting];
                    msg = string.Format(Message.LoadMessage("AntlrNoMatch_2"),
                                        GetTokenErrorDisplay(ute.UnexpectedToken),
                                        tokenName);
                }
            }
                //Cuando falta algun simbolo (creo que esta excepcion la lanza el Lexer)
            else if (e is MissingTokenException)
            {
                var mte = (MissingTokenException) e;
                string tokenName = "<unknown>";
                if (mte.Expecting == Token.EOF)
                {
                    tokenName = "EOF";
                    msg = string.Format(Message.LoadMessage("AntlrUnexpect_1"),
                                        GetTokenErrorDisplay(e.Token));
                }
                else
                {
                    tokenName = tokenNames[mte.Expecting];
                    msg = string.Format(Message.LoadMessage("AntlrUnexpect_2"),
                                        tokenName, GetTokenErrorDisplay(e.Token));
                }
            }
                //Cuando se encuentra un token que no corresponde, (creo que esta excepcion la lanza el Parser)
            else if (e is MismatchedTokenException)
            {
                var mte = (MismatchedTokenException) e;
                string tokenName = "<unknown>";
                if (mte.Expecting == Token.EOF)
                {
                    tokenName = "EOF";
                    msg = string.Format(Message.LoadMessage("AntlrNoMatch_1"),
                                        GetTokenErrorDisplay(e.Token));
                }
                else if (e.Token.TokenIndex != Token.EOF)
                {
                    tokenName = tokenNames[mte.Expecting];
                    msg = string.Format(Message.LoadMessage("AntlrNoMatch_2"),
                                        GetTokenErrorDisplay(e.Token), tokenName);
                }
                else
                {
                    tokenName = tokenNames[mte.Expecting];
                    msg = string.Format(Message.LoadMessage("AntlrNoMatch_3"),
                                        tokenName);
                    e.CharPositionInLine = 0;
                    e.Line = 1;
                }
            }
                //Desconozco cuando se lanza.
            else if (e is MismatchedTreeNodeException)
            {
                var mtne = (MismatchedTreeNodeException) e;
                string tokenName = "<unknown>";
                if (mtne.expecting == Token.EOF)
                    tokenName = "EOF";
                else
                    tokenName = tokenNames[mtne.expecting];
                // The ternary operator is only necessary because of a bug in the .NET framework
                msg = string.Format(Message.LoadMessage("AntlrDesconocida_1"),
                                    ((mtne.Node != null && mtne.Node.ToString() != null)
                                         ?
                                             mtne.Node
                                         : string.Empty),
                                    tokenName);
            }
                //Cuando no hay alternativa posible.
            else if (e is NoViableAltException)
            {
                var nvae = (NoViableAltException) e;
                // for development, can add "decision=<<"+nvae.grammarDecisionDescription+">>"
                // and "(decision="+nvae.decisionNumber+") and
                // "state "+nvae.stateNumber                     
                if (e.Token.TokenIndex == Token.EOF)
                {
                    e.CharPositionInLine = 0;
                    e.Line = 1;
                    msg = Message.LoadMessage("AntlrMissing");
                }
                else
                    msg = string.Format(Message.LoadMessage("AntlrNoMatch_1")
                                        , GetTokenErrorDisplay(e.Token));
            }
                //Cuando se tiene la seccion de decl del let vacia.
            else if (e is EarlyExitException)
                //EarlyExitException eee = (EarlyExitException)e;
                // for development, can add "(decision="+eee.decisionNumber+")"                
                msg = Message.LoadMessage("AntlrLetDeclEmpty");
                //Desconozco...
            else if (e is MismatchedSetException)
            {
                var mse = (MismatchedSetException) e;
                msg = string.Format(Message.LoadMessage("AntlrDesconocida_2"), GetTokenErrorDisplay(e.Token),
                                    mse.expecting);
            }
                //Desconozco..
            else if (e is MismatchedNotSetException)
            {
                var mse = (MismatchedNotSetException) e;
                msg = string.Format(Message.LoadMessage("AntlrDesconocida_2"), GetTokenErrorDisplay(e.Token),
                                    mse.expecting);
            }
                //Desconozco...
            else if (e is FailedPredicateException)
            {
                var fpe = (FailedPredicateException) e;
                msg = string.Format(Message.LoadMessage("AntlrDesconocida_3"), fpe.ruleName, fpe.predicateText);
            }
            return msg;
        }

        public override void DisplayRecognitionError(string[] tokenNames, RecognitionException e)
        {
            string msg = GetErrorMessage(e, tokenNames);

            Errors.Add(new ErrorMessage(msg, e.Line, e.CharPositionInLine));
        }
    }
}