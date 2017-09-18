using System;
using System.Collections.Generic;
using System.Linq;
using Bengala.AST;
using Bengala.AST.SemanticsUtils;
using Bengala.AST.Utils;

namespace Bengala.Analysis
{
    public class StaticChecker : AstVisitor<bool>
    {
        Scope _scope;
        readonly ErrorListener _errorListener;
        public StaticChecker()
        {
            //TODO: Add another constructor overload for _scope
            _scope = new Scope(null);
        }
        public StaticChecker(ErrorListener errorListener)
        {
            _scope = new Scope(null);
            _errorListener = errorListener;

            //TODO: move to another place
            ScopeInitializator scopeInitializator = new ScopeInitializator();
            scopeInitializator.InitScope(_scope);
            
        }

        public override bool VisitNode(AstNode node)
        {
            throw new NotImplementedException("I don't know how to do it");
        }

        public override bool VisitIntLiteral(IntLiteral literal)
        {
            literal.ReturnType = TigerType.GetType<IntType>();
            return true;
        }

        public override bool VisitStringLiteral(StringLiteral stringLiteral)
        {
            stringLiteral.ReturnType = TigerType.GetType<StringType>();
            return true;
        }

        public override bool VisitVar(VarAST ast)
        {
            //es posible que se quite
            ast.CurrentScope = _scope;

            TigerType tt;
            //comprobar que la variable esta definida
            if (_scope.HasVar(ast.VarId, out tt) != ScopeLocation.NotDeclared)
            {
                //dar el valor correspondiente al tipo de esa expresion
                ast.ReturnType = tt;

                if (IsVarFromDifferentFunction(ast))
                    SetVarAsUsedForAnotherFunction(ast);

                return true;
            }
            //error en caso q la variable no este definida
            _errorListener.Add(new ErrorMessage(string.Format(Message.LoadMessage("VarUndecl"), ast.VarId), ast.Line, ast.Columns));
            ast.ReturnType = TigerType.GetType<ErrorType>();
            return false;
        }

        public override bool VisitBinaryExpression(BinaryExpressionAST expr)
        {
            expr.LeftExp.Accept(this);
            TigerType leftType = expr.LeftExp.ReturnType;

            expr.RightExp.Accept(this);
            TigerType rightType = expr.RightExp.ReturnType;

            TigerType tt;
            if (CheckOperator(leftType, rightType, expr.Operator, out tt))
            {
                expr.ReturnType = tt;
                return true;
            }
            _errorListener.Add(
                new ErrorMessage(string.Format(Message.LoadMessage("SupportOp"), expr.Operator, leftType, rightType), expr.Line,
                    expr.Columns));
            expr.ReturnType = TigerType.GetType<ErrorType>();
            return false;
        }

        public override bool VisitIfExpression(IfExpressionAST ifExpr)
        {
            ifExpr.ExpConditional.Accept(this);
            ifExpr.ReturnType = TigerType.GetType<ErrorType>();
            if (ifExpr.ExpConditional.ReturnType != TigerType.GetType<IntType>())
                _errorListener.Add(new ErrorMessage(Message.LoadMessage("IfCond"), ifExpr.Line, ifExpr.Columns));
            else if (ifExpr.ExpressionThen.Accept(this))
            {
                if (ifExpr.ExpressionElse != null)
                {
                    ifExpr.ExpressionElse.Accept(this);
                    ifExpr.ReturnType = ifExpr.ExpressionElse.ReturnType.CanConvertTo(ifExpr.ExpressionThen.ReturnType)
                                     ?
                                         ifExpr.ExpressionThen.ReturnType
                                     :
                                         ifExpr.ExpressionThen.ReturnType.CanConvertTo(ifExpr.ExpressionElse.ReturnType)
                                             ?
                                                 ifExpr.ExpressionElse.ReturnType
                                             : TigerType.GetType<ErrorType>();
                    if (!(ifExpr.ReturnType is ErrorType))
                        return ifExpr.AlwaysReturn = true;

                    _errorListener.Add(
                        new ErrorMessage(
                            string.Format(Message.LoadMessage("Match"), ifExpr.ExpressionThen.ReturnType,
                                          ifExpr.ExpressionElse.ReturnType), ifExpr.Line, ifExpr.Columns));
                }
                ifExpr.AlwaysReturn = false;
                ifExpr.ReturnType = TigerType.GetType<NoType>();
                return true;
            }
            return false;
        }

        public override bool VisitVarDeclaration(VarDeclarationAST expr)
        {
            expr.CurrentScope = _scope;

            ScopeLocation idLocation = _scope.HasVar(expr.Id);
            if (idLocation == ScopeLocation.DeclaredLocal)
            {
                _errorListener.Add(new ErrorMessage(string.Format(Message.LoadMessage("VarDecl"), expr.Id), expr.Line, expr.Columns));
                expr.ReturnType = TigerType.GetType<ErrorType>();
                return false;
            }
            if (idLocation != ScopeLocation.NotDeclared)
                _errorListener.Add(new WarningMessage(string.Format(Message.LoadMessage("Hide"), expr.Id), expr.Line, expr.Columns));

            //TODO: Rename ExpressionValue to NamedExpression
            expr.ExpressionValue.Accept(this);


            //se asume q no habran problemas de compatibilidad
            expr.ReturnType = TigerType.GetType<NoType>();

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
                        expr.ReturnType = TigerType.GetType<ErrorType>();
                        _scope.AddVar(expr.Id, TigerType.GetType<ErrorType>().TypeID);
                        return false;
                    }
                    expr.ReturnType = expr.ExpressionValue.ReturnType;
                    //si me especifica el tipo explicitamente .
                    _scope.AddVar(expr.Id, tt.TypeID);
                    return true;
                }
                // no existe el tipo de la variable
                _errorListener.Add(new ErrorMessage(string.Format(Message.LoadMessage("TypeUndecl"), expr.TypeId), expr.Line, expr.Columns));
                expr.ReturnType = TigerType.GetType<ErrorType>();
                _scope.AddVar(expr.Id, TigerType.GetType<ErrorType>().TypeID);
                return false;
            }
            if (!expr.ExpressionValue.ReturnType.IsLegalType)
            {
                _errorListener.Add(new ErrorMessage(string.Format(Message.LoadMessage("InferType"), expr.Id), expr.Line, expr.Columns));
                expr.ReturnType = TigerType.GetType<ErrorType>();
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

        public override bool VisitAssignExpression(AssignExpressionAST assignExpression)
        {
            assignExpression.CurrentScope = _scope;

            assignExpression.ReturnType = TigerType.GetType<ErrorType>();
            if (!assignExpression.LeftExpression.Accept(this) || !assignExpression.RightExpression.Accept(this))
                return false;
            if (assignExpression.RightExpression.ReturnType.CanConvertTo(assignExpression.LeftExpression.ReturnType))
            {
                assignExpression.ReturnType = TigerType.GetType<NoType>();
                return true;
            }
            _errorListener.Add(
                new ErrorMessage(
                    string.Format(Message.LoadMessage("Match"), assignExpression.LeftExpression.ReturnType, assignExpression.RightExpression.ReturnType),
                    assignExpression.Line, assignExpression.Columns));
            return false;
        }

        public override bool VisitForExpression(ForExpressionAST forExpression)
        {
            forExpression.ExpressionFrom.Accept(this);
            forExpression.ReturnType = TigerType.GetType<ErrorType>();
            //si la expresion "from" no es de tipo entero.
            if (forExpression.ExpressionFrom.ReturnType != TigerType.GetType<IntType>())
                _errorListener.Add(new ErrorMessage(string.Format(Message.LoadMessage("ForInit"), forExpression.VarId), forExpression.Line,
                    forExpression.Columns));
            else
            {
                forExpression.ExpressionTo.Accept(this);
                // si la expresion "to" no es tipo entero.
                if (forExpression.ExpressionTo.ReturnType != TigerType.GetType<IntType>())
                    _errorListener.Add(new ErrorMessage(string.Format(Message.LoadMessage("ForStop"), forExpression.VarId),
                        forExpression.Line, forExpression.Columns));
                else
                {
                    var sc = new Scope(_scope, forExpression);
                    forExpression.CurrentScope = sc;
                    if (_scope.HasVar(forExpression.VarId) != ScopeLocation.NotDeclared)
                    {
                        _errorListener.Add(new ErrorMessage(string.Format(Message.LoadMessage("VarDecl"), forExpression.VarId),
                            forExpression.Line, forExpression.Columns));
                        forExpression.ReturnType = TigerType.GetType<ErrorType>();
                        return false;
                    }
                    //annado la variable al scope 
                    sc.AddVarFor(forExpression.VarId, TigerType.Int);

                    if (forExpression.BodyExpressions.Accept(this))
                    {
                        forExpression.ReturnType = forExpression.BodyExpressions.ReturnType;
                        return true;
                    }
                }
            }
            return false;
        }

        public override bool VisitFunctionDeclaration(FunctionDeclarationAST functionDeclaration)
        {
            //chequear la semantica del cuerpo de la funcion, la signatura ya fue chequeada en el let correspondiente
            //here we create a new scope for this function and its parameters
            PushScope(new Scope(_scope));

            //verificar que todos los tipos de los parametros existan y si ya existe el nombre de los parametros
            //en el scope

            if (!CheckFunctionParams(functionDeclaration))
            {
                PopScope();
                return false;
            }

            functionDeclaration.CurrentScope = _scope;
            TigerType retType = functionDeclaration.ReturnTypeId != null ?
                                    functionDeclaration.CurrentScope.GetType(functionDeclaration.ReturnTypeId) : TigerType.GetType<NoType>();


            //poner esta funcion como la funcion actual de scope donde se encuentra.
            FunctionInfo temp = _scope.CurrentFunction;
            _scope.CurrentFunction = _scope.GetFunction(functionDeclaration.FunctionId);

            functionDeclaration.ExprInstructions.Accept(this);

            _scope.CurrentFunction = temp;

            if (!functionDeclaration.ExprInstructions.AlwaysReturn && retType != TigerType.GetType<NoType>())
                _errorListener.Add(new ErrorMessage(string.Format(Message.LoadMessage("FuncDeclRet"), functionDeclaration.FunctionId),
                    functionDeclaration.Line, functionDeclaration.Columns));
            else if (string.IsNullOrEmpty(functionDeclaration.ReturnTypeId) ||
                     functionDeclaration.ExprInstructions.ReturnType.CanConvertTo(
                         _scope.GetType(functionDeclaration.ReturnTypeId)))
            {
                PopScope();
                return true;
            }
            else
                _errorListener.Add(
                    new ErrorMessage(
                        string.Format(Message.LoadMessage("Match"), functionDeclaration.ReturnTypeId,
                            functionDeclaration.ExprInstructions.ReturnType), functionDeclaration.Line,
                        functionDeclaration.Columns));
            functionDeclaration.ReturnType = TigerType.GetType<ErrorType>();

            PopScope();
            return false;
        }

        public override bool VisitNegExpression(NegExpressionAST negExpression)
        {
            negExpression.CurrentScope = _scope;
            if (negExpression.Expression.Accept(this))
            {
                if (negExpression.Expression.ReturnType == TigerType.GetType<IntType>())
                {
                    negExpression.ReturnType = TigerType.GetType<IntType>();
                    return true;
                }
                _errorListener.Add(new ErrorMessage(string.Format(Message.LoadMessage("NegExp"), negExpression.Expression.ReturnType), negExpression.Line,
                                               negExpression.Columns));
            }
            negExpression.ReturnType = TigerType.GetType<ErrorType>();
            return false;
        }

        public override bool VisitNilLiteral(NilLiteral nil)
        {
            nil.ReturnType = TigerType.GetType<NilType>();
            return true;
        }

        public override bool VisitSequence(SequenceExpressionAST sequenceExpression)
        {
            //Se asume q no se retorna nada
            sequenceExpression.ReturnType = TigerType.GetType<NoType>();

            foreach (var exp in sequenceExpression.ExpressionList)
                if (!exp.Accept(this))
                    //hubo error
                    sequenceExpression.ReturnType = TigerType.GetType<ErrorType>();

            ExpressionAST last = sequenceExpression.ExpressionList.LastOrDefault();
            if (last != null)
            {
                //si existe una ultima expresion, esta define el retorno del let
                sequenceExpression.AlwaysReturn = last.AlwaysReturn;
                sequenceExpression.ReturnType = last.ReturnType;
            }
            //true si no hubo ningun error
            return sequenceExpression.ReturnType != TigerType.GetType<ErrorType>();
        }

        public override bool VisitTypeDeclaration(TypeDeclarationAST typeDeclaration)
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

        public override bool VisitWhileExpression(WhileExpressionAST whileExpression)
        {
            whileExpression.ExpressionConditional.Accept(this);
            whileExpression.ReturnType = TigerType.GetType<ErrorType>();
            if (whileExpression.ExpressionConditional.ReturnType != TigerType.GetType<IntType>())
                _errorListener.Add(new ErrorMessage(Message.LoadMessage("IfCond"), whileExpression.Line, whileExpression.Columns));
            else
            {
                //guardo, si hay, el ciclo previo
                LoopAST prevLoop = _scope.ContainerLoop;
                _scope.ContainerLoop = whileExpression;
                if (whileExpression.BodyExpressions.Accept(this))
                {
                    //repongo el ciclo q habia
                    _scope.ContainerLoop = prevLoop;
                    whileExpression.ReturnType = TigerType.GetType<NoType>();
                    return true;
                }
            }
            return false;
        }

        public override bool VisitBreakStatement(BreakAST breakStm)
        {
            breakStm.CurrentScope = _scope;

            if (!_scope.IsInLoop)
            {
                _errorListener.Add(new ErrorMessage(Message.LoadMessage("Break"),breakStm.Line, breakStm.Columns));
                breakStm.ReturnType = TigerType.GetType<ErrorType>();
                return false;
            }
            breakStm.BreakeableLoop = _scope.ContainerLoop;
            breakStm.ReturnType = TigerType.GetType<NoType>();
            return true;
        }

        public override bool VisitFunctionInvocation(CallFunctionAST functionInvocation)
        {
            //probablemente se cambie
            functionInvocation.CurrentScope = _scope;

            bool ok = true;

            FunctionInfo functionInfo;
            string message;
            //verificar que la funcion existe.
            if (_scope.HasFunction(functionInvocation.FunctionId, out functionInfo) != ScopeLocation.NotDeclared)
            {
                //coger su tipo de retorno
                if (functionInfo.ParameterList.Count == functionInvocation.RealParam.Count)
                {
                    for (int i = 0; i < functionInfo.ParameterList.Count; i++)
                    {
                        //no es necesario chequear q el chequeo de los parametros sea true o no
                        functionInvocation.RealParam[i].Accept(this);
                        ok = ok && (functionInvocation.RealParam[i].ReturnType.CanConvertTo(functionInfo.ParameterList[i].Value));
                    }
                    if (ok)
                    {
                        functionInvocation.AlwaysReturn = !(functionInfo.FunctionReturnType is NoType);
                        functionInvocation.ReturnType = functionInfo.FunctionReturnType;
                        return true;
                    }
                    message = string.Format(Message.LoadMessage("FuncParams"), functionInvocation.FunctionId);
                }
                else
                    message = string.Format(Message.LoadMessage("FuncParamsCount"), functionInvocation.FunctionId, functionInvocation.RealParam.Count);
            }
            else
                message = string.Format(Message.LoadMessage("FuncUndecl"), functionInvocation.FunctionId);
            _errorListener.Add(new ErrorMessage(message, functionInvocation.Line, functionInvocation.Columns));
            functionInvocation.ReturnType = TigerType.GetType<ErrorType>();
            return false;
        }

        public override bool VisitRecordAccess(RecordAccessAST recordAccess)
        {
            recordAccess.CurrentScope = _scope;
            recordAccess.ExpressionRecord.Accept(this);
            TigerType record = recordAccess.ExpressionRecord.ReturnType;
            if (record is RecordType)
            {
                //verificar que el record contiene el campo
                var r = (RecordType)record;
                if (r.Contains(recordAccess.FieldId))
                {
                    recordAccess.ReturnType = r[recordAccess.FieldId];
                    return recordAccess.AlwaysReturn = true;
                }
                _errorListener.Add(new ErrorMessage(string.Format(Message.LoadMessage("RecField"), r.TypeID, recordAccess.FieldId), recordAccess.Line,
                                               recordAccess.Columns));
                recordAccess.ReturnType = TigerType.GetType<ErrorType>();
                return false;
            }
            //la expresion no es un record
            recordAccess.ReturnType = TigerType.GetType<ErrorType>();
            _errorListener.Add(new ErrorMessage(Message.LoadMessage("RecAccess"), recordAccess.Line, recordAccess.Columns));
            return false;
        }

        public override bool VisitRecordInstantiation(RecordInstantiationAST recordInstantiation)
        {
            recordInstantiation.CurrentScope = _scope;

            TigerType tt;
            RecordType rt;
            //verificando que exista el tipo, 
            if (_scope.HasType(recordInstantiation.Id, out tt) != ScopeLocation.NotDeclared && (rt = tt as RecordType) != null)
            {
                recordInstantiation.ReturnType = rt;
                if (recordInstantiation.ExpressionValue == null)
                    return true;

                foreach (var kvp in recordInstantiation.ExpressionValue)
                {
                    kvp.Value.Accept(this);
                    if (rt.Contains(kvp.Key))
                    {
                        if (kvp.Value.ReturnType.CanConvertTo(rt[kvp.Key]))
                            continue;
                        _errorListener.Add(
                            new ErrorMessage(
                                string.Format(Message.LoadMessage("Match"), kvp.Value.ReturnType, rt[kvp.Key].TypeID),
                                recordInstantiation.Line, recordInstantiation.Columns));
                    }
                    else
                        _errorListener.Add(new ErrorMessage(string.Format(Message.LoadMessage("RecField"), recordInstantiation.Id, kvp.Key), recordInstantiation.Line,
                                                       recordInstantiation.Columns));
                    recordInstantiation.ReturnType = TigerType.GetType<ErrorType>();
                }
                return recordInstantiation.ReturnType != TigerType.GetType<ErrorType>();
            }
            recordInstantiation.ReturnType = TigerType.GetType<ErrorType>();
            _errorListener.Add(new ErrorMessage(string.Format(Message.LoadMessage("TypeUndecl"), recordInstantiation.Id), recordInstantiation.Line, recordInstantiation.Columns));
            return false;
        }

        public override bool VisitRecordDeclaration(RecordDeclarationAST recordDeclaration)
        {
            recordDeclaration.CurrentScope = _scope;

            //se asuma que no habra problemas
            recordDeclaration.ReturnType = TigerType.GetType<NoType>();
            //la clase base verifica el ID del type
            if (VisitTypeDeclaration(recordDeclaration))
            {
                var rt = new RecordType(recordDeclaration.TypeId);
                //se anade el record creado al scope para q puedan haber records recursivos en su def
                _scope.AddType(recordDeclaration.TypeId, rt);
                //se verifica cada una de las declaraciones de los campos del record
                int savedErrorPos = _errorListener.Count;
                foreach (var kvp in recordDeclaration.Fields)
                {
                    if (!rt.Contains(kvp.Key))
                    {
                        TigerType tt;
                        if (_scope.HasType(kvp.Value, out tt) == ScopeLocation.NotDeclared)
                        {
                            KeyValuePair<string, string> savedKvp = kvp;
                            _scope.TypeAdded += (sender, args) =>
                            {
                                if (args.TypeName == savedKvp.Value)
                                    rt.AddField(savedKvp.Key, args.NewType);
                            };
                            _scope.FinalizeScope += (sender, args) =>
                            {
                                if (sender.HasType(savedKvp.Value) ==
                                    ScopeLocation.NotDeclared)
                                {
                                    _errorListener.Insert(savedErrorPos,
                                                     new ErrorMessage(
                                                         string.Format(
                                                             Message.LoadMessage("TypeUndecl"),
                                                             savedKvp.Value), recordDeclaration.Line, recordDeclaration.Columns));
                                    recordDeclaration.ReturnType = TigerType.GetType<ErrorType>();
                                }
                            };
                        }
                        else
                        {
                            rt.AddField(kvp.Key, tt);
                        }
                    }
                    else
                    {
                        _errorListener.Add(new ErrorMessage(string.Format(Message.LoadMessage("RecDecl"), kvp.Key, recordDeclaration.TypeId),
                                                       recordDeclaration.Line, recordDeclaration.Columns));
                    }
                }
                //TODO aqui se ve el prob con los ret Types y los return true pq no se puede decir nada en este momento
                return true;
            }
            return false;
        }

        public override bool VisitLetExpression(LetExpressionAST letExpr)
        {
            //First, we process the type declaration in the let.
            var typeDeclarations = letExpr.DeclarationList.Where(x => x is TypeDeclarationAST).ToList();

            //el let abre un nuevo scope
            var sc = new Scope(_scope, _scope.ContainerLoop);
            PushScope(sc);
            letExpr.CurrentScope = sc;

            foreach (var typeDeclaration in typeDeclarations)
                typeDeclaration.Accept(this);

            //notify that all types in this scope where proccessed. Then if there was fields with unsolved
            //types, this is the momemnt to catch errors.
            sc.NotifyEndTypeScope();

            //Declaration List

            //se asume q no habra problemas
            letExpr.ReturnType = TigerType.GetType<NoType>();

            //Second. We do a first pass over function declarations
            //to verifies function signatures and then add function signature to the scope.

            var functionDeclarations = letExpr.DeclarationList.Where(x => (x is FunctionDeclarationAST)).Cast<FunctionDeclarationAST>();
            foreach (var fDecl in functionDeclarations)
            {
                //se hace el primer chequeo
                CheckFunctionSignature(fDecl);
            }

            //a medida q aparezcan las funciones se incrementa el contador
            var noTypeDeclarations = letExpr.DeclarationList.Where(x => !(x is TypeDeclarationAST));
            foreach (var noTypeDeclaration in noTypeDeclarations)
            {
                FunctionDeclarationAST fDecl;
                //si es una declaracion de funcion
                if ((fDecl = noTypeDeclaration as FunctionDeclarationAST) != null)
                {
                    //segundo chequeo para comprobar el cuerpo 
                    if (!fDecl.Accept(this))
                        letExpr.ReturnType = TigerType.GetType<ErrorType>();
                }
                else if (!noTypeDeclaration.Accept(this))
                    letExpr.ReturnType = TigerType.GetType<ErrorType>();
            }

            //cierra el scope pq no habran mas declaraciones


            //chequeo de semantica del cuerpo del let
            if (letExpr.SequenceExpressionList.Accept(this) && letExpr.ReturnType == TigerType.GetType<NoType>())
            {
                letExpr.AlwaysReturn = letExpr.SequenceExpressionList.AlwaysReturn;
                letExpr.ReturnType = letExpr.SequenceExpressionList.ReturnType;
                PopScope();
                return true;
            }
            PopScope();
            return false;
        }


        #region Helper functions

        private void PushScope(Scope newScope)
        {
            _scope = newScope;
        }

        private void PopScope()
        {
            _scope = _scope.Parent;
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

        /// <summary>
        /// This method verifies that the function signature is correct. That is that the return type and parameter types are already
        /// defined. It also verifies that the function does not exist in the scope.
        /// </summary>
        /// <param name="fDecl"></param>
        /// <returns></returns>
        private bool CheckFunctionSignature(FunctionDeclarationAST fDecl)
        {
            TigerType ret = null;
            if (!string.IsNullOrEmpty(fDecl.ReturnTypeId))
            {
                //si se especifica retorno pero este no es un tipo ya definido ERROR
                //esto lo garantiza haber organizado las declaraciones
                if (_scope.HasType(fDecl.ReturnTypeId, out ret) == ScopeLocation.NotDeclared)
                {
                    _errorListener.Add(new ErrorMessage(
                                      string.Format(Message.LoadMessage("TypeUndecl"), fDecl.ReturnTypeId), fDecl.Line,
                                      fDecl.Columns));
                    fDecl.ReturnType = TigerType.GetType<ErrorType>();
                    return false;
                }
                if (!ret.IsLegalType)
                {
                    //TODO: Hasta que punto interesa lanzar este error??
                    _errorListener.Add(new ErrorMessage(
                                      string.Format(Message.LoadMessage("InavalidRet"), fDecl.ReturnTypeId), fDecl.Line,
                                      fDecl.Columns));
                    fDecl.ReturnType = TigerType.GetType<ErrorType>();
                    return false;
                }
            }

            //ver que la funcion no este previamente declarada
            if (_scope.HasFunction(fDecl.FunctionId) == ScopeLocation.NotDeclared)
            {
                var paramsInfo = new List<KeyValuePair<string, TigerType>>();
                foreach (var nameType in fDecl.ParameterList)
                {
                    TigerType t;
                    //verificar si existe el tipo del parametro.
                    if (_scope.HasType(nameType.Value, out t) == ScopeLocation.NotDeclared)
                    {
                        _errorListener.Add(new ErrorMessage(
                            $"Type {nameType.Value} in parameter {fDecl.FunctionId} is not defined", fDecl.Line, fDecl.Columns));
                        fDecl.ReturnType = TigerType.GetType<ErrorType>();
                        return false;
                    }
                    paramsInfo.Add(new KeyValuePair<string, TigerType>(nameType.Key, t));
                }
                var funInfo = new FunctionInfo(paramsInfo, ret ?? TigerType.GetType<NoType>())
                {
                    FunctionName = fDecl.FunctionId,
                    FunctionParent = _scope.CurrentFunction
                };
                //se anade en el padre para q este disponible en el scope donde se declara
                _scope.AddFunction(fDecl.FunctionId, funInfo);
                return true;
            }

            //ya habia una funcion con ese nombre
            _errorListener.Add(new ErrorMessage(string.Format(Message.LoadMessage("FuncDecl"), fDecl.FunctionId), fDecl.Line,
                                           fDecl.Columns));
            return false;
        }

        private bool CheckFunctionParams(FunctionDeclarationAST fDecl)
        {
            int posParam = 0;
            //get from the scope the function signature.
            if (_scope.HasFunction(fDecl.FunctionId) == ScopeLocation.NotDeclared)
            {
                _errorListener.Add(new ErrorMessage(
                    $"It is expected that the function {fDecl.FunctionId} is at scope at this point",fDecl.Line,fDecl.Columns));
                return false;
            }
            var funInfo = _scope.GetFunction(fDecl.FunctionId);
            foreach (var parameter in funInfo.ParameterList)
            {
                //existen dos parametros con el mismo nombre.
                if (_scope.HasVar(parameter.Key) == ScopeLocation.DeclaredLocal)
                {
                    _errorListener.Add(
                        new ErrorMessage(
                            string.Format(Message.LoadMessage("FuncDeclParams"), parameter.Key,fDecl.FunctionId), fDecl.Line,
                            fDecl.Columns));
                    fDecl.ReturnType = TigerType.GetType<ErrorType>();
                    return false;
                }
                //existe una variable con el mismo nombre que este parametro en un ambito mas externo
                if (_scope.HasVar(parameter.Key) != ScopeLocation.NotDeclared)
                {
                    _errorListener.Add(
                        new WarningMessage(string.Format(Message.LoadMessage("Hide"), parameter.Key),
                            fDecl.Line, fDecl.Columns));
                }
                //se anade este valor al scope de la funcion
                var parameterTypeId = fDecl.ParameterList.First(x => x.Key == parameter.Key).Value;
                _scope.AddVarParameter(parameter.Key, parameterTypeId, posParam, fDecl.FunctionId);
                posParam++;
            }
            return true;
        }

        /// <summary>
        /// Este metodo devuelve true si la variable actual se declaro en otra funcion que no el la actual.
        /// </summary>
        /// <returns></returns>
        private bool IsVarFromDifferentFunction(VarAST ast)
        {
            VarInfo varInfo = _scope.GetVarInfo(ast.VarId);
            return (varInfo.FunctionNameParent != _scope.CurrentFunction?.FunctionName);
        }

        private void SetVarAsUsedForAnotherFunction(VarAST ast)
        {
            ast.IsForeignVar = true;

            VarInfo varInfo = _scope.GetVarInfo(ast.VarId);
            varInfo.IsLocalVariable = false;
            varInfo.IsUsedForAnotherFunction = true;
            FunctionInfo funInfo = _scope.GetFunction(varInfo.FunctionNameParent);
            funInfo.ContainVarsUsedForAnotherFunction = true;
            if (!funInfo.VarsUsedForAnotherFunction.Contains(varInfo))
                _scope.AsociatteVarToFunctionAsUsedForChildFunction(ast.VarId);
        }

        #endregion

    }
}