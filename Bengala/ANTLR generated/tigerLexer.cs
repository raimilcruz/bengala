// $ANTLR 3.1.1 C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g 2009-04-02 14:05:17

using System;
using Antlr.Runtime;
using IList 		= System.Collections.IList;
using ArrayList 	= System.Collections.ArrayList;
using Stack 		= Antlr.Runtime.Collections.StackList;


public partial class tigerLexer : Lexer {
    public const int FUNCTION = 11;
    public const int CHAR_STRING = 54;
    public const int LT = 42;
    public const int WHILE = 5;
    public const int MULTILINECOMENTS = 59;
    public const int INTCONST = 55;
    public const int MOD = 32;
    public const int CHAR = 24;
    public const int FOR = 7;
    public const int DO = 6;
    public const int NOT = 45;
    public const int ID = 26;
    public const int AND = 46;
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
    public const int INT = 21;
    public const int SEMICOLON = 52;
    public const int LKEY = 35;
    public const int MINUS = 28;
    public const int OF = 13;
    public const int COLON = 50;
    public const int WS = 23;
    public const int NIL = 17;
    public const int OR = 47;
    public const int ASSIGN = 48;
    public const int GT = 41;
    public const int END = 4;
    public const int LET = 14;
    public const int LE = 44;
    public const int STRING = 22;

    // delegates
    // delegators

    public tigerLexer() 
    {
		InitializeCyclicDFAs();
    }
    public tigerLexer(ICharStream input)
		: this(input, null) {
    }
    public tigerLexer(ICharStream input, RecognizerSharedState state)
		: base(input, state) {
		InitializeCyclicDFAs(); 

    }
    
    override public string GrammarFileName
    {
    	get { return "C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g";} 
    }

    // $ANTLR start "END"
    public void mEND() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = END;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:7:5: ( 'end' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:7:7: 'end'
            {
            	Match("end"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "END"

    // $ANTLR start "WHILE"
    public void mWHILE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = WHILE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:8:7: ( 'while' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:8:9: 'while'
            {
            	Match("while"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "WHILE"

    // $ANTLR start "DO"
    public void mDO() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DO;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:9:4: ( 'do' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:9:6: 'do'
            {
            	Match("do"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DO"

    // $ANTLR start "FOR"
    public void mFOR() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = FOR;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:10:5: ( 'for' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:10:7: 'for'
            {
            	Match("for"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "FOR"

    // $ANTLR start "IF"
    public void mIF() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = IF;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:11:4: ( 'if' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:11:6: 'if'
            {
            	Match("if"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "IF"

    // $ANTLR start "THEN"
    public void mTHEN() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = THEN;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:12:6: ( 'then' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:12:8: 'then'
            {
            	Match("then"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "THEN"

    // $ANTLR start "ELSE"
    public void mELSE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ELSE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:13:6: ( 'else' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:13:8: 'else'
            {
            	Match("else"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ELSE"

    // $ANTLR start "FUNCTION"
    public void mFUNCTION() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = FUNCTION;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:14:10: ( 'function' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:14:12: 'function'
            {
            	Match("function"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "FUNCTION"

    // $ANTLR start "ARRAY"
    public void mARRAY() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ARRAY;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:15:7: ( 'array' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:15:9: 'array'
            {
            	Match("array"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ARRAY"

    // $ANTLR start "OF"
    public void mOF() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = OF;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:16:4: ( 'of' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:16:6: 'of'
            {
            	Match("of"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "OF"

    // $ANTLR start "LET"
    public void mLET() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = LET;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:17:5: ( 'let' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:17:7: 'let'
            {
            	Match("let"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "LET"

    // $ANTLR start "IN"
    public void mIN() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = IN;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:18:4: ( 'in' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:18:6: 'in'
            {
            	Match("in"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "IN"

    // $ANTLR start "TYPE"
    public void mTYPE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = TYPE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:19:6: ( 'type' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:19:8: 'type'
            {
            	Match("type"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "TYPE"

    // $ANTLR start "NIL"
    public void mNIL() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = NIL;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:20:5: ( 'nil' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:20:7: 'nil'
            {
            	Match("nil"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "NIL"

    // $ANTLR start "TO"
    public void mTO() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = TO;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:21:4: ( 'to' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:21:6: 'to'
            {
            	Match("to"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "TO"

    // $ANTLR start "BREAK"
    public void mBREAK() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = BREAK;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:22:7: ( 'break' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:22:9: 'break'
            {
            	Match("break"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "BREAK"

    // $ANTLR start "VAR"
    public void mVAR() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = VAR;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:23:5: ( 'var' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:23:7: 'var'
            {
            	Match("var"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "VAR"

    // $ANTLR start "INT"
    public void mINT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = INT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:24:5: ( 'int' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:24:7: 'int'
            {
            	Match("int"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "INT"

    // $ANTLR start "STRING"
    public void mSTRING() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = STRING;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:25:8: ( 'string' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:25:10: 'string'
            {
            	Match("string"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "STRING"

    // $ANTLR start "WS"
    public void mWS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = WS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:44:9: ( ( ' ' | '\\t' | '\\n' | '\\r' ) )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:44:11: ( ' ' | '\\t' | '\\n' | '\\r' )
            {
            	if ( (input.LA(1) >= '\t' && input.LA(1) <= '\n') || input.LA(1) == '\r' || input.LA(1) == ' ' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}

            	_channel = HIDDEN;

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "WS"

    // $ANTLR start "ID"
    public void mID() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ID;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:45:10: ( CHAR ( CHAR | DIGIT )* )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:45:12: CHAR ( CHAR | DIGIT )*
            {
            	mCHAR(); 
            	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:45:17: ( CHAR | DIGIT )*
            	do 
            	{
            	    int alt1 = 2;
            	    int LA1_0 = input.LA(1);

            	    if ( ((LA1_0 >= '0' && LA1_0 <= '9') || (LA1_0 >= 'A' && LA1_0 <= 'Z') || LA1_0 == '_' || (LA1_0 >= 'a' && LA1_0 <= 'z')) )
            	    {
            	        alt1 = 1;
            	    }


            	    switch (alt1) 
            		{
            			case 1 :
            			    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:
            			    {
            			    	if ( (input.LA(1) >= '0' && input.LA(1) <= '9') || (input.LA(1) >= 'A' && input.LA(1) <= 'Z') || input.LA(1) == '_' || (input.LA(1) >= 'a' && input.LA(1) <= 'z') ) 
            			    	{
            			    	    input.Consume();

            			    	}
            			    	else 
            			    	{
            			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            			    	    Recover(mse);
            			    	    throw mse;}


            			    }
            			    break;

            			default:
            			    goto loop1;
            	    }
            	} while (true);

            	loop1:
            		;	// Stops C# compiler whining that label 'loop1' has no statements


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ID"

    // $ANTLR start "PLUS"
    public void mPLUS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = PLUS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:48:12: ( '+' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:48:14: '+'
            {
            	Match('+'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "PLUS"

    // $ANTLR start "MINUS"
    public void mMINUS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = MINUS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:49:13: ( '-' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:49:15: '-'
            {
            	Match('-'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "MINUS"

    // $ANTLR start "ASTER"
    public void mASTER() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ASTER;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:50:14: ( '*' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:50:16: '*'
            {
            	Match('*'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ASTER"

    // $ANTLR start "SLASH"
    public void mSLASH() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SLASH;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:51:13: ( '/' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:51:15: '/'
            {
            	Match('/'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SLASH"

    // $ANTLR start "POW"
    public void mPOW() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = POW;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:52:13: ( '^' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:52:15: '^'
            {
            	Match('^'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "POW"

    // $ANTLR start "MOD"
    public void mMOD() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = MOD;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:53:21: ( '%' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:53:23: '%'
            {
            	Match('%'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "MOD"

    // $ANTLR start "LPAREN"
    public void mLPAREN() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = LPAREN;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:55:18: ( '(' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:55:20: '('
            {
            	Match('('); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "LPAREN"

    // $ANTLR start "RPAREN"
    public void mRPAREN() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = RPAREN;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:56:17: ( ')' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:56:19: ')'
            {
            	Match(')'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "RPAREN"

    // $ANTLR start "LKEY"
    public void mLKEY() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = LKEY;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:57:17: ( '{' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:57:19: '{'
            {
            	Match('{'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "LKEY"

    // $ANTLR start "RKEY"
    public void mRKEY() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = RKEY;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:58:17: ( '}' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:58:19: '}'
            {
            	Match('}'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "RKEY"

    // $ANTLR start "LBRACKET"
    public void mLBRACKET() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = LBRACKET;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:59:18: ( '[' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:59:20: '['
            {
            	Match('['); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "LBRACKET"

    // $ANTLR start "RBRACKET"
    public void mRBRACKET() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = RBRACKET;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:60:18: ( ']' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:60:20: ']'
            {
            	Match(']'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "RBRACKET"

    // $ANTLR start "EQUAL"
    public void mEQUAL() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = EQUAL;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:62:13: ( '=' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:62:15: '='
            {
            	Match('='); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "EQUAL"

    // $ANTLR start "DISTINC"
    public void mDISTINC() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DISTINC;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:63:14: ( '<>' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:63:16: '<>'
            {
            	Match("<>"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DISTINC"

    // $ANTLR start "GT"
    public void mGT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = GT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:64:11: ( '>' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:64:13: '>'
            {
            	Match('>'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "GT"

    // $ANTLR start "LT"
    public void mLT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = LT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:65:10: ( '<' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:65:12: '<'
            {
            	Match('<'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "LT"

    // $ANTLR start "GE"
    public void mGE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = GE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:66:9: ( '>=' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:66:11: '>='
            {
            	Match(">="); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "GE"

    // $ANTLR start "LE"
    public void mLE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = LE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:67:9: ( '<=' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:67:11: '<='
            {
            	Match("<="); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "LE"

    // $ANTLR start "NOT"
    public void mNOT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = NOT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:69:10: ( '!' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:69:12: '!'
            {
            	Match('!'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "NOT"

    // $ANTLR start "AND"
    public void mAND() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = AND;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:70:11: ( '&' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:70:13: '&'
            {
            	Match('&'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "AND"

    // $ANTLR start "OR"
    public void mOR() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = OR;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:71:10: ( '|' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:71:12: '|'
            {
            	Match('|'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "OR"

    // $ANTLR start "ASSIGN"
    public void mASSIGN() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ASSIGN;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:73:14: ( ':=' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:73:16: ':='
            {
            	Match(":="); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ASSIGN"

    // $ANTLR start "DOT"
    public void mDOT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DOT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:74:11: ( '.' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:74:13: '.'
            {
            	Match('.'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DOT"

    // $ANTLR start "COLON"
    public void mCOLON() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = COLON;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:75:13: ( ':' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:75:15: ':'
            {
            	Match(':'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "COLON"

    // $ANTLR start "COMMA"
    public void mCOMMA() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = COMMA;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:76:13: ( ',' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:76:15: ','
            {
            	Match(','); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "COMMA"

    // $ANTLR start "SEMICOLON"
    public void mSEMICOLON() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SEMICOLON;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:77:16: ( ';' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:77:18: ';'
            {
            	Match(';'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SEMICOLON"

    // $ANTLR start "CHAR"
    public void mCHAR() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:80:20: ( 'a' .. 'z' | 'A' .. 'Z' | '_' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:
            {
            	if ( (input.LA(1) >= 'A' && input.LA(1) <= 'Z') || input.LA(1) == '_' || (input.LA(1) >= 'a' && input.LA(1) <= 'z') ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "CHAR"

    // $ANTLR start "DIGIT"
    public void mDIGIT() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:82:20: ( '0' .. '9' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:82:22: '0' .. '9'
            {
            	MatchRange('0','9'); 

            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "DIGIT"

    // $ANTLR start "QUOTE"
    public void mQUOTE() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:84:21: ( '\"' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:84:23: '\"'
            {
            	Match('\"'); 

            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "QUOTE"

    // $ANTLR start "CHAR_STRING"
    public void mCHAR_STRING() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:86:33: (~ ( '\"' | '\\\\' | '\\n' | '\\t' ) )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:86:35: ~ ( '\"' | '\\\\' | '\\n' | '\\t' )
            {
            	if ( (input.LA(1) >= '\u0000' && input.LA(1) <= '\b') || (input.LA(1) >= '\u000B' && input.LA(1) <= '!') || (input.LA(1) >= '#' && input.LA(1) <= '[') || (input.LA(1) >= ']' && input.LA(1) <= '\uFFFF') ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "CHAR_STRING"

    // $ANTLR start "INTCONST"
    public void mINTCONST() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = INTCONST;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:88:19: ( ( DIGIT )+ )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:88:21: ( DIGIT )+
            {
            	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:88:21: ( DIGIT )+
            	int cnt2 = 0;
            	do 
            	{
            	    int alt2 = 2;
            	    int LA2_0 = input.LA(1);

            	    if ( ((LA2_0 >= '0' && LA2_0 <= '9')) )
            	    {
            	        alt2 = 1;
            	    }


            	    switch (alt2) 
            		{
            			case 1 :
            			    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:88:21: DIGIT
            			    {
            			    	mDIGIT(); 

            			    }
            			    break;

            			default:
            			    if ( cnt2 >= 1 ) goto loop2;
            		            EarlyExitException eee =
            		                new EarlyExitException(2, input);
            		            throw eee;
            	    }
            	    cnt2++;
            	} while (true);

            	loop2:
            		;	// Stops C# compiler whinging that label 'loop2' has no statements


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "INTCONST"

    // $ANTLR start "STRINGCONST"
    public void mSTRINGCONST() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = STRINGCONST;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:90:18: ( QUOTE ( STRINGCOMPONENTS )* QUOTE )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:90:20: QUOTE ( STRINGCOMPONENTS )* QUOTE
            {
            	mQUOTE(); 
            	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:90:26: ( STRINGCOMPONENTS )*
            	do 
            	{
            	    int alt3 = 2;
            	    int LA3_0 = input.LA(1);

            	    if ( ((LA3_0 >= '\u0000' && LA3_0 <= '\b') || (LA3_0 >= '\u000B' && LA3_0 <= '!') || (LA3_0 >= '#' && LA3_0 <= '\uFFFF')) )
            	    {
            	        alt3 = 1;
            	    }


            	    switch (alt3) 
            		{
            			case 1 :
            			    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:90:26: STRINGCOMPONENTS
            			    {
            			    	mSTRINGCOMPONENTS(); 

            			    }
            			    break;

            			default:
            			    goto loop3;
            	    }
            	} while (true);

            	loop3:
            		;	// Stops C# compiler whining that label 'loop3' has no statements

            	mQUOTE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "STRINGCONST"

    // $ANTLR start "STRINGCOMPONENTS"
    public void mSTRINGCOMPONENTS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = STRINGCOMPONENTS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:91:21: ( CHAR_STRING | '\\\\^' '@' .. '_' | '\\\\n' | '\\\\\\\"' | '\\\\\\\\' | '\\\\t' | '\\\\' DIGIT DIGIT DIGIT | '\\\\' ( ' ' | '\\t' | '\\n' | '\\r' | '\\f' )+ '\\\\' )
            int alt5 = 8;
            alt5 = dfa5.Predict(input);
            switch (alt5) 
            {
                case 1 :
                    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:91:23: CHAR_STRING
                    {
                    	mCHAR_STRING(); 

                    }
                    break;
                case 2 :
                    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:92:22: '\\\\^' '@' .. '_'
                    {
                    	Match("\\^"); 

                    	MatchRange('@','_'); 

                    }
                    break;
                case 3 :
                    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:93:22: '\\\\n'
                    {
                    	Match("\\n"); 


                    }
                    break;
                case 4 :
                    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:94:22: '\\\\\\\"'
                    {
                    	Match("\\\""); 


                    }
                    break;
                case 5 :
                    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:95:22: '\\\\\\\\'
                    {
                    	Match("\\\\"); 


                    }
                    break;
                case 6 :
                    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:96:22: '\\\\t'
                    {
                    	Match("\\t"); 


                    }
                    break;
                case 7 :
                    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:97:22: '\\\\' DIGIT DIGIT DIGIT
                    {
                    	Match('\\'); 
                    	mDIGIT(); 
                    	mDIGIT(); 
                    	mDIGIT(); 

                    }
                    break;
                case 8 :
                    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:98:22: '\\\\' ( ' ' | '\\t' | '\\n' | '\\r' | '\\f' )+ '\\\\'
                    {
                    	Match('\\'); 
                    	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:98:27: ( ' ' | '\\t' | '\\n' | '\\r' | '\\f' )+
                    	int cnt4 = 0;
                    	do 
                    	{
                    	    int alt4 = 2;
                    	    int LA4_0 = input.LA(1);

                    	    if ( ((LA4_0 >= '\t' && LA4_0 <= '\n') || (LA4_0 >= '\f' && LA4_0 <= '\r') || LA4_0 == ' ') )
                    	    {
                    	        alt4 = 1;
                    	    }


                    	    switch (alt4) 
                    		{
                    			case 1 :
                    			    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:
                    			    {
                    			    	if ( (input.LA(1) >= '\t' && input.LA(1) <= '\n') || (input.LA(1) >= '\f' && input.LA(1) <= '\r') || input.LA(1) == ' ' ) 
                    			    	{
                    			    	    input.Consume();

                    			    	}
                    			    	else 
                    			    	{
                    			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
                    			    	    Recover(mse);
                    			    	    throw mse;}


                    			    }
                    			    break;

                    			default:
                    			    if ( cnt4 >= 1 ) goto loop4;
                    		            EarlyExitException eee =
                    		                new EarlyExitException(4, input);
                    		            throw eee;
                    	    }
                    	    cnt4++;
                    	} while (true);

                    	loop4:
                    		;	// Stops C# compiler whinging that label 'loop4' has no statements

                    	Match('\\'); 

                    }
                    break;

            }
            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "STRINGCOMPONENTS"

    // $ANTLR start "LINECOMENTS"
    public void mLINECOMENTS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = LINECOMENTS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:100:18: ( '//' (~ ( '\\n' | '\\r' ) )* )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:100:19: '//' (~ ( '\\n' | '\\r' ) )*
            {
            	Match("//"); 

            	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:100:24: (~ ( '\\n' | '\\r' ) )*
            	do 
            	{
            	    int alt6 = 2;
            	    int LA6_0 = input.LA(1);

            	    if ( ((LA6_0 >= '\u0000' && LA6_0 <= '\t') || (LA6_0 >= '\u000B' && LA6_0 <= '\f') || (LA6_0 >= '\u000E' && LA6_0 <= '\uFFFF')) )
            	    {
            	        alt6 = 1;
            	    }


            	    switch (alt6) 
            		{
            			case 1 :
            			    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:100:25: ~ ( '\\n' | '\\r' )
            			    {
            			    	if ( (input.LA(1) >= '\u0000' && input.LA(1) <= '\t') || (input.LA(1) >= '\u000B' && input.LA(1) <= '\f') || (input.LA(1) >= '\u000E' && input.LA(1) <= '\uFFFF') ) 
            			    	{
            			    	    input.Consume();

            			    	}
            			    	else 
            			    	{
            			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            			    	    Recover(mse);
            			    	    throw mse;}


            			    }
            			    break;

            			default:
            			    goto loop6;
            	    }
            	} while (true);

            	loop6:
            		;	// Stops C# compiler whining that label 'loop6' has no statements

            	_channel = HIDDEN;

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "LINECOMENTS"

    // $ANTLR start "MULTILINECOMENTS"
    public void mMULTILINECOMENTS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = MULTILINECOMENTS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:102:21: ( '/*' (~ ( '/' | '*' ) | '/' ~ '*' | '*' ~ '/' | MULTILINECOMENTS )* '*/' )
            // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:102:22: '/*' (~ ( '/' | '*' ) | '/' ~ '*' | '*' ~ '/' | MULTILINECOMENTS )* '*/'
            {
            	Match("/*"); 

            	// C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:102:26: (~ ( '/' | '*' ) | '/' ~ '*' | '*' ~ '/' | MULTILINECOMENTS )*
            	do 
            	{
            	    int alt7 = 5;
            	    int LA7_0 = input.LA(1);

            	    if ( (LA7_0 == '*') )
            	    {
            	        int LA7_1 = input.LA(2);

            	        if ( ((LA7_1 >= '\u0000' && LA7_1 <= '.') || (LA7_1 >= '0' && LA7_1 <= '\uFFFF')) )
            	        {
            	            alt7 = 3;
            	        }


            	    }
            	    else if ( ((LA7_0 >= '\u0000' && LA7_0 <= ')') || (LA7_0 >= '+' && LA7_0 <= '.') || (LA7_0 >= '0' && LA7_0 <= '\uFFFF')) )
            	    {
            	        alt7 = 1;
            	    }
            	    else if ( (LA7_0 == '/') )
            	    {
            	        int LA7_3 = input.LA(2);

            	        if ( ((LA7_3 >= '\u0000' && LA7_3 <= ')') || (LA7_3 >= '+' && LA7_3 <= '\uFFFF')) )
            	        {
            	            alt7 = 2;
            	        }
            	        else if ( (LA7_3 == '*') )
            	        {
            	            alt7 = 4;
            	        }


            	    }


            	    switch (alt7) 
            		{
            			case 1 :
            			    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:102:27: ~ ( '/' | '*' )
            			    {
            			    	if ( (input.LA(1) >= '\u0000' && input.LA(1) <= ')') || (input.LA(1) >= '+' && input.LA(1) <= '.') || (input.LA(1) >= '0' && input.LA(1) <= '\uFFFF') ) 
            			    	{
            			    	    input.Consume();

            			    	}
            			    	else 
            			    	{
            			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            			    	    Recover(mse);
            			    	    throw mse;}


            			    }
            			    break;
            			case 2 :
            			    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:103:8: '/' ~ '*'
            			    {
            			    	Match('/'); 
            			    	if ( (input.LA(1) >= '\u0000' && input.LA(1) <= ')') || (input.LA(1) >= '+' && input.LA(1) <= '\uFFFF') ) 
            			    	{
            			    	    input.Consume();

            			    	}
            			    	else 
            			    	{
            			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            			    	    Recover(mse);
            			    	    throw mse;}


            			    }
            			    break;
            			case 3 :
            			    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:104:8: '*' ~ '/'
            			    {
            			    	Match('*'); 
            			    	if ( (input.LA(1) >= '\u0000' && input.LA(1) <= '.') || (input.LA(1) >= '0' && input.LA(1) <= '\uFFFF') ) 
            			    	{
            			    	    input.Consume();

            			    	}
            			    	else 
            			    	{
            			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            			    	    Recover(mse);
            			    	    throw mse;}


            			    }
            			    break;
            			case 4 :
            			    // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:105:8: MULTILINECOMENTS
            			    {
            			    	mMULTILINECOMENTS(); 

            			    }
            			    break;

            			default:
            			    goto loop7;
            	    }
            	} while (true);

            	loop7:
            		;	// Stops C# compiler whining that label 'loop7' has no statements

            	Match("*/"); 

            	_channel = HIDDEN;

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "MULTILINECOMENTS"

    override public void mTokens() // throws RecognitionException 
    {
        // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:8: ( END | WHILE | DO | FOR | IF | THEN | ELSE | FUNCTION | ARRAY | OF | LET | IN | TYPE | NIL | TO | BREAK | VAR | INT | STRING | WS | ID | PLUS | MINUS | ASTER | SLASH | POW | MOD | LPAREN | RPAREN | LKEY | RKEY | LBRACKET | RBRACKET | EQUAL | DISTINC | GT | LT | GE | LE | NOT | AND | OR | ASSIGN | DOT | COLON | COMMA | SEMICOLON | INTCONST | STRINGCONST | STRINGCOMPONENTS | LINECOMENTS | MULTILINECOMENTS )
        int alt8 = 52;
        alt8 = dfa8.Predict(input);
        switch (alt8) 
        {
            case 1 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:10: END
                {
                	mEND(); 

                }
                break;
            case 2 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:14: WHILE
                {
                	mWHILE(); 

                }
                break;
            case 3 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:20: DO
                {
                	mDO(); 

                }
                break;
            case 4 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:23: FOR
                {
                	mFOR(); 

                }
                break;
            case 5 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:27: IF
                {
                	mIF(); 

                }
                break;
            case 6 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:30: THEN
                {
                	mTHEN(); 

                }
                break;
            case 7 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:35: ELSE
                {
                	mELSE(); 

                }
                break;
            case 8 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:40: FUNCTION
                {
                	mFUNCTION(); 

                }
                break;
            case 9 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:49: ARRAY
                {
                	mARRAY(); 

                }
                break;
            case 10 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:55: OF
                {
                	mOF(); 

                }
                break;
            case 11 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:58: LET
                {
                	mLET(); 

                }
                break;
            case 12 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:62: IN
                {
                	mIN(); 

                }
                break;
            case 13 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:65: TYPE
                {
                	mTYPE(); 

                }
                break;
            case 14 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:70: NIL
                {
                	mNIL(); 

                }
                break;
            case 15 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:74: TO
                {
                	mTO(); 

                }
                break;
            case 16 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:77: BREAK
                {
                	mBREAK(); 

                }
                break;
            case 17 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:83: VAR
                {
                	mVAR(); 

                }
                break;
            case 18 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:87: INT
                {
                	mINT(); 

                }
                break;
            case 19 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:91: STRING
                {
                	mSTRING(); 

                }
                break;
            case 20 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:98: WS
                {
                	mWS(); 

                }
                break;
            case 21 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:101: ID
                {
                	mID(); 

                }
                break;
            case 22 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:104: PLUS
                {
                	mPLUS(); 

                }
                break;
            case 23 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:109: MINUS
                {
                	mMINUS(); 

                }
                break;
            case 24 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:115: ASTER
                {
                	mASTER(); 

                }
                break;
            case 25 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:121: SLASH
                {
                	mSLASH(); 

                }
                break;
            case 26 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:127: POW
                {
                	mPOW(); 

                }
                break;
            case 27 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:131: MOD
                {
                	mMOD(); 

                }
                break;
            case 28 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:135: LPAREN
                {
                	mLPAREN(); 

                }
                break;
            case 29 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:142: RPAREN
                {
                	mRPAREN(); 

                }
                break;
            case 30 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:149: LKEY
                {
                	mLKEY(); 

                }
                break;
            case 31 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:154: RKEY
                {
                	mRKEY(); 

                }
                break;
            case 32 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:159: LBRACKET
                {
                	mLBRACKET(); 

                }
                break;
            case 33 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:168: RBRACKET
                {
                	mRBRACKET(); 

                }
                break;
            case 34 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:177: EQUAL
                {
                	mEQUAL(); 

                }
                break;
            case 35 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:183: DISTINC
                {
                	mDISTINC(); 

                }
                break;
            case 36 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:191: GT
                {
                	mGT(); 

                }
                break;
            case 37 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:194: LT
                {
                	mLT(); 

                }
                break;
            case 38 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:197: GE
                {
                	mGE(); 

                }
                break;
            case 39 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:200: LE
                {
                	mLE(); 

                }
                break;
            case 40 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:203: NOT
                {
                	mNOT(); 

                }
                break;
            case 41 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:207: AND
                {
                	mAND(); 

                }
                break;
            case 42 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:211: OR
                {
                	mOR(); 

                }
                break;
            case 43 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:214: ASSIGN
                {
                	mASSIGN(); 

                }
                break;
            case 44 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:221: DOT
                {
                	mDOT(); 

                }
                break;
            case 45 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:225: COLON
                {
                	mCOLON(); 

                }
                break;
            case 46 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:231: COMMA
                {
                	mCOMMA(); 

                }
                break;
            case 47 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:237: SEMICOLON
                {
                	mSEMICOLON(); 

                }
                break;
            case 48 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:247: INTCONST
                {
                	mINTCONST(); 

                }
                break;
            case 49 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:256: STRINGCONST
                {
                	mSTRINGCONST(); 

                }
                break;
            case 50 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:268: STRINGCOMPONENTS
                {
                	mSTRINGCOMPONENTS(); 

                }
                break;
            case 51 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:285: LINECOMENTS
                {
                	mLINECOMENTS(); 

                }
                break;
            case 52 :
                // C:\\Users\\Y & A\\Documents\\Visual Studio 2008\\Projects\\Tiger Compiler (codename Bengala) - Release 1\\Bengala\\tiger.g:1:297: MULTILINECOMENTS
                {
                	mMULTILINECOMENTS(); 

                }
                break;

        }

    }


    protected DFA5 dfa5;
    protected DFA8 dfa8;
	private void InitializeCyclicDFAs()
	{
	    this.dfa5 = new DFA5(this);
	    this.dfa8 = new DFA8(this);
	    this.dfa5.specialStateTransitionHandler = new DFA.SpecialStateTransitionHandler(DFA5_SpecialStateTransition);
	    this.dfa8.specialStateTransitionHandler = new DFA.SpecialStateTransitionHandler(DFA8_SpecialStateTransition);
	}

    const string DFA5_eotS =
        "\x0a\uffff";
    const string DFA5_eofS =
        "\x0a\uffff";
    const string DFA5_minS =
        "\x01\x00\x01\uffff\x01\x09\x07\uffff";
    const string DFA5_maxS =
        "\x01\uffff\x01\uffff\x01\x74\x07\uffff";
    const string DFA5_acceptS =
        "\x01\uffff\x01\x01\x01\uffff\x01\x02\x01\x03\x01\x04\x01\x05\x01"+
        "\x06\x01\x07\x01\x08";
    const string DFA5_specialS =
        "\x01\x00\x09\uffff}>";
    static readonly string[] DFA5_transitionS = {
            "\x09\x01\x02\uffff\x17\x01\x01\uffff\x39\x01\x01\x02\uffa3"+
            "\x01",
            "",
            "\x02\x09\x01\uffff\x02\x09\x12\uffff\x01\x09\x01\uffff\x01"+
            "\x05\x0d\uffff\x0a\x08\x22\uffff\x01\x06\x01\uffff\x01\x03\x0f"+
            "\uffff\x01\x04\x05\uffff\x01\x07",
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
            get { return "91:1: STRINGCOMPONENTS : ( CHAR_STRING | '\\\\^' '@' .. '_' | '\\\\n' | '\\\\\\\"' | '\\\\\\\\' | '\\\\t' | '\\\\' DIGIT DIGIT DIGIT | '\\\\' ( ' ' | '\\t' | '\\n' | '\\r' | '\\f' )+ '\\\\' );"; }
        }

    }


    protected internal int DFA5_SpecialStateTransition(DFA dfa, int s, IIntStream _input) //throws NoViableAltException
    {
            IIntStream input = _input;
    	int _s = s;
        switch ( s )
        {
               	case 0 : 
                   	int LA5_0 = input.LA(1);

                   	s = -1;
                   	if ( ((LA5_0 >= '\u0000' && LA5_0 <= '\b') || (LA5_0 >= '\u000B' && LA5_0 <= '!') || (LA5_0 >= '#' && LA5_0 <= '[') || (LA5_0 >= ']' && LA5_0 <= '\uFFFF')) ) { s = 1; }

                   	else if ( (LA5_0 == '\\') ) { s = 2; }

                   	if ( s >= 0 ) return s;
                   	break;
        }
        NoViableAltException nvae =
            new NoViableAltException(dfa.Description, 5, _s, input);
        dfa.Error(nvae);
        throw nvae;
    }
    const string DFA8_eotS =
        "\x01\uffff\x0d\x2c\x05\uffff\x01\x42\x09\uffff\x01\x4e\x01\x50"+
        "\x03\uffff\x01\x55\x07\uffff\x02\x2c\x01\uffff\x01\x2c\x01\x5d\x02"+
        "\x2c\x01\x60\x01\x62\x02\x2c\x01\x65\x01\x2c\x01\x67\x05\x2c\x1d"+
        "\uffff\x01\x6d\x02\x2c\x01\uffff\x01\x70\x01\x2c\x01\uffff\x01\x72"+
        "\x01\uffff\x02\x2c\x01\uffff\x01\x2c\x01\uffff\x01\x76\x01\x77\x01"+
        "\x2c\x01\x79\x01\x2c\x01\uffff\x01\x7b\x01\x2c\x01\uffff\x01\x2c"+
        "\x01\uffff\x01\x7e\x01\x7f\x01\x2c\x02\uffff\x01\x2c\x01\uffff\x01"+
        "\x2c\x01\uffff\x01\u0083\x01\x2c\x02\uffff\x01\u0085\x01\u0086\x01"+
        "\x2c\x01\uffff\x01\x2c\x02\uffff\x01\u0089\x01\x2c\x01\uffff\x01"+
        "\u008b\x01\uffff";
    const string DFA8_eofS =
        "\u008c\uffff";
    const string DFA8_minS =
        "\x01\x00\x01\x6c\x01\x68\x02\x6f\x01\x66\x01\x68\x01\x72\x01\x66"+
        "\x01\x65\x01\x69\x01\x72\x01\x61\x01\x74\x05\uffff\x01\x2a\x09\uffff"+
        "\x02\x3d\x03\uffff\x01\x3d\x07\uffff\x01\x64\x01\x73\x01\uffff\x01"+
        "\x69\x01\x30\x01\x72\x01\x6e\x02\x30\x01\x65\x01\x70\x01\x30\x01"+
        "\x72\x01\x30\x01\x74\x01\x6c\x01\x65\x02\x72\x1d\uffff\x01\x30\x01"+
        "\x65\x01\x6c\x01\uffff\x01\x30\x01\x63\x01\uffff\x01\x30\x01\uffff"+
        "\x01\x6e\x01\x65\x01\uffff\x01\x61\x01\uffff\x02\x30\x01\x61\x01"+
        "\x30\x01\x69\x01\uffff\x01\x30\x01\x65\x01\uffff\x01\x74\x01\uffff"+
        "\x02\x30\x01\x79\x02\uffff\x01\x6b\x01\uffff\x01\x6e\x01\uffff\x01"+
        "\x30\x01\x69\x02\uffff\x02\x30\x01\x67\x01\uffff\x01\x6f\x02\uffff"+
        "\x01\x30\x01\x6e\x01\uffff\x01\x30\x01\uffff";
    const string DFA8_maxS =
        "\x01\uffff\x01\x6e\x01\x68\x01\x6f\x01\x75\x01\x6e\x01\x79\x01"+
        "\x72\x01\x66\x01\x65\x01\x69\x01\x72\x01\x61\x01\x74\x05\uffff\x01"+
        "\x2f\x09\uffff\x01\x3e\x01\x3d\x03\uffff\x01\x3d\x07\uffff\x01\x64"+
        "\x01\x73\x01\uffff\x01\x69\x01\x7a\x01\x72\x01\x6e\x02\x7a\x01\x65"+
        "\x01\x70\x01\x7a\x01\x72\x01\x7a\x01\x74\x01\x6c\x01\x65\x02\x72"+
        "\x1d\uffff\x01\x7a\x01\x65\x01\x6c\x01\uffff\x01\x7a\x01\x63\x01"+
        "\uffff\x01\x7a\x01\uffff\x01\x6e\x01\x65\x01\uffff\x01\x61\x01\uffff"+
        "\x02\x7a\x01\x61\x01\x7a\x01\x69\x01\uffff\x01\x7a\x01\x65\x01\uffff"+
        "\x01\x74\x01\uffff\x02\x7a\x01\x79\x02\uffff\x01\x6b\x01\uffff\x01"+
        "\x6e\x01\uffff\x01\x7a\x01\x69\x02\uffff\x02\x7a\x01\x67\x01\uffff"+
        "\x01\x6f\x02\uffff\x01\x7a\x01\x6e\x01\uffff\x01\x7a\x01\uffff";
    const string DFA8_acceptS =
        "\x0e\uffff\x01\x14\x01\x15\x01\x16\x01\x17\x01\x18\x01\uffff\x01"+
        "\x1a\x01\x1b\x01\x1c\x01\x1d\x01\x1e\x01\x1f\x01\x20\x01\x21\x01"+
        "\x22\x02\uffff\x01\x28\x01\x29\x01\x2a\x01\uffff\x01\x2c\x01\x2e"+
        "\x01\x2f\x01\x30\x01\x31\x01\x14\x01\x32\x02\uffff\x01\x15\x10\uffff"+
        "\x01\x16\x01\x17\x01\x18\x01\x33\x01\x34\x01\x19\x01\x1a\x01\x1b"+
        "\x01\x1c\x01\x1d\x01\x1e\x01\x1f\x01\x20\x01\x21\x01\x22\x01\x23"+
        "\x01\x27\x01\x25\x01\x26\x01\x24\x01\x28\x01\x29\x01\x2a\x01\x2b"+
        "\x01\x2d\x01\x2c\x01\x2e\x01\x2f\x01\x30\x03\uffff\x01\x03\x02\uffff"+
        "\x01\x05\x01\uffff\x01\x0c\x02\uffff\x01\x0f\x01\uffff\x01\x0a\x05"+
        "\uffff\x01\x01\x02\uffff\x01\x04\x01\uffff\x01\x12\x03\uffff\x01"+
        "\x0b\x01\x0e\x01\uffff\x01\x11\x01\uffff\x01\x07\x02\uffff\x01\x06"+
        "\x01\x0d\x03\uffff\x01\x02\x01\uffff\x01\x09\x01\x10\x02\uffff\x01"+
        "\x13\x01\uffff\x01\x08";
    const string DFA8_specialS =
        "\x01\x00\u008b\uffff}>";
    static readonly string[] DFA8_transitionS = {
            "\x09\x29\x02\x28\x02\x29\x01\x0e\x12\x29\x01\x0e\x01\x1f\x01"+
            "\x27\x02\x29\x01\x15\x01\x20\x01\x29\x01\x16\x01\x17\x01\x12"+
            "\x01\x10\x01\x24\x01\x11\x01\x23\x01\x13\x0a\x26\x01\x22\x01"+
            "\x25\x01\x1d\x01\x1c\x01\x1e\x02\x29\x1a\x0f\x01\x1a\x01\x29"+
            "\x01\x1b\x01\x14\x01\x0f\x01\x29\x01\x07\x01\x0b\x01\x0f\x01"+
            "\x03\x01\x01\x01\x04\x02\x0f\x01\x05\x02\x0f\x01\x09\x01\x0f"+
            "\x01\x0a\x01\x08\x03\x0f\x01\x0d\x01\x06\x01\x0f\x01\x0c\x01"+
            "\x02\x03\x0f\x01\x18\x01\x21\x01\x19\uff82\x29",
            "\x01\x2b\x01\uffff\x01\x2a",
            "\x01\x2d",
            "\x01\x2e",
            "\x01\x2f\x05\uffff\x01\x30",
            "\x01\x31\x07\uffff\x01\x32",
            "\x01\x33\x06\uffff\x01\x35\x09\uffff\x01\x34",
            "\x01\x36",
            "\x01\x37",
            "\x01\x38",
            "\x01\x39",
            "\x01\x3a",
            "\x01\x3b",
            "\x01\x3c",
            "",
            "",
            "",
            "",
            "",
            "\x01\x41\x04\uffff\x01\x40",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "\x01\x4d\x01\x4c",
            "\x01\x4f",
            "",
            "",
            "",
            "\x01\x54",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "\x01\x5a",
            "\x01\x5b",
            "",
            "\x01\x5c",
            "\x0a\x2c\x07\uffff\x1a\x2c\x04\uffff\x01\x2c\x01\uffff\x1a"+
            "\x2c",
            "\x01\x5e",
            "\x01\x5f",
            "\x0a\x2c\x07\uffff\x1a\x2c\x04\uffff\x01\x2c\x01\uffff\x1a"+
            "\x2c",
            "\x0a\x2c\x07\uffff\x1a\x2c\x04\uffff\x01\x2c\x01\uffff\x13"+
            "\x2c\x01\x61\x06\x2c",
            "\x01\x63",
            "\x01\x64",
            "\x0a\x2c\x07\uffff\x1a\x2c\x04\uffff\x01\x2c\x01\uffff\x1a"+
            "\x2c",
            "\x01\x66",
            "\x0a\x2c\x07\uffff\x1a\x2c\x04\uffff\x01\x2c\x01\uffff\x1a"+
            "\x2c",
            "\x01\x68",
            "\x01\x69",
            "\x01\x6a",
            "\x01\x6b",
            "\x01\x6c",
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
            "\x0a\x2c\x07\uffff\x1a\x2c\x04\uffff\x01\x2c\x01\uffff\x1a"+
            "\x2c",
            "\x01\x6e",
            "\x01\x6f",
            "",
            "\x0a\x2c\x07\uffff\x1a\x2c\x04\uffff\x01\x2c\x01\uffff\x1a"+
            "\x2c",
            "\x01\x71",
            "",
            "\x0a\x2c\x07\uffff\x1a\x2c\x04\uffff\x01\x2c\x01\uffff\x1a"+
            "\x2c",
            "",
            "\x01\x73",
            "\x01\x74",
            "",
            "\x01\x75",
            "",
            "\x0a\x2c\x07\uffff\x1a\x2c\x04\uffff\x01\x2c\x01\uffff\x1a"+
            "\x2c",
            "\x0a\x2c\x07\uffff\x1a\x2c\x04\uffff\x01\x2c\x01\uffff\x1a"+
            "\x2c",
            "\x01\x78",
            "\x0a\x2c\x07\uffff\x1a\x2c\x04\uffff\x01\x2c\x01\uffff\x1a"+
            "\x2c",
            "\x01\x7a",
            "",
            "\x0a\x2c\x07\uffff\x1a\x2c\x04\uffff\x01\x2c\x01\uffff\x1a"+
            "\x2c",
            "\x01\x7c",
            "",
            "\x01\x7d",
            "",
            "\x0a\x2c\x07\uffff\x1a\x2c\x04\uffff\x01\x2c\x01\uffff\x1a"+
            "\x2c",
            "\x0a\x2c\x07\uffff\x1a\x2c\x04\uffff\x01\x2c\x01\uffff\x1a"+
            "\x2c",
            "\x01\u0080",
            "",
            "",
            "\x01\u0081",
            "",
            "\x01\u0082",
            "",
            "\x0a\x2c\x07\uffff\x1a\x2c\x04\uffff\x01\x2c\x01\uffff\x1a"+
            "\x2c",
            "\x01\u0084",
            "",
            "",
            "\x0a\x2c\x07\uffff\x1a\x2c\x04\uffff\x01\x2c\x01\uffff\x1a"+
            "\x2c",
            "\x0a\x2c\x07\uffff\x1a\x2c\x04\uffff\x01\x2c\x01\uffff\x1a"+
            "\x2c",
            "\x01\u0087",
            "",
            "\x01\u0088",
            "",
            "",
            "\x0a\x2c\x07\uffff\x1a\x2c\x04\uffff\x01\x2c\x01\uffff\x1a"+
            "\x2c",
            "\x01\u008a",
            "",
            "\x0a\x2c\x07\uffff\x1a\x2c\x04\uffff\x01\x2c\x01\uffff\x1a"+
            "\x2c",
            ""
    };

    static readonly short[] DFA8_eot = DFA.UnpackEncodedString(DFA8_eotS);
    static readonly short[] DFA8_eof = DFA.UnpackEncodedString(DFA8_eofS);
    static readonly char[] DFA8_min = DFA.UnpackEncodedStringToUnsignedChars(DFA8_minS);
    static readonly char[] DFA8_max = DFA.UnpackEncodedStringToUnsignedChars(DFA8_maxS);
    static readonly short[] DFA8_accept = DFA.UnpackEncodedString(DFA8_acceptS);
    static readonly short[] DFA8_special = DFA.UnpackEncodedString(DFA8_specialS);
    static readonly short[][] DFA8_transition = DFA.UnpackEncodedStringArray(DFA8_transitionS);

    protected class DFA8 : DFA
    {
        public DFA8(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 8;
            this.eot = DFA8_eot;
            this.eof = DFA8_eof;
            this.min = DFA8_min;
            this.max = DFA8_max;
            this.accept = DFA8_accept;
            this.special = DFA8_special;
            this.transition = DFA8_transition;

        }

        override public string Description
        {
            get { return "1:1: Tokens : ( END | WHILE | DO | FOR | IF | THEN | ELSE | FUNCTION | ARRAY | OF | LET | IN | TYPE | NIL | TO | BREAK | VAR | INT | STRING | WS | ID | PLUS | MINUS | ASTER | SLASH | POW | MOD | LPAREN | RPAREN | LKEY | RKEY | LBRACKET | RBRACKET | EQUAL | DISTINC | GT | LT | GE | LE | NOT | AND | OR | ASSIGN | DOT | COLON | COMMA | SEMICOLON | INTCONST | STRINGCONST | STRINGCOMPONENTS | LINECOMENTS | MULTILINECOMENTS );"; }
        }

    }


    protected internal int DFA8_SpecialStateTransition(DFA dfa, int s, IIntStream _input) //throws NoViableAltException
    {
            IIntStream input = _input;
    	int _s = s;
        switch ( s )
        {
               	case 0 : 
                   	int LA8_0 = input.LA(1);

                   	s = -1;
                   	if ( (LA8_0 == 'e') ) { s = 1; }

                   	else if ( (LA8_0 == 'w') ) { s = 2; }

                   	else if ( (LA8_0 == 'd') ) { s = 3; }

                   	else if ( (LA8_0 == 'f') ) { s = 4; }

                   	else if ( (LA8_0 == 'i') ) { s = 5; }

                   	else if ( (LA8_0 == 't') ) { s = 6; }

                   	else if ( (LA8_0 == 'a') ) { s = 7; }

                   	else if ( (LA8_0 == 'o') ) { s = 8; }

                   	else if ( (LA8_0 == 'l') ) { s = 9; }

                   	else if ( (LA8_0 == 'n') ) { s = 10; }

                   	else if ( (LA8_0 == 'b') ) { s = 11; }

                   	else if ( (LA8_0 == 'v') ) { s = 12; }

                   	else if ( (LA8_0 == 's') ) { s = 13; }

                   	else if ( (LA8_0 == '\r' || LA8_0 == ' ') ) { s = 14; }

                   	else if ( ((LA8_0 >= 'A' && LA8_0 <= 'Z') || LA8_0 == '_' || LA8_0 == 'c' || (LA8_0 >= 'g' && LA8_0 <= 'h') || (LA8_0 >= 'j' && LA8_0 <= 'k') || LA8_0 == 'm' || (LA8_0 >= 'p' && LA8_0 <= 'r') || LA8_0 == 'u' || (LA8_0 >= 'x' && LA8_0 <= 'z')) ) { s = 15; }

                   	else if ( (LA8_0 == '+') ) { s = 16; }

                   	else if ( (LA8_0 == '-') ) { s = 17; }

                   	else if ( (LA8_0 == '*') ) { s = 18; }

                   	else if ( (LA8_0 == '/') ) { s = 19; }

                   	else if ( (LA8_0 == '^') ) { s = 20; }

                   	else if ( (LA8_0 == '%') ) { s = 21; }

                   	else if ( (LA8_0 == '(') ) { s = 22; }

                   	else if ( (LA8_0 == ')') ) { s = 23; }

                   	else if ( (LA8_0 == '{') ) { s = 24; }

                   	else if ( (LA8_0 == '}') ) { s = 25; }

                   	else if ( (LA8_0 == '[') ) { s = 26; }

                   	else if ( (LA8_0 == ']') ) { s = 27; }

                   	else if ( (LA8_0 == '=') ) { s = 28; }

                   	else if ( (LA8_0 == '<') ) { s = 29; }

                   	else if ( (LA8_0 == '>') ) { s = 30; }

                   	else if ( (LA8_0 == '!') ) { s = 31; }

                   	else if ( (LA8_0 == '&') ) { s = 32; }

                   	else if ( (LA8_0 == '|') ) { s = 33; }

                   	else if ( (LA8_0 == ':') ) { s = 34; }

                   	else if ( (LA8_0 == '.') ) { s = 35; }

                   	else if ( (LA8_0 == ',') ) { s = 36; }

                   	else if ( (LA8_0 == ';') ) { s = 37; }

                   	else if ( ((LA8_0 >= '0' && LA8_0 <= '9')) ) { s = 38; }

                   	else if ( (LA8_0 == '\"') ) { s = 39; }

                   	else if ( ((LA8_0 >= '\t' && LA8_0 <= '\n')) ) { s = 40; }

                   	else if ( ((LA8_0 >= '\u0000' && LA8_0 <= '\b') || (LA8_0 >= '\u000B' && LA8_0 <= '\f') || (LA8_0 >= '\u000E' && LA8_0 <= '\u001F') || (LA8_0 >= '#' && LA8_0 <= '$') || LA8_0 == '\'' || (LA8_0 >= '?' && LA8_0 <= '@') || LA8_0 == '\\' || LA8_0 == '`' || (LA8_0 >= '~' && LA8_0 <= '\uFFFF')) ) { s = 41; }

                   	if ( s >= 0 ) return s;
                   	break;
        }
        NoViableAltException nvae =
            new NoViableAltException(dfa.Description, 8, _s, input);
        dfa.Error(nvae);
        throw nvae;
    }
 
    
}
