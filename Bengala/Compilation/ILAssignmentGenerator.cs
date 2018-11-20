using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Bengala.AST;
using Bengala.AST.SemanticsUtils;
using Bengala.Compilation.Helpers;

namespace Bengala.Compilation
{
    public abstract class ILAssignmentGenerator
    {
        protected ILCodeGenerator CodeGenerator { get; }

        protected ILAssignmentGenerator(ILCodeGenerator codeGenerator)
        {
            CodeGenerator = codeGenerator;
        }
        public abstract void GenerateCode(ILCode code, ExpressionAst rightExpr);
    }

    public class ILGeneratorArrayAssignment:ILAssignmentGenerator
    {
        private readonly ArrayAccessAST _arrayAccess;

        public ILGeneratorArrayAssignment(ILCodeGenerator codeGenerator, ArrayAccessAST arrayAccess):base(codeGenerator)
        {
            _arrayAccess = arrayAccess;
        }
        public override void GenerateCode(ILCode code, ExpressionAst rightExpr)
        {
            //--->
            bool pushOnStack = code.PushOnStack;

            //cargo primero el array.
            code.PushOnStack = true;
            _arrayAccess.Array.Accept(CodeGenerator);
            //cargo el indice al cual voy a acceder
            code.PushOnStack = true;
            _arrayAccess.Indexer.Accept(CodeGenerator);
            //cargo el valor que le voy a asignar
            code.PushOnStack = true;
            //generar el codigo de la expresion que quiero asignar.
            rightExpr.Accept(CodeGenerator);

            ILGenerator il = code.Method.GetILGenerator();

            //aca tengo que pedir el tipo del array , y luego el type asociado a el.
            string typeCodeName = _arrayAccess.CurrentScope.GetTypeInfo(_arrayAccess.Array.ReturnType.TypeID).CodeName;
            Type t = code.DefinedType[typeCodeName];
            il.Emit(OpCodes.Stelem, t.IsArray ? t.GetElementType() : t);

            //<----
            code.PushOnStack = pushOnStack;
        }
    }


    internal class ILGeneratorVarAssignment : ILAssignmentGenerator
    {
        private readonly VarAST _varAst;
        public ILGeneratorVarAssignment(ILCodeGenerator ilCodeGenerator, VarAST varAst):base(ilCodeGenerator)
        {
            _varAst = varAst;
        }

        public override void GenerateCode(ILCode code, ExpressionAst rightExpr)
        {
            ILGenerator il = code.Method.GetILGenerator();

            //buscar la variable
            VarInfo varInfo = _varAst.CurrentScope.GetVarInfo(_varAst.VarId);

            //---> 

            bool pushOnStack = code.PushOnStack;
            code.PushOnStack = true;

            if (_varAst.IsForeignVar)
            //aqui se entra cuando se usa una variable se usa en una funcion que no fue quien la declaro
            {
                string currentParentFunctionCodeName = _varAst.CurrentScope.CurrentFunction.FunctionParent.CodeName;
                //carga el respectivo campo de la clase contenedora.
                //como mis metodos son de instancia y las varibles tambien tengo que cargar el parametro 0
                il.Emit(OpCodes.Ldarg_0);
                //ahora viene cargar la variable de verdad. 
                TypeCodeInfo typeCodeInfo = code.GetWrapperAsociatteTo(currentParentFunctionCodeName);
                while (!typeCodeInfo.ContainFieldInLevel1(varInfo.CodeName))
                {
                    //cargo el campo que representa al padre del tipo actual
                    string parentInstanceName = typeCodeInfo.FieldNameOfParent;
                    il.Emit(OpCodes.Ldfld, typeCodeInfo.GetField(parentInstanceName));
                    typeCodeInfo = typeCodeInfo.Parent;
                }
                //pongo el valor que quiero asignar en la pila
                rightExpr.Accept(CodeGenerator);
                //lo asigno.
                il.Emit(OpCodes.Stfld, typeCodeInfo.GetField(varInfo.CodeName));
            }
            else
            {
                if (varInfo.IsLocalVariable)
                {
                    rightExpr.Accept(CodeGenerator);
                    il.Emit(OpCodes.Stloc, code.DefinedLocal[varInfo.CodeName].LocalIndex);
                }
                else if (varInfo.IsParameterFunction)
                {
                    rightExpr.Accept(CodeGenerator);
                    il.Emit(OpCodes.Starg, varInfo.ParameterNumber + 1);
                }
                else // tengo que acceder a la variable a travez de la instancia que tengo como varible local.
                {
                    //se asume que el wrapper siempre esta como primera variable del metodo.
                    il.Emit(OpCodes.Ldloc_0);
                    rightExpr.Accept(CodeGenerator);
                    il.Emit(OpCodes.Stfld, code.DefinedField[varInfo.CodeName]);
                }
            }
            code.PushOnStack = pushOnStack;
        }
    }

    internal class ILGeneratorRecordFieldAssignment : ILAssignmentGenerator
    {
        public ILGeneratorRecordFieldAssignment(ILCodeGenerator ilCodeGenerator, RecordAccessAST assignExpression):base(ilCodeGenerator)
        {
            
        }

        public override void GenerateCode(ILCode code, ExpressionAst rightExpr)
        {
           
        }
    }
}
