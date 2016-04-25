#region Usings

using System.Collections.Generic;
using System.Reflection.Emit;
using Bengala.AST.CodeGenerationUtils;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Representa una variable : "var" + 3 .Tambien puede ser usado para representa la asignacion a una variable
    /// </summary>
    public class VarAST : LValueAST
    {
        #region Fields and Properties

        public string VarId { get; private set; }

        #endregion

        #region Constructors

        public VarAST(string varId, int line, int col) : base(line, col)
        {
            VarId = varId;
            AlwaysReturn = true;
        }

        #endregion

        #region Instance Methods

        public bool IsForeignVar { get; private set; }

        public override bool CheckSemantic(Scope scope, List<Message> listError)
        {
            //es posible que se quite
            CurrentScope = scope;

            TigerType tt;
            //comprobar que la variable esta definida
            if (scope.HasVar(VarId, out tt) != ScopeLocation.NotDeclared)
            {
                //dar el valor correspondiente al tipo de esa expresion
                ReturnType = tt;

                if (IsVarFromDifferentFunction())
                    SetVarAsUsedForAnotherFunction();

                return true;
            }
            //error en caso q la variable no este definida
            listError.Add(new ErrorMessage(string.Format(Message.LoadMessage("VarUndecl"), VarId), Line, Columns));
            ReturnType = TigerType.GetType<ErrorType>();
            return false;
        }

        private void SetVarAsUsedForAnotherFunction()
        {
            IsForeignVar = true;

            VarInfo varInfo = CurrentScope.GetVarInfo(VarId);
            varInfo.IsLocalVariable = false;
            varInfo.IsUsedForAnotherFunction = true;
            FunctionInfo funInfo = CurrentScope.GetFunction(varInfo.FunctionNameParent);
            funInfo.ContainVarsUsedForAnotherFunction = true;
            if (!funInfo.VarsUsedForAnotherFunction.Contains(varInfo))
                CurrentScope.AsociatteVarToFunctionAsUsedForChildFunction(VarId);
        }

        public override void GenerateCode(ILCode code)
        {
            ILGenerator il = code.Method.GetILGenerator();

            //buscar la variable
            VarInfo varInfo = CurrentScope.GetVarInfo(VarId);
            string currentFunctionCodeName = CurrentScope.CurrentFunction.CodeName;
            if (IsForeignVar) //se entra aca cuando la variable pertenece a otra funcion que no es la actual .
            {
                //carga el respectivo campo de la clase contenedora.
                //como mis metodos son de instancia y las varibles tambien tengo que cargar el parametro 0
                il.Emit(OpCodes.Ldarg_0);
                //ahora viene cargar la variable de verdad. 
                TypeCodeInfo typeCodeInfo =
                    code.GetWrapperAsociatteTo(CurrentScope.CurrentFunction.FunctionParent.CodeName);
                while (typeCodeInfo != null && !typeCodeInfo.ContainFieldInLevel1(varInfo.CodeName))
                {
                    //cargo el campo que representa al padre del tipo actual
                    il.Emit(OpCodes.Ldfld, typeCodeInfo.GetField(typeCodeInfo.FieldNameOfParent));
                    typeCodeInfo = typeCodeInfo.Parent;
                }
                il.Emit(OpCodes.Ldfld, typeCodeInfo.GetField(varInfo.CodeName));
            }
            else
            {
                if (varInfo.IsLocalVariable)
                    il.Emit(OpCodes.Ldloc, code.DefinedLocal[varInfo.CodeName].LocalIndex);
                else if (varInfo.IsParameterFunction)
                    il.Emit(OpCodes.Ldarg, varInfo.ParameterNumber + 1);
                else // tengo que acceder a la variable a travez de la instancia que tengo como varible local.
                {
                    TypeCodeInfo typeCodeInfo = code.GetWrapperAsociatteTo(currentFunctionCodeName);
                    //cargar esta variable local donde estan todas las variables que son usadas por  los demas metodos.
                    il.Emit(OpCodes.Ldloc_0);
                    il.Emit(OpCodes.Ldfld, typeCodeInfo.GetField(varInfo.CodeName));
                }
            }

            //ver si debo dejar el valor en la pila.
            if (!code.PushOnStack)
                il.Emit(OpCodes.Pop);
        }

        /// <summary>
        /// Este metodo se usa cuando a la variable se le asigna un valor
        /// </summary>
        /// <param name="code"></param>
        /// <param name="exp"></param>
        public override void GenerateCode(ILCode code, ExpressionAST exp)
        {
            ILGenerator il = code.Method.GetILGenerator();

            //buscar la variable
            VarInfo varInfo = CurrentScope.GetVarInfo(VarId);

            //---> 

            bool pushOnStack = code.PushOnStack;
            code.PushOnStack = true;

            if (IsForeignVar)
                //aqui se entra cuando se usa una variable se usa en una funcion que no fue quien la declaro
            {
                string currentParentFunctionCodeName = CurrentScope.CurrentFunction.FunctionParent.CodeName;
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
                exp.GenerateCode(code);
                //lo asigno.
                il.Emit(OpCodes.Stfld, typeCodeInfo.GetField(varInfo.CodeName));
            }
            else
            {
                if (varInfo.IsLocalVariable)
                {
                    exp.GenerateCode(code);
                    il.Emit(OpCodes.Stloc, code.DefinedLocal[varInfo.CodeName].LocalIndex);
                }
                else if (varInfo.IsParameterFunction)
                {
                    exp.GenerateCode(code);
                    il.Emit(OpCodes.Starg, varInfo.ParameterNumber + 1);
                }
                else // tengo que acceder a la variable a travez de la instancia que tengo como varible local.
                {
                    //se asume que el wrapper siempre esta como primera variable del metodo.
                    il.Emit(OpCodes.Ldloc_0);
                    exp.GenerateCode(code);
                    il.Emit(OpCodes.Stfld, code.DefinedField[varInfo.CodeName]);
                }
            }
            code.PushOnStack = pushOnStack;
        }

        /// <summary>
        /// Este metodo devuelve true si la variable actual se declaro en otra funcion que no el la actual.
        /// </summary>
        /// <returns></returns>
        private bool IsVarFromDifferentFunction()
        {
            VarInfo varInfo = CurrentScope.GetVarInfo(VarId);
            return (varInfo.FunctionNameParent != CurrentScope.CurrentFunction.FunctionName);
        }

        #endregion

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitVar(this);
        }
    }
}