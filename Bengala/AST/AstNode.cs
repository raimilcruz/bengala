using System.Collections.Generic;

namespace Bengala.AST
{
    public abstract class AstNode
    {
        /// <summary>
        /// Devuelve la posicion de la expresion en el fichero de codigo
        /// </summary>
        public int Line { get; set; }

        /// <summary>
        /// Devuelve la columna donde la comienza la expresion en el fichero de codigo
        /// </summary>
        public int Columns { get; set; }

        public int CodeOffSet { get; set; }

        public abstract T Accept<T>(AstVisitor<T> visitor);

        public abstract IEnumerable<AstNode> Children { get; }
    }

    public class CodeOffSet
    {
        public int Start { get; set; }
        public int End { get; set; }

        public CodeOffSet(int start,int end)
        {
            Start = start;
            End = end;
        }
    }
}
