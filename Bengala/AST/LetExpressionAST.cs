#region Usings

using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Bengala.AST.CodeGenerationUtils;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    public class LetExpressionAST : ExpressionAST
    {
        #region Fields and Properties

        /// <summary>
        /// lista de declaraciones del let
        /// </summary>
        public List<Declaration> DeclarationList;

        /// <summary>
        /// cuerpo del let
        /// </summary>
        public SequenceExpressionAST SequenceExpressionList { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// </summary>
        /// <param name="declarationList">lista de declaraciones del let</param>
        /// <param name="seqExpList">cuerpo del let</param>
        public LetExpressionAST(List<Declaration> declarationList, SequenceExpressionAST seqExpList) : base(0, 0)
            //no problemas con pasar 0,0 let no produce errores propios
        {
            SequenceExpressionList = seqExpList;
            DeclarationList = declarationList ?? new List<Declaration>();
        }

        #endregion

        #region Instance Methods

    
     

        #region Generacion de codigo

        public override void GenerateCode(ILCode code)
        {
            //declarar lo que se declarable
            foreach (var item in DeclarationList)
                item.GenerateCode(code);

            //crear la instancia del tipo wrapper que esta como variable local en la funcion
            CreateInstanceOfWrapper(code);
            //generar el codigo para el cuerpo ejecutable del let.
            SequenceExpressionList.GenerateCode(code);
        }

        private void CreateInstanceOfWrapper(ILCode code)
        {
            string currentFunctionCodeName = CurrentScope.CurrentFunction.CodeName;

            TypeCodeInfo typeCodeInfo = code.GetWrapperAsociatteTo(currentFunctionCodeName);

            ILGenerator il = code.Method.GetILGenerator();
            //crear la instancia del objeto que contiene las variable mias que son usadas por otras funciones
            il.Emit(OpCodes.Newobj, typeCodeInfo.DefaultConstructor());
            il.Emit(OpCodes.Stloc_0);

            //asignarle la instancia de mi clase a esta objeto para que tenga la referencia a su padre.
            //locaL_0.parent =  this.

            if (CurrentFunctionName() != "main$")
            {
                FieldBuilder parent = typeCodeInfo.GetField("parent");
                il.Emit(OpCodes.Ldloc_0);
                //cargar el  this
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Stfld, parent);

                //darle los valores  de las variables a los campos de esta clase.
                InitilizateFieldFromParameters(code);
            }
            //obligo a que se lanze el evento que estan esperando los que inicializa las varialbles
            code.ThrowEventForFunction(currentFunctionCodeName);
        }

        /// <summary>
        /// Este metodo asigna los valores de los parametros que son usados por otra funciones a los campos de la clase que 
        /// tengo como variable local
        /// </summary>
        /// <param name="code"></param>
        private void InitilizateFieldFromParameters(ILCode code)
        {
            ILGenerator il = code.Method.GetILGenerator();
            FunctionInfo funInfo = CurrentScope.GetFunction(CurrentFunctionName());
            for (int i = 0; i < funInfo.ParameterList.Count; i++)
            {
                VarInfo varInfo = CurrentScope.GetVarInfo(funInfo.ParameterList[0].Key);
                if (funInfo.VarsUsedForAnotherFunction.Contains(varInfo))
                {
                    //se asume que el wrapper siempre esta como primera variable del metodo.
                    il.Emit(OpCodes.Ldloc_0);
                    il.Emit(OpCodes.Ldarg, i + 1);
                    il.Emit(OpCodes.Stfld, code.DefinedField[varInfo.CodeName]);
                }
            }
        }

        private string CurrentFunctionName()
        {
            return CurrentScope.CurrentFunction.FunctionName;
        }

        #endregion

        #endregion

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitLetExpression(this);
        }
    }
}