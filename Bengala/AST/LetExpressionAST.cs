#region Usings

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
        public List<ExpressionAST> DeclarationList;

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
        public LetExpressionAST(List<ExpressionAST> declarationList, SequenceExpressionAST seqExpList) : base(0, 0)
            //no problemas con pasar 0,0 let no produce errores propios
        {
            SequenceExpressionList = seqExpList;
            DeclarationList = declarationList ?? new List<ExpressionAST>();
        }

        #endregion

        #region Instance Methods

        public override bool CheckSemantic(Scope scope, List<Message> listError)
        {
            //ordenar las declaraciones de manera q los tipos sean lo primero
            SortDeclarations();

            //el let abre un nuevo scope
            var sc = new Scope(scope, scope.ContainerLoop);
            CurrentScope = sc;

            int i = 0;
            for (; i < DeclarationList.Count; i++)
            {
                if (!(DeclarationList[i] is TypeDeclarationAST))
                    break;
                DeclarationList[i].CheckSemantic(sc, listError);
            }

            sc.Close();
            //Declaration List

            //se asume q no habra problemas
            ReturnType = TigerType.GetType<NoType>();

            //primera pasada por las funciones
            //se almacenan los scopes creados por cada una de ellas 
            //para que sean esos los q se le pasen en la 2da pasada
            var funcScopes = new List<Scope>();
            foreach (var fun in DeclarationList.GetRange(i, DeclarationList.Count - i))
            {
                FunctionDeclarationAST fDecl;
                //si esta declaracion es una funcion...
                if ((fDecl = fun as FunctionDeclarationAST) != null)
                {
                    //se crea el scope para esta funcion
                    var thisScope = new Scope(sc);
                    funcScopes.Add(thisScope);

                    //se hace el primer chequeo
                    FirstFunctionCheck(thisScope, fDecl, listError);
                }
            }

            //a medida q aparezcan las funciones se incrementa el contador
            //para q el chequeo de cada una se haga con el scope creado en la 1ra pasada
            int currentFunScope = 0;
            for (; i < DeclarationList.Count; i++)
            {
                ExpressionAST dec = DeclarationList[i];
                FunctionDeclarationAST fDecl;
                //si es una declaracion de funcion
                if ((fDecl = dec as FunctionDeclarationAST) != null)
                {
                    //segundo chequeo para comprobar el cuerpo 
                    if (!fDecl.CheckSemantic(funcScopes[currentFunScope++], listError))
                        ReturnType = TigerType.GetType<ErrorType>();
                }
                else if (!dec.CheckSemantic(sc, listError))
                    ReturnType = TigerType.GetType<ErrorType>();
            }

            //cierra el scope pq no habran mas declaraciones


            //chequeo de semantica del cuerpo del let
            if (SequenceExpressionList.CheckSemantic(sc, listError) && ReturnType == TigerType.GetType<NoType>())
            {
                AlwaysReturn = SequenceExpressionList.AlwaysReturn;
                ReturnType = SequenceExpressionList.ReturnType;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Primera pasada por las declaraciones de funciones
        /// solo para verificar su nombre, tipo de retorno y parametros
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="fDecl"></param>
        /// <param name="listError"></param>
        /// <returns></returns>
        private bool FirstFunctionCheck(Scope scope, FunctionDeclarationAST fDecl, List<Message> listError)
        {
            TigerType ret = null;
            if (!string.IsNullOrEmpty(fDecl.ReturnTypeId))
            {
                //si se especifica retorno pero este no es un tipo ya definido ERROR
                //esto lo garantiza haber organizado las declaraciones
                if (scope.HasType(fDecl.ReturnTypeId, out ret) == ScopeLocation.NotDeclared)
                {
                    listError.Add(new ErrorMessage(
                                      string.Format(Message.LoadMessage("TypeUndecl"), fDecl.ReturnTypeId), fDecl.Line,
                                      fDecl.Columns));
                    ReturnType = TigerType.GetType<ErrorType>();
                    return false;
                }
                if (!ret.IsLegalType)
                {
                    //TODO: Hasta que punto interesa lanzar este error??
                    listError.Add(new ErrorMessage(
                                      string.Format(Message.LoadMessage("InavalidRet"), fDecl.ReturnTypeId), fDecl.Line,
                                      fDecl.Columns));
                    ReturnType = TigerType.GetType<ErrorType>();
                    return false;
                }
            }

            //ver que la funcion no este previamente declarada
            if (scope.HasFunction(fDecl.FunctionId) == ScopeLocation.NotDeclared)
            {
                //verificar que todos los tipos de los parametros existan y si ya existe el nombre de los parametros
                //en el scope
                var paramsInfo = new List<KeyValuePair<string, TigerType>>();
                if (fDecl.ParameterList != null)
                {
                    int posParam = 0;
                    foreach (var nameType in fDecl.ParameterList)
                    {
                        TigerType t;
                        //verificar si existe el tipo del parametro.
                        if (scope.HasType(nameType.Value, out t) != ScopeLocation.NotDeclared)
                        {
                            //existen dos parametros con el mismo nombre.
                            if (scope.HasVar(nameType.Key) == ScopeLocation.DeclaredLocal)
                            {
                                listError.Add(
                                    new ErrorMessage(
                                        string.Format(Message.LoadMessage("FuncDeclParams"), nameType.Key), fDecl.Line,
                                        fDecl.Columns));
                                ReturnType = TigerType.GetType<ErrorType>();
                                return false;
                            }
                            //existe una variable con el mismo nombre que este parametro en un ambito mas externo
                            if (scope.HasVar(nameType.Key) != ScopeLocation.NotDeclared)
                            {
                                listError.Add(
                                    new WarningMessage(string.Format(Message.LoadMessage("Hide"), nameType.Key),
                                                       fDecl.Line, fDecl.Columns));
                            }
                            //se anade este valor al scope de la funcion
                            scope.AddVarParameter(nameType.Key, nameType.Value, posParam, fDecl.FunctionId);
                            //annadiendo la informacion del parametro.
                            paramsInfo.Add(new KeyValuePair<string, TigerType>(nameType.Key, t));

                            posParam++;
                        }
                    }
                }
                //se anade en el padre para q este desiponible en el scope donde se declara
                var funInfo = new FunctionInfo(paramsInfo, ret ?? TigerType.GetType<NoType>());
                funInfo.FunctionName = fDecl.FunctionId;
                funInfo.FunctionParent = scope.CurrentFunction;
                scope.Parent.AddFunction(fDecl.FunctionId, funInfo);

                return true;
            }
            //ya habia una funcion con ese nombre
            listError.Add(new ErrorMessage(string.Format(Message.LoadMessage("FuncDecl"), fDecl.FunctionId), fDecl.Line,
                                           fDecl.Columns));
            return false;
        }

        /// <summary>
        /// Ordena las declaraciones de manera q las declaraciones de tipo son lo primero
        /// </summary>
        private void SortDeclarations()
        {
            int pos = 0;
            for (int i = 0; i < DeclarationList.Count; i++)
            {
                if (DeclarationList[i] as TypeDeclarationAST != null)
                {
                    DeclarationList.Insert(pos++, DeclarationList[i]);
                    DeclarationList.RemoveAt(i + 1);
                }
            }
        }

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