grammar Tiger;

options
{
	k=2;
}


/*
 * Parser Rules
 */
@header
{
	using Bengala.AST;	
}
//I comment the old way to build the AST in this program rule for references
program    /*returns [ExpressionAst res]	  */: e =exp //{$ctx.res = $e.res;}
		EOF;

//expresiones de tiger
exp      : ifExp 
						| forExp 
						| letExp 
						| assignment 
						| arrayInstance
						| whileInstr 
						| breakInstr 
						| recordInstance 
						| expOrAnd;
						
						
												
expOrAnd  : e1 =expCOMP expOrAndList*;
expOrAndList : 	(s = AND |s = OR) e2= expCOMP;
								           								         
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
									   
expSumRes  : e1 = expPorDiv srList = sumResList*;
sumResList :  ( s = MINUS | s = PLUS )  e2 =expPorDiv  ;
									   
expPorDiv  : e1 =expMod  expPorDivList* ;
expPorDivList: (s = SLASH  | s = ASTER) e2 =expMod;
									   
expMod	   : f = factor    (s = MOD  e2 =factor)? ;	
									   
factor     :  negExpr | fExp ;
negExpr    : m = MINUS  f = fExp ;

						
fExp 	 : 
			intRule  
						| strRule 
						| nilRule
						| seqExp 
						//llamada a funcion		 
						| fCall 			
						| prefixExpr 
						;
			


intRule : i = INTCONST;

strRule : s = STRINGCONST ;

nilRule : n = 'nil';

seqExp 	 : LPAREN (exp (SEMICOLON exp)*)? RPAREN ;

fCall	   : ID LPAREN argList = listExp?  RPAREN ;

listExp    :exp (COMMA exp)*;

ifExp 	   : 'if' cond= exp 'then' e1=exp ('else' e2=exp)? ;

forExp 	   :f = 'for' id =ID ASSIGN e1=exp  'to' e2=exp 'do' e3=exp ;

letExp 	   : 'let'   (d =decl)+  'in' (insts = instructions)? 'end' ;

//conjunto de instrucciones que va dentro de un let
instructions : exp (SEMICOLON exp )*;

assignment  : prefixExpr ASSIGN exp;

arrayInstance : id = ID LBRACKET sizeExp = exp RBRACKET 'of' initExp= exp;

whileInstr : 'while' cond = exp 'do' body= exp ;

/*
example of matches: 
- foo
- foo.foo
- foo[1].foo
*/
prefixExpr 	: id = ID prefixAccess*; 
prefixAccess: DOT ID | 
			  LBRACKET exp  RBRACKET ;


//assignacion de valores a los campos de un record
fieldList : fieldInstance (COMMA fieldInstance)*;
fieldInstance : id =  ID EQUAL e=exp;


breakInstr : 'break';
	
//la instanciacion de un record
recordInstance : id =  ID LKEY   fields=fieldList?  RKEY ;
	
//las posibles declaraciones
decl 	: t1 =  typeDecl 
						| v1 =  varDecl  
						| f1 =  funDecl  ;

//declaraciones de tipos  : Alias , Record ,Array
typeDecl     : 'type' id = ID EQUAL typeDefinition;
typeDefinition : aliasType | recordDef | arrayType;
aliasType	: type_id = typeId;
recordDef   : LKEY (typeList =typeFields)?  RKEY;
arrayType	: 'array' 'of' typeOfArray = typeId; 

typeId 	     : ID;

varDecl      : /*VAR*/'var'   id = ID (
								  ASSIGN value = exp  
						                 |COLON type_Id = typeId ASSIGN value =exp 
						                );

//representa la declaracion tanto de parametros de una funcion como los campos de un record
typeFields 	: formalParameter  (COMMA formalParameter)*;
formalParameter : id = ID COLON type= typeId;

//la declaracion de una funcion o un procedimiento
funDecl : f  = 'function'   fId = ID LPAREN (pList = typeFields)? RPAREN  (COLON ret =typeId)?  
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

