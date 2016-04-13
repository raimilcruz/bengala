grammar tiger;
	

options
{
	k=2;
	language = CSharp2;
}

	

tokens
{
	END      = 'end';
	WHILE    = 'while';
	DO       = 'do';
	FOR      = 'for';
	IF       = 'if';
	THEN     = 'then';
	ELSE     = 'else';
	FUNCTION = 'function';
	ARRAY    = 'array';
	OF       = 'of';
	LET      = 'let';
	IN       = 'in';
	TYPE     = 'type';
	NIL      = 'nil';
	TO       = 'to';
	BREAK    = 'break';
	VAR      = 'var';	
	INT      = 'int';
	STRING   = 'string';
	
	
}					
				
@header
{
	using Bengala.AST;
	using Bengala.AST.SemanticsUtils;
	using System.Collections.Generic;
}		

WS						: (' '|'\t'|'\n'|'\r') {$channel = HIDDEN;};
ID	 					: CHAR (CHAR | DIGIT)* ;


PLUS 						: '+';
MINUS 						: '-';
ASTER  						: '*';
SLASH 						: '/';
POW   						: '^';
MOD             				: '%';

LPAREN 	    					: '(';
RPAREN	    					: ')';
LKEY  	    					: '{';
RKEY  	    					: '}';
LBRACKET    					: '[';
RBRACKET    					: ']';
			
EQUAL 						: '=';
DISTINC 					: '<>';
GT  						: '>';
LT 						: '<';
GE						: '>=';
LE						: '<=';
	
NOT						: '!';
AND 						: '&';
OR 						: '|';

ASSIGN 						: ':=';
DOT 						: '.';
COLON 						: ':';
COMMA 						: ',';
SEMICOLON 					: ';';


fragment CHAR 					: 'a'..'z' |'A'..'Z' |'_' ;
	
fragment DIGIT					: '0'..'9';	

fragment QUOTE						: '"';

fragment CHAR_STRING 			        : ~('"'|'\\'|'\n'|'\t');

INTCONST 					    : DIGIT+ ;

STRINGCONST 					: QUOTE STRINGCOMPONENTS* QUOTE;
STRINGCOMPONENTS				: CHAR_STRING
				                |'\\^' '@'..'_'
			               		|'\\n'
			                	|'\\\"'
			                	|'\\\\'
			                	|'\\t'
			                	|'\\'DIGIT DIGIT DIGIT
			                	|'\\' (' '|'\t'|'\n'|'\r'|'\f')+ '\\';	

LINECOMENTS 					:'//' (~('\n'|'\r'))* {$channel = HIDDEN;};

MULTILINECOMENTS				:'/*'(~('/'|'*')
						|'/'~'*'
						|'*'~'/'
						|MULTILINECOMENTS
						)* '*/' {$channel = HIDDEN;};
		
//Un programa tiger es una expresion
program    returns [ExpressionAST res]	   	: e =exp {res = e;} EOF;

//expresiones de tiger
exp        returns [ExpressionAST res]	 	: ifE = ifExp {res = ifE;}
						| forE = forExp {res = forE;}
						| let= letExp {res = let;}
						| (lvalue ASSIGN)=>lv= lvalue s = ASSIGN initExp = exp  {res = new AssignExpressionAST(lv,initExp, s.Line, s.CharPositionInLine);}
						| (ID LBRACKET exp? RBRACKET OF )=> id = ID LBRACKET  sizeExp = exp RBRACKET OF initExp= exp {res = new ArrayInstatiationAST(id.Text,sizeExp,initExp, id.Line, id.CharPositionInLine);}
						| whileE =  whileInstr {res = whileE;}
						| breakE = breakInstr {res = breakE;}
						| record = recordInstance {res =record;}
						| e= expOrAnd {res = e;};
						
						
												
expOrAnd   returns[ExpressionAST res]		: e1 =expCOMP 	{res= e1;} (
									     (s = AND
								            |s = OR)      e2= expCOMP   {  res = LogicalExpressionAST.GetLogicalExpressionAST(res,e2,s.Text, s.Line, s.CharPositionInLine);}
								           )* ;
								           								         
expCOMP    returns[ExpressionAST res]		: e1 =expEQ   	{res = e1;}(
                                                                            (s = GT
									    |s = LT									  
									    |s = GE
									    |s = LE)     e2 =expEQ      {  res = BinaryExpressionAST.GetBinaryExpressionAST(res,e2,s.Text, s.Line, s.CharPositionInLine);}
									   )? ;
									 
expEQ 	   returns[ExpressionAST res]		: e1 =expNE   	{res = e1;}(
									     s = EQUAL   e2 =expNE      {  res = BinaryExpressionAST.GetBinaryExpressionAST(res,e2,s.Text, s.Line, s.CharPositionInLine);}
                                                                           )? ;
                                                                           
expNE 	   returns[ExpressionAST res]		: e1 =expSumRes {res = e1;}(
									     s = DISTINC e2 =expSumRes  {  res = BinaryExpressionAST.GetBinaryExpressionAST(res,e2,s.Text, s.Line, s.CharPositionInLine);}									    
									   )? ;
									   
expSumRes  returns[ExpressionAST res]		: e1 =expPorDiv {res =e1;} (
									    ( s = MINUS   
									    |s = PLUS
									    )    e2 =expPorDiv  {  res = BinaryExpressionAST.GetBinaryExpressionAST(res,e2,s.Text, s.Line, s.CharPositionInLine);}
									   )* ;
									   
expPorDiv  returns[ExpressionAST res]		: e1 =expMod    {res = e1;}((
									     s = SLASH  
									    |s = ASTER)   e2 =expMod     {  res = BinaryExpressionAST.GetBinaryExpressionAST(res,e2,s.Text, s.Line, s.CharPositionInLine);}
									   )* ;
									   
expMod	   returns[ExpressionAST res]           : f = factor    {res = f;} (
									     s = MOD     e2 =factor     {  res = BinaryExpressionAST.GetBinaryExpressionAST(res,e2,s.Text, s.Line, s.CharPositionInLine);}
									   )? ;	
									   
factor     returns[ExpressionAST res]		:                           ( m = MINUS?   f =fExp       {
                                                                                                           if(m!=null)
													       res= new NegExpressionAST(f, m.Line, m.CharPositionInLine);
								                                           else
								                                               res = f;
								                                         })
								             | n = NIL {res = new NilAST(n.Line, n.CharPositionInLine);};	
								                                                 
seqExp 	   returns[List<ExpressionAST> parametros]	
	   @init
	   {
	       parametros = new List<ExpressionAST>();
	   }
	   					: LPAREN (temp= exp{parametros.Add(temp);} (SEMICOLON temp1 = exp{parametros.Add(temp1);})*)? RPAREN ;
						
fExp 	   returns [ExpressionAST res] 
	   @init
	   {
	      argList =new  List<ExpressionAST>(); 
	   }		
						: i= INTCONST {res = new IntAST(int.Parse(i.Text), i.Line, i.CharPositionInLine);} 
						| s = STRINGCONST {res = new StringAST(s.Text,s.Line, s.CharPositionInLine);}
						| seq_exp = seqExp {res = new SequenceExpressionAST(seq_exp);}
						//llamada a funcion		 
						| id = ID LPAREN argList = listExp? {res = new CallFunctionAST(id.Text,argList, id.Line, id.CharPositionInLine);} RPAREN
			
						| l_value =lvalue {res = l_value ;}
						;
			
listExp    returns[List<ExpressionAST> res]
	   @init
	   {
	       res = new List<ExpressionAST>();
	   }	
						:temp =  exp {res.Add(temp);}(COMMA temp1= exp{res.Add(temp1);})*;

ifExp 	   returns[ExpressionAST res]		:i = IF cond= exp THEN e1=exp (ELSE e2=exp)?{res = new IfExpressionAST(cond,e1,e2, i.Line, i.CharPositionInLine);}  ;

forExp 	   returns[ExpressionAST res]		:f = FOR id =ID ASSIGN e1=exp  TO e2=exp DO e3=exp {res = new ForExpressionAST(id.Text,e1,e2,e3, f.Line, f.CharPositionInLine);};

letExp 	   returns[ExpressionAST res]
	   @init
	   {
	       List<ExpressionAST> list = new List<ExpressionAST>();
	       insts = new List<ExpressionAST>();       
	   }
						: l = LET   (d =decl{list.Add(d);})+  IN (insts = instructions)? END {  res = new LetExpressionAST(list,new SequenceExpressionAST(insts));};

//conjunto de instrucciones que va dentro de un let
instructions 
	returns[List<ExpressionAST> res]
	@init{res = new List<ExpressionAST>();}
						: e1 = exp{ res.Add(e1);} (SEMICOLON e2=exp {  res.Add(e2);})*;						


whileInstr 
	returns[ExpressionAST res]		: id = WHILE cond = exp DO body= exp {  res = new WhileExpressionAST(cond,body,id.Line, id.CharPositionInLine);};
	
lvalue 	returns[LValueAST res]	       	        : id = ID {  res = new VarAST(id.Text, id.Line, id.CharPositionInLine);}(DOT fieldId= ID {res= new RecordAccessAST(fieldId.Text,res, fieldId.Line, fieldId.CharPositionInLine);}| LBRACKET indexExp=exp  RBRACKET {res = new ArrayAccessAST(res,indexExp, indexExp.Line, indexExp.Columns);})* ;


//assignacion de valores a los campos de un record
fieldList 
	returns[List<KeyValuePair<string, ExpressionAST>> res] 
	@init
	{
	    res = new List<KeyValuePair<string,ExpressionAST>>();
	}					
						: id =  ID EQUAL e1=exp {  res.Add(new KeyValuePair<string,ExpressionAST>(id.Text,e1)); }(COMMA id2 =ID EQUAL e2= exp{res.Add(new KeyValuePair<string,ExpressionAST>(id2.Text,e2));})*;//incompleta

breakInstr 
	returns[ExpressionAST res]		: id =  BREAK           {  res = new BreakAST(id.Line, id.CharPositionInLine);};
	
//la instanciacion de un record
recordInstance 
	returns[ExpressionAST res]   		: id =  ID LKEY   l=fieldList?  RKEY {  res = new RecordInstantiationAST(id.Text,l, id.Line, id.CharPositionInLine);};
	
//las posibles declaraciones
decl 	returns[ExpressionAST res]		: t1 =  typeDecl {  res=t1;}
						| v1 =  varDecl  {  res=v1;} 
						| f1 =  funDecl  {  res= f1;};

//declaraciones de tipos  : Alias , Record ,Array
typeDecl     returns[ExpressionAST res]		: TYPE id = ID EQUAL (
								       type_id = typeId              {  res = new AliasAST(id.Text,type_id, id.Line, id.CharPositionInLine);}
								       |LKEY (typeList =typeFields)? {  res = new RecordDeclarationAST(id.Text,typeList, id.Line, id.CharPositionInLine);} RKEY
								       |ARRAY OF typeOfArray=typeId  {  res = new ArrayDeclarationAST(id.Text,typeOfArray, id.Line, id.CharPositionInLine);}
								      ) ;

typeId 	     returns[string res]		: id = ID        {  res = id.Text;}
						| i  = INT       {  res = i.Text; }
						| s  = STRING    {  res = s.Text; };

varDecl      returns[ExpressionAST res]		: VAR   id = ID (
								  ASSIGN value1 = exp {value2=value1;} 
						                 |COLON type_Id = typeId ASSIGN value2 =exp {value1=value2;}
						                ){res = new VarDeclarationAST(id.Text,type_Id,value2, id.Line, id.CharPositionInLine);} ;

//representa la declaracion tanto de parametros de una funcion como los campos de un record
typeFields 	
	returns[List<KeyValuePair<string, string>> res]	
	@init
	{
	    res = new List<KeyValuePair<string,string>>();
	}
						: id = ID COLON type_id = typeId {res.Add(new KeyValuePair<string,string>(id.Text,type_id));} 
							( 
							 COMMA      id1 = ID COLON typeId1 =typeId {res.Add(new KeyValuePair<string,string>(id1.Text,typeId1));}
							)* ;

//la declaracion de una funcion o un procedimiento
funDecl returns[ExpressionAST res]		: f  = FUNCTION     fId = ID LPAREN (pList = typeFields)? RPAREN  (COLON ret =typeId)?  
                                                          EQUAL    body = exp{res = new FunctionDeclarationAST(fId.Text,pList,body,ret, f.Line, f.CharPositionInLine);} ;	








	



	

