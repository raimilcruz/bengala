#region Usings

using System;
using System.Reflection.Emit;

#endregion

namespace Bengala.AST.SemanticsUtils
{
    /// <summary>
    /// Representa al tipo string en Tiger.
    /// </summary>
    public class StringType : TigerType
    {
        #region Constructors

        public StringType()
        {
            IsPredifined = true;
            TypeID = String;
        }

        #endregion

        #region Instance Methods

        protected override void AddPredefinedOperations()
        {
            #region Operations

            #region Concatenacion

            //operacion + con otro string 
            AddBinaryOperation(this, this, Operators.Plus,
                               x =>
                               x.Method.GetILGenerator().Emit(OpCodes.Call,
                                                              typeof (string).GetMethod("Concat",
                                                                                        new[]
                                                                                            {
                                                                                                typeof (string),
                                                                                                typeof (string)
                                                                                            })), true);

            //operacion + con int a la derecha
            AddBinaryOperation(GetType<IntType>(), this, Operators.Plus,
                               x =>
                                   {
                                       ILGenerator il = x.Method.GetILGenerator();
                                       il.Emit(OpCodes.Box, typeof (Int32));
                                       il.Emit(OpCodes.Call,
                                               typeof (string).GetMethod("Concat",
                                                                         new[] {typeof (string), typeof (string)}));
                                   }, true);

            #endregion

            #region Comparison 

            // > 
            AddBinaryOperation(this, GetType<IntType>(), Operators.GreaterThan,
                               code =>
                                   {
                                       ILGenerator il = code.Method.GetILGenerator();
                                       il.Emit(OpCodes.Callvirt,
                                               typeof (string).GetMethod("CompareTo", new[] {typeof (string)}));
                                       //
                                       il.Emit(OpCodes.Ldc_I4_0);
                                       il.Emit(OpCodes.Cgt);

                                       if (!code.PushOnStack)
                                           il.Emit(OpCodes.Pop);
                                   }, true);
            // >= 
            AddBinaryOperation(this, GetType<IntType>(), Operators.GreaterEqual,
                               code =>
                                   {
                                       ILGenerator il = code.Method.GetILGenerator();
                                       il.Emit(OpCodes.Callvirt,
                                               typeof (string).GetMethod("CompareTo", new[] {typeof (string)}));
                                       //
                                       il.Emit(OpCodes.Ldc_I4_M1);
                                       il.Emit(OpCodes.Cgt);

                                       if (!code.PushOnStack)
                                           il.Emit(OpCodes.Pop);
                                   }, true);
            // <
            AddBinaryOperation(this, GetType<IntType>(), Operators.LessThan,
                               code =>
                                   {
                                       ILGenerator il = code.Method.GetILGenerator();
                                       il.Emit(OpCodes.Callvirt,
                                               typeof (string).GetMethod("CompareTo", new[] {typeof (string)}));
                                       //
                                       il.Emit(OpCodes.Ldc_I4_0);
                                       il.Emit(OpCodes.Clt);
                                       if (!code.PushOnStack)
                                           il.Emit(OpCodes.Pop);
                                   }, true);
            //// <=
            AddBinaryOperation(this, GetType<IntType>(), Operators.LessEqual,
                               code =>
                                   {
                                       ILGenerator il = code.Method.GetILGenerator();
                                       il.Emit(OpCodes.Callvirt,
                                               typeof (string).GetMethod("CompareTo", new[] {typeof (string)}));
                                       //
                                       il.Emit(OpCodes.Ldc_I4_1);
                                       il.Emit(OpCodes.Clt);
                                       if (!code.PushOnStack)
                                           il.Emit(OpCodes.Pop);
                                   }, true);

            #endregion

            #region Equality String-String

            //operacion ==
            AddBinaryOperation(this, GetType<IntType>(), Operators.Equal, code =>
                                                                              {
                                                                                  ILGenerator il =
                                                                                      code.Method.GetILGenerator();
                                                                                  il.Emit(OpCodes.Call,
                                                                                          typeof (string).GetMethod(
                                                                                              "op_Equality",
                                                                                              new[]
                                                                                                  {
                                                                                                      typeof (string),
                                                                                                      typeof (string)
                                                                                                  }));
                                                                                  //pregunto si debo dejar el resultado en la pila
                                                                                  if (!code.PushOnStack)
                                                                                      il.Emit(OpCodes.Pop);
                                                                              }, true);

            //operacion <>
            AddBinaryOperation(this, GetType<IntType>(), Operators.NotEqual, code =>
                                                                                 {
                                                                                     ILGenerator il =
                                                                                         code.Method.GetILGenerator();
                                                                                     //se asume que los operandos ya estan en la pilas.
                                                                                     //los comparo
                                                                                     il.Emit(OpCodes.Call,
                                                                                             typeof (string).GetMethod(
                                                                                                 "op_Inequality",
                                                                                                 new[]
                                                                                                     {
                                                                                                         typeof (string)
                                                                                                         ,
                                                                                                         typeof (string)
                                                                                                     }));
                                                                                     //pregunto si debo dejar el valor en la pila
                                                                                     if (!code.PushOnStack)
                                                                                         il.Emit(OpCodes.Pop);
                                                                                 }, true);

            #endregion

            #region Equality String -Nil

            AddBinaryOperation(GetType<NilType>(), GetType<IntType>(), Operators.Equal, EqualOpGenerator, true);

            //operacion <>
            AddBinaryOperation(GetType<NilType>(), GetType<IntType>(), Operators.NotEqual, NeqOpGenerator, true);

            #endregion

            #endregion
        }

        public override string ToString()
        {
            return "string";
        }

        #endregion
    }
}