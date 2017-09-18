#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Bengala.AST.CodeGenerationUtils;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Representa una sequencia de expresiones del lenguaje Tiger.
    /// sequence expression :   '(' exp (',' exp)* ')'
    /// </summary>
    public class SequenceExpressionAST : ExpressionAST
    {
        #region Fields and Properties

        /// <summary>
        /// Devuelve la lista de expresiones que forman la secuencia
        /// </summary>
        public List<ExpressionAST> ExpressionList { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Lista de expresiones de la secuencia
        /// </summary>
        /// <param name="expList"></param>
        public SequenceExpressionAST(List<ExpressionAST> expList) : base(0, 0)
            //no hay problemas con pasar 0,0 pq esta instruccion no emite errores
        {
            ExpressionList = expList ?? new List<ExpressionAST>();
        }

        #endregion

        #region Instance Methods

     
        public override void GenerateCode(ILCode code)
        {
            bool pushStack = code.PushOnStack;
            
            //recorrer las expresiones y generar un codigo para cada una.
            for (int i = 0; i < ExpressionList.Count - 1; i++)
            {
                var item = ExpressionList[i];
                //si la expresion no es la ultima no debe poner valor en la pila.
                code.PushOnStack = false;
                item.GenerateCode(code);
            }
            code.PushOnStack = pushStack;
            if(ExpressionList.Count > 0)
                ExpressionList.LastOrDefault().GenerateCode(code);
        }

        #endregion

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitSequence(this);
        }
    }
}