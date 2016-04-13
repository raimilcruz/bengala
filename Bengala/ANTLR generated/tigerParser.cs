// $ANTLR 3.1.1 C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g 2009-04-02 14:05:17

	using Bengala.AST;
	using Bengala.AST.SemanticsUtils;
	using System.Collections.Generic;


using System;
using Antlr.Runtime;
using IList 		= System.Collections.IList;
using ArrayList 	= System.Collections.ArrayList;
using Stack 		= Antlr.Runtime.Collections.StackList;

using IDictionary	= System.Collections.IDictionary;
using Hashtable 	= System.Collections.Hashtable;

public partial class tigerParser : Parser
{
    public static readonly string[] tokenNames = new string[] 
	{
        "<invalid>", 
		"<EOR>", 
		"<DOWN>", 
		"<UP>", 
		"END", 
		"WHILE", 
		"DO", 
		"FOR", 
		"IF", 
		"THEN", 
		"ELSE", 
		"FUNCTION", 
		"ARRAY", 
		"OF", 
		"LET", 
		"IN", 
		"TYPE", 
		"NIL", 
		"TO", 
		"BREAK", 
		"VAR", 
		"INT", 
		"STRING", 
		"WS", 
		"CHAR", 
		"DIGIT", 
		"ID", 
		"PLUS", 
		"MINUS", 
		"ASTER", 
		"SLASH", 
		"POW", 
		"MOD", 
		"LPAREN", 
		"RPAREN", 
		"LKEY", 
		"RKEY", 
		"LBRACKET", 
		"RBRACKET", 
		"EQUAL", 
		"DISTINC", 
		"GT", 
		"LT", 
		"GE", 
		"LE", 
		"NOT", 
		"AND", 
		"OR", 
		"ASSIGN", 
		"DOT", 
		"COLON", 
		"COMMA", 
		"SEMICOLON", 
		"QUOTE", 
		"CHAR_STRING", 
		"INTCONST", 
		"STRINGCOMPONENTS", 
		"STRINGCONST", 
		"LINECOMENTS", 
		"MULTILINECOMENTS"
    };

    public const int FUNCTION = 11;
    public const int CHAR_STRING = 54;
    public const int LT = 42;
    public const int WHILE = 5;
    public const int MULTILINECOMENTS = 59;
    public const int INTCONST = 55;
    public const int MOD = 32;
    public const int CHAR = 24;
    public const int DO = 6;
    public const int FOR = 7;
    public const int NOT = 45;
    public const int AND = 46;
    public const int ID = 26;
    public const int EOF = -1;
    public const int BREAK = 19;
    public const int LPAREN = 33;
    public const int TYPE = 16;
    public const int IF = 8;
    public const int LBRACKET = 37;
    public const int STRINGCOMPONENTS = 56;
    public const int QUOTE = 53;
    public const int RPAREN = 34;
    public const int SLASH = 30;
    public const int IN = 15;
    public const int THEN = 9;
    public const int POW = 31;
    public const int DISTINC = 40;
    public const int COMMA = 51;
    public const int EQUAL = 39;
    public const int RKEY = 36;
    public const int STRINGCONST = 57;
    public const int PLUS = 27;
    public const int VAR = 20;
    public const int DIGIT = 25;
    public const int RBRACKET = 38;
    public const int DOT = 49;
    public const int ARRAY = 12;
    public const int GE = 43;
    public const int LINECOMENTS = 58;
    public const int TO = 18;
    public const int ELSE = 10;
    public const int ASTER = 29;
    public const int SEMICOLON = 52;
    public const int INT = 21;
    public const int MINUS = 28;
    public const int LKEY = 35;
    public const int OF = 13;
    public const int COLON = 50;
    public const int WS = 23;
    public const int NIL = 17;
    public const int OR = 47;
    public const int ASSIGN = 48;
    public const int GT = 41;
    public const int END = 4;
    public const int LE = 44;
    public const int LET = 14;
    public const int STRING = 22;

    // delegates
    // delegators



        public tigerParser(ITokenStream input)
    		: this(input, new RecognizerSharedState()) {
        }

        public tigerParser(ITokenStream input, RecognizerSharedState state)
    		: base(input, state) {
            InitializeCyclicDFAs();

             
        }
        

    override public string[] TokenNames {
		get { return tigerParser.tokenNames; }
    }

    override public string GrammarFileName {
		get { return "C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g"; }
    }



    // $ANTLR start "program"
    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:109:1: program returns [ExpressionAST res] : e= exp EOF ;
    public ExpressionAST program() // throws RecognitionException [1]
    {   
        ExpressionAST res = default(ExpressionAST);

        ExpressionAST e = default(ExpressionAST);


        try 
    	{
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:109:44: (e= exp EOF )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:109:46: e= exp EOF
            {
            	PushFollow(FOLLOW_exp_in_program1145);
            	e = exp();
            	state.followingStackPointer--;
            	if (state.failed) return res;
            	if ( state.backtracking == 0 ) 
            	{
            	  res = e;
            	}
            	Match(input,EOF,FOLLOW_EOF_in_program1149); if (state.failed) return res;

            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return res;
    }
    // $ANTLR end "program"


    // $ANTLR start "exp"
    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:112:1: exp returns [ExpressionAST res] : (ifE= ifExp | forE= forExp | let= letExp | ( lvalue ASSIGN )=>lv= lvalue s= ASSIGN initExp= exp | ( ID LBRACKET ( exp )? RBRACKET OF )=>id= ID LBRACKET sizeExp= exp RBRACKET OF initExp= exp | whileE= whileInstr | breakE= breakInstr | record= recordInstance | e= expOrAnd );
    public ExpressionAST exp() // throws RecognitionException [1]
    {   
        ExpressionAST res = default(ExpressionAST);

        IToken s = null;
        IToken id = null;
        ExpressionAST ifE = default(ExpressionAST);

        ExpressionAST forE = default(ExpressionAST);

        ExpressionAST let = default(ExpressionAST);

        LValueAST lv = default(LValueAST);

        ExpressionAST initExp = default(ExpressionAST);

        ExpressionAST sizeExp = default(ExpressionAST);

        ExpressionAST whileE = default(ExpressionAST);

        ExpressionAST breakE = default(ExpressionAST);

        ExpressionAST record = default(ExpressionAST);

        ExpressionAST e = default(ExpressionAST);


        try 
    	{
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:112:42: (ifE= ifExp | forE= forExp | let= letExp | ( lvalue ASSIGN )=>lv= lvalue s= ASSIGN initExp= exp | ( ID LBRACKET ( exp )? RBRACKET OF )=>id= ID LBRACKET sizeExp= exp RBRACKET OF initExp= exp | whileE= whileInstr | breakE= breakInstr | record= recordInstance | e= expOrAnd )
            int alt1 = 9;
            alt1 = dfa1.Predict(input);
            switch (alt1) 
            {
                case 1 :
                    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:112:44: ifE= ifExp
                    {
                    	PushFollow(FOLLOW_ifExp_in_exp1175);
                    	ifE = ifExp();
                    	state.followingStackPointer--;
                    	if (state.failed) return res;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	  res = ifE;
                    	}

                    }
                    break;
                case 2 :
                    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:113:9: forE= forExp
                    {
                    	PushFollow(FOLLOW_forExp_in_exp1191);
                    	forE = forExp();
                    	state.followingStackPointer--;
                    	if (state.failed) return res;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	  res = forE;
                    	}

                    }
                    break;
                case 3 :
                    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:114:9: let= letExp
                    {
                    	PushFollow(FOLLOW_letExp_in_exp1206);
                    	let = letExp();
                    	state.followingStackPointer--;
                    	if (state.failed) return res;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	  res = let;
                    	}

                    }
                    break;
                case 4 :
                    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:115:9: ( lvalue ASSIGN )=>lv= lvalue s= ASSIGN initExp= exp
                    {
                    	PushFollow(FOLLOW_lvalue_in_exp1227);
                    	lv = lvalue();
                    	state.followingStackPointer--;
                    	if (state.failed) return res;
                    	s=(IToken)Match(input,ASSIGN,FOLLOW_ASSIGN_in_exp1233); if (state.failed) return res;
                    	PushFollow(FOLLOW_exp_in_exp1239);
                    	initExp = exp();
                    	state.followingStackPointer--;
                    	if (state.failed) return res;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	  res = new AssignExpressionAST(lv,initExp, s.Line, s.CharPositionInLine);
                    	}

                    }
                    break;
                case 5 :
                    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:116:9: ( ID LBRACKET ( exp )? RBRACKET OF )=>id= ID LBRACKET sizeExp= exp RBRACKET OF initExp= exp
                    {
                    	id=(IToken)Match(input,ID,FOLLOW_ID_in_exp1271); if (state.failed) return res;
                    	Match(input,LBRACKET,FOLLOW_LBRACKET_in_exp1273); if (state.failed) return res;
                    	PushFollow(FOLLOW_exp_in_exp1280);
                    	sizeExp = exp();
                    	state.followingStackPointer--;
                    	if (state.failed) return res;
                    	Match(input,RBRACKET,FOLLOW_RBRACKET_in_exp1282); if (state.failed) return res;
                    	Match(input,OF,FOLLOW_OF_in_exp1284); if (state.failed) return res;
                    	PushFollow(FOLLOW_exp_in_exp1289);
                    	initExp = exp();
                    	state.followingStackPointer--;
                    	if (state.failed) return res;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	  res = new ArrayInstatiationAST(id.Text,sizeExp,initExp, id.Line, id.CharPositionInLine);
                    	}

                    }
                    break;
                case 6 :
                    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:117:9: whileE= whileInstr
                    {
                    	PushFollow(FOLLOW_whileInstr_in_exp1306);
                    	whileE = whileInstr();
                    	state.followingStackPointer--;
                    	if (state.failed) return res;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	  res = whileE;
                    	}

                    }
                    break;
                case 7 :
                    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:118:9: breakE= breakInstr
                    {
                    	PushFollow(FOLLOW_breakInstr_in_exp1322);
                    	breakE = breakInstr();
                    	state.followingStackPointer--;
                    	if (state.failed) return res;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	  res = breakE;
                    	}

                    }
                    break;
                case 8 :
                    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:119:9: record= recordInstance
                    {
                    	PushFollow(FOLLOW_recordInstance_in_exp1338);
                    	record = recordInstance();
                    	state.followingStackPointer--;
                    	if (state.failed) return res;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	  res =record;
                    	}

                    }
                    break;
                case 9 :
                    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:120:9: e= expOrAnd
                    {
                    	PushFollow(FOLLOW_expOrAnd_in_exp1353);
                    	e = expOrAnd();
                    	state.followingStackPointer--;
                    	if (state.failed) return res;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	  res = e;
                    	}

                    }
                    break;

            }
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return res;
    }
    // $ANTLR end "exp"


    // $ANTLR start "expOrAnd"
    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:124:1: expOrAnd returns [ExpressionAST res] : e1= expCOMP ( (s= AND | s= OR ) e2= expCOMP )* ;
    public ExpressionAST expOrAnd() // throws RecognitionException [1]
    {   
        ExpressionAST res = default(ExpressionAST);

        IToken s = null;
        ExpressionAST e1 = default(ExpressionAST);

        ExpressionAST e2 = default(ExpressionAST);


        try 
    	{
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:124:40: (e1= expCOMP ( (s= AND | s= OR ) e2= expCOMP )* )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:124:42: e1= expCOMP ( (s= AND | s= OR ) e2= expCOMP )*
            {
            	PushFollow(FOLLOW_expCOMP_in_expOrAnd1398);
            	e1 = expCOMP();
            	state.followingStackPointer--;
            	if (state.failed) return res;
            	if ( state.backtracking == 0 ) 
            	{
            	  res= e1;
            	}
            	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:124:66: ( (s= AND | s= OR ) e2= expCOMP )*
            	do 
            	{
            	    int alt3 = 2;
            	    alt3 = dfa3.Predict(input);
            	    switch (alt3) 
            		{
            			case 1 :
            			    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:125:15: (s= AND | s= OR ) e2= expCOMP
            			    {
            			    	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:125:15: (s= AND | s= OR )
            			    	int alt2 = 2;
            			    	int LA2_0 = input.LA(1);

            			    	if ( (LA2_0 == AND) )
            			    	{
            			    	    alt2 = 1;
            			    	}
            			    	else if ( (LA2_0 == OR) )
            			    	{
            			    	    alt2 = 2;
            			    	}
            			    	else 
            			    	{
            			    	    if ( state.backtracking > 0 ) {state.failed = true; return res;}
            			    	    NoViableAltException nvae_d2s0 =
            			    	        new NoViableAltException("", 2, 0, input);

            			    	    throw nvae_d2s0;
            			    	}
            			    	switch (alt2) 
            			    	{
            			    	    case 1 :
            			    	        // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:125:16: s= AND
            			    	        {
            			    	        	s=(IToken)Match(input,AND,FOLLOW_AND_in_expOrAnd1424); if (state.failed) return res;

            			    	        }
            			    	        break;
            			    	    case 2 :
            			    	        // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:126:22: s= OR
            			    	        {
            			    	        	s=(IToken)Match(input,OR,FOLLOW_OR_in_expOrAnd1451); if (state.failed) return res;

            			    	        }
            			    	        break;

            			    	}

            			    	PushFollow(FOLLOW_expCOMP_in_expOrAnd1462);
            			    	e2 = expCOMP();
            			    	state.followingStackPointer--;
            			    	if (state.failed) return res;
            			    	if ( state.backtracking == 0 ) 
            			    	{
            			    	    res = LogicalExpressionAST.GetLogicalExpressionAST(res,e2,s.Text, s.Line, s.CharPositionInLine);
            			    	}

            			    }
            			    break;

            			default:
            			    goto loop3;
            	    }
            	} while (true);

            	loop3:
            		;	// Stops C# compiler whining that label 'loop3' has no statements


            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return res;
    }
    // $ANTLR end "expOrAnd"


    // $ANTLR start "expCOMP"
    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:129:1: expCOMP returns [ExpressionAST res] : e1= expEQ ( (s= GT | s= LT | s= GE | s= LE ) e2= expEQ )? ;
    public ExpressionAST expCOMP() // throws RecognitionException [1]
    {   
        ExpressionAST res = default(ExpressionAST);

        IToken s = null;
        ExpressionAST e1 = default(ExpressionAST);

        ExpressionAST e2 = default(ExpressionAST);


        try 
    	{
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:129:40: (e1= expEQ ( (s= GT | s= LT | s= GE | s= LE ) e2= expEQ )? )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:129:42: e1= expEQ ( (s= GT | s= LT | s= GE | s= LE ) e2= expEQ )?
            {
            	PushFollow(FOLLOW_expEQ_in_expCOMP1543);
            	e1 = expEQ();
            	state.followingStackPointer--;
            	if (state.failed) return res;
            	if ( state.backtracking == 0 ) 
            	{
            	  res = e1;
            	}
            	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:129:66: ( (s= GT | s= LT | s= GE | s= LE ) e2= expEQ )?
            	int alt5 = 2;
            	alt5 = dfa5.Predict(input);
            	switch (alt5) 
            	{
            	    case 1 :
            	        // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:130:77: (s= GT | s= LT | s= GE | s= LE ) e2= expEQ
            	        {
            	        	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:130:77: (s= GT | s= LT | s= GE | s= LE )
            	        	int alt4 = 4;
            	        	switch ( input.LA(1) ) 
            	        	{
            	        	case GT:
            	        		{
            	        	    alt4 = 1;
            	        	    }
            	        	    break;
            	        	case LT:
            	        		{
            	        	    alt4 = 2;
            	        	    }
            	        	    break;
            	        	case GE:
            	        		{
            	        	    alt4 = 3;
            	        	    }
            	        	    break;
            	        	case LE:
            	        		{
            	        	    alt4 = 4;
            	        	    }
            	        	    break;
            	        		default:
            	        		    if ( state.backtracking > 0 ) {state.failed = true; return res;}
            	        		    NoViableAltException nvae_d4s0 =
            	        		        new NoViableAltException("", 4, 0, input);

            	        		    throw nvae_d4s0;
            	        	}

            	        	switch (alt4) 
            	        	{
            	        	    case 1 :
            	        	        // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:130:78: s= GT
            	        	        {
            	        	        	s=(IToken)Match(input,GT,FOLLOW_GT_in_expCOMP1632); if (state.failed) return res;

            	        	        }
            	        	        break;
            	        	    case 2 :
            	        	        // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:131:15: s= LT
            	        	        {
            	        	        	s=(IToken)Match(input,LT,FOLLOW_LT_in_expCOMP1652); if (state.failed) return res;

            	        	        }
            	        	        break;
            	        	    case 3 :
            	        	        // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:132:15: s= GE
            	        	        {
            	        	        	s=(IToken)Match(input,GE,FOLLOW_GE_in_expCOMP1683); if (state.failed) return res;

            	        	        }
            	        	        break;
            	        	    case 4 :
            	        	        // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:133:15: s= LE
            	        	        {
            	        	        	s=(IToken)Match(input,LE,FOLLOW_LE_in_expCOMP1703); if (state.failed) return res;

            	        	        }
            	        	        break;

            	        	}

            	        	PushFollow(FOLLOW_expEQ_in_expCOMP1713);
            	        	e2 = expEQ();
            	        	state.followingStackPointer--;
            	        	if (state.failed) return res;
            	        	if ( state.backtracking == 0 ) 
            	        	{
            	        	    res = BinaryExpressionAST.GetBinaryExpressionAST(res,e2,s.Text, s.Line, s.CharPositionInLine);
            	        	}

            	        }
            	        break;

            	}


            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return res;
    }
    // $ANTLR end "expCOMP"


    // $ANTLR start "expEQ"
    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:136:1: expEQ returns [ExpressionAST res] : e1= expNE (s= EQUAL e2= expNE )? ;
    public ExpressionAST expEQ() // throws RecognitionException [1]
    {   
        ExpressionAST res = default(ExpressionAST);

        IToken s = null;
        ExpressionAST e1 = default(ExpressionAST);

        ExpressionAST e2 = default(ExpressionAST);


        try 
    	{
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:136:39: (e1= expNE (s= EQUAL e2= expNE )? )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:136:41: e1= expNE (s= EQUAL e2= expNE )?
            {
            	PushFollow(FOLLOW_expNE_in_expEQ1765);
            	e1 = expNE();
            	state.followingStackPointer--;
            	if (state.failed) return res;
            	if ( state.backtracking == 0 ) 
            	{
            	  res = e1;
            	}
            	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:136:65: (s= EQUAL e2= expNE )?
            	int alt6 = 2;
            	alt6 = dfa6.Predict(input);
            	switch (alt6) 
            	{
            	    case 1 :
            	        // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:137:15: s= EQUAL e2= expNE
            	        {
            	        	s=(IToken)Match(input,EQUAL,FOLLOW_EQUAL_in_expEQ1791); if (state.failed) return res;
            	        	PushFollow(FOLLOW_expNE_in_expEQ1798);
            	        	e2 = expNE();
            	        	state.followingStackPointer--;
            	        	if (state.failed) return res;
            	        	if ( state.backtracking == 0 ) 
            	        	{
            	        	    res = BinaryExpressionAST.GetBinaryExpressionAST(res,e2,s.Text, s.Line, s.CharPositionInLine);
            	        	}

            	        }
            	        break;

            	}


            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return res;
    }
    // $ANTLR end "expEQ"


    // $ANTLR start "expNE"
    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:140:1: expNE returns [ExpressionAST res] : e1= expSumRes (s= DISTINC e2= expSumRes )? ;
    public ExpressionAST expNE() // throws RecognitionException [1]
    {   
        ExpressionAST res = default(ExpressionAST);

        IToken s = null;
        ExpressionAST e1 = default(ExpressionAST);

        ExpressionAST e2 = default(ExpressionAST);


        try 
    	{
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:140:39: (e1= expSumRes (s= DISTINC e2= expSumRes )? )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:140:41: e1= expSumRes (s= DISTINC e2= expSumRes )?
            {
            	PushFollow(FOLLOW_expSumRes_in_expNE1978);
            	e1 = expSumRes();
            	state.followingStackPointer--;
            	if (state.failed) return res;
            	if ( state.backtracking == 0 ) 
            	{
            	  res = e1;
            	}
            	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:140:66: (s= DISTINC e2= expSumRes )?
            	int alt7 = 2;
            	alt7 = dfa7.Predict(input);
            	switch (alt7) 
            	{
            	    case 1 :
            	        // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:141:15: s= DISTINC e2= expSumRes
            	        {
            	        	s=(IToken)Match(input,DISTINC,FOLLOW_DISTINC_in_expNE2001); if (state.failed) return res;
            	        	PushFollow(FOLLOW_expSumRes_in_expNE2006);
            	        	e2 = expSumRes();
            	        	state.followingStackPointer--;
            	        	if (state.failed) return res;
            	        	if ( state.backtracking == 0 ) 
            	        	{
            	        	    res = BinaryExpressionAST.GetBinaryExpressionAST(res,e2,s.Text, s.Line, s.CharPositionInLine);
            	        	}

            	        }
            	        break;

            	}


            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return res;
    }
    // $ANTLR end "expNE"


    // $ANTLR start "expSumRes"
    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:144:1: expSumRes returns [ExpressionAST res] : e1= expPorDiv ( (s= MINUS | s= PLUS ) e2= expPorDiv )* ;
    public ExpressionAST expSumRes() // throws RecognitionException [1]
    {   
        ExpressionAST res = default(ExpressionAST);

        IToken s = null;
        ExpressionAST e1 = default(ExpressionAST);

        ExpressionAST e2 = default(ExpressionAST);


        try 
    	{
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:144:40: (e1= expPorDiv ( (s= MINUS | s= PLUS ) e2= expPorDiv )* )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:144:42: e1= expPorDiv ( (s= MINUS | s= PLUS ) e2= expPorDiv )*
            {
            	PushFollow(FOLLOW_expPorDiv_in_expSumRes2066);
            	e1 = expPorDiv();
            	state.followingStackPointer--;
            	if (state.failed) return res;
            	if ( state.backtracking == 0 ) 
            	{
            	  res =e1;
            	}
            	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:144:67: ( (s= MINUS | s= PLUS ) e2= expPorDiv )*
            	do 
            	{
            	    int alt9 = 2;
            	    alt9 = dfa9.Predict(input);
            	    switch (alt9) 
            		{
            			case 1 :
            			    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:145:14: (s= MINUS | s= PLUS ) e2= expPorDiv
            			    {
            			    	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:145:14: (s= MINUS | s= PLUS )
            			    	int alt8 = 2;
            			    	int LA8_0 = input.LA(1);

            			    	if ( (LA8_0 == MINUS) )
            			    	{
            			    	    alt8 = 1;
            			    	}
            			    	else if ( (LA8_0 == PLUS) )
            			    	{
            			    	    alt8 = 2;
            			    	}
            			    	else 
            			    	{
            			    	    if ( state.backtracking > 0 ) {state.failed = true; return res;}
            			    	    NoViableAltException nvae_d8s0 =
            			    	        new NoViableAltException("", 8, 0, input);

            			    	    throw nvae_d8s0;
            			    	}
            			    	switch (alt8) 
            			    	{
            			    	    case 1 :
            			    	        // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:145:16: s= MINUS
            			    	        {
            			    	        	s=(IToken)Match(input,MINUS,FOLLOW_MINUS_in_expSumRes2091); if (state.failed) return res;

            			    	        }
            			    	        break;
            			    	    case 2 :
            			    	        // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:146:15: s= PLUS
            			    	        {
            			    	        	s=(IToken)Match(input,PLUS,FOLLOW_PLUS_in_expSumRes2114); if (state.failed) return res;

            			    	        }
            			    	        break;

            			    	}

            			    	PushFollow(FOLLOW_expPorDiv_in_expSumRes2137);
            			    	e2 = expPorDiv();
            			    	state.followingStackPointer--;
            			    	if (state.failed) return res;
            			    	if ( state.backtracking == 0 ) 
            			    	{
            			    	    res = BinaryExpressionAST.GetBinaryExpressionAST(res,e2,s.Text, s.Line, s.CharPositionInLine);
            			    	}

            			    }
            			    break;

            			default:
            			    goto loop9;
            	    }
            	} while (true);

            	loop9:
            		;	// Stops C# compiler whining that label 'loop9' has no statements


            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return res;
    }
    // $ANTLR end "expSumRes"


    // $ANTLR start "expPorDiv"
    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:150:1: expPorDiv returns [ExpressionAST res] : e1= expMod ( (s= SLASH | s= ASTER ) e2= expMod )* ;
    public ExpressionAST expPorDiv() // throws RecognitionException [1]
    {   
        ExpressionAST res = default(ExpressionAST);

        IToken s = null;
        ExpressionAST e1 = default(ExpressionAST);

        ExpressionAST e2 = default(ExpressionAST);


        try 
    	{
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:150:40: (e1= expMod ( (s= SLASH | s= ASTER ) e2= expMod )* )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:150:42: e1= expMod ( (s= SLASH | s= ASTER ) e2= expMod )*
            {
            	PushFollow(FOLLOW_expMod_in_expPorDiv2184);
            	e1 = expMod();
            	state.followingStackPointer--;
            	if (state.failed) return res;
            	if ( state.backtracking == 0 ) 
            	{
            	  res = e1;
            	}
            	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:150:67: ( (s= SLASH | s= ASTER ) e2= expMod )*
            	do 
            	{
            	    int alt11 = 2;
            	    alt11 = dfa11.Predict(input);
            	    switch (alt11) 
            		{
            			case 1 :
            			    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:150:68: (s= SLASH | s= ASTER ) e2= expMod
            			    {
            			    	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:150:68: (s= SLASH | s= ASTER )
            			    	int alt10 = 2;
            			    	int LA10_0 = input.LA(1);

            			    	if ( (LA10_0 == SLASH) )
            			    	{
            			    	    alt10 = 1;
            			    	}
            			    	else if ( (LA10_0 == ASTER) )
            			    	{
            			    	    alt10 = 2;
            			    	}
            			    	else 
            			    	{
            			    	    if ( state.backtracking > 0 ) {state.failed = true; return res;}
            			    	    NoViableAltException nvae_d10s0 =
            			    	        new NoViableAltException("", 10, 0, input);

            			    	    throw nvae_d10s0;
            			    	}
            			    	switch (alt10) 
            			    	{
            			    	    case 1 :
            			    	        // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:151:15: s= SLASH
            			    	        {
            			    	        	s=(IToken)Match(input,SLASH,FOLLOW_SLASH_in_expPorDiv2211); if (state.failed) return res;

            			    	        }
            			    	        break;
            			    	    case 2 :
            			    	        // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:152:15: s= ASTER
            			    	        {
            			    	        	s=(IToken)Match(input,ASTER,FOLLOW_ASTER_in_expPorDiv2233); if (state.failed) return res;

            			    	        }
            			    	        break;

            			    	}

            			    	PushFollow(FOLLOW_expMod_in_expPorDiv2241);
            			    	e2 = expMod();
            			    	state.followingStackPointer--;
            			    	if (state.failed) return res;
            			    	if ( state.backtracking == 0 ) 
            			    	{
            			    	    res = BinaryExpressionAST.GetBinaryExpressionAST(res,e2,s.Text, s.Line, s.CharPositionInLine);
            			    	}

            			    }
            			    break;

            			default:
            			    goto loop11;
            	    }
            	} while (true);

            	loop11:
            		;	// Stops C# compiler whining that label 'loop11' has no statements


            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return res;
    }
    // $ANTLR end "expPorDiv"


    // $ANTLR start "expMod"
    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:155:1: expMod returns [ExpressionAST res] : f= factor (s= MOD e2= factor )? ;
    public ExpressionAST expMod() // throws RecognitionException [1]
    {   
        ExpressionAST res = default(ExpressionAST);

        IToken s = null;
        ExpressionAST f = default(ExpressionAST);

        ExpressionAST e2 = default(ExpressionAST);


        try 
    	{
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:155:48: (f= factor (s= MOD e2= factor )? )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:155:50: f= factor (s= MOD e2= factor )?
            {
            	PushFollow(FOLLOW_factor_in_expMod2303);
            	f = factor();
            	state.followingStackPointer--;
            	if (state.failed) return res;
            	if ( state.backtracking == 0 ) 
            	{
            	  res = f;
            	}
            	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:155:75: (s= MOD e2= factor )?
            	int alt12 = 2;
            	alt12 = dfa12.Predict(input);
            	switch (alt12) 
            	{
            	    case 1 :
            	        // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:156:15: s= MOD e2= factor
            	        {
            	        	s=(IToken)Match(input,MOD,FOLLOW_MOD_in_expMod2330); if (state.failed) return res;
            	        	PushFollow(FOLLOW_factor_in_expMod2339);
            	        	e2 = factor();
            	        	state.followingStackPointer--;
            	        	if (state.failed) return res;
            	        	if ( state.backtracking == 0 ) 
            	        	{
            	        	    res = BinaryExpressionAST.GetBinaryExpressionAST(res,e2,s.Text, s.Line, s.CharPositionInLine);
            	        	}

            	        }
            	        break;

            	}


            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return res;
    }
    // $ANTLR end "expMod"


    // $ANTLR start "factor"
    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:159:1: factor returns [ExpressionAST res] : ( ( (m= MINUS )? f= fExp ) | n= NIL );
    public ExpressionAST factor() // throws RecognitionException [1]
    {   
        ExpressionAST res = default(ExpressionAST);

        IToken m = null;
        IToken n = null;
        ExpressionAST f = default(ExpressionAST);


        try 
    	{
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:159:40: ( ( (m= MINUS )? f= fExp ) | n= NIL )
            int alt14 = 2;
            int LA14_0 = input.LA(1);

            if ( (LA14_0 == ID || LA14_0 == MINUS || LA14_0 == LPAREN || LA14_0 == INTCONST || LA14_0 == STRINGCONST) )
            {
                alt14 = 1;
            }
            else if ( (LA14_0 == NIL) )
            {
                alt14 = 2;
            }
            else 
            {
                if ( state.backtracking > 0 ) {state.failed = true; return res;}
                NoViableAltException nvae_d14s0 =
                    new NoViableAltException("", 14, 0, input);

                throw nvae_d14s0;
            }
            switch (alt14) 
            {
                case 1 :
                    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:159:68: ( (m= MINUS )? f= fExp )
                    {
                    	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:159:68: ( (m= MINUS )? f= fExp )
                    	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:159:70: (m= MINUS )? f= fExp
                    	{
                    		// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:159:72: (m= MINUS )?
                    		int alt13 = 2;
                    		int LA13_0 = input.LA(1);

                    		if ( (LA13_0 == MINUS) )
                    		{
                    		    alt13 = 1;
                    		}
                    		switch (alt13) 
                    		{
                    		    case 1 :
                    		        // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:159:72: m= MINUS
                    		        {
                    		        	m=(IToken)Match(input,MINUS,FOLLOW_MINUS_in_factor2422); if (state.failed) return res;

                    		        }
                    		        break;

                    		}

                    		PushFollow(FOLLOW_fExp_in_factor2430);
                    		f = fExp();
                    		state.followingStackPointer--;
                    		if (state.failed) return res;
                    		if ( state.backtracking == 0 ) 
                    		{

                    		                                                                                                             if(m!=null)
                    		  													       res= new NegExpressionAST(f, m.Line, m.CharPositionInLine);
                    		  								                                           else
                    		  								                                               res = f;
                    		  								                                         
                    		}

                    	}


                    }
                    break;
                case 2 :
                    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:165:24: n= NIL
                    {
                    	n=(IToken)Match(input,NIL,FOLLOW_NIL_in_factor2468); if (state.failed) return res;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	  res = new NilAST(n.Line, n.CharPositionInLine);
                    	}

                    }
                    break;

            }
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return res;
    }
    // $ANTLR end "factor"


    // $ANTLR start "seqExp"
    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:167:1: seqExp returns [List<ExpressionAST> parametros] : LPAREN (temp= exp ( SEMICOLON temp1= exp )* )? RPAREN ;
    public List<ExpressionAST> seqExp() // throws RecognitionException [1]
    {   
        List<ExpressionAST> parametros = default(List<ExpressionAST>);

        ExpressionAST temp = default(ExpressionAST);

        ExpressionAST temp1 = default(ExpressionAST);



        	       parametros = new List<ExpressionAST>();
        	   
        try 
    	{
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:172:10: ( LPAREN (temp= exp ( SEMICOLON temp1= exp )* )? RPAREN )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:172:12: LPAREN (temp= exp ( SEMICOLON temp1= exp )* )? RPAREN
            {
            	Match(input,LPAREN,FOLLOW_LPAREN_in_seqExp2566); if (state.failed) return parametros;
            	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:172:19: (temp= exp ( SEMICOLON temp1= exp )* )?
            	int alt16 = 2;
            	alt16 = dfa16.Predict(input);
            	switch (alt16) 
            	{
            	    case 1 :
            	        // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:172:20: temp= exp ( SEMICOLON temp1= exp )*
            	        {
            	        	PushFollow(FOLLOW_exp_in_seqExp2572);
            	        	temp = exp();
            	        	state.followingStackPointer--;
            	        	if (state.failed) return parametros;
            	        	if ( state.backtracking == 0 ) 
            	        	{
            	        	  parametros.Add(temp);
            	        	}
            	        	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:172:53: ( SEMICOLON temp1= exp )*
            	        	do 
            	        	{
            	        	    int alt15 = 2;
            	        	    int LA15_0 = input.LA(1);

            	        	    if ( (LA15_0 == SEMICOLON) )
            	        	    {
            	        	        alt15 = 1;
            	        	    }


            	        	    switch (alt15) 
            	        		{
            	        			case 1 :
            	        			    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:172:54: SEMICOLON temp1= exp
            	        			    {
            	        			    	Match(input,SEMICOLON,FOLLOW_SEMICOLON_in_seqExp2576); if (state.failed) return parametros;
            	        			    	PushFollow(FOLLOW_exp_in_seqExp2582);
            	        			    	temp1 = exp();
            	        			    	state.followingStackPointer--;
            	        			    	if (state.failed) return parametros;
            	        			    	if ( state.backtracking == 0 ) 
            	        			    	{
            	        			    	  parametros.Add(temp1);
            	        			    	}

            	        			    }
            	        			    break;

            	        			default:
            	        			    goto loop15;
            	        	    }
            	        	} while (true);

            	        	loop15:
            	        		;	// Stops C# compiler whining that label 'loop15' has no statements


            	        }
            	        break;

            	}

            	Match(input,RPAREN,FOLLOW_RPAREN_in_seqExp2589); if (state.failed) return parametros;

            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return parametros;
    }
    // $ANTLR end "seqExp"


    // $ANTLR start "fExp"
    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:174:1: fExp returns [ExpressionAST res] : (i= INTCONST | s= STRINGCONST | seq_exp= seqExp | id= ID LPAREN (argList= listExp )? RPAREN | l_value= lvalue );
    public ExpressionAST fExp() // throws RecognitionException [1]
    {   
        ExpressionAST res = default(ExpressionAST);

        IToken i = null;
        IToken s = null;
        IToken id = null;
        List<ExpressionAST> seq_exp = default(List<ExpressionAST>);

        List<ExpressionAST> argList = default(List<ExpressionAST>);

        LValueAST l_value = default(LValueAST);



        	      argList =new  List<ExpressionAST>(); 
        	   
        try 
    	{
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:179:7: (i= INTCONST | s= STRINGCONST | seq_exp= seqExp | id= ID LPAREN (argList= listExp )? RPAREN | l_value= lvalue )
            int alt18 = 5;
            alt18 = dfa18.Predict(input);
            switch (alt18) 
            {
                case 1 :
                    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:179:9: i= INTCONST
                    {
                    	i=(IToken)Match(input,INTCONST,FOLLOW_INTCONST_in_fExp2637); if (state.failed) return res;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	  res = new IntAST(int.Parse(i.Text), i.Line, i.CharPositionInLine);
                    	}

                    }
                    break;
                case 2 :
                    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:180:9: s= STRINGCONST
                    {
                    	s=(IToken)Match(input,STRINGCONST,FOLLOW_STRINGCONST_in_fExp2654); if (state.failed) return res;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	  res = new StringAST(s.Text,s.Line, s.CharPositionInLine);
                    	}

                    }
                    break;
                case 3 :
                    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:181:9: seq_exp= seqExp
                    {
                    	PushFollow(FOLLOW_seqExp_in_fExp2670);
                    	seq_exp = seqExp();
                    	state.followingStackPointer--;
                    	if (state.failed) return res;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	  res = new SequenceExpressionAST(seq_exp);
                    	}

                    }
                    break;
                case 4 :
                    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:183:9: id= ID LPAREN (argList= listExp )? RPAREN
                    {
                    	id=(IToken)Match(input,ID,FOLLOW_ID_in_fExp2693); if (state.failed) return res;
                    	Match(input,LPAREN,FOLLOW_LPAREN_in_fExp2695); if (state.failed) return res;
                    	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:183:32: (argList= listExp )?
                    	int alt17 = 2;
                    	alt17 = dfa17.Predict(input);
                    	switch (alt17) 
                    	{
                    	    case 1 :
                    	        // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:183:32: argList= listExp
                    	        {
                    	        	PushFollow(FOLLOW_listExp_in_fExp2701);
                    	        	argList = listExp();
                    	        	state.followingStackPointer--;
                    	        	if (state.failed) return res;

                    	        }
                    	        break;

                    	}

                    	if ( state.backtracking == 0 ) 
                    	{
                    	  res = new CallFunctionAST(id.Text,argList, id.Line, id.CharPositionInLine);
                    	}
                    	Match(input,RPAREN,FOLLOW_RPAREN_in_fExp2706); if (state.failed) return res;

                    }
                    break;
                case 5 :
                    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:185:9: l_value= lvalue
                    {
                    	PushFollow(FOLLOW_lvalue_in_fExp2723);
                    	l_value = lvalue();
                    	state.followingStackPointer--;
                    	if (state.failed) return res;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	  res = l_value ;
                    	}

                    }
                    break;

            }
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return res;
    }
    // $ANTLR end "fExp"


    // $ANTLR start "listExp"
    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:188:1: listExp returns [List<ExpressionAST> res] : temp= exp ( COMMA temp1= exp )* ;
    public List<ExpressionAST> listExp() // throws RecognitionException [1]
    {   
        List<ExpressionAST> res = default(List<ExpressionAST>);

        ExpressionAST temp = default(ExpressionAST);

        ExpressionAST temp1 = default(ExpressionAST);



        	       res = new List<ExpressionAST>();
        	   
        try 
    	{
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:193:7: (temp= exp ( COMMA temp1= exp )* )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:193:8: temp= exp ( COMMA temp1= exp )*
            {
            	PushFollow(FOLLOW_exp_in_listExp2773);
            	temp = exp();
            	state.followingStackPointer--;
            	if (state.failed) return res;
            	if ( state.backtracking == 0 ) 
            	{
            	  res.Add(temp);
            	}
            	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:193:36: ( COMMA temp1= exp )*
            	do 
            	{
            	    int alt19 = 2;
            	    int LA19_0 = input.LA(1);

            	    if ( (LA19_0 == COMMA) )
            	    {
            	        alt19 = 1;
            	    }


            	    switch (alt19) 
            		{
            			case 1 :
            			    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:193:37: COMMA temp1= exp
            			    {
            			    	Match(input,COMMA,FOLLOW_COMMA_in_listExp2777); if (state.failed) return res;
            			    	PushFollow(FOLLOW_exp_in_listExp2782);
            			    	temp1 = exp();
            			    	state.followingStackPointer--;
            			    	if (state.failed) return res;
            			    	if ( state.backtracking == 0 ) 
            			    	{
            			    	  res.Add(temp1);
            			    	}

            			    }
            			    break;

            			default:
            			    goto loop19;
            	    }
            	} while (true);

            	loop19:
            		;	// Stops C# compiler whining that label 'loop19' has no statements


            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return res;
    }
    // $ANTLR end "listExp"


    // $ANTLR start "ifExp"
    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:195:1: ifExp returns [ExpressionAST res] : i= IF cond= exp THEN e1= exp ( ELSE e2= exp )? ;
    public ExpressionAST ifExp() // throws RecognitionException [1]
    {   
        ExpressionAST res = default(ExpressionAST);

        IToken i = null;
        ExpressionAST cond = default(ExpressionAST);

        ExpressionAST e1 = default(ExpressionAST);

        ExpressionAST e2 = default(ExpressionAST);


        try 
    	{
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:195:39: (i= IF cond= exp THEN e1= exp ( ELSE e2= exp )? )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:195:40: i= IF cond= exp THEN e1= exp ( ELSE e2= exp )?
            {
            	i=(IToken)Match(input,IF,FOLLOW_IF_in_ifExp2804); if (state.failed) return res;
            	PushFollow(FOLLOW_exp_in_ifExp2809);
            	cond = exp();
            	state.followingStackPointer--;
            	if (state.failed) return res;
            	Match(input,THEN,FOLLOW_THEN_in_ifExp2811); if (state.failed) return res;
            	PushFollow(FOLLOW_exp_in_ifExp2815);
            	e1 = exp();
            	state.followingStackPointer--;
            	if (state.failed) return res;
            	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:195:69: ( ELSE e2= exp )?
            	int alt20 = 2;
            	alt20 = dfa20.Predict(input);
            	switch (alt20) 
            	{
            	    case 1 :
            	        // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:195:70: ELSE e2= exp
            	        {
            	        	Match(input,ELSE,FOLLOW_ELSE_in_ifExp2818); if (state.failed) return res;
            	        	PushFollow(FOLLOW_exp_in_ifExp2822);
            	        	e2 = exp();
            	        	state.followingStackPointer--;
            	        	if (state.failed) return res;

            	        }
            	        break;

            	}

            	if ( state.backtracking == 0 ) 
            	{
            	  res = new IfExpressionAST(cond,e1,e2, i.Line, i.CharPositionInLine);
            	}

            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return res;
    }
    // $ANTLR end "ifExp"


    // $ANTLR start "forExp"
    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:197:1: forExp returns [ExpressionAST res] : f= FOR id= ID ASSIGN e1= exp TO e2= exp DO e3= exp ;
    public ExpressionAST forExp() // throws RecognitionException [1]
    {   
        ExpressionAST res = default(ExpressionAST);

        IToken f = null;
        IToken id = null;
        ExpressionAST e1 = default(ExpressionAST);

        ExpressionAST e2 = default(ExpressionAST);

        ExpressionAST e3 = default(ExpressionAST);


        try 
    	{
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:197:40: (f= FOR id= ID ASSIGN e1= exp TO e2= exp DO e3= exp )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:197:41: f= FOR id= ID ASSIGN e1= exp TO e2= exp DO e3= exp
            {
            	f=(IToken)Match(input,FOR,FOLLOW_FOR_in_forExp2846); if (state.failed) return res;
            	id=(IToken)Match(input,ID,FOLLOW_ID_in_forExp2851); if (state.failed) return res;
            	Match(input,ASSIGN,FOLLOW_ASSIGN_in_forExp2853); if (state.failed) return res;
            	PushFollow(FOLLOW_exp_in_forExp2857);
            	e1 = exp();
            	state.followingStackPointer--;
            	if (state.failed) return res;
            	Match(input,TO,FOLLOW_TO_in_forExp2860); if (state.failed) return res;
            	PushFollow(FOLLOW_exp_in_forExp2864);
            	e2 = exp();
            	state.followingStackPointer--;
            	if (state.failed) return res;
            	Match(input,DO,FOLLOW_DO_in_forExp2866); if (state.failed) return res;
            	PushFollow(FOLLOW_exp_in_forExp2870);
            	e3 = exp();
            	state.followingStackPointer--;
            	if (state.failed) return res;
            	if ( state.backtracking == 0 ) 
            	{
            	  res = new ForExpressionAST(id.Text,e1,e2,e3, f.Line, f.CharPositionInLine);
            	}

            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return res;
    }
    // $ANTLR end "forExp"


    // $ANTLR start "letExp"
    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:199:1: letExp returns [ExpressionAST res] : l= LET (d= decl )+ IN (insts= instructions )? END ;
    public ExpressionAST letExp() // throws RecognitionException [1]
    {   
        ExpressionAST res = default(ExpressionAST);

        IToken l = null;
        ExpressionAST d = default(ExpressionAST);

        List<ExpressionAST> insts = default(List<ExpressionAST>);



        	       List<ExpressionAST> list = new List<ExpressionAST>();
        	       insts = new List<ExpressionAST>();       
        	   
        try 
    	{
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:205:7: (l= LET (d= decl )+ IN (insts= instructions )? END )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:205:9: l= LET (d= decl )+ IN (insts= instructions )? END
            {
            	l=(IToken)Match(input,LET,FOLLOW_LET_in_letExp2910); if (state.failed) return res;
            	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:205:19: (d= decl )+
            	int cnt21 = 0;
            	do 
            	{
            	    int alt21 = 2;
            	    int LA21_0 = input.LA(1);

            	    if ( (LA21_0 == FUNCTION || LA21_0 == TYPE || LA21_0 == VAR) )
            	    {
            	        alt21 = 1;
            	    }


            	    switch (alt21) 
            		{
            			case 1 :
            			    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:205:20: d= decl
            			    {
            			    	PushFollow(FOLLOW_decl_in_letExp2918);
            			    	d = decl();
            			    	state.followingStackPointer--;
            			    	if (state.failed) return res;
            			    	if ( state.backtracking == 0 ) 
            			    	{
            			    	  list.Add(d);
            			    	}

            			    }
            			    break;

            			default:
            			    if ( cnt21 >= 1 ) goto loop21;
            			    if ( state.backtracking > 0 ) {state.failed = true; return res;}
            		            EarlyExitException eee =
            		                new EarlyExitException(21, input);
            		            throw eee;
            	    }
            	    cnt21++;
            	} while (true);

            	loop21:
            		;	// Stops C# compiler whinging that label 'loop21' has no statements

            	Match(input,IN,FOLLOW_IN_in_letExp2924); if (state.failed) return res;
            	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:205:48: (insts= instructions )?
            	int alt22 = 2;
            	alt22 = dfa22.Predict(input);
            	switch (alt22) 
            	{
            	    case 1 :
            	        // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:205:49: insts= instructions
            	        {
            	        	PushFollow(FOLLOW_instructions_in_letExp2931);
            	        	insts = instructions();
            	        	state.followingStackPointer--;
            	        	if (state.failed) return res;

            	        }
            	        break;

            	}

            	Match(input,END,FOLLOW_END_in_letExp2935); if (state.failed) return res;
            	if ( state.backtracking == 0 ) 
            	{
            	    res = new LetExpressionAST(list,new SequenceExpressionAST(insts));
            	}

            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return res;
    }
    // $ANTLR end "letExp"


    // $ANTLR start "instructions"
    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:208:1: instructions returns [List<ExpressionAST> res] : e1= exp ( SEMICOLON e2= exp )* ;
    public List<ExpressionAST> instructions() // throws RecognitionException [1]
    {   
        List<ExpressionAST> res = default(List<ExpressionAST>);

        ExpressionAST e1 = default(ExpressionAST);

        ExpressionAST e2 = default(ExpressionAST);


        res = new List<ExpressionAST>();
        try 
    	{
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:211:7: (e1= exp ( SEMICOLON e2= exp )* )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:211:9: e1= exp ( SEMICOLON e2= exp )*
            {
            	PushFollow(FOLLOW_exp_in_instructions2966);
            	e1 = exp();
            	state.followingStackPointer--;
            	if (state.failed) return res;
            	if ( state.backtracking == 0 ) 
            	{
            	   res.Add(e1);
            	}
            	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:211:33: ( SEMICOLON e2= exp )*
            	do 
            	{
            	    int alt23 = 2;
            	    int LA23_0 = input.LA(1);

            	    if ( (LA23_0 == SEMICOLON) )
            	    {
            	        alt23 = 1;
            	    }


            	    switch (alt23) 
            		{
            			case 1 :
            			    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:211:34: SEMICOLON e2= exp
            			    {
            			    	Match(input,SEMICOLON,FOLLOW_SEMICOLON_in_instructions2970); if (state.failed) return res;
            			    	PushFollow(FOLLOW_exp_in_instructions2974);
            			    	e2 = exp();
            			    	state.followingStackPointer--;
            			    	if (state.failed) return res;
            			    	if ( state.backtracking == 0 ) 
            			    	{
            			    	    res.Add(e2);
            			    	}

            			    }
            			    break;

            			default:
            			    goto loop23;
            	    }
            	} while (true);

            	loop23:
            		;	// Stops C# compiler whining that label 'loop23' has no statements


            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return res;
    }
    // $ANTLR end "instructions"


    // $ANTLR start "whileInstr"
    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:214:1: whileInstr returns [ExpressionAST res] : id= WHILE cond= exp DO body= exp ;
    public ExpressionAST whileInstr() // throws RecognitionException [1]
    {   
        ExpressionAST res = default(ExpressionAST);

        IToken id = null;
        ExpressionAST cond = default(ExpressionAST);

        ExpressionAST body = default(ExpressionAST);


        try 
    	{
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:215:30: (id= WHILE cond= exp DO body= exp )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:215:32: id= WHILE cond= exp DO body= exp
            {
            	id=(IToken)Match(input,WHILE,FOLLOW_WHILE_in_whileInstr3003); if (state.failed) return res;
            	PushFollow(FOLLOW_exp_in_whileInstr3009);
            	cond = exp();
            	state.followingStackPointer--;
            	if (state.failed) return res;
            	Match(input,DO,FOLLOW_DO_in_whileInstr3011); if (state.failed) return res;
            	PushFollow(FOLLOW_exp_in_whileInstr3016);
            	body = exp();
            	state.followingStackPointer--;
            	if (state.failed) return res;
            	if ( state.backtracking == 0 ) 
            	{
            	    res = new WhileExpressionAST(cond,body,id.Line, id.CharPositionInLine);
            	}

            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return res;
    }
    // $ANTLR end "whileInstr"


    // $ANTLR start "lvalue"
    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:217:1: lvalue returns [LValueAST res] : id= ID ( DOT fieldId= ID | LBRACKET indexExp= exp RBRACKET )* ;
    public LValueAST lvalue() // throws RecognitionException [1]
    {   
        LValueAST res = default(LValueAST);

        IToken id = null;
        IToken fieldId = null;
        ExpressionAST indexExp = default(ExpressionAST);


        try 
    	{
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:217:48: (id= ID ( DOT fieldId= ID | LBRACKET indexExp= exp RBRACKET )* )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:217:50: id= ID ( DOT fieldId= ID | LBRACKET indexExp= exp RBRACKET )*
            {
            	id=(IToken)Match(input,ID,FOLLOW_ID_in_lvalue3051); if (state.failed) return res;
            	if ( state.backtracking == 0 ) 
            	{
            	    res = new VarAST(id.Text, id.Line, id.CharPositionInLine);
            	}
            	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:217:120: ( DOT fieldId= ID | LBRACKET indexExp= exp RBRACKET )*
            	do 
            	{
            	    int alt24 = 3;
            	    alt24 = dfa24.Predict(input);
            	    switch (alt24) 
            		{
            			case 1 :
            			    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:217:121: DOT fieldId= ID
            			    {
            			    	Match(input,DOT,FOLLOW_DOT_in_lvalue3055); if (state.failed) return res;
            			    	fieldId=(IToken)Match(input,ID,FOLLOW_ID_in_lvalue3060); if (state.failed) return res;
            			    	if ( state.backtracking == 0 ) 
            			    	{
            			    	  res= new RecordAccessAST(fieldId.Text,res, fieldId.Line, fieldId.CharPositionInLine);
            			    	}

            			    }
            			    break;
            			case 2 :
            			    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:217:226: LBRACKET indexExp= exp RBRACKET
            			    {
            			    	Match(input,LBRACKET,FOLLOW_LBRACKET_in_lvalue3065); if (state.failed) return res;
            			    	PushFollow(FOLLOW_exp_in_lvalue3069);
            			    	indexExp = exp();
            			    	state.followingStackPointer--;
            			    	if (state.failed) return res;
            			    	Match(input,RBRACKET,FOLLOW_RBRACKET_in_lvalue3072); if (state.failed) return res;
            			    	if ( state.backtracking == 0 ) 
            			    	{
            			    	  res = new ArrayAccessAST(res,indexExp, indexExp.Line, indexExp.Columns);
            			    	}

            			    }
            			    break;

            			default:
            			    goto loop24;
            	    }
            	} while (true);

            	loop24:
            		;	// Stops C# compiler whining that label 'loop24' has no statements


            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return res;
    }
    // $ANTLR end "lvalue"


    // $ANTLR start "fieldList"
    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:221:1: fieldList returns [List<KeyValuePair<string, ExpressionAST>> res] : id= ID EQUAL e1= exp ( COMMA id2= ID EQUAL e2= exp )* ;
    public List<KeyValuePair<string, ExpressionAST>> fieldList() // throws RecognitionException [1]
    {   
        List<KeyValuePair<string, ExpressionAST>> res = default(List<KeyValuePair<string, ExpressionAST>>);

        IToken id = null;
        IToken id2 = null;
        ExpressionAST e1 = default(ExpressionAST);

        ExpressionAST e2 = default(ExpressionAST);



        	    res = new List<KeyValuePair<string,ExpressionAST>>();
        	
        try 
    	{
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:227:7: (id= ID EQUAL e1= exp ( COMMA id2= ID EQUAL e2= exp )* )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:227:9: id= ID EQUAL e1= exp ( COMMA id2= ID EQUAL e2= exp )*
            {
            	id=(IToken)Match(input,ID,FOLLOW_ID_in_fieldList3116); if (state.failed) return res;
            	Match(input,EQUAL,FOLLOW_EQUAL_in_fieldList3118); if (state.failed) return res;
            	PushFollow(FOLLOW_exp_in_fieldList3122);
            	e1 = exp();
            	state.followingStackPointer--;
            	if (state.failed) return res;
            	if ( state.backtracking == 0 ) 
            	{
            	    res.Add(new KeyValuePair<string,ExpressionAST>(id.Text,e1)); 
            	}
            	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:227:96: ( COMMA id2= ID EQUAL e2= exp )*
            	do 
            	{
            	    int alt25 = 2;
            	    int LA25_0 = input.LA(1);

            	    if ( (LA25_0 == COMMA) )
            	    {
            	        alt25 = 1;
            	    }


            	    switch (alt25) 
            		{
            			case 1 :
            			    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:227:97: COMMA id2= ID EQUAL e2= exp
            			    {
            			    	Match(input,COMMA,FOLLOW_COMMA_in_fieldList3126); if (state.failed) return res;
            			    	id2=(IToken)Match(input,ID,FOLLOW_ID_in_fieldList3131); if (state.failed) return res;
            			    	Match(input,EQUAL,FOLLOW_EQUAL_in_fieldList3133); if (state.failed) return res;
            			    	PushFollow(FOLLOW_exp_in_fieldList3138);
            			    	e2 = exp();
            			    	state.followingStackPointer--;
            			    	if (state.failed) return res;
            			    	if ( state.backtracking == 0 ) 
            			    	{
            			    	  res.Add(new KeyValuePair<string,ExpressionAST>(id2.Text,e2));
            			    	}

            			    }
            			    break;

            			default:
            			    goto loop25;
            	    }
            	} while (true);

            	loop25:
            		;	// Stops C# compiler whining that label 'loop25' has no statements


            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return res;
    }
    // $ANTLR end "fieldList"


    // $ANTLR start "breakInstr"
    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:229:1: breakInstr returns [ExpressionAST res] : id= BREAK ;
    public ExpressionAST breakInstr() // throws RecognitionException [1]
    {   
        ExpressionAST res = default(ExpressionAST);

        IToken id = null;

        try 
    	{
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:230:30: (id= BREAK )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:230:32: id= BREAK
            {
            	id=(IToken)Match(input,BREAK,FOLLOW_BREAK_in_breakInstr3160); if (state.failed) return res;
            	if ( state.backtracking == 0 ) 
            	{
            	    res = new BreakAST(id.Line, id.CharPositionInLine);
            	}

            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return res;
    }
    // $ANTLR end "breakInstr"


    // $ANTLR start "recordInstance"
    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:233:1: recordInstance returns [ExpressionAST res] : id= ID LKEY (l= fieldList )? RKEY ;
    public ExpressionAST recordInstance() // throws RecognitionException [1]
    {   
        ExpressionAST res = default(ExpressionAST);

        IToken id = null;
        List<KeyValuePair<string, ExpressionAST>> l = default(List<KeyValuePair<string, ExpressionAST>>);


        try 
    	{
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:234:33: (id= ID LKEY (l= fieldList )? RKEY )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:234:35: id= ID LKEY (l= fieldList )? RKEY
            {
            	id=(IToken)Match(input,ID,FOLLOW_ID_in_recordInstance3196); if (state.failed) return res;
            	Match(input,LKEY,FOLLOW_LKEY_in_recordInstance3198); if (state.failed) return res;
            	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:234:52: (l= fieldList )?
            	int alt26 = 2;
            	int LA26_0 = input.LA(1);

            	if ( (LA26_0 == ID) )
            	{
            	    alt26 = 1;
            	}
            	switch (alt26) 
            	{
            	    case 1 :
            	        // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:234:52: l= fieldList
            	        {
            	        	PushFollow(FOLLOW_fieldList_in_recordInstance3204);
            	        	l = fieldList();
            	        	state.followingStackPointer--;
            	        	if (state.failed) return res;

            	        }
            	        break;

            	}

            	Match(input,RKEY,FOLLOW_RKEY_in_recordInstance3208); if (state.failed) return res;
            	if ( state.backtracking == 0 ) 
            	{
            	    res = new RecordInstantiationAST(id.Text,l, id.Line, id.CharPositionInLine);
            	}

            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return res;
    }
    // $ANTLR end "recordInstance"


    // $ANTLR start "decl"
    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:237:1: decl returns [ExpressionAST res] : (t1= typeDecl | v1= varDecl | f1= funDecl );
    public ExpressionAST decl() // throws RecognitionException [1]
    {   
        ExpressionAST res = default(ExpressionAST);

        ExpressionAST t1 = default(ExpressionAST);

        ExpressionAST v1 = default(ExpressionAST);

        ExpressionAST f1 = default(ExpressionAST);


        try 
    	{
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:237:35: (t1= typeDecl | v1= varDecl | f1= funDecl )
            int alt27 = 3;
            switch ( input.LA(1) ) 
            {
            case TYPE:
            	{
                alt27 = 1;
                }
                break;
            case VAR:
            	{
                alt27 = 2;
                }
                break;
            case FUNCTION:
            	{
                alt27 = 3;
                }
                break;
            	default:
            	    if ( state.backtracking > 0 ) {state.failed = true; return res;}
            	    NoViableAltException nvae_d27s0 =
            	        new NoViableAltException("", 27, 0, input);

            	    throw nvae_d27s0;
            }

            switch (alt27) 
            {
                case 1 :
                    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:237:37: t1= typeDecl
                    {
                    	PushFollow(FOLLOW_typeDecl_in_decl3230);
                    	t1 = typeDecl();
                    	state.followingStackPointer--;
                    	if (state.failed) return res;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	    res=t1;
                    	}

                    }
                    break;
                case 2 :
                    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:238:9: v1= varDecl
                    {
                    	PushFollow(FOLLOW_varDecl_in_decl3247);
                    	v1 = varDecl();
                    	state.followingStackPointer--;
                    	if (state.failed) return res;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	    res=v1;
                    	}

                    }
                    break;
                case 3 :
                    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:239:9: f1= funDecl
                    {
                    	PushFollow(FOLLOW_funDecl_in_decl3266);
                    	f1 = funDecl();
                    	state.followingStackPointer--;
                    	if (state.failed) return res;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	    res= f1;
                    	}

                    }
                    break;

            }
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return res;
    }
    // $ANTLR end "decl"


    // $ANTLR start "typeDecl"
    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:242:1: typeDecl returns [ExpressionAST res] : TYPE id= ID EQUAL (type_id= typeId | LKEY (typeList= typeFields )? RKEY | ARRAY OF typeOfArray= typeId ) ;
    public ExpressionAST typeDecl() // throws RecognitionException [1]
    {   
        ExpressionAST res = default(ExpressionAST);

        IToken id = null;
        string type_id = default(string);

        List<KeyValuePair<string, string>> typeList = default(List<KeyValuePair<string, string>>);

        string typeOfArray = default(string);


        try 
    	{
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:242:42: ( TYPE id= ID EQUAL (type_id= typeId | LKEY (typeList= typeFields )? RKEY | ARRAY OF typeOfArray= typeId ) )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:242:44: TYPE id= ID EQUAL (type_id= typeId | LKEY (typeList= typeFields )? RKEY | ARRAY OF typeOfArray= typeId )
            {
            	Match(input,TYPE,FOLLOW_TYPE_in_typeDecl3286); if (state.failed) return res;
            	id=(IToken)Match(input,ID,FOLLOW_ID_in_typeDecl3292); if (state.failed) return res;
            	Match(input,EQUAL,FOLLOW_EQUAL_in_typeDecl3294); if (state.failed) return res;
            	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:242:63: (type_id= typeId | LKEY (typeList= typeFields )? RKEY | ARRAY OF typeOfArray= typeId )
            	int alt29 = 3;
            	switch ( input.LA(1) ) 
            	{
            	case INT:
            	case STRING:
            	case ID:
            		{
            	    alt29 = 1;
            	    }
            	    break;
            	case LKEY:
            		{
            	    alt29 = 2;
            	    }
            	    break;
            	case ARRAY:
            		{
            	    alt29 = 3;
            	    }
            	    break;
            		default:
            		    if ( state.backtracking > 0 ) {state.failed = true; return res;}
            		    NoViableAltException nvae_d29s0 =
            		        new NoViableAltException("", 29, 0, input);

            		    throw nvae_d29s0;
            	}

            	switch (alt29) 
            	{
            	    case 1 :
            	        // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:243:16: type_id= typeId
            	        {
            	        	PushFollow(FOLLOW_typeId_in_typeDecl3317);
            	        	type_id = typeId();
            	        	state.followingStackPointer--;
            	        	if (state.failed) return res;
            	        	if ( state.backtracking == 0 ) 
            	        	{
            	        	    res = new AliasAST(id.Text,type_id, id.Line, id.CharPositionInLine);
            	        	}

            	        }
            	        break;
            	    case 2 :
            	        // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:244:17: LKEY (typeList= typeFields )? RKEY
            	        {
            	        	Match(input,LKEY,FOLLOW_LKEY_in_typeDecl3350); if (state.failed) return res;
            	        	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:244:22: (typeList= typeFields )?
            	        	int alt28 = 2;
            	        	int LA28_0 = input.LA(1);

            	        	if ( (LA28_0 == ID) )
            	        	{
            	        	    alt28 = 1;
            	        	}
            	        	switch (alt28) 
            	        	{
            	        	    case 1 :
            	        	        // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:244:23: typeList= typeFields
            	        	        {
            	        	        	PushFollow(FOLLOW_typeFields_in_typeDecl3356);
            	        	        	typeList = typeFields();
            	        	        	state.followingStackPointer--;
            	        	        	if (state.failed) return res;

            	        	        }
            	        	        break;

            	        	}

            	        	if ( state.backtracking == 0 ) 
            	        	{
            	        	    res = new RecordDeclarationAST(id.Text,typeList, id.Line, id.CharPositionInLine);
            	        	}
            	        	Match(input,RKEY,FOLLOW_RKEY_in_typeDecl3362); if (state.failed) return res;

            	        }
            	        break;
            	    case 3 :
            	        // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:245:17: ARRAY OF typeOfArray= typeId
            	        {
            	        	Match(input,ARRAY,FOLLOW_ARRAY_in_typeDecl3380); if (state.failed) return res;
            	        	Match(input,OF,FOLLOW_OF_in_typeDecl3382); if (state.failed) return res;
            	        	PushFollow(FOLLOW_typeId_in_typeDecl3386);
            	        	typeOfArray = typeId();
            	        	state.followingStackPointer--;
            	        	if (state.failed) return res;
            	        	if ( state.backtracking == 0 ) 
            	        	{
            	        	    res = new ArrayDeclarationAST(id.Text,typeOfArray, id.Line, id.CharPositionInLine);
            	        	}

            	        }
            	        break;

            	}


            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return res;
    }
    // $ANTLR end "typeDecl"


    // $ANTLR start "typeId"
    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:248:1: typeId returns [string res] : (id= ID | i= INT | s= STRING );
    public string typeId() // throws RecognitionException [1]
    {   
        string res = default(string);

        IToken id = null;
        IToken i = null;
        IToken s = null;

        try 
    	{
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:248:35: (id= ID | i= INT | s= STRING )
            int alt30 = 3;
            switch ( input.LA(1) ) 
            {
            case ID:
            	{
                alt30 = 1;
                }
                break;
            case INT:
            	{
                alt30 = 2;
                }
                break;
            case STRING:
            	{
                alt30 = 3;
                }
                break;
            	default:
            	    if ( state.backtracking > 0 ) {state.failed = true; return res;}
            	    NoViableAltException nvae_d30s0 =
            	        new NoViableAltException("", 30, 0, input);

            	    throw nvae_d30s0;
            }

            switch (alt30) 
            {
                case 1 :
                    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:248:37: id= ID
                    {
                    	id=(IToken)Match(input,ID,FOLLOW_ID_in_typeId3428); if (state.failed) return res;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	    res = id.Text;
                    	}

                    }
                    break;
                case 2 :
                    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:249:9: i= INT
                    {
                    	i=(IToken)Match(input,INT,FOLLOW_INT_in_typeId3452); if (state.failed) return res;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	    res = i.Text; 
                    	}

                    }
                    break;
                case 3 :
                    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:250:9: s= STRING
                    {
                    	s=(IToken)Match(input,STRING,FOLLOW_STRING_in_typeId3475); if (state.failed) return res;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	    res = s.Text; 
                    	}

                    }
                    break;

            }
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return res;
    }
    // $ANTLR end "typeId"


    // $ANTLR start "varDecl"
    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:252:1: varDecl returns [ExpressionAST res] : VAR id= ID ( ASSIGN value1= exp | COLON type_Id= typeId ASSIGN value2= exp ) ;
    public ExpressionAST varDecl() // throws RecognitionException [1]
    {   
        ExpressionAST res = default(ExpressionAST);

        IToken id = null;
        ExpressionAST value1 = default(ExpressionAST);

        string type_Id = default(string);

        ExpressionAST value2 = default(ExpressionAST);


        try 
    	{
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:252:42: ( VAR id= ID ( ASSIGN value1= exp | COLON type_Id= typeId ASSIGN value2= exp ) )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:252:44: VAR id= ID ( ASSIGN value1= exp | COLON type_Id= typeId ASSIGN value2= exp )
            {
            	Match(input,VAR,FOLLOW_VAR_in_varDecl3497); if (state.failed) return res;
            	id=(IToken)Match(input,ID,FOLLOW_ID_in_varDecl3505); if (state.failed) return res;
            	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:252:58: ( ASSIGN value1= exp | COLON type_Id= typeId ASSIGN value2= exp )
            	int alt31 = 2;
            	int LA31_0 = input.LA(1);

            	if ( (LA31_0 == ASSIGN) )
            	{
            	    alt31 = 1;
            	}
            	else if ( (LA31_0 == COLON) )
            	{
            	    alt31 = 2;
            	}
            	else 
            	{
            	    if ( state.backtracking > 0 ) {state.failed = true; return res;}
            	    NoViableAltException nvae_d31s0 =
            	        new NoViableAltException("", 31, 0, input);

            	    throw nvae_d31s0;
            	}
            	switch (alt31) 
            	{
            	    case 1 :
            	        // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:253:11: ASSIGN value1= exp
            	        {
            	        	Match(input,ASSIGN,FOLLOW_ASSIGN_in_varDecl3519); if (state.failed) return res;
            	        	PushFollow(FOLLOW_exp_in_varDecl3525);
            	        	value1 = exp();
            	        	state.followingStackPointer--;
            	        	if (state.failed) return res;
            	        	if ( state.backtracking == 0 ) 
            	        	{
            	        	  value2=value1;
            	        	}

            	        }
            	        break;
            	    case 2 :
            	        // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:254:25: COLON type_Id= typeId ASSIGN value2= exp
            	        {
            	        	Match(input,COLON,FOLLOW_COLON_in_varDecl3554); if (state.failed) return res;
            	        	PushFollow(FOLLOW_typeId_in_varDecl3560);
            	        	type_Id = typeId();
            	        	state.followingStackPointer--;
            	        	if (state.failed) return res;
            	        	Match(input,ASSIGN,FOLLOW_ASSIGN_in_varDecl3562); if (state.failed) return res;
            	        	PushFollow(FOLLOW_exp_in_varDecl3567);
            	        	value2 = exp();
            	        	state.followingStackPointer--;
            	        	if (state.failed) return res;
            	        	if ( state.backtracking == 0 ) 
            	        	{
            	        	  value1=value2;
            	        	}

            	        }
            	        break;

            	}

            	if ( state.backtracking == 0 ) 
            	{
            	  res = new VarDeclarationAST(id.Text,type_Id,value2, id.Line, id.CharPositionInLine);
            	}

            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return res;
    }
    // $ANTLR end "varDecl"


    // $ANTLR start "typeFields"
    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:258:1: typeFields returns [List<KeyValuePair<string, string>> res] : id= ID COLON type_id= typeId ( COMMA id1= ID COLON typeId1= typeId )* ;
    public List<KeyValuePair<string, string>> typeFields() // throws RecognitionException [1]
    {   
        List<KeyValuePair<string, string>> res = default(List<KeyValuePair<string, string>>);

        IToken id = null;
        IToken id1 = null;
        string type_id = default(string);

        string typeId1 = default(string);



        	    res = new List<KeyValuePair<string,string>>();
        	
        try 
    	{
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:264:7: (id= ID COLON type_id= typeId ( COMMA id1= ID COLON typeId1= typeId )* )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:264:9: id= ID COLON type_id= typeId ( COMMA id1= ID COLON typeId1= typeId )*
            {
            	id=(IToken)Match(input,ID,FOLLOW_ID_in_typeFields3628); if (state.failed) return res;
            	Match(input,COLON,FOLLOW_COLON_in_typeFields3630); if (state.failed) return res;
            	PushFollow(FOLLOW_typeId_in_typeFields3636);
            	type_id = typeId();
            	state.followingStackPointer--;
            	if (state.failed) return res;
            	if ( state.backtracking == 0 ) 
            	{
            	  res.Add(new KeyValuePair<string,string>(id.Text,type_id));
            	}
            	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:265:8: ( COMMA id1= ID COLON typeId1= typeId )*
            	do 
            	{
            	    int alt32 = 2;
            	    int LA32_0 = input.LA(1);

            	    if ( (LA32_0 == COMMA) )
            	    {
            	        alt32 = 1;
            	    }


            	    switch (alt32) 
            		{
            			case 1 :
            			    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:266:9: COMMA id1= ID COLON typeId1= typeId
            			    {
            			    	Match(input,COMMA,FOLLOW_COMMA_in_typeFields3659); if (state.failed) return res;
            			    	id1=(IToken)Match(input,ID,FOLLOW_ID_in_typeFields3670); if (state.failed) return res;
            			    	Match(input,COLON,FOLLOW_COLON_in_typeFields3672); if (state.failed) return res;
            			    	PushFollow(FOLLOW_typeId_in_typeFields3677);
            			    	typeId1 = typeId();
            			    	state.followingStackPointer--;
            			    	if (state.failed) return res;
            			    	if ( state.backtracking == 0 ) 
            			    	{
            			    	  res.Add(new KeyValuePair<string,string>(id1.Text,typeId1));
            			    	}

            			    }
            			    break;

            			default:
            			    goto loop32;
            	    }
            	} while (true);

            	loop32:
            		;	// Stops C# compiler whining that label 'loop32' has no statements


            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return res;
    }
    // $ANTLR end "typeFields"


    // $ANTLR start "funDecl"
    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:270:1: funDecl returns [ExpressionAST res] : f= FUNCTION fId= ID LPAREN (pList= typeFields )? RPAREN ( COLON ret= typeId )? EQUAL body= exp ;
    public ExpressionAST funDecl() // throws RecognitionException [1]
    {   
        ExpressionAST res = default(ExpressionAST);

        IToken f = null;
        IToken fId = null;
        List<KeyValuePair<string, string>> pList = default(List<KeyValuePair<string, string>>);

        string ret = default(string);

        ExpressionAST body = default(ExpressionAST);


        try 
    	{
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:270:37: (f= FUNCTION fId= ID LPAREN (pList= typeFields )? RPAREN ( COLON ret= typeId )? EQUAL body= exp )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:270:39: f= FUNCTION fId= ID LPAREN (pList= typeFields )? RPAREN ( COLON ret= typeId )? EQUAL body= exp
            {
            	f=(IToken)Match(input,FUNCTION,FOLLOW_FUNCTION_in_funDecl3708); if (state.failed) return res;
            	fId=(IToken)Match(input,ID,FOLLOW_ID_in_funDecl3718); if (state.failed) return res;
            	Match(input,LPAREN,FOLLOW_LPAREN_in_funDecl3720); if (state.failed) return res;
            	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:270:73: (pList= typeFields )?
            	int alt33 = 2;
            	int LA33_0 = input.LA(1);

            	if ( (LA33_0 == ID) )
            	{
            	    alt33 = 1;
            	}
            	switch (alt33) 
            	{
            	    case 1 :
            	        // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:270:74: pList= typeFields
            	        {
            	        	PushFollow(FOLLOW_typeFields_in_funDecl3727);
            	        	pList = typeFields();
            	        	state.followingStackPointer--;
            	        	if (state.failed) return res;

            	        }
            	        break;

            	}

            	Match(input,RPAREN,FOLLOW_RPAREN_in_funDecl3731); if (state.failed) return res;
            	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:270:103: ( COLON ret= typeId )?
            	int alt34 = 2;
            	int LA34_0 = input.LA(1);

            	if ( (LA34_0 == COLON) )
            	{
            	    alt34 = 1;
            	}
            	switch (alt34) 
            	{
            	    case 1 :
            	        // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:270:104: COLON ret= typeId
            	        {
            	        	Match(input,COLON,FOLLOW_COLON_in_funDecl3735); if (state.failed) return res;
            	        	PushFollow(FOLLOW_typeId_in_funDecl3740);
            	        	ret = typeId();
            	        	state.followingStackPointer--;
            	        	if (state.failed) return res;

            	        }
            	        break;

            	}

            	Match(input,EQUAL,FOLLOW_EQUAL_in_funDecl3804); if (state.failed) return res;
            	PushFollow(FOLLOW_exp_in_funDecl3813);
            	body = exp();
            	state.followingStackPointer--;
            	if (state.failed) return res;
            	if ( state.backtracking == 0 ) 
            	{
            	  res = new FunctionDeclarationAST(fId.Text,pList,body,ret, f.Line, f.CharPositionInLine);
            	}

            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return res;
    }
    // $ANTLR end "funDecl"

    // $ANTLR start "synpred1_tiger"
    public void synpred1_tiger_fragment() {
        // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:115:9: ( lvalue ASSIGN )
        // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:115:10: lvalue ASSIGN
        {
        	PushFollow(FOLLOW_lvalue_in_synpred1_tiger1219);
        	lvalue();
        	state.followingStackPointer--;
        	if (state.failed) return ;
        	Match(input,ASSIGN,FOLLOW_ASSIGN_in_synpred1_tiger1221); if (state.failed) return ;

        }
    }
    // $ANTLR end "synpred1_tiger"

    // $ANTLR start "synpred2_tiger"
    public void synpred2_tiger_fragment() {
        // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:116:9: ( ID LBRACKET ( exp )? RBRACKET OF )
        // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:116:10: ID LBRACKET ( exp )? RBRACKET OF
        {
        	Match(input,ID,FOLLOW_ID_in_synpred2_tiger1253); if (state.failed) return ;
        	Match(input,LBRACKET,FOLLOW_LBRACKET_in_synpred2_tiger1255); if (state.failed) return ;
        	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:116:22: ( exp )?
        	int alt35 = 2;
        	alt35 = dfa35.Predict(input);
        	switch (alt35) 
        	{
        	    case 1 :
        	        // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:116:22: exp
        	        {
        	        	PushFollow(FOLLOW_exp_in_synpred2_tiger1257);
        	        	exp();
        	        	state.followingStackPointer--;
        	        	if (state.failed) return ;

        	        }
        	        break;

        	}

        	Match(input,RBRACKET,FOLLOW_RBRACKET_in_synpred2_tiger1260); if (state.failed) return ;
        	Match(input,OF,FOLLOW_OF_in_synpred2_tiger1262); if (state.failed) return ;

        }
    }
    // $ANTLR end "synpred2_tiger"

    // Delegated rules

   	public bool synpred1_tiger() 
   	{
   	    state.backtracking++;
   	    int start = input.Mark();
   	    try 
   	    {
   	        synpred1_tiger_fragment(); // can never throw exception
   	    }
   	    catch (RecognitionException re) 
   	    {
   	        Console.Error.WriteLine("impossible: "+re);
   	    }
   	    bool success = !state.failed;
   	    input.Rewind(start);
   	    state.backtracking--;
   	    state.failed = false;
   	    return success;
   	}
   	public bool synpred2_tiger() 
   	{
   	    state.backtracking++;
   	    int start = input.Mark();
   	    try 
   	    {
   	        synpred2_tiger_fragment(); // can never throw exception
   	    }
   	    catch (RecognitionException re) 
   	    {
   	        Console.Error.WriteLine("impossible: "+re);
   	    }
   	    bool success = !state.failed;
   	    input.Rewind(start);
   	    state.backtracking--;
   	    state.failed = false;
   	    return success;
   	}


   	protected DFA1 dfa1;
   	protected DFA3 dfa3;
   	protected DFA5 dfa5;
   	protected DFA6 dfa6;
   	protected DFA7 dfa7;
   	protected DFA9 dfa9;
   	protected DFA11 dfa11;
   	protected DFA12 dfa12;
   	protected DFA16 dfa16;
   	protected DFA18 dfa18;
   	protected DFA17 dfa17;
   	protected DFA20 dfa20;
   	protected DFA22 dfa22;
   	protected DFA24 dfa24;
   	protected DFA35 dfa35;
	private void InitializeCyclicDFAs()
	{
    	this.dfa1 = new DFA1(this);
    	this.dfa3 = new DFA3(this);
    	this.dfa5 = new DFA5(this);
    	this.dfa6 = new DFA6(this);
    	this.dfa7 = new DFA7(this);
    	this.dfa9 = new DFA9(this);
    	this.dfa11 = new DFA11(this);
    	this.dfa12 = new DFA12(this);
    	this.dfa16 = new DFA16(this);
    	this.dfa18 = new DFA18(this);
    	this.dfa17 = new DFA17(this);
    	this.dfa20 = new DFA20(this);
    	this.dfa22 = new DFA22(this);
    	this.dfa24 = new DFA24(this);
    	this.dfa35 = new DFA35(this);
	    this.dfa1.specialStateTransitionHandler = new DFA.SpecialStateTransitionHandler(DFA1_SpecialStateTransition);














	}

    const string DFA1_eotS =
        "\x2e\uffff";
    const string DFA1_eofS =
        "\x04\uffff\x01\x07\x29\uffff";
    const string DFA1_minS =
        "\x01\x05\x03\uffff\x01\x04\x07\uffff\x01\x00\x02\uffff\x01\x00"+
        "\x1e\uffff";
    const string DFA1_maxS =
        "\x01\x39\x03\uffff\x01\x34\x07\uffff\x01\x00\x02\uffff\x01\x00"+
        "\x1e\uffff";
    const string DFA1_acceptS =
        "\x01\uffff\x01\x01\x01\x02\x01\x03\x01\uffff\x01\x06\x01\x07\x01"+
        "\x09\x05\uffff\x01\x08\x1e\uffff\x01\x04\x01\x05";
    const string DFA1_specialS =
        "\x04\uffff\x01\x00\x07\uffff\x01\x01\x02\uffff\x01\x02\x1e\uffff}>";
    static readonly string[] DFA1_transitionS = {
            "\x01\x05\x01\uffff\x01\x02\x01\x01\x05\uffff\x01\x03\x02\uffff"+
            "\x01\x07\x01\uffff\x01\x06\x06\uffff\x01\x04\x01\uffff\x01\x07"+
            "\x04\uffff\x01\x07\x15\uffff\x01\x07\x01\uffff\x01\x07",
            "",
            "",
            "",
            "\x01\x07\x01\uffff\x01\x07\x02\uffff\x03\x07\x03\uffff\x02"+
            "\x07\x01\uffff\x01\x07\x01\uffff\x01\x07\x06\uffff\x04\x07\x01"+
            "\uffff\x03\x07\x01\x0d\x01\x07\x01\x0c\x07\x07\x01\uffff\x02"+
            "\x07\x01\x2c\x01\x0f\x01\uffff\x02\x07",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "\x01\uffff",
            "",
            "",
            "\x01\uffff",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            ""
    };

    static readonly short[] DFA1_eot = DFA.UnpackEncodedString(DFA1_eotS);
    static readonly short[] DFA1_eof = DFA.UnpackEncodedString(DFA1_eofS);
    static readonly char[] DFA1_min = DFA.UnpackEncodedStringToUnsignedChars(DFA1_minS);
    static readonly char[] DFA1_max = DFA.UnpackEncodedStringToUnsignedChars(DFA1_maxS);
    static readonly short[] DFA1_accept = DFA.UnpackEncodedString(DFA1_acceptS);
    static readonly short[] DFA1_special = DFA.UnpackEncodedString(DFA1_specialS);
    static readonly short[][] DFA1_transition = DFA.UnpackEncodedStringArray(DFA1_transitionS);

    protected class DFA1 : DFA
    {
        public DFA1(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 1;
            this.eot = DFA1_eot;
            this.eof = DFA1_eof;
            this.min = DFA1_min;
            this.max = DFA1_max;
            this.accept = DFA1_accept;
            this.special = DFA1_special;
            this.transition = DFA1_transition;

        }

        override public string Description
        {
            get { return "112:1: exp returns [ExpressionAST res] : (ifE= ifExp | forE= forExp | let= letExp | ( lvalue ASSIGN )=>lv= lvalue s= ASSIGN initExp= exp | ( ID LBRACKET ( exp )? RBRACKET OF )=>id= ID LBRACKET sizeExp= exp RBRACKET OF initExp= exp | whileE= whileInstr | breakE= breakInstr | record= recordInstance | e= expOrAnd );"; }
        }

    }


    protected internal int DFA1_SpecialStateTransition(DFA dfa, int s, IIntStream _input) //throws NoViableAltException
    {
            ITokenStream input = (ITokenStream)_input;
    	int _s = s;
        switch ( s )
        {
               	case 0 : 
                   	int LA1_4 = input.LA(1);

                   	 
                   	int index1_4 = input.Index();
                   	input.Rewind();
                   	s = -1;
                   	if ( (LA1_4 == LBRACKET) ) { s = 12; }

                   	else if ( (LA1_4 == LKEY) ) { s = 13; }

                   	else if ( (LA1_4 == EOF || LA1_4 == END || LA1_4 == DO || (LA1_4 >= THEN && LA1_4 <= FUNCTION) || (LA1_4 >= IN && LA1_4 <= TYPE) || LA1_4 == TO || LA1_4 == VAR || (LA1_4 >= PLUS && LA1_4 <= SLASH) || (LA1_4 >= MOD && LA1_4 <= RPAREN) || LA1_4 == RKEY || (LA1_4 >= RBRACKET && LA1_4 <= LE) || (LA1_4 >= AND && LA1_4 <= OR) || (LA1_4 >= COMMA && LA1_4 <= SEMICOLON)) ) { s = 7; }

                   	else if ( (LA1_4 == DOT) ) { s = 15; }

                   	else if ( (LA1_4 == ASSIGN) && (synpred1_tiger()) ) { s = 44; }

                   	 
                   	input.Seek(index1_4);
                   	if ( s >= 0 ) return s;
                   	break;
               	case 1 : 
                   	int LA1_12 = input.LA(1);

                   	 
                   	int index1_12 = input.Index();
                   	input.Rewind();
                   	s = -1;
                   	if ( (synpred1_tiger()) ) { s = 44; }

                   	else if ( (synpred2_tiger()) ) { s = 45; }

                   	else if ( (true) ) { s = 7; }

                   	 
                   	input.Seek(index1_12);
                   	if ( s >= 0 ) return s;
                   	break;
               	case 2 : 
                   	int LA1_15 = input.LA(1);

                   	 
                   	int index1_15 = input.Index();
                   	input.Rewind();
                   	s = -1;
                   	if ( (synpred1_tiger()) ) { s = 44; }

                   	else if ( (true) ) { s = 7; }

                   	 
                   	input.Seek(index1_15);
                   	if ( s >= 0 ) return s;
                   	break;
        }
        if (state.backtracking > 0) {state.failed = true; return -1;}
        NoViableAltException nvae =
            new NoViableAltException(dfa.Description, 1, _s, input);
        dfa.Error(nvae);
        throw nvae;
    }
    const string DFA3_eotS =
        "\x12\uffff";
    const string DFA3_eofS =
        "\x01\x01\x11\uffff";
    const string DFA3_minS =
        "\x01\x04\x11\uffff";
    const string DFA3_maxS =
        "\x01\x34\x11\uffff";
    const string DFA3_acceptS =
        "\x01\uffff\x01\x02\x0e\uffff\x01\x01\x01\uffff";
    const string DFA3_specialS =
        "\x12\uffff}>";
    static readonly string[] DFA3_transitionS = {
            "\x01\x01\x01\uffff\x01\x01\x02\uffff\x03\x01\x03\uffff\x02"+
            "\x01\x01\uffff\x01\x01\x01\uffff\x01\x01\x0d\uffff\x01\x01\x01"+
            "\uffff\x01\x01\x01\uffff\x01\x01\x07\uffff\x02\x10\x03\uffff"+
            "\x02\x01",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            ""
    };

    static readonly short[] DFA3_eot = DFA.UnpackEncodedString(DFA3_eotS);
    static readonly short[] DFA3_eof = DFA.UnpackEncodedString(DFA3_eofS);
    static readonly char[] DFA3_min = DFA.UnpackEncodedStringToUnsignedChars(DFA3_minS);
    static readonly char[] DFA3_max = DFA.UnpackEncodedStringToUnsignedChars(DFA3_maxS);
    static readonly short[] DFA3_accept = DFA.UnpackEncodedString(DFA3_acceptS);
    static readonly short[] DFA3_special = DFA.UnpackEncodedString(DFA3_specialS);
    static readonly short[][] DFA3_transition = DFA.UnpackEncodedStringArray(DFA3_transitionS);

    protected class DFA3 : DFA
    {
        public DFA3(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 3;
            this.eot = DFA3_eot;
            this.eof = DFA3_eof;
            this.min = DFA3_min;
            this.max = DFA3_max;
            this.accept = DFA3_accept;
            this.special = DFA3_special;
            this.transition = DFA3_transition;

        }

        override public string Description
        {
            get { return "()* loopback of 124:66: ( (s= AND | s= OR ) e2= expCOMP )*"; }
        }

    }

    const string DFA5_eotS =
        "\x16\uffff";
    const string DFA5_eofS =
        "\x01\x05\x15\uffff";
    const string DFA5_minS =
        "\x01\x04\x15\uffff";
    const string DFA5_maxS =
        "\x01\x34\x15\uffff";
    const string DFA5_acceptS =
        "\x01\uffff\x01\x01\x03\uffff\x01\x02\x10\uffff";
    const string DFA5_specialS =
        "\x16\uffff}>";
    static readonly string[] DFA5_transitionS = {
            "\x01\x05\x01\uffff\x01\x05\x02\uffff\x03\x05\x03\uffff\x02"+
            "\x05\x01\uffff\x01\x05\x01\uffff\x01\x05\x0d\uffff\x01\x05\x01"+
            "\uffff\x01\x05\x01\uffff\x01\x05\x02\uffff\x04\x01\x01\uffff"+
            "\x02\x05\x03\uffff\x02\x05",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            ""
    };

    static readonly short[] DFA5_eot = DFA.UnpackEncodedString(DFA5_eotS);
    static readonly short[] DFA5_eof = DFA.UnpackEncodedString(DFA5_eofS);
    static readonly char[] DFA5_min = DFA.UnpackEncodedStringToUnsignedChars(DFA5_minS);
    static readonly char[] DFA5_max = DFA.UnpackEncodedStringToUnsignedChars(DFA5_maxS);
    static readonly short[] DFA5_accept = DFA.UnpackEncodedString(DFA5_acceptS);
    static readonly short[] DFA5_special = DFA.UnpackEncodedString(DFA5_specialS);
    static readonly short[][] DFA5_transition = DFA.UnpackEncodedStringArray(DFA5_transitionS);

    protected class DFA5 : DFA
    {
        public DFA5(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 5;
            this.eot = DFA5_eot;
            this.eof = DFA5_eof;
            this.min = DFA5_min;
            this.max = DFA5_max;
            this.accept = DFA5_accept;
            this.special = DFA5_special;
            this.transition = DFA5_transition;

        }

        override public string Description
        {
            get { return "129:66: ( (s= GT | s= LT | s= GE | s= LE ) e2= expEQ )?"; }
        }

    }

    const string DFA6_eotS =
        "\x17\uffff";
    const string DFA6_eofS =
        "\x01\x02\x16\uffff";
    const string DFA6_minS =
        "\x01\x04\x16\uffff";
    const string DFA6_maxS =
        "\x01\x34\x16\uffff";
    const string DFA6_acceptS =
        "\x01\uffff\x01\x01\x01\x02\x14\uffff";
    const string DFA6_specialS =
        "\x17\uffff}>";
    static readonly string[] DFA6_transitionS = {
            "\x01\x02\x01\uffff\x01\x02\x02\uffff\x03\x02\x03\uffff\x02"+
            "\x02\x01\uffff\x01\x02\x01\uffff\x01\x02\x0d\uffff\x01\x02\x01"+
            "\uffff\x01\x02\x01\uffff\x01\x02\x01\x01\x01\uffff\x04\x02\x01"+
            "\uffff\x02\x02\x03\uffff\x02\x02",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            ""
    };

    static readonly short[] DFA6_eot = DFA.UnpackEncodedString(DFA6_eotS);
    static readonly short[] DFA6_eof = DFA.UnpackEncodedString(DFA6_eofS);
    static readonly char[] DFA6_min = DFA.UnpackEncodedStringToUnsignedChars(DFA6_minS);
    static readonly char[] DFA6_max = DFA.UnpackEncodedStringToUnsignedChars(DFA6_maxS);
    static readonly short[] DFA6_accept = DFA.UnpackEncodedString(DFA6_acceptS);
    static readonly short[] DFA6_special = DFA.UnpackEncodedString(DFA6_specialS);
    static readonly short[][] DFA6_transition = DFA.UnpackEncodedStringArray(DFA6_transitionS);

    protected class DFA6 : DFA
    {
        public DFA6(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 6;
            this.eot = DFA6_eot;
            this.eof = DFA6_eof;
            this.min = DFA6_min;
            this.max = DFA6_max;
            this.accept = DFA6_accept;
            this.special = DFA6_special;
            this.transition = DFA6_transition;

        }

        override public string Description
        {
            get { return "136:65: (s= EQUAL e2= expNE )?"; }
        }

    }

    const string DFA7_eotS =
        "\x18\uffff";
    const string DFA7_eofS =
        "\x01\x02\x17\uffff";
    const string DFA7_minS =
        "\x01\x04\x17\uffff";
    const string DFA7_maxS =
        "\x01\x34\x17\uffff";
    const string DFA7_acceptS =
        "\x01\uffff\x01\x01\x01\x02\x15\uffff";
    const string DFA7_specialS =
        "\x18\uffff}>";
    static readonly string[] DFA7_transitionS = {
            "\x01\x02\x01\uffff\x01\x02\x02\uffff\x03\x02\x03\uffff\x02"+
            "\x02\x01\uffff\x01\x02\x01\uffff\x01\x02\x0d\uffff\x01\x02\x01"+
            "\uffff\x01\x02\x01\uffff\x02\x02\x01\x01\x04\x02\x01\uffff\x02"+
            "\x02\x03\uffff\x02\x02",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            ""
    };

    static readonly short[] DFA7_eot = DFA.UnpackEncodedString(DFA7_eotS);
    static readonly short[] DFA7_eof = DFA.UnpackEncodedString(DFA7_eofS);
    static readonly char[] DFA7_min = DFA.UnpackEncodedStringToUnsignedChars(DFA7_minS);
    static readonly char[] DFA7_max = DFA.UnpackEncodedStringToUnsignedChars(DFA7_maxS);
    static readonly short[] DFA7_accept = DFA.UnpackEncodedString(DFA7_acceptS);
    static readonly short[] DFA7_special = DFA.UnpackEncodedString(DFA7_specialS);
    static readonly short[][] DFA7_transition = DFA.UnpackEncodedStringArray(DFA7_transitionS);

    protected class DFA7 : DFA
    {
        public DFA7(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 7;
            this.eot = DFA7_eot;
            this.eof = DFA7_eof;
            this.min = DFA7_min;
            this.max = DFA7_max;
            this.accept = DFA7_accept;
            this.special = DFA7_special;
            this.transition = DFA7_transition;

        }

        override public string Description
        {
            get { return "140:66: (s= DISTINC e2= expSumRes )?"; }
        }

    }

    const string DFA9_eotS =
        "\x1a\uffff";
    const string DFA9_eofS =
        "\x01\x01\x19\uffff";
    const string DFA9_minS =
        "\x01\x04\x19\uffff";
    const string DFA9_maxS =
        "\x01\x34\x19\uffff";
    const string DFA9_acceptS =
        "\x01\uffff\x01\x02\x16\uffff\x01\x01\x01\uffff";
    const string DFA9_specialS =
        "\x1a\uffff}>";
    static readonly string[] DFA9_transitionS = {
            "\x01\x01\x01\uffff\x01\x01\x02\uffff\x03\x01\x03\uffff\x02"+
            "\x01\x01\uffff\x01\x01\x01\uffff\x01\x01\x06\uffff\x02\x18\x05"+
            "\uffff\x01\x01\x01\uffff\x01\x01\x01\uffff\x07\x01\x01\uffff"+
            "\x02\x01\x03\uffff\x02\x01",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            ""
    };

    static readonly short[] DFA9_eot = DFA.UnpackEncodedString(DFA9_eotS);
    static readonly short[] DFA9_eof = DFA.UnpackEncodedString(DFA9_eofS);
    static readonly char[] DFA9_min = DFA.UnpackEncodedStringToUnsignedChars(DFA9_minS);
    static readonly char[] DFA9_max = DFA.UnpackEncodedStringToUnsignedChars(DFA9_maxS);
    static readonly short[] DFA9_accept = DFA.UnpackEncodedString(DFA9_acceptS);
    static readonly short[] DFA9_special = DFA.UnpackEncodedString(DFA9_specialS);
    static readonly short[][] DFA9_transition = DFA.UnpackEncodedStringArray(DFA9_transitionS);

    protected class DFA9 : DFA
    {
        public DFA9(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 9;
            this.eot = DFA9_eot;
            this.eof = DFA9_eof;
            this.min = DFA9_min;
            this.max = DFA9_max;
            this.accept = DFA9_accept;
            this.special = DFA9_special;
            this.transition = DFA9_transition;

        }

        override public string Description
        {
            get { return "()* loopback of 144:67: ( (s= MINUS | s= PLUS ) e2= expPorDiv )*"; }
        }

    }

    const string DFA11_eotS =
        "\x1c\uffff";
    const string DFA11_eofS =
        "\x01\x01\x1b\uffff";
    const string DFA11_minS =
        "\x01\x04\x1b\uffff";
    const string DFA11_maxS =
        "\x01\x34\x1b\uffff";
    const string DFA11_acceptS =
        "\x01\uffff\x01\x02\x18\uffff\x01\x01\x01\uffff";
    const string DFA11_specialS =
        "\x1c\uffff}>";
    static readonly string[] DFA11_transitionS = {
            "\x01\x01\x01\uffff\x01\x01\x02\uffff\x03\x01\x03\uffff\x02"+
            "\x01\x01\uffff\x01\x01\x01\uffff\x01\x01\x06\uffff\x02\x01\x02"+
            "\x1a\x03\uffff\x01\x01\x01\uffff\x01\x01\x01\uffff\x07\x01\x01"+
            "\uffff\x02\x01\x03\uffff\x02\x01",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            ""
    };

    static readonly short[] DFA11_eot = DFA.UnpackEncodedString(DFA11_eotS);
    static readonly short[] DFA11_eof = DFA.UnpackEncodedString(DFA11_eofS);
    static readonly char[] DFA11_min = DFA.UnpackEncodedStringToUnsignedChars(DFA11_minS);
    static readonly char[] DFA11_max = DFA.UnpackEncodedStringToUnsignedChars(DFA11_maxS);
    static readonly short[] DFA11_accept = DFA.UnpackEncodedString(DFA11_acceptS);
    static readonly short[] DFA11_special = DFA.UnpackEncodedString(DFA11_specialS);
    static readonly short[][] DFA11_transition = DFA.UnpackEncodedStringArray(DFA11_transitionS);

    protected class DFA11 : DFA
    {
        public DFA11(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 11;
            this.eot = DFA11_eot;
            this.eof = DFA11_eof;
            this.min = DFA11_min;
            this.max = DFA11_max;
            this.accept = DFA11_accept;
            this.special = DFA11_special;
            this.transition = DFA11_transition;

        }

        override public string Description
        {
            get { return "()* loopback of 150:67: ( (s= SLASH | s= ASTER ) e2= expMod )*"; }
        }

    }

    const string DFA12_eotS =
        "\x1d\uffff";
    const string DFA12_eofS =
        "\x01\x02\x1c\uffff";
    const string DFA12_minS =
        "\x01\x04\x1c\uffff";
    const string DFA12_maxS =
        "\x01\x34\x1c\uffff";
    const string DFA12_acceptS =
        "\x01\uffff\x01\x01\x01\x02\x1a\uffff";
    const string DFA12_specialS =
        "\x1d\uffff}>";
    static readonly string[] DFA12_transitionS = {
            "\x01\x02\x01\uffff\x01\x02\x02\uffff\x03\x02\x03\uffff\x02"+
            "\x02\x01\uffff\x01\x02\x01\uffff\x01\x02\x06\uffff\x04\x02\x01"+
            "\uffff\x01\x01\x01\uffff\x01\x02\x01\uffff\x01\x02\x01\uffff"+
            "\x07\x02\x01\uffff\x02\x02\x03\uffff\x02\x02",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            ""
    };

    static readonly short[] DFA12_eot = DFA.UnpackEncodedString(DFA12_eotS);
    static readonly short[] DFA12_eof = DFA.UnpackEncodedString(DFA12_eofS);
    static readonly char[] DFA12_min = DFA.UnpackEncodedStringToUnsignedChars(DFA12_minS);
    static readonly char[] DFA12_max = DFA.UnpackEncodedStringToUnsignedChars(DFA12_maxS);
    static readonly short[] DFA12_accept = DFA.UnpackEncodedString(DFA12_acceptS);
    static readonly short[] DFA12_special = DFA.UnpackEncodedString(DFA12_specialS);
    static readonly short[][] DFA12_transition = DFA.UnpackEncodedStringArray(DFA12_transitionS);

    protected class DFA12 : DFA
    {
        public DFA12(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 12;
            this.eot = DFA12_eot;
            this.eof = DFA12_eof;
            this.min = DFA12_min;
            this.max = DFA12_max;
            this.accept = DFA12_accept;
            this.special = DFA12_special;
            this.transition = DFA12_transition;

        }

        override public string Description
        {
            get { return "155:75: (s= MOD e2= factor )?"; }
        }

    }

    const string DFA16_eotS =
        "\x0d\uffff";
    const string DFA16_eofS =
        "\x0d\uffff";
    const string DFA16_minS =
        "\x01\x05\x0c\uffff";
    const string DFA16_maxS =
        "\x01\x39\x0c\uffff";
    const string DFA16_acceptS =
        "\x01\uffff\x01\x01\x0a\uffff\x01\x02";
    const string DFA16_specialS =
        "\x0d\uffff}>";
    static readonly string[] DFA16_transitionS = {
            "\x01\x01\x01\uffff\x02\x01\x05\uffff\x01\x01\x02\uffff\x01"+
            "\x01\x01\uffff\x01\x01\x06\uffff\x01\x01\x01\uffff\x01\x01\x04"+
            "\uffff\x01\x01\x01\x0c\x14\uffff\x01\x01\x01\uffff\x01\x01",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            ""
    };

    static readonly short[] DFA16_eot = DFA.UnpackEncodedString(DFA16_eotS);
    static readonly short[] DFA16_eof = DFA.UnpackEncodedString(DFA16_eofS);
    static readonly char[] DFA16_min = DFA.UnpackEncodedStringToUnsignedChars(DFA16_minS);
    static readonly char[] DFA16_max = DFA.UnpackEncodedStringToUnsignedChars(DFA16_maxS);
    static readonly short[] DFA16_accept = DFA.UnpackEncodedString(DFA16_acceptS);
    static readonly short[] DFA16_special = DFA.UnpackEncodedString(DFA16_specialS);
    static readonly short[][] DFA16_transition = DFA.UnpackEncodedStringArray(DFA16_transitionS);

    protected class DFA16 : DFA
    {
        public DFA16(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 16;
            this.eot = DFA16_eot;
            this.eof = DFA16_eof;
            this.min = DFA16_min;
            this.max = DFA16_max;
            this.accept = DFA16_accept;
            this.special = DFA16_special;
            this.transition = DFA16_transition;

        }

        override public string Description
        {
            get { return "172:19: (temp= exp ( SEMICOLON temp1= exp )* )?"; }
        }

    }

    const string DFA18_eotS =
        "\x24\uffff";
    const string DFA18_eofS =
        "\x04\uffff\x01\x06\x1f\uffff";
    const string DFA18_minS =
        "\x01\x1a\x03\uffff\x01\x04\x1f\uffff";
    const string DFA18_maxS =
        "\x01\x39\x03\uffff\x01\x34\x1f\uffff";
    const string DFA18_acceptS =
        "\x01\uffff\x01\x01\x01\x02\x01\x03\x01\uffff\x01\x04\x01\x05\x1d"+
        "\uffff";
    const string DFA18_specialS =
        "\x24\uffff}>";
    static readonly string[] DFA18_transitionS = {
            "\x01\x04\x06\uffff\x01\x03\x15\uffff\x01\x01\x01\uffff\x01"+
            "\x02",
            "",
            "",
            "",
            "\x01\x06\x01\uffff\x01\x06\x02\uffff\x03\x06\x03\uffff\x02"+
            "\x06\x01\uffff\x01\x06\x01\uffff\x01\x06\x06\uffff\x04\x06\x01"+
            "\uffff\x01\x06\x01\x05\x01\x06\x01\uffff\x09\x06\x01\uffff\x02"+
            "\x06\x01\uffff\x01\x06\x01\uffff\x02\x06",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            ""
    };

    static readonly short[] DFA18_eot = DFA.UnpackEncodedString(DFA18_eotS);
    static readonly short[] DFA18_eof = DFA.UnpackEncodedString(DFA18_eofS);
    static readonly char[] DFA18_min = DFA.UnpackEncodedStringToUnsignedChars(DFA18_minS);
    static readonly char[] DFA18_max = DFA.UnpackEncodedStringToUnsignedChars(DFA18_maxS);
    static readonly short[] DFA18_accept = DFA.UnpackEncodedString(DFA18_acceptS);
    static readonly short[] DFA18_special = DFA.UnpackEncodedString(DFA18_specialS);
    static readonly short[][] DFA18_transition = DFA.UnpackEncodedStringArray(DFA18_transitionS);

    protected class DFA18 : DFA
    {
        public DFA18(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 18;
            this.eot = DFA18_eot;
            this.eof = DFA18_eof;
            this.min = DFA18_min;
            this.max = DFA18_max;
            this.accept = DFA18_accept;
            this.special = DFA18_special;
            this.transition = DFA18_transition;

        }

        override public string Description
        {
            get { return "174:1: fExp returns [ExpressionAST res] : (i= INTCONST | s= STRINGCONST | seq_exp= seqExp | id= ID LPAREN (argList= listExp )? RPAREN | l_value= lvalue );"; }
        }

    }

    const string DFA17_eotS =
        "\x0d\uffff";
    const string DFA17_eofS =
        "\x0d\uffff";
    const string DFA17_minS =
        "\x01\x05\x0c\uffff";
    const string DFA17_maxS =
        "\x01\x39\x0c\uffff";
    const string DFA17_acceptS =
        "\x01\uffff\x01\x01\x0a\uffff\x01\x02";
    const string DFA17_specialS =
        "\x0d\uffff}>";
    static readonly string[] DFA17_transitionS = {
            "\x01\x01\x01\uffff\x02\x01\x05\uffff\x01\x01\x02\uffff\x01"+
            "\x01\x01\uffff\x01\x01\x06\uffff\x01\x01\x01\uffff\x01\x01\x04"+
            "\uffff\x01\x01\x01\x0c\x14\uffff\x01\x01\x01\uffff\x01\x01",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            ""
    };

    static readonly short[] DFA17_eot = DFA.UnpackEncodedString(DFA17_eotS);
    static readonly short[] DFA17_eof = DFA.UnpackEncodedString(DFA17_eofS);
    static readonly char[] DFA17_min = DFA.UnpackEncodedStringToUnsignedChars(DFA17_minS);
    static readonly char[] DFA17_max = DFA.UnpackEncodedStringToUnsignedChars(DFA17_maxS);
    static readonly short[] DFA17_accept = DFA.UnpackEncodedString(DFA17_acceptS);
    static readonly short[] DFA17_special = DFA.UnpackEncodedString(DFA17_specialS);
    static readonly short[][] DFA17_transition = DFA.UnpackEncodedStringArray(DFA17_transitionS);

    protected class DFA17 : DFA
    {
        public DFA17(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 17;
            this.eot = DFA17_eot;
            this.eof = DFA17_eof;
            this.min = DFA17_min;
            this.max = DFA17_max;
            this.accept = DFA17_accept;
            this.special = DFA17_special;
            this.transition = DFA17_transition;

        }

        override public string Description
        {
            get { return "183:32: (argList= listExp )?"; }
        }

    }

    const string DFA20_eotS =
        "\x10\uffff";
    const string DFA20_eofS =
        "\x01\x02\x0f\uffff";
    const string DFA20_minS =
        "\x01\x04\x0f\uffff";
    const string DFA20_maxS =
        "\x01\x34\x0f\uffff";
    const string DFA20_acceptS =
        "\x01\uffff\x01\x01\x01\x02\x0d\uffff";
    const string DFA20_specialS =
        "\x10\uffff}>";
    static readonly string[] DFA20_transitionS = {
            "\x01\x02\x01\uffff\x01\x02\x02\uffff\x01\x02\x01\x01\x01\x02"+
            "\x03\uffff\x02\x02\x01\uffff\x01\x02\x01\uffff\x01\x02\x0d\uffff"+
            "\x01\x02\x01\uffff\x01\x02\x01\uffff\x01\x02\x0c\uffff\x02\x02",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            ""
    };

    static readonly short[] DFA20_eot = DFA.UnpackEncodedString(DFA20_eotS);
    static readonly short[] DFA20_eof = DFA.UnpackEncodedString(DFA20_eofS);
    static readonly char[] DFA20_min = DFA.UnpackEncodedStringToUnsignedChars(DFA20_minS);
    static readonly char[] DFA20_max = DFA.UnpackEncodedStringToUnsignedChars(DFA20_maxS);
    static readonly short[] DFA20_accept = DFA.UnpackEncodedString(DFA20_acceptS);
    static readonly short[] DFA20_special = DFA.UnpackEncodedString(DFA20_specialS);
    static readonly short[][] DFA20_transition = DFA.UnpackEncodedStringArray(DFA20_transitionS);

    protected class DFA20 : DFA
    {
        public DFA20(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 20;
            this.eot = DFA20_eot;
            this.eof = DFA20_eof;
            this.min = DFA20_min;
            this.max = DFA20_max;
            this.accept = DFA20_accept;
            this.special = DFA20_special;
            this.transition = DFA20_transition;

        }

        override public string Description
        {
            get { return "195:69: ( ELSE e2= exp )?"; }
        }

    }

    const string DFA22_eotS =
        "\x0d\uffff";
    const string DFA22_eofS =
        "\x0d\uffff";
    const string DFA22_minS =
        "\x01\x04\x0c\uffff";
    const string DFA22_maxS =
        "\x01\x39\x0c\uffff";
    const string DFA22_acceptS =
        "\x01\uffff\x01\x01\x0a\uffff\x01\x02";
    const string DFA22_specialS =
        "\x0d\uffff}>";
    static readonly string[] DFA22_transitionS = {
            "\x01\x0c\x01\x01\x01\uffff\x02\x01\x05\uffff\x01\x01\x02\uffff"+
            "\x01\x01\x01\uffff\x01\x01\x06\uffff\x01\x01\x01\uffff\x01\x01"+
            "\x04\uffff\x01\x01\x15\uffff\x01\x01\x01\uffff\x01\x01",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            ""
    };

    static readonly short[] DFA22_eot = DFA.UnpackEncodedString(DFA22_eotS);
    static readonly short[] DFA22_eof = DFA.UnpackEncodedString(DFA22_eofS);
    static readonly char[] DFA22_min = DFA.UnpackEncodedStringToUnsignedChars(DFA22_minS);
    static readonly char[] DFA22_max = DFA.UnpackEncodedStringToUnsignedChars(DFA22_maxS);
    static readonly short[] DFA22_accept = DFA.UnpackEncodedString(DFA22_acceptS);
    static readonly short[] DFA22_special = DFA.UnpackEncodedString(DFA22_specialS);
    static readonly short[][] DFA22_transition = DFA.UnpackEncodedStringArray(DFA22_transitionS);

    protected class DFA22 : DFA
    {
        public DFA22(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 22;
            this.eot = DFA22_eot;
            this.eof = DFA22_eof;
            this.min = DFA22_min;
            this.max = DFA22_max;
            this.accept = DFA22_accept;
            this.special = DFA22_special;
            this.transition = DFA22_transition;

        }

        override public string Description
        {
            get { return "205:48: (insts= instructions )?"; }
        }

    }

    const string DFA24_eotS =
        "\x20\uffff";
    const string DFA24_eofS =
        "\x01\x01\x1f\uffff";
    const string DFA24_minS =
        "\x01\x04\x1f\uffff";
    const string DFA24_maxS =
        "\x01\x34\x1f\uffff";
    const string DFA24_acceptS =
        "\x01\uffff\x01\x03\x1c\uffff\x01\x01\x01\x02";
    const string DFA24_specialS =
        "\x20\uffff}>";
    static readonly string[] DFA24_transitionS = {
            "\x01\x01\x01\uffff\x01\x01\x02\uffff\x03\x01\x03\uffff\x02"+
            "\x01\x01\uffff\x01\x01\x01\uffff\x01\x01\x06\uffff\x04\x01\x01"+
            "\uffff\x01\x01\x01\uffff\x01\x01\x01\uffff\x01\x01\x01\x1f\x07"+
            "\x01\x01\uffff\x03\x01\x01\x1e\x01\uffff\x02\x01",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            ""
    };

    static readonly short[] DFA24_eot = DFA.UnpackEncodedString(DFA24_eotS);
    static readonly short[] DFA24_eof = DFA.UnpackEncodedString(DFA24_eofS);
    static readonly char[] DFA24_min = DFA.UnpackEncodedStringToUnsignedChars(DFA24_minS);
    static readonly char[] DFA24_max = DFA.UnpackEncodedStringToUnsignedChars(DFA24_maxS);
    static readonly short[] DFA24_accept = DFA.UnpackEncodedString(DFA24_acceptS);
    static readonly short[] DFA24_special = DFA.UnpackEncodedString(DFA24_specialS);
    static readonly short[][] DFA24_transition = DFA.UnpackEncodedStringArray(DFA24_transitionS);

    protected class DFA24 : DFA
    {
        public DFA24(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 24;
            this.eot = DFA24_eot;
            this.eof = DFA24_eof;
            this.min = DFA24_min;
            this.max = DFA24_max;
            this.accept = DFA24_accept;
            this.special = DFA24_special;
            this.transition = DFA24_transition;

        }

        override public string Description
        {
            get { return "()* loopback of 217:120: ( DOT fieldId= ID | LBRACKET indexExp= exp RBRACKET )*"; }
        }

    }

    const string DFA35_eotS =
        "\x0d\uffff";
    const string DFA35_eofS =
        "\x0d\uffff";
    const string DFA35_minS =
        "\x01\x05\x0c\uffff";
    const string DFA35_maxS =
        "\x01\x39\x0c\uffff";
    const string DFA35_acceptS =
        "\x01\uffff\x01\x01\x0a\uffff\x01\x02";
    const string DFA35_specialS =
        "\x0d\uffff}>";
    static readonly string[] DFA35_transitionS = {
            "\x01\x01\x01\uffff\x02\x01\x05\uffff\x01\x01\x02\uffff\x01"+
            "\x01\x01\uffff\x01\x01\x06\uffff\x01\x01\x01\uffff\x01\x01\x04"+
            "\uffff\x01\x01\x04\uffff\x01\x0c\x10\uffff\x01\x01\x01\uffff"+
            "\x01\x01",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            ""
    };

    static readonly short[] DFA35_eot = DFA.UnpackEncodedString(DFA35_eotS);
    static readonly short[] DFA35_eof = DFA.UnpackEncodedString(DFA35_eofS);
    static readonly char[] DFA35_min = DFA.UnpackEncodedStringToUnsignedChars(DFA35_minS);
    static readonly char[] DFA35_max = DFA.UnpackEncodedStringToUnsignedChars(DFA35_maxS);
    static readonly short[] DFA35_accept = DFA.UnpackEncodedString(DFA35_acceptS);
    static readonly short[] DFA35_special = DFA.UnpackEncodedString(DFA35_specialS);
    static readonly short[][] DFA35_transition = DFA.UnpackEncodedStringArray(DFA35_transitionS);

    protected class DFA35 : DFA
    {
        public DFA35(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 35;
            this.eot = DFA35_eot;
            this.eof = DFA35_eof;
            this.min = DFA35_min;
            this.max = DFA35_max;
            this.accept = DFA35_accept;
            this.special = DFA35_special;
            this.transition = DFA35_transition;

        }

        override public string Description
        {
            get { return "116:22: ( exp )?"; }
        }

    }

 

    public static readonly BitSet FOLLOW_exp_in_program1145 = new BitSet(new ulong[]{0x0000000000000000UL});
    public static readonly BitSet FOLLOW_EOF_in_program1149 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_ifExp_in_exp1175 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_forExp_in_exp1191 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_letExp_in_exp1206 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_lvalue_in_exp1227 = new BitSet(new ulong[]{0x0001000000000000UL});
    public static readonly BitSet FOLLOW_ASSIGN_in_exp1233 = new BitSet(new ulong[]{0x02800002140A41A0UL});
    public static readonly BitSet FOLLOW_exp_in_exp1239 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_ID_in_exp1271 = new BitSet(new ulong[]{0x0000002000000000UL});
    public static readonly BitSet FOLLOW_LBRACKET_in_exp1273 = new BitSet(new ulong[]{0x02800002140A41A0UL});
    public static readonly BitSet FOLLOW_exp_in_exp1280 = new BitSet(new ulong[]{0x0000004000000000UL});
    public static readonly BitSet FOLLOW_RBRACKET_in_exp1282 = new BitSet(new ulong[]{0x0000000000002000UL});
    public static readonly BitSet FOLLOW_OF_in_exp1284 = new BitSet(new ulong[]{0x02800002140A41A0UL});
    public static readonly BitSet FOLLOW_exp_in_exp1289 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_whileInstr_in_exp1306 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_breakInstr_in_exp1322 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_recordInstance_in_exp1338 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_expOrAnd_in_exp1353 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_expCOMP_in_expOrAnd1398 = new BitSet(new ulong[]{0x0000C00000000002UL});
    public static readonly BitSet FOLLOW_AND_in_expOrAnd1424 = new BitSet(new ulong[]{0x02800002140A41A0UL});
    public static readonly BitSet FOLLOW_OR_in_expOrAnd1451 = new BitSet(new ulong[]{0x02800002140A41A0UL});
    public static readonly BitSet FOLLOW_expCOMP_in_expOrAnd1462 = new BitSet(new ulong[]{0x0000C00000000002UL});
    public static readonly BitSet FOLLOW_expEQ_in_expCOMP1543 = new BitSet(new ulong[]{0x00001E0000000002UL});
    public static readonly BitSet FOLLOW_GT_in_expCOMP1632 = new BitSet(new ulong[]{0x02800002140A41A0UL});
    public static readonly BitSet FOLLOW_LT_in_expCOMP1652 = new BitSet(new ulong[]{0x02800002140A41A0UL});
    public static readonly BitSet FOLLOW_GE_in_expCOMP1683 = new BitSet(new ulong[]{0x02800002140A41A0UL});
    public static readonly BitSet FOLLOW_LE_in_expCOMP1703 = new BitSet(new ulong[]{0x02800002140A41A0UL});
    public static readonly BitSet FOLLOW_expEQ_in_expCOMP1713 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_expNE_in_expEQ1765 = new BitSet(new ulong[]{0x0000008000000002UL});
    public static readonly BitSet FOLLOW_EQUAL_in_expEQ1791 = new BitSet(new ulong[]{0x02800002140A41A0UL});
    public static readonly BitSet FOLLOW_expNE_in_expEQ1798 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_expSumRes_in_expNE1978 = new BitSet(new ulong[]{0x0000010000000002UL});
    public static readonly BitSet FOLLOW_DISTINC_in_expNE2001 = new BitSet(new ulong[]{0x02800002140A41A0UL});
    public static readonly BitSet FOLLOW_expSumRes_in_expNE2006 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_expPorDiv_in_expSumRes2066 = new BitSet(new ulong[]{0x0000000018000002UL});
    public static readonly BitSet FOLLOW_MINUS_in_expSumRes2091 = new BitSet(new ulong[]{0x02800002140A41A0UL});
    public static readonly BitSet FOLLOW_PLUS_in_expSumRes2114 = new BitSet(new ulong[]{0x02800002140A41A0UL});
    public static readonly BitSet FOLLOW_expPorDiv_in_expSumRes2137 = new BitSet(new ulong[]{0x0000000018000002UL});
    public static readonly BitSet FOLLOW_expMod_in_expPorDiv2184 = new BitSet(new ulong[]{0x0000000060000002UL});
    public static readonly BitSet FOLLOW_SLASH_in_expPorDiv2211 = new BitSet(new ulong[]{0x02800002140A41A0UL});
    public static readonly BitSet FOLLOW_ASTER_in_expPorDiv2233 = new BitSet(new ulong[]{0x02800002140A41A0UL});
    public static readonly BitSet FOLLOW_expMod_in_expPorDiv2241 = new BitSet(new ulong[]{0x0000000060000002UL});
    public static readonly BitSet FOLLOW_factor_in_expMod2303 = new BitSet(new ulong[]{0x0000000100000002UL});
    public static readonly BitSet FOLLOW_MOD_in_expMod2330 = new BitSet(new ulong[]{0x02800002140A41A0UL});
    public static readonly BitSet FOLLOW_factor_in_expMod2339 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_MINUS_in_factor2422 = new BitSet(new ulong[]{0x0280000214000000UL});
    public static readonly BitSet FOLLOW_fExp_in_factor2430 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_NIL_in_factor2468 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_LPAREN_in_seqExp2566 = new BitSet(new ulong[]{0x02800006140A41A0UL});
    public static readonly BitSet FOLLOW_exp_in_seqExp2572 = new BitSet(new ulong[]{0x0010000400000000UL});
    public static readonly BitSet FOLLOW_SEMICOLON_in_seqExp2576 = new BitSet(new ulong[]{0x02800002140A41A0UL});
    public static readonly BitSet FOLLOW_exp_in_seqExp2582 = new BitSet(new ulong[]{0x0010000400000000UL});
    public static readonly BitSet FOLLOW_RPAREN_in_seqExp2589 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_INTCONST_in_fExp2637 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_STRINGCONST_in_fExp2654 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_seqExp_in_fExp2670 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_ID_in_fExp2693 = new BitSet(new ulong[]{0x0000000200000000UL});
    public static readonly BitSet FOLLOW_LPAREN_in_fExp2695 = new BitSet(new ulong[]{0x02800006140A41A0UL});
    public static readonly BitSet FOLLOW_listExp_in_fExp2701 = new BitSet(new ulong[]{0x0000000400000000UL});
    public static readonly BitSet FOLLOW_RPAREN_in_fExp2706 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_lvalue_in_fExp2723 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_exp_in_listExp2773 = new BitSet(new ulong[]{0x0008000000000002UL});
    public static readonly BitSet FOLLOW_COMMA_in_listExp2777 = new BitSet(new ulong[]{0x02800002140A41A0UL});
    public static readonly BitSet FOLLOW_exp_in_listExp2782 = new BitSet(new ulong[]{0x0008000000000002UL});
    public static readonly BitSet FOLLOW_IF_in_ifExp2804 = new BitSet(new ulong[]{0x02800002140A41A0UL});
    public static readonly BitSet FOLLOW_exp_in_ifExp2809 = new BitSet(new ulong[]{0x0000000000000200UL});
    public static readonly BitSet FOLLOW_THEN_in_ifExp2811 = new BitSet(new ulong[]{0x02800002140A41A0UL});
    public static readonly BitSet FOLLOW_exp_in_ifExp2815 = new BitSet(new ulong[]{0x0000000000000402UL});
    public static readonly BitSet FOLLOW_ELSE_in_ifExp2818 = new BitSet(new ulong[]{0x02800002140A41A0UL});
    public static readonly BitSet FOLLOW_exp_in_ifExp2822 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_FOR_in_forExp2846 = new BitSet(new ulong[]{0x0000000004000000UL});
    public static readonly BitSet FOLLOW_ID_in_forExp2851 = new BitSet(new ulong[]{0x0001000000000000UL});
    public static readonly BitSet FOLLOW_ASSIGN_in_forExp2853 = new BitSet(new ulong[]{0x02800002140A41A0UL});
    public static readonly BitSet FOLLOW_exp_in_forExp2857 = new BitSet(new ulong[]{0x0000000000040000UL});
    public static readonly BitSet FOLLOW_TO_in_forExp2860 = new BitSet(new ulong[]{0x02800002140A41A0UL});
    public static readonly BitSet FOLLOW_exp_in_forExp2864 = new BitSet(new ulong[]{0x0000000000000040UL});
    public static readonly BitSet FOLLOW_DO_in_forExp2866 = new BitSet(new ulong[]{0x02800002140A41A0UL});
    public static readonly BitSet FOLLOW_exp_in_forExp2870 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_LET_in_letExp2910 = new BitSet(new ulong[]{0x0000000000110800UL});
    public static readonly BitSet FOLLOW_decl_in_letExp2918 = new BitSet(new ulong[]{0x0000000000118800UL});
    public static readonly BitSet FOLLOW_IN_in_letExp2924 = new BitSet(new ulong[]{0x02800002140A41B0UL});
    public static readonly BitSet FOLLOW_instructions_in_letExp2931 = new BitSet(new ulong[]{0x0000000000000010UL});
    public static readonly BitSet FOLLOW_END_in_letExp2935 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_exp_in_instructions2966 = new BitSet(new ulong[]{0x0010000000000002UL});
    public static readonly BitSet FOLLOW_SEMICOLON_in_instructions2970 = new BitSet(new ulong[]{0x02800002140A41A0UL});
    public static readonly BitSet FOLLOW_exp_in_instructions2974 = new BitSet(new ulong[]{0x0010000000000002UL});
    public static readonly BitSet FOLLOW_WHILE_in_whileInstr3003 = new BitSet(new ulong[]{0x02800002140A41A0UL});
    public static readonly BitSet FOLLOW_exp_in_whileInstr3009 = new BitSet(new ulong[]{0x0000000000000040UL});
    public static readonly BitSet FOLLOW_DO_in_whileInstr3011 = new BitSet(new ulong[]{0x02800002140A41A0UL});
    public static readonly BitSet FOLLOW_exp_in_whileInstr3016 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_ID_in_lvalue3051 = new BitSet(new ulong[]{0x0002002000000002UL});
    public static readonly BitSet FOLLOW_DOT_in_lvalue3055 = new BitSet(new ulong[]{0x0000000004000000UL});
    public static readonly BitSet FOLLOW_ID_in_lvalue3060 = new BitSet(new ulong[]{0x0002002000000002UL});
    public static readonly BitSet FOLLOW_LBRACKET_in_lvalue3065 = new BitSet(new ulong[]{0x02800002140A41A0UL});
    public static readonly BitSet FOLLOW_exp_in_lvalue3069 = new BitSet(new ulong[]{0x0000004000000000UL});
    public static readonly BitSet FOLLOW_RBRACKET_in_lvalue3072 = new BitSet(new ulong[]{0x0002002000000002UL});
    public static readonly BitSet FOLLOW_ID_in_fieldList3116 = new BitSet(new ulong[]{0x0000008000000000UL});
    public static readonly BitSet FOLLOW_EQUAL_in_fieldList3118 = new BitSet(new ulong[]{0x02800002140A41A0UL});
    public static readonly BitSet FOLLOW_exp_in_fieldList3122 = new BitSet(new ulong[]{0x0008000000000002UL});
    public static readonly BitSet FOLLOW_COMMA_in_fieldList3126 = new BitSet(new ulong[]{0x0000000004000000UL});
    public static readonly BitSet FOLLOW_ID_in_fieldList3131 = new BitSet(new ulong[]{0x0000008000000000UL});
    public static readonly BitSet FOLLOW_EQUAL_in_fieldList3133 = new BitSet(new ulong[]{0x02800002140A41A0UL});
    public static readonly BitSet FOLLOW_exp_in_fieldList3138 = new BitSet(new ulong[]{0x0008000000000002UL});
    public static readonly BitSet FOLLOW_BREAK_in_breakInstr3160 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_ID_in_recordInstance3196 = new BitSet(new ulong[]{0x0000000800000000UL});
    public static readonly BitSet FOLLOW_LKEY_in_recordInstance3198 = new BitSet(new ulong[]{0x0000001004000000UL});
    public static readonly BitSet FOLLOW_fieldList_in_recordInstance3204 = new BitSet(new ulong[]{0x0000001000000000UL});
    public static readonly BitSet FOLLOW_RKEY_in_recordInstance3208 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_typeDecl_in_decl3230 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_varDecl_in_decl3247 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_funDecl_in_decl3266 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_TYPE_in_typeDecl3286 = new BitSet(new ulong[]{0x0000000004000000UL});
    public static readonly BitSet FOLLOW_ID_in_typeDecl3292 = new BitSet(new ulong[]{0x0000008000000000UL});
    public static readonly BitSet FOLLOW_EQUAL_in_typeDecl3294 = new BitSet(new ulong[]{0x0000000804601000UL});
    public static readonly BitSet FOLLOW_typeId_in_typeDecl3317 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_LKEY_in_typeDecl3350 = new BitSet(new ulong[]{0x0000001004000000UL});
    public static readonly BitSet FOLLOW_typeFields_in_typeDecl3356 = new BitSet(new ulong[]{0x0000001000000000UL});
    public static readonly BitSet FOLLOW_RKEY_in_typeDecl3362 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_ARRAY_in_typeDecl3380 = new BitSet(new ulong[]{0x0000000000002000UL});
    public static readonly BitSet FOLLOW_OF_in_typeDecl3382 = new BitSet(new ulong[]{0x0000000004600000UL});
    public static readonly BitSet FOLLOW_typeId_in_typeDecl3386 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_ID_in_typeId3428 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_INT_in_typeId3452 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_STRING_in_typeId3475 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_VAR_in_varDecl3497 = new BitSet(new ulong[]{0x0000000004000000UL});
    public static readonly BitSet FOLLOW_ID_in_varDecl3505 = new BitSet(new ulong[]{0x0005000000000000UL});
    public static readonly BitSet FOLLOW_ASSIGN_in_varDecl3519 = new BitSet(new ulong[]{0x02800002140A41A0UL});
    public static readonly BitSet FOLLOW_exp_in_varDecl3525 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_COLON_in_varDecl3554 = new BitSet(new ulong[]{0x0000000004600000UL});
    public static readonly BitSet FOLLOW_typeId_in_varDecl3560 = new BitSet(new ulong[]{0x0001000000000000UL});
    public static readonly BitSet FOLLOW_ASSIGN_in_varDecl3562 = new BitSet(new ulong[]{0x02800002140A41A0UL});
    public static readonly BitSet FOLLOW_exp_in_varDecl3567 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_ID_in_typeFields3628 = new BitSet(new ulong[]{0x0004000000000000UL});
    public static readonly BitSet FOLLOW_COLON_in_typeFields3630 = new BitSet(new ulong[]{0x0000000004600000UL});
    public static readonly BitSet FOLLOW_typeId_in_typeFields3636 = new BitSet(new ulong[]{0x0008000000000002UL});
    public static readonly BitSet FOLLOW_COMMA_in_typeFields3659 = new BitSet(new ulong[]{0x0000000004000000UL});
    public static readonly BitSet FOLLOW_ID_in_typeFields3670 = new BitSet(new ulong[]{0x0004000000000000UL});
    public static readonly BitSet FOLLOW_COLON_in_typeFields3672 = new BitSet(new ulong[]{0x0000000004600000UL});
    public static readonly BitSet FOLLOW_typeId_in_typeFields3677 = new BitSet(new ulong[]{0x0008000000000002UL});
    public static readonly BitSet FOLLOW_FUNCTION_in_funDecl3708 = new BitSet(new ulong[]{0x0000000004000000UL});
    public static readonly BitSet FOLLOW_ID_in_funDecl3718 = new BitSet(new ulong[]{0x0000000200000000UL});
    public static readonly BitSet FOLLOW_LPAREN_in_funDecl3720 = new BitSet(new ulong[]{0x0000000404000000UL});
    public static readonly BitSet FOLLOW_typeFields_in_funDecl3727 = new BitSet(new ulong[]{0x0000000400000000UL});
    public static readonly BitSet FOLLOW_RPAREN_in_funDecl3731 = new BitSet(new ulong[]{0x0004008000000000UL});
    public static readonly BitSet FOLLOW_COLON_in_funDecl3735 = new BitSet(new ulong[]{0x0000000004600000UL});
    public static readonly BitSet FOLLOW_typeId_in_funDecl3740 = new BitSet(new ulong[]{0x0000008000000000UL});
    public static readonly BitSet FOLLOW_EQUAL_in_funDecl3804 = new BitSet(new ulong[]{0x02800002140A41A0UL});
    public static readonly BitSet FOLLOW_exp_in_funDecl3813 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_lvalue_in_synpred1_tiger1219 = new BitSet(new ulong[]{0x0001000000000000UL});
    public static readonly BitSet FOLLOW_ASSIGN_in_synpred1_tiger1221 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_ID_in_synpred2_tiger1253 = new BitSet(new ulong[]{0x0000002000000000UL});
    public static readonly BitSet FOLLOW_LBRACKET_in_synpred2_tiger1255 = new BitSet(new ulong[]{0x02800042140A41A0UL});
    public static readonly BitSet FOLLOW_exp_in_synpred2_tiger1257 = new BitSet(new ulong[]{0x0000004000000000UL});
    public static readonly BitSet FOLLOW_RBRACKET_in_synpred2_tiger1260 = new BitSet(new ulong[]{0x0000000000002000UL});
    public static readonly BitSet FOLLOW_OF_in_synpred2_tiger1262 = new BitSet(new ulong[]{0x0000000000000002UL});

}
