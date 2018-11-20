#region Usings

using System.Reflection.Emit;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST.Types
{
    /// <summary>
    /// Esta clase representa al tipo entero en el lenguaje Tiger.
    /// </summary>
    public class IntType : TigerType
    {
        public IntType()
        {
            IsPredifined = true;
            TypeID = Int;
        }

        public override bool IsValueType
        {
            get { return true; }
        }

        protected override void AddPredefinedOperations()
        {
            #region Operations

            #region Aritmeticals

            //operacion +
            AddBinaryOperation(this, this, Operators.Plus, x => x.Method.GetILGenerator().Emit(OpCodes.Add), true);

            //operacion *
            AddBinaryOperation(this, this, Operators.Prod, x => x.Method.GetILGenerator().Emit(OpCodes.Mul), true);

            //operacion /
            AddBinaryOperation(this, this, Operators.Div, x => x.Method.GetILGenerator().Emit(OpCodes.Div), true);

            //operacion -
            AddBinaryOperation(this, this, Operators.Minus, x => x.Method.GetILGenerator().Emit(OpCodes.Sub), true);

            //operacion %
            AddBinaryOperation(this, this, Operators.Mod, x => x.Method.GetILGenerator().Emit(OpCodes.Rem), true);

            #endregion

            #region Comparison

            //operacion ==
            AddBinaryOperation(this, this, Operators.Equal, code =>
                                                                {
                                                                    ILGenerator il = code.Method.GetILGenerator();
                                                                    il.Emit(OpCodes.Ceq);
                                                                    //pregunto si debo dejar el resultado en la pila
                                                                    if (!code.PushOnStack)
                                                                        il.Emit(OpCodes.Pop);
                                                                }, true);

            //operacion <>
            AddBinaryOperation(this, this, Operators.NotEqual, code =>
                                                                   {
                                                                       ILGenerator il = code.Method.GetILGenerator();
                                                                       //se asume que los operandos ya estan en la pilas.
                                                                       //los comparo
                                                                       il.Emit(OpCodes.Ceq);
                                                                       //cargo el valor 0
                                                                       il.Emit(OpCodes.Ldc_I4_0);
                                                                       //comparo el resultado de la igualdad con el cero.
                                                                       il.Emit(OpCodes.Ceq);

                                                                       //pregunto si debo dejar el valor en la pila
                                                                       if (!code.PushOnStack)
                                                                           il.Emit(OpCodes.Pop);
                                                                   }, true);

            //operacion >
            AddBinaryOperation(this, this, Operators.GreaterThan, x => x.Method.GetILGenerator().Emit(OpCodes.Cgt), true);

            //operacion <
            AddBinaryOperation(this, this, Operators.LessThan, x => x.Method.GetILGenerator().Emit(OpCodes.Clt), true);

            //operacion <=
            AddBinaryOperation(this, this, Operators.LessEqual, code =>
                                                                    {
                                                                        ILGenerator il = code.Method.GetILGenerator();
                                                                        il.Emit(OpCodes.Cgt);
                                                                        //cargo el cero 
                                                                        il.Emit(OpCodes.Ldc_I4_0);
                                                                        il.Emit(OpCodes.Ceq);
                                                                    }, true);

            //operacion >=
            AddBinaryOperation(this, this, Operators.GreaterEqual, code =>
                                                                       {
                                                                           ILGenerator il = code.Method.GetILGenerator();
                                                                           //comparo si menor 
                                                                           il.Emit(OpCodes.Clt);
                                                                           //cargo el 0 para la pila
                                                                           il.Emit(OpCodes.Ldc_I4_0);
                                                                           //comparo sin es igual ahora.
                                                                           il.Emit(OpCodes.Ceq);
                                                                       }, true);

            #endregion

            //and
            AddBinaryOperation(this, this, Operators.And, x => { }, true);
            //or
            AddBinaryOperation(this, this, Operators.Or, x => { }, true);

            //operacion + con string a la derecha
            AddBinaryOperation(GetType<StringType>(), GetType<StringType>(), Operators.Plus,
                               x =>
                               x.Method.GetILGenerator().Emit(OpCodes.Call,
                                                              typeof (string).GetMethod("Concat",
                                                                                        new[]
                                                                                            {
                                                                                                typeof (object),
                                                                                                typeof (object)
                                                                                            })), true);

            #endregion
        }

        public override bool SupportsOperator(TigerType other, Operators op)
        {
            switch (op)
            {
                case Operators.And:
                case Operators.Or:
                    return other == this;
                default:
                    return base.SupportsOperator(other, op);
            }
        }

        public override string ToString()
        {
            return "int";
        }
    }
}