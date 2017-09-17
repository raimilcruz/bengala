using System;
using Bengala.AST;
using Bengala.AST.SemanticsUtils;

namespace Bengala.Analysis
{
    public class StaticChecker : AstVisitor<bool>
    {
        Scope _scope;
        ErrorListener _errorListener;
        public StaticChecker()
        {
            //TODO: Add another constructor overload for _scope
            _scope = new Scope(null);
        }
        public StaticChecker(ErrorListener errorListener)
        {
            _errorListener = errorListener;
        }

        public override bool VisitNode(AstNode node) {
            throw new NotImplementedException("I don't know how to do it");
        }

        public override bool VisitIntAst(IntAST ast)
        {
            ast.NodeType = TigerType.GetType<IntType>();
            return true;
        }

        public override bool VisitStringAst(StringAST ast) {
            ast.NodeType = TigerType.GetType<StringType>();
            return true;
        }

        public override bool VisitVar(VarAST ast) {
            //lookup in the scope for the type
            throw new NotImplementedException("StaticChecker.VisitVar");
        }

        public override bool VisitBinaryExpression(BinaryExpressionAST expr)
        {
            expr.LeftExp.Accept(this);
            TigerType leftType = expr.LeftExp.NodeType;

            expr.RightExp.Accept(this);
            TigerType rightType = expr.RightExp.NodeType;

            TigerType tt;
            if (CheckOperator(leftType, rightType,expr.Operator, out tt))
            {
                expr.NodeType = tt;
                return true;
            }
            _errorListener.Add(
                new ErrorMessage(string.Format(Message.LoadMessage("SupportOp"), expr.Operator, leftType, rightType), expr.Line,
                    expr.Columns));
            expr.NodeType = TigerType.GetType<ErrorType>();
            return false;
        }
        
        public override bool VisitIfExpression(IfExpressionAST ast)
        {
            ast.ExpConditional.Accept(this);

            var returnType = TigerType.GetType<ErrorType>();
            if (ast.ExpConditional.NodeType != TigerType.GetType<IntType>())
                _errorListener.Add(new ErrorMessage(Message.LoadMessage("IfCond"), ast.Line, ast.Columns));
            else if (ast.ExpressionThen.Accept(this))
            {
                if (ast.ExpressionElse != null)
                {
                    ast.ExpressionElse.Accept(this);
                    returnType = ast.ExpressionElse.NodeType.CanConvertTo(ast.ExpressionThen.NodeType)
                        ?
                        ast.ExpressionThen.NodeType
                        :
                        ast.ExpressionThen.NodeType.CanConvertTo(ast.ExpressionElse.NodeType)
                            ?
                            ast.ExpressionElse.NodeType
                            : TigerType.GetType<ErrorType>();
                    if (!(returnType is ErrorType))
                        return ast.AlwaysReturn = true;

                    _errorListener.Add(
                        new ErrorMessage(
                            string.Format(Message.LoadMessage("Match"), ast.ExpressionThen.NodeType,
                                ast.ExpressionElse.NodeType), ast.Line, ast.Columns));
                }
                ast.AlwaysReturn = false;
                ast.NodeType = TigerType.GetType<NoType>();
                return true;
            }
            return false;

        }

        public override bool VisitVarDeclaration(VarDeclarationAST expr)
        {
            //linea nueva ,posiblemente cambiable.

            //se crea un nuevo scope aca
            var outerScope = _scope;
            _scope=  new Scope(outerScope);

            ScopeLocation idLocation = _scope.HasVar(expr.Id);
            if (idLocation == ScopeLocation.DeclaredLocal)
            {
                _errorListener.Add(new ErrorMessage(string.Format(Message.LoadMessage("VarDecl"), expr.Id), expr.Line, expr.Columns));
                expr.NodeType = TigerType.GetType<ErrorType>();
                return false;
            }
            if (idLocation != ScopeLocation.NotDeclared)
                _errorListener.Add(new WarningMessage(string.Format(Message.LoadMessage("Hide"), expr.Id), expr.Line, expr.Columns));

            //TODO: Rename ExpressionValue to NamedExpression
            expr.ExpressionValue.Accept(this);


            //se asume q no habran problemas de compatibilidad
            expr.NodeType = TigerType.GetType<NoType>();

            // si se expecifico de forma explicita el tipo de la variable...
            if (!string.IsNullOrEmpty(expr.TypeId))
            {
                TigerType tt;
                //existe el tipo
                if (_scope.HasType(expr.TypeId, out tt) != ScopeLocation.NotDeclared)
                {
                    //el tipo de la variable no machea con el de la expression
                    if (!expr.ExpressionValue.ReturnType.CanConvertTo(tt))
                    {
                        _errorListener.Add(
                            new ErrorMessage(
                                string.Format(Message.LoadMessage("Match"), expr.TypeId, expr.ExpressionValue.ReturnType.TypeID),
                                expr.Line, expr.Columns));
                        expr.NodeType= TigerType.GetType<ErrorType>();
                        _scope.AddVar(expr.Id, TigerType.GetType<ErrorType>().TypeID);
                        return false;
                    }
                    expr.NodeType = expr.ExpressionValue.ReturnType;
                    //si me especifica el tipo explicitamente .
                    _scope.AddVar(expr.Id, tt.TypeID);
                    return true;
                }
                // no existe el tipo de la variable
                _errorListener.Add(new ErrorMessage(string.Format(Message.LoadMessage("TypeUndecl"), expr.TypeId), expr.Line, expr.Columns));
                expr.NodeType = TigerType.GetType<ErrorType>();
                _scope.AddVar(expr.Id, TigerType.GetType<ErrorType>().TypeID);
                return false;
            }
            if (!expr.ExpressionValue.ReturnType.IsLegalType)
            {
                _errorListener.Add(new ErrorMessage(string.Format(Message.LoadMessage("InferType"), expr.Id), expr.Line, expr.Columns));
                expr.NodeType = TigerType.GetType<ErrorType>();
                _scope.AddVar(expr.Id, TigerType.GetType<ErrorType>().TypeID);
                return false;
            }
            _scope.AddVar(expr.Id, expr.ExpressionValue.ReturnType.TypeID);
            return true;
        }

        public override bool VisitAlias(AliasAST alias)
        {
            //se asume que no habra problema
            alias.ReturnType = TigerType.GetType<NoType>();
            //la clase base verifica q el id del type sea valido
            //aqui si se ve q el return true hace falta
            if (VisitTypeDeclaration(alias))
            {
                TigerType tt;
                //se verifica que exista el tipo del cual se esta creando un alias
                if (_scope.HasType(alias.AliasToWho, out tt) != ScopeLocation.NotDeclared)
                {
                    //se anade una nueva entrada del mismo type, lo q con otro id
                    _scope.AddAlias(alias.TypeId, alias.AliasToWho);
                    return true;
                }
                int savedErrorPos = _errorListener.Count;
                //manejador de evento
                _scope.TypeAdded += (sender, args) =>
                {
                    if (args.TypeName == alias.AliasToWho)
                        _scope.AddType(alias.TypeId, args.NewType);
                };

                //manejador de evento
                _scope.FinalizeScope += (sender, args) =>
                {
                    if (sender.HasType(alias.AliasToWho) == ScopeLocation.NotDeclared)
                    {
                        _errorListener.Insert(savedErrorPos,
                                         new ErrorMessage(
                                             string.Format(
                                                 Message.LoadMessage("TypeUndecl"),
                                                 alias.AliasToWho), alias.Line, alias.Columns));
                        alias.ReturnType = TigerType.GetType<ErrorType>();
                    }
                };
                return true;
            }
            return false;
        }

        public override bool VisitArrayAccess(ArrayAccessAST arrayAccess)
        {
            //esto es para quedarme con el scope actual
            arrayAccess.CurrentScope = _scope;


            arrayAccess.ReturnType = TigerType.GetType<ErrorType>();
            //visit the expression represeting the array
            arrayAccess.Array.Accept(this);
            //verifico que la expresion 'array' sea de tipo ArrayType
            var arrayType = arrayAccess.Array.ReturnType as ArrayType;
            if (arrayType != null)
            {
                arrayAccess.Indexer.Accept(this);
                //verifico que la expresion que indexada sea del tipo IntType
                var intType = arrayAccess.Indexer.ReturnType as IntType;
                if (intType != null)
                {
                    arrayAccess.ReturnType = arrayType.BaseType;
                    return arrayAccess.AlwaysReturn = true;
                }
                _errorListener.Add(new ErrorMessage(Message.LoadMessage("ArrayIndex"), arrayAccess.Line, arrayAccess.Columns));
                return false;
            }
            _errorListener.Add(new ErrorMessage(Message.LoadMessage("Index"), arrayAccess.Line, arrayAccess.Columns));
            return false;
        }

        public override bool VisitArrayDeclaration(ArrayDeclarationAST arrayDeclaration)
        {
            arrayDeclaration.CurrentScope = _scope;

            //la clase base chequea q el id sea valido
            if (VisitTypeDeclaration(arrayDeclaration))
            {
                TigerType tt;
                if (_scope.HasType(arrayDeclaration.BaseTypeID, out tt) != ScopeLocation.NotDeclared)
                {
                    var at = new ArrayType(tt, arrayDeclaration.TypeId);
                    _scope.AddType(arrayDeclaration.TypeId, at);
                    return true;
                }
                int savedErrorPos = _errorListener.Count;
                _scope.TypeAdded += (sender, args) =>
                {
                    if (args.TypeName == arrayDeclaration.BaseTypeID)
                        _scope.AddType(arrayDeclaration.TypeId, new ArrayType(args.NewType, arrayDeclaration.TypeId));
                };
                _scope.FinalizeScope += (sender, args) =>
                {
                    if (sender.HasType(arrayDeclaration.BaseTypeID) == ScopeLocation.NotDeclared)
                    {
                        _errorListener.Insert(savedErrorPos,
                                         new ErrorMessage(
                                             string.Format(
                                                 Message.LoadMessage("TypeUndecl"),
                                                 arrayDeclaration.BaseTypeID), arrayDeclaration.Line, arrayDeclaration.Columns));
                        arrayDeclaration.ReturnType = TigerType.GetType<ErrorType>();
                    }
                };
                return true;
            }
            return false;
        }

        public override bool VisitArrayInstantiation(ArrayInstatiationAST arrayInstatiation)
        {
            arrayInstatiation.CurrentScope = _scope;

            arrayInstatiation.ReturnType = TigerType.GetType<ErrorType>();
            TigerType t;
            if (_scope.HasType(arrayInstatiation.ArrayTypeIdentifier, out t) != ScopeLocation.NotDeclared)
            //Chequeo si este tipo de array fue declarado
            {
                var typeArray = t as ArrayType;
                if (typeArray != null)
                {
                    arrayInstatiation.SizeExp.Accept(this);
                    if (arrayInstatiation.SizeExp.ReturnType != TigerType.GetType<IntType>())
                        //Chequeo que el length del array sea un entero                   
                        _errorListener.Add(new ErrorMessage(Message.LoadMessage("ArrayIndex"), arrayInstatiation.Line, arrayInstatiation.Columns));
                    else
                    {
                        arrayInstatiation.InitializationExp.Accept(this);
                        if (!arrayInstatiation.InitializationExp.ReturnType.CanConvertTo(typeArray.BaseType))
                            _errorListener.Add(
                                new ErrorMessage(
                                    string.Format(Message.LoadMessage("Match"), arrayInstatiation.InitializationExp.ReturnType,
                                                  typeArray.BaseType), arrayInstatiation.Line, arrayInstatiation.Columns));
                        else
                        {
                            arrayInstatiation.ReturnType = typeArray;
                            return arrayInstatiation.AlwaysReturn = true;
                        }
                    }
                    return false;
                }
            }
            _errorListener.Add(new ErrorMessage(Message.LoadMessage("TypeUndecl"), arrayInstatiation.Line, arrayInstatiation.Columns));
            return false;
        }

        public override bool VisitLetExpression(LetExpressionAST expr)
        {
            throw new NotImplementedException();
        }


        #region Helper functions

        bool VisitTypeDeclaration(TypeDeclarationAST typeDeclaration)
        {
            TigerType tt;
            if (_scope.HasType(typeDeclaration.TypeId, out tt) != ScopeLocation.NotDeclared)
            {
                _errorListener.Add(new ErrorMessage(string.Format(Message.LoadMessage("TypeDecl"), typeDeclaration.TypeId), typeDeclaration.Line, typeDeclaration.Columns));
                typeDeclaration.ReturnType = TigerType.GetType<ErrorType>();
                return false;
            }
            typeDeclaration.ReturnType = TigerType.GetType<NoType>();
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="leftType"></param>
        /// <param name="rightType"></param>
        /// <param name="op"></param>
        /// <param name="tt"></param>
        /// <returns></returns>
        bool CheckOperator(TigerType leftType, TigerType rightType, Operators op, out TigerType tt)
        {
            if (leftType.SupportsOperator(rightType, op))
            {
                tt = leftType.GetOperationResult(rightType, op);
                return true;
            }
            if (rightType.SupportsOperator(leftType, op))
            {
                tt = rightType.GetOperationResult(leftType, op);
                return true;
            }
            tt = null;
            return false;
        }

        #endregion

    }
}