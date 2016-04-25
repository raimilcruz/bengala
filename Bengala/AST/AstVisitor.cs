using Bengala.AST.SemanticsUtils;
using System;

namespace Bengala.AST
{
    public abstract class ErrorListener {
        public abstract void Add(ErrorMessage msg);
        public abstract void Add(WarningMessage msg);
        
    }
    public abstract class AstVisitor<T>
    {
        public abstract T VisitNode(AstNode node);

        public abstract T VisitIntAst(IntAST ast);

        public abstract T VisitStringAst(StringAST ast);

        //TODO: Check if VarAst is used in both side: left and right
        public abstract T VisitVar(VarAST ast);

        public abstract T VisitIfExpression(IfExpressionAST ast);

        public abstract T VisitBinaryExpression(BinaryExpressionAST expr);

        public abstract T VisitLetExpression(LetExpressionAST expr);

        public abstract T VisitVarDeclaration(VarDeclarationAST expr);
    }
    public class StaticSemanticsChecker : AstVisitor<bool>
    {
        Scope _scope;
        ErrorListener _errorListener;
        public StaticSemanticsChecker()
        {
            //TODO: Add another constructor overload for _scope
            _scope = new Scope(null);
        }
        public StaticSemanticsChecker(ErrorListener errorListener)
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



        protected virtual bool CheckOperator(TigerType leftType, TigerType rightType, Operators op, out TigerType tt)
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

        public override bool VisitLetExpression(LetExpressionAST expr)
        {
            throw new NotImplementedException();
        }
    }
}