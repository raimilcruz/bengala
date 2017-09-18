using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bengala;
using Bengala.AST.SemanticsUtils;

namespace Bengala.AST
{
    public abstract class AstNode
    {
        public abstract T Accept<T>(AstVisitor<T> visitor);

               
    }
}
