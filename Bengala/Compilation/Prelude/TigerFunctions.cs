#region Usings

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Bengala.AST.SemanticsUtils;
using Bengala.AST.Types;
using Bengala.AST.Utils;
using Bengala.Compilation.Helpers;

#endregion

namespace Bengala.Compilation.Prelude
{
    /// <summary>
    /// Constituye el paquete de las funciones basicas del Tiger
    /// </summary>
    public class TigerIlFunctions
    {
        #region Function'Generators

        private static void PrintFunction(string functionName, ILCode codeInfo)
        {
            TypeBuilder type = codeInfo.Type;

            MethodBuilder print = type.DefineMethod(functionName, MethodAttributes.Static | MethodAttributes.Public,
                                                    null, new[] {typeof (string)});

            ILGenerator il = print.GetILGenerator();

            //definiendo el cuerpo del metodo print .
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Call, typeof (Console).GetMethod("Write", new[] {typeof (string)}));
            il.Emit(OpCodes.Ret);

            codeInfo.DefinedMethod.Add(functionName, print);
        }

        private static void PrintIntFunction(string functionName, ILCode codeInfo)
        {
            TypeBuilder type = codeInfo.Type;

            MethodBuilder print = type.DefineMethod(functionName, MethodAttributes.Static | MethodAttributes.Public,
                                                    null, new[] {typeof (int)});

            ILGenerator il = print.GetILGenerator();

            //definiendo el cuerpo del metodo print .
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Call, typeof (Console).GetMethod("WriteLine", new[] {typeof (int)}));
            il.Emit(OpCodes.Ret);

            codeInfo.DefinedMethod.Add(functionName, print);
        }

        private static void SizeFunction(string functionName, ILCode codeInfo)
        {
            TypeBuilder type = codeInfo.Type;
            // size(s:string):int
            MethodBuilder mBuilder = type.DefineMethod(functionName, MethodAttributes.Static | MethodAttributes.Public,
                                                       typeof (int), new[] {typeof (string)});

            ILGenerator il = mBuilder.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Callvirt, typeof (string).GetProperty("Length").GetGetMethod());
            il.Emit(OpCodes.Ret);

            codeInfo.DefinedMethod.Add(functionName, mBuilder);
        }

        private static void SubStringFunction(string functionName, ILCode codeInfo)
        {
            TypeBuilder type = codeInfo.Type;
            // substring(s:string,i:int,n:int):string
            MethodBuilder mBuilder = type.DefineMethod(functionName, MethodAttributes.Static | MethodAttributes.Public,
                                                       typeof (string),
                                                       new[] {typeof (string), typeof (int), typeof (int)});

            ILGenerator il = mBuilder.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Ldarg_2);


            il.Emit(OpCodes.Callvirt, typeof (string).GetMethod("Substring", new[] {typeof (int), typeof (int)}));
            il.Emit(OpCodes.Ret);

            codeInfo.DefinedMethod.Add(functionName, mBuilder);
        }

        private static void NotFunction(string functionName, ILCode codeInfo)
        {
            TypeBuilder type = codeInfo.Type;
            // substring(s:string,i:int,n:int):string
            MethodBuilder mBuilder = type.DefineMethod(functionName, MethodAttributes.Static | MethodAttributes.Public,
                                                       typeof (int), new[] {typeof (int)});

            ILGenerator il = mBuilder.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);

            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Ceq);
            il.Emit(OpCodes.Ret);

            codeInfo.DefinedMethod.Add(functionName, mBuilder);
        }

        private static void GetCharFunction(string functionName, ILCode codeInfo)
        {
            TypeBuilder type = codeInfo.Type;
            // substring(s:string,i:int,n:int):string
            MethodBuilder size = type.DefineMethod(functionName, MethodAttributes.Static | MethodAttributes.Public,
                                                   typeof (string), null);

            ILGenerator il = size.GetILGenerator();

            il.Emit(OpCodes.Call, typeof (Console).GetMethod("ReadLine", Type.EmptyTypes));

            il.Emit(OpCodes.Ret);

            codeInfo.DefinedMethod.Add(functionName, size);
        }

        private static void OrdFunction(string functionName, ILCode codeInfo)
        {
            TypeBuilder type = codeInfo.Type;
            //ord(s:string)
            MethodBuilder ord = type.DefineMethod(functionName, MethodAttributes.Static | MethodAttributes.Public,
                                                  typeof (int), new[] {typeof (string)});

            ILGenerator il = ord.GetILGenerator();
            //TODO: Esto fue cambiado
            LocalBuilder result = il.DeclareLocal(typeof(int));
            Label asciiValue = il.DefineLabel();
            Label end = il.DefineLabel();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Call, typeof (string).GetMethod("IsNullOrEmpty"));

            il.Emit(OpCodes.Brfalse_S, asciiValue);
            il.Emit(OpCodes.Ldc_I4_M1);
            il.Emit(OpCodes.Stloc_0);

            il.Emit(OpCodes.Br_S, end);

            il.MarkLabel(asciiValue);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Callvirt, typeof (string).GetMethod("get_Chars", new[] {typeof (int)}));
            il.Emit(OpCodes.Conv_I1);
            il.Emit(OpCodes.Stloc_0);

            il.MarkLabel(end);

            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ret);

            codeInfo.DefinedMethod.Add(functionName, ord);
        }

        private static void CharFunction(string functionName, ILCode codeInfo)
        {
            TypeBuilder type = codeInfo.Type;
            MethodBuilder mBuilder = type.DefineMethod(functionName, MethodAttributes.Public | MethodAttributes.Static,
                                                       typeof (string), new[] {typeof (int)});

            ILGenerator il = mBuilder.GetILGenerator();
            LocalBuilder localChar = il.DeclareLocal(typeof (Int16));
            Label toValue = il.DefineLabel();

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldc_I4, 255);
            il.Emit(OpCodes.Cgt);


            il.Emit(OpCodes.Brfalse, toValue);
            il.Emit(OpCodes.Ldstr, "No existe caracter ascci para ese numero");
            il.Emit(OpCodes.Newobj, typeof (ArgumentException).GetConstructor(new[] {typeof (string)}));
            il.Emit(OpCodes.Throw);

            il.MarkLabel(toValue);

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Conv_I2);
            il.Emit(OpCodes.Stloc_0);
            il.Emit(OpCodes.Ldloca, localChar);
            il.Emit(OpCodes.Call, typeof (char).GetMethod("ToString", new Type[0]));
            il.Emit(OpCodes.Ret);

            codeInfo.DefinedMethod.Add(functionName, mBuilder);
        }

        private static void ConcatFunction(string functionName, ILCode codeInfo)
        {
            TypeBuilder type = codeInfo.Type;
            MethodBuilder mBuilder = type.DefineMethod(functionName, MethodAttributes.Public | MethodAttributes.Static,
                                                       typeof (string), new[] {typeof (string), typeof (string)});

            ILGenerator il = mBuilder.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Call, typeof (string).GetMethod("Concat", new[] {typeof (string), typeof (string)}));
            il.Emit(OpCodes.Ret);

            codeInfo.DefinedMethod.Add(functionName, mBuilder);
        }

        private static void ExitFunction(string functionName, ILCode codeInfo)
        {
            TypeBuilder type = codeInfo.Type;
            MethodBuilder mBuilder = type.DefineMethod(functionName, MethodAttributes.Public | MethodAttributes.Static,
                                                       null, new[] {typeof (int)});

            ILGenerator il = mBuilder.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Call, typeof (Environment).GetMethod("Exit", new[] {typeof (int)}));
            il.Emit(OpCodes.Ret);

            codeInfo.DefinedMethod.Add(functionName, mBuilder);
        }

        private static void FlushFunction(string functionName, ILCode codeInfo)
        {
            TypeBuilder type = codeInfo.Type;
            MethodBuilder mBuilder = type.DefineMethod(functionName, MethodAttributes.Public | MethodAttributes.Static,
                                                       null, Type.EmptyTypes);

            ILGenerator il = mBuilder.GetILGenerator();
            il.Emit(OpCodes.Call, typeof (Console).GetMethod("get_Out", Type.EmptyTypes));
            il.Emit(OpCodes.Callvirt, typeof (TextWriter).GetMethod("Flush", Type.EmptyTypes));
            il.Emit(OpCodes.Ret);

            codeInfo.DefinedMethod.Add(functionName, mBuilder);
        }

        #endregion

        #region Adding predifined functions

        public static void AddPrintFunctionToScope(Inizializator<ILCode> init)
        {
            FunctionInfo funInfo;
            funInfo = new FunctionInfo(new List<KeyValuePair<string, TigerType>>(), TigerType.GetType<NoType>());
            funInfo.ParameterList.Add(new KeyValuePair<string, TigerType>("s", TigerType.GetType<StringType>()));
            funInfo.FunctionName = "print";
            funInfo.IsPredifined = true;

            var print = new FunctionPredifined<ILCode>(funInfo, PrintFunction);

            init.AddPredifinedFunction(print);
        }

        public static void AddPrintIntFunctionToScope(Inizializator<ILCode> init)
        {
            FunctionInfo funInfo;
            funInfo = new FunctionInfo(new List<KeyValuePair<string, TigerType>>(), TigerType.GetType<NoType>());
            funInfo.ParameterList.Add(new KeyValuePair<string, TigerType>("i", TigerType.GetType<IntType>()));
            funInfo.FunctionName = "printi";
            funInfo.IsPredifined = true;

            var printi = new FunctionPredifined<ILCode>(funInfo, PrintIntFunction);

            init.AddPredifinedFunction(printi);
        }

        public static void AddSizeFunctionToScope(Inizializator<ILCode> init)
        {
            FunctionInfo funInfo;
            funInfo = new FunctionInfo(new List<KeyValuePair<string, TigerType>>(),
                                       TigerType.GetType<IntType>());
            funInfo.ParameterList.Add(GetKeyValue("s", TigerType.GetType<StringType>()));
            funInfo.FunctionName = "size";
            funInfo.IsPredifined = true;
            var size = new FunctionPredifined<ILCode>(funInfo, SizeFunction);

            init.AddPredifinedFunction(size);
        }

        public static void AddSubStringFunctionToScope(Inizializator<ILCode> init)
        {
            FunctionInfo funInfo;
            funInfo = new FunctionInfo(new List<KeyValuePair<string, TigerType>>(), TigerType.GetType<StringType>());
            funInfo.ParameterList.Add(GetKeyValue("s", TigerType.GetType<StringType>()));
            funInfo.ParameterList.Add(GetKeyValue("i", TigerType.GetType<IntType>()));
            funInfo.ParameterList.Add(GetKeyValue("n", TigerType.GetType<IntType>()));
            funInfo.FunctionName = "substring";
            funInfo.IsPredifined = true;
            var subString = new FunctionPredifined<ILCode>(funInfo, SubStringFunction);

            init.AddPredifinedFunction(subString);
        }

        public static void AddNotFunctionToScope(Inizializator<ILCode> init)
        {
            FunctionInfo funInfo;
            funInfo = new FunctionInfo(new List<KeyValuePair<string, TigerType>>(), TigerType.GetType<IntType>());
            funInfo.ParameterList.Add(GetKeyValue("i", TigerType.GetType<IntType>()));
            funInfo.FunctionName = "not";
            funInfo.IsPredifined = true;
            var not = new FunctionPredifined<ILCode>(funInfo, NotFunction);

            init.AddPredifinedFunction(not);
        }

        public static void AddGetCharFunctionToScope(Inizializator<ILCode> init)
        {
            FunctionInfo funInfo;
            funInfo = new FunctionInfo(new List<KeyValuePair<string, TigerType>>(), TigerType.GetType<StringType>());
            funInfo.FunctionName = "getchar";
            funInfo.IsPredifined = true;
            var getChar = new FunctionPredifined<ILCode>(funInfo, GetCharFunction);

            init.AddPredifinedFunction(getChar);
        }

        public static void AddOrdFunctionToScope(Inizializator<ILCode> init)
        {
            FunctionInfo funInfo;
            funInfo = new FunctionInfo(new List<KeyValuePair<string, TigerType>>(), TigerType.GetType<IntType>());
            funInfo.ParameterList.Add(GetKeyValue("s", TigerType.GetType<StringType>()));
            funInfo.FunctionName = "ord";
            funInfo.IsPredifined = true;
            var ord = new FunctionPredifined<ILCode>(funInfo, OrdFunction);

            init.AddPredifinedFunction(ord);
        }

        public static void AddCharFunctionToScope(Inizializator<ILCode> init)
        {
            FunctionInfo funInfo;
            funInfo = new FunctionInfo(new List<KeyValuePair<string, TigerType>>(), TigerType.GetType<StringType>());
            funInfo.ParameterList.Add(GetKeyValue("i", TigerType.GetType<IntType>()));
            funInfo.FunctionName = "chr";
            funInfo.IsPredifined = true;
            var chr = new FunctionPredifined<ILCode>(funInfo, CharFunction);

            init.AddPredifinedFunction(chr);
        }

        public static void AddConcatFunctionToScope(Inizializator<ILCode> init)
        {
            FunctionInfo funInfo;
            funInfo = new FunctionInfo(new List<KeyValuePair<string, TigerType>>(), TigerType.GetType<StringType>());
            funInfo.ParameterList.Add(GetKeyValue("s1", TigerType.GetType<StringType>()));
            funInfo.ParameterList.Add(GetKeyValue("s2", TigerType.GetType<StringType>()));
            funInfo.FunctionName = "concat";
            funInfo.IsPredifined = true;
            var concat = new FunctionPredifined<ILCode>(funInfo, ConcatFunction);

            init.AddPredifinedFunction(concat);
        }

        public static void AddExitFunctionToScope(Inizializator<ILCode> init)
        {
            FunctionInfo funInfo;
            funInfo = new FunctionInfo(new List<KeyValuePair<string, TigerType>>(), TigerType.GetType<NoType>());
            funInfo.ParameterList.Add(GetKeyValue("i", TigerType.GetType<IntType>()));

            funInfo.FunctionName = "exit";
            funInfo.IsPredifined = true;
            var exit = new FunctionPredifined<ILCode>(funInfo, ExitFunction);

            init.AddPredifinedFunction(exit);
        }

        public static void AddFlushFunctionToScope(Inizializator<ILCode> init)
        {
            var funInfo = new FunctionInfo(new List<KeyValuePair<string, TigerType>>(), TigerType.GetType<NoType>())
                                       {
                                           FunctionName = "flush",
                                           IsPredifined = true
                                       };
            var exit = new FunctionPredifined<ILCode>(funInfo, FlushFunction);

            init.AddPredifinedFunction(exit);
        }



        //public static void AddLengthFunctionToScope(Inizializator<ILCode> init)
        //{
        //    FunctionInfo funInfo;
        //    funInfo = new FunctionInfo(new List<KeyValuePair<string, TigerType>>(), TigerType.GetType<IntType>());
        //    funInfo.ParameterList.Add(GetKeyValue("elems", TigerType.GetType<ArrayType>()));

        //    funInfo.FunctionName = "length";
        //    funInfo.IsPredifined = true;
        //    FunctionPredifined<ILCode> exit = new FunctionPredifined<ILCode>(funInfo, TigerIlFunctions.ExitFunction);

        //    init.AddPredifinedFunction(exit);
        //} 

        #endregion

        private static KeyValuePair<string, TigerType> GetKeyValue(string name, TigerType type)
        {
            return new KeyValuePair<string, TigerType>(name, type);
        }


        /// <summary>
        /// Annade los tipos predefinidos
        /// </summary>
        /// <param name="init"></param>
        private static void AddTypes(Inizializator<ILCode> init)
        {
            //anadir los tipos
            //--> int
            var typeTemp = new PrefinedType<ILCode>(TigerType.GetType<IntType>(),
                                                    (name, code) =>
                                                    code.DefinedType.Add(name, typeof (int)));
            init.AddPredifinedTypes(typeTemp);
            //<-- end int
            //--> string
            typeTemp = new PrefinedType<ILCode>(TigerType.GetType<StringType>(),
                                                (name, code) => code.DefinedType.Add(name, typeof (string)));

            init.AddPredifinedTypes(typeTemp);
            //<-- end string

            //--> errorType
            typeTemp = new PrefinedType<ILCode>(TigerType.GetType<ErrorType>(),
                                                (name, code) => { });
            init.AddPredifinedTypes(typeTemp);
            //<-- end errorType
        }

        /// <summary>
        /// Annade las funciones predifinidas
        /// </summary>
        /// <param name="init"></param>
        /// <remarks>Esta funcion asume que las funciones que ella llama contienen en el nombre el patron "FuncionToScope"</remarks>
        private static void AddFunctionsToScope(Inizializator<ILCode> init)
        {
            Type current = typeof (TigerIlFunctions);
            //se queda con todos los metodos publicos que contienes "FunctionToScope"
            IEnumerable<MethodInfo> methodsToCall = from m in current.GetMethods()
                                                    where m.Name.Contains("FunctionToScope")
                                                    select m;
            foreach (var met in methodsToCall)
                met.Invoke(null, new object[] {init});
        }

        /// <summary>
        /// Metodo encargado de annadir al scope las funciones predefinidas ,asi como asociarlas con sus generadores
        /// de codigo
        /// </summary>
        /// <param name="init"></param>
        public static void InitScope(Inizializator<ILCode> init)
        {
            AddFunctionsToScope(init);

            AddTypes(init);
        }
    }
}