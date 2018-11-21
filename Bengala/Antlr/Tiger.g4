grammar Tiger;

/*
 * Parser Rules
 */

//I comment the old way to build the AST in this program rule for references
program    /*returns [ExpressionAst res]*/	   	: e =exp //{$ctx.res = $e.res;}
		EOF;

//expresiones de tiger
exp        : ifE = ifExp 
						| forE = forExp 
						| let= letExp 
						//| (lvalue ASSIGN)=>lv= lvalue s = ASSIGN initExp = exp  {res = new AssignExpressionAst(lv,initExp, s.Line, s.CharPositionInLine);}
						//| (ID LBRACKET exp? RBRACKET OF )=> id = ID LBRACKET  sizeExp = exp RBRACKET OF initExp= exp {res = new ArrayInstatiationAST(id.Text,sizeExp,initExp, id.Line, id.CharPositionInLine);}
						| whileE =  whileInstr 
						| breakE = breakInstr 
						| record = recordInstance 
						| e= expOrAnd;
						
						
												
expOrAnd  : e1 =expCOMP 	(
									     (s = AND
								            |s = OR)      e2= expCOMP  
								           )* ;
								           								         
expCOMP    : e1 =expEQ   	
			(
                                      (s = GT
									    |s = LT									  
									    |s = GE
									    |s = LE)     e2 =expEQ     
									   )? ;
									 
expEQ 	   : e1 =expNE   	(
									     s = EQUAL   e2 =expNE      
                                                                           )? ;
                                                                           
expNE 	   : e1 =expSumRes (
									     s = DISTINC e2 =expSumRes  
									   )? ;
									   
expSumRes  : e1 =expPorDiv (
									    ( s = MINUS   
									    |s = PLUS
									    )    e2 =expPorDiv  
									   )* ;
									   
expPorDiv  : e1 =expMod    ((
									     s = SLASH  
									    |s = ASTER)   e2 =expMod     
									   )* ;
									   
expMod	   : f = factor    (
									     s = MOD     e2 =factor     
									   )? ;	
									   
factor     :                           ( m = MINUS?   f =fExp  )
								             | n = NIL ;	
								                                                 
seqExp 	 : LPAREN (temp= exp (SEMICOLON temp1 = exp)*)? RPAREN ;
						
fExp 	 : i= INTCONST  
						| s = STRINGCONST 
						| seq_exp = seqExp 
						//llamada a funcion		 
						| id = ID LPAREN argList = listExp?  RPAREN
			
						| l_value =lvalue 
						;
			
listExp    :temp =  exp (COMMA temp1= exp)*;

ifExp 	   :i = IF cond= exp THEN e1=exp (ELSE e2=exp)? ;

forExp 	   :f = FOR id =ID ASSIGN e1=exp  TO e2=exp DO e3=exp ;

letExp 	   : l = LET   (d =decl)+  IN (insts = instructions)? END ;

//conjunto de instrucciones que va dentro de un let
instructions : e1 = exp (SEMICOLON e2=exp )*;						


whileInstr : id = WHILE cond = exp DO body= exp ;
	
lvalue 	: id = ID (DOT fieldId= ID | LBRACKET indexExp=exp  RBRACKET )* ;


//assignacion de valores a los campos de un record
fieldList : id =  ID EQUAL e1=exp (COMMA id2 =ID EQUAL e2= exp)*;//incompleta

breakInstr : id =  BREAK           ;
	
//la instanciacion de un record
recordInstance : id =  ID LKEY   l=fieldList?  RKEY ;
	
//las posibles declaraciones
decl 	: t1 =  typeDecl 
						| v1 =  varDecl  
						| f1 =  funDecl  ;

//declaraciones de tipos  : Alias , Record ,Array
typeDecl     : TYPE id = ID EQUAL (
								       type_id = typeId              
								       |LKEY (typeList =typeFields)?  RKEY
								       |ARRAY OF typeOfArray=typeId  
								      ) ;

typeId 	     : id = ID        
						| i  = INT       
						| s  = STRING    ;

varDecl      : VAR   id = ID (
								  ASSIGN value1 = exp  
						                 |COLON type_Id = typeId ASSIGN value2 =exp 
						                );

//representa la declaracion tanto de parametros de una funcion como los campos de un record
typeFields 	: id = ID COLON type_id = typeId  
							( 
							 COMMA      id1 = ID COLON typeId1 =typeId 
							)* ;

//la declaracion de una funcion o un procedimiento
funDecl : f  = FUNCTION     fId = ID LPAREN (pList = typeFields)? RPAREN  (COLON ret =typeId)?  
                                                          EQUAL    body = exp ;	




/*
 * Lexer Rules
 */

WS
	:	 (' '|'\t'|'\n'|'\r')  -> channel(HIDDEN)
	;

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

LINECOMENTS 					:'//' (~('\n'|'\r'))* -> channel(HIDDEN);

MULTILINECOMENTS				:'/*'(~('/'|'*')
						|'/'~'*'
						|'*'~'/'
						|MULTILINECOMENTS
						)* '*/' -> channel (HIDDEN);
