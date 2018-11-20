#region Usings

using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Representa la instruction :   arrayType '[' SizeExp ']' 'of' initiaExp
    /// </summary>
    public class ArrayInstatiationAST : ExpressionAst
    {
        #region Fields and Properties

        public readonly string ArrayTypeIdentifier;
        public readonly ExpressionAst InitializationExp;
        public readonly ExpressionAst SizeExp;

        #endregion

        #region Constructors

        public ArrayInstatiationAST(string arrayType, ExpressionAst sizeExp, ExpressionAst initializationExp)
        {
            ArrayTypeIdentifier = arrayType;
            this.SizeExp = sizeExp;
            this.InitializationExp = initializationExp;
        }

        public ArrayInstatiationAST(string arrayType, ExpressionAst sizeExp, ExpressionAst initializationExp, int line, int col)
            : base(line, col)
        {
            this.ArrayTypeIdentifier = arrayType;
            this.SizeExp = sizeExp;
            this.InitializationExp = initializationExp;
        }

        #endregion


        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitArrayInstantiation(this);
        }
    }
}