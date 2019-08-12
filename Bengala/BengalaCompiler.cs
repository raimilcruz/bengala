using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Antlr4.Runtime;
using Bengala.Analysis;
using Bengala.Antlr;
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
        public bool GenerateCode { get; set; }

        public BengalaCompiler()
        {
            GenerateCode = true;
        }


        private const string MainFunction = "main$";
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

        private ILCode CreateCode(string moduleName, string typeName, AstNode exp)
        {
            if (Path.GetFileName(moduleName) != moduleName)
                throw new Exception("can only output into current directory!");

            var name = new AssemblyName(Path.GetFileNameWithoutExtension(moduleName));
            AssemblyBuilder asmb = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.Save);
            ModuleBuilder modb = asmb.DefineDynamicModule(moduleName);
            TypeBuilder typeBuilder = modb.DefineType(typeName);
            MethodBuilder methMain = typeBuilder.DefineMethod("Main", MethodAttributes.Static, typeof(void),
                                                              Type.EmptyTypes);

            var ilcode = new ILCode { Type = typeBuilder, Method = methMain, Module = modb };

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

        public List<AnalysisError> Compile(string filename)
        {
            var errors = new List<AnalysisError>();
            if (File.Exists(filename))
            {
                try
                {
                    errors.AddRange(Compile(filename, " Bengala"));
                }
                catch (Exception e)
                {
                    errors.Add(new AnalysisError(
                        $"There was an internal error in the compiler: \"{e.Message}\"", 0, 0));
                }
            }
            else
                errors.Add(new AnalysisError($"El fichero \"{filename}\" no existe", 0, 0));
            return errors;
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
        private IEnumerable<AnalysisError> Compile(string filename, string typeName)
        {
            var s = new StreamReader(filename);
            var stm = new AntlrInputStream(s); ;
            var lexer = new TigerLexer(stm);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new BengalaParser(tokenStream);
            parser.ConfigErrorListeners();
            var expContext = parser.program();

            var contextVisitor = new BuildAstVisitor();

            var errorsWarning = parser.Errors;

            if (parser.NumberOfSyntaxErrors != 0 || parser.Errors.Any())
                return errorsWarning;

            var astRoot = expContext.Accept(contextVisitor);

            var generalScope = new Scope(null);
            init = new Inizializator<ILCode>(generalScope);
            InitScope(generalScope);

            var errorListener = new BengalaBaseErrorListener();
            var staticAnalysisVisitor = new StaticChecker(errorListener, generalScope);
            astRoot.Accept(staticAnalysisVisitor);

            if (GenerateCode && !errorListener.Errors.Any()) 
                CreateCode(FileName(filename), typeName, astRoot);

            return errorListener.Errors;
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