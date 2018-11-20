using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using Bengala.AST;
using Bengala.AST.Errors;
using Bengala.AST.SemanticsUtils;
using Bengala.AST.Types;
using Bengala.AST.Utils;
using Bengala.Compilation;
using Bengala.Compilation.Helpers;
using Bengala.Compilation.Prelude;

namespace Bengala
{
    /// <summary>
    /// Representa a un compilador de Tiger que genera IL
    /// </summary>
    public class BengalaCompiler
    {
        private const string MainFunction = "main$";
        protected ExpressionAst exp;
        protected Inizializator<ILCode> init;

        private static TypeBuilder AddFunctionMainToCode(ILCode code, MethodBuilder main)
        {
            code.DefinedMethod.Add(MainFunction, main);
            //crear lo que sera el wrapper a esta funcion
            string currentWrapper = string.Format("Tg_{0}", MainFunction);
            TypeBuilder typeNested = code.Type.DefineNestedType(currentWrapper,
                                                                TypeAttributes.NestedPublic);

            //crear en la variable de instancia de la clase contenedora,pues el Let asume que la tiene creada.
            ILGenerator il = main.GetILGenerator();
            il.DeclareLocal(typeNested);

            //annadir a la clase code el tipo.
            code.DefinedType.Add(typeNested.Name, typeNested);
            //asociar el tipo wrapper al metodo
            code.AsociatteMethodToWrapper(MainFunction, currentWrapper);

            code.GetWrapperAsociatteTo(MainFunction);
            //PopulateNestedTypeWithVar(code, typeNested, typeCodeInfo);

            return typeNested;
        }

        private ILCode CreateCode(string moduleName, string typeName)
        {
            if (Path.GetFileName(moduleName) != moduleName)
                throw new Exception("can only output into current directory!");

            var name = new AssemblyName(Path.GetFileNameWithoutExtension(moduleName));
            AssemblyBuilder asmb = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.Save);
            ModuleBuilder modb = asmb.DefineDynamicModule(moduleName);
            TypeBuilder typeBuilder = modb.DefineType(typeName);
            MethodBuilder methMain = typeBuilder.DefineMethod("Main", MethodAttributes.Static, typeof (void),
                                                              Type.EmptyTypes);

            var ilcode = new ILCode {Type = typeBuilder, Method = methMain, Module = modb};

            init.CodeInfo = ilcode;
            init.GeneratePredifinedCode();

            TypeBuilder nested = AddFunctionMainToCode(ilcode, methMain);

            ilcode.Type = nested;

            ILCodeGenerator codeGenerator = new ILCodeGenerator(ilcode);
            //generacion de codigos
            exp.Accept(codeGenerator);
            ILGenerator il = methMain.GetILGenerator();

            il.Emit(OpCodes.Ret);

            nested.CreateType();
            typeBuilder.CreateType();
            modb.CreateGlobalFunctions();
            asmb.SetEntryPoint(methMain);
            asmb.Save(moduleName);

            return ilcode;
        }

        public List<Message> Compile(string filename)
        {
            var errores = new List<Message>();
            if (File.Exists(filename))
            {
                try
                {
                    errores = Compile(filename, " Bengala");
                    return errores;
                }
                catch
                {
                    errores.Clear();
                    errores.Add(new ErrorMessage(
                                    string.Format("El fichero \"{0}\" no es un fichero de texto", filename), 0, 0));
                    return errores;
                }
            }
            errores.Add(new ErrorMessage(string.Format("El fichero \"{0}\" no existe", filename), 0, 0));
            return errores;
        }

        private static string FileName(string filename)
        {
            string fileNameWithExtension = Path.GetFileNameWithoutExtension(filename);
            return fileNameWithExtension + ".exe";
        }

        /// <summary>
        /// Compila el codigo que se encuentra en el fichero filename
        /// </summary>
        /// <param name="filename">El fichero a compilar</param>
        /// <param name="typeName">El nombre del tipo que contendra las funciones que se definan</param>
        /// <returns>Retorna una lista con los errores que se produjeron</returns>
        private List<Message> Compile(string filename, string typeName)
        {
            //var stm = new StreamReader(filename);
            //var reader = new ANTLRReaderStream(stm);
            //var lexer = new BengalaLexer(reader);
            //var cm = new CommonTokenStream(lexer);
            //var parser = new BengalaParser(lexer.Errors, cm);

            //exp = parser.program();

            //List<Message> erroresWarning = parser.Errors;
            ////if (parser.NumberOfSyntaxErrors == 0)
            //if (parser.Errors.Count == 0)
            //{
            //    var generalScope = new Scope(null);
            //    init = new Inizializator<ILCode>(generalScope);

            //    InitScope(generalScope);

            //    erroresWarning = new List<Message>();
            //    exp.CheckSemantic(generalScope, erroresWarning);

            //    var realErrores = from message in erroresWarning
            //                      where message is ErrorMessage 
            //                      select message;
            //    if (realErrores.Count() == 0)
            //        CreateCode(FileName(filename), typeName);
            //}
            //return erroresWarning ?? new List<Message>();
            return new List<Message>();
        }

        private static void AddMainToScope(Scope scope)
        {
            //---> principal function
            var funInfo = new FunctionInfo(new List<KeyValuePair<string, TigerType>>(), TigerType.GetType<NoType>())
                              {
                                  FunctionName = MainFunction,
                                  CodeName = MainFunction
                              };
            scope.AddFunction(MainFunction, funInfo);
            //setear este funcion como la funcion actul.
            scope.CurrentFunction = funInfo;
            //<--- end of principal function
        }

        private void InitScope(Scope scope)
        {
            init = new Inizializator<ILCode>(scope);

            //anadir la funcion main al scope ,esta no genera codigo es solo para que toda instruccion en tiger este dentro de alguna
            //funcion.
            AddMainToScope(scope);
            //annadir las funcione predefinidas

            TigerIlFunctions.InitScope(init);


            init.InitializeScope();
        }
    }

    public delegate void GenerateCode(ILCode code);
}