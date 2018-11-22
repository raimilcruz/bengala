using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Bengala.AST;
using Bengala.AST.SemanticsUtils;
using Bengala.AST.Types;
using Bengala.Compilation.Helpers;
using TypeInfo = Bengala.AST.SemanticsUtils.TypeInfo;

namespace Bengala.Compilation
{
    public class ILCodeGenerator : AstVisitor<Unit>
    {
        private ILCode code;

        public ILCodeGenerator(ILCode code)
        {
            this.code = code;
        }
        public override Unit VisitNode(AstNode node)
        {
            throw new System.NotImplementedException();
        }

        public override Unit VisitIntLiteral(IntLiteral literal)
        {
            if (code.PushOnStack)
                //cargando el entero para la pila
                code.Method.GetILGenerator().Emit(OpCodes.Ldc_I4, literal.Value);
            return Unit.Create();
        }

        public override Unit VisitStringLiteral(StringLiteral literal)
        {
            ILGenerator il = code.Method.GetILGenerator();
            il.Emit(OpCodes.Ldstr, literal.Value);
            if (!code.PushOnStack)
                il.Emit(OpCodes.Pop);

            return Unit.Create();
        }

        public override Unit VisitVar(VarAST ast)
        {
            ILGenerator il = code.Method.GetILGenerator();

            //buscar la variable
            VarInfo varInfo = ast.CurrentScope.GetVarInfo(ast.VarId);
            string currentFunctionCodeName = ast.CurrentScope.CurrentFunction.CodeName;
            if (ast.IsForeignVar) //se entra aca cuando la variable pertenece a otra funcion que no es la actual .
            {
                //carga el respectivo campo de la clase contenedora.
                //como mis metodos son de instancia y las varibles tambien tengo que cargar el parametro 0
                il.Emit(OpCodes.Ldarg_0);
                //ahora viene cargar la variable de verdad. 
                TypeCodeInfo typeCodeInfo =
                    code.GetWrapperAsociatteTo(ast.CurrentScope.CurrentFunction.FunctionParent.CodeName);
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
            return Unit.Create();
        }

        public override Unit VisitIfExpression(IfExpressionAST ast)
        {
            //---> quedandome con el valor .
            bool pushOnStack = code.PushOnStack;

            ILGenerator il = code.Method.GetILGenerator();

            //generar el codigo de la condicional
            code.PushOnStack = true;
            ast.ExpConditional.Accept(this);

            //definiendo donde esta la marca del else
            Label parteElse = il.DefineLabel();
            //definiendo el final del if
            Label endIf = il.DefineLabel();

            //si la comparacion fue false salta a la parteElse
            il.Emit(OpCodes.Brfalse, parteElse);

            //sino ejecuta la parte del then
            code.PushOnStack = true;
            ast.ExpressionThen.Accept(this);
            //saltar al final del if
            il.Emit(OpCodes.Br, endIf);
            //marca la donde empieza el else
            il.MarkLabel(parteElse);
            //generar el else si lo tengo
            if (ast.ExpressionElse != null)
            {
                code.PushOnStack = true;
                ast.ExpressionElse.Accept(this);
            }
            //marcando el final de if
            il.MarkLabel(endIf);

            //quitar lo que hay en la pila "si hay algo" y no debo ponerlo
            if (!(ast.ReturnType is NoType) && !pushOnStack)
                il.Emit(OpCodes.Pop);
            //<--- poniendo el valor
            code.PushOnStack = pushOnStack;

            return Unit.Create();
        }

        public override Unit VisitBinaryExpression(BinaryExpressionAst expr)
        {
            var binaryOperationGen = new BinaryExpressionILGenerator(expr,this);
            binaryOperationGen.GenerateCode(code);

            return Unit.Create();
        }

        public override Unit VisitLetExpression(LetExpressionAST expr)
        {
            //declarar lo que se declarable
            foreach (var item in expr.DeclarationList)
                item.Accept(this);

            //crear la instancia del tipo wrapper que esta como variable local en la funcion
            CreateInstanceOfWrapper(expr);
            //generar el codigo para el cuerpo ejecutable del let.
            expr.SequenceExpressionList.Accept(this);

            return Unit.Create();
        }

        public override Unit VisitVarDeclaration(VarDeclarationAST expr)
        {
            VarInfo varInfo = expr.CurrentScope.GetVarInfo(expr.Id);
            //nombre de la variable generado
            string varCodeName = varInfo.CodeName;
            // necesito el tipo de la variable.
            string typeCodeName = varInfo.TypeInfo.CodeName;
            Type varType = code.DefinedType[typeCodeName];

            //aca hay que tener en cuenta que las variable que son usadas por otras funciones se declaran en la declaracion de
            //la funcion a la que pertenecen

            if (!varInfo.IsUsedForAnotherFunction)
            {
                if (!varInfo.IsParameterFunction)
                //no tengo en cuenta los parametro de funcion porque esos los declara la funcion
                {
                    //generar la variable como local a la funcion
                    ILGenerator il = code.Method.GetILGenerator();
                    LocalBuilder local = il.DeclareLocal(varType);
                    // local.SetLocalSymInfo(varCodeName);
                    //adicionarla a las varibles locales
                    code.DefinedLocal.Add(varCodeName, local);
                }
            }
            else
            {
                if (!varInfo.IsParameterFunction)
                {
                    string currentFunction = expr.CurrentScope.CurrentFunction.CodeName;
                    TypeCodeInfo wrapper = code.GetWrapperAsociatteTo(currentFunction);

                    FieldBuilder field = wrapper.Type.DefineField(varInfo.CodeName, varType, FieldAttributes.Public);
                    //añadir el field a la clase ILCode
                    code.DefinedField.Add(varInfo.CodeName, field);
                    //añadida al wrapper
                    wrapper.AddField(varCodeName, field);
                }
            }
            //generar la inicializacion de la variable
            code.OnBeginMethod += (theCode, e) => code_OnBeginMethod_var(expr, theCode, e);

            return Unit.Create();
        }

        public override Unit VisitAlias(AliasAST alias)
        {
            throw new System.NotImplementedException();
        }

        public override Unit VisitArrayAccess(ArrayAccessAST arrayAccess)
        {
            //--->
            bool pushOnStack = code.PushOnStack;

            ILGenerator il = code.Method.GetILGenerator();
            //cargar el array
            if (pushOnStack)
            {
                code.PushOnStack = true;
                arrayAccess.Array.Accept(this);
                //cargar el indexer
                code.PushOnStack = true;
                arrayAccess.Indexer.Accept(this);

                //aca tengo que pedir el tipo del array , y luego el type asociado a el.
                string typeCodeName = arrayAccess.CurrentScope.GetTypeInfo(arrayAccess.Array.ReturnType.TypeID).CodeName;
                Type t = code.DefinedType[typeCodeName];
                il.Emit(OpCodes.Ldelem, t.IsArray ? t.GetElementType() : t);
            }
            //<---
            code.PushOnStack = pushOnStack;

            return Unit.Create();
        }

        public override Unit VisitArrayDeclaration(ArrayDeclarationAST arrayDeclaration)
        {
            //quedandome con el TypeInfo de tipo base del array.
            TypeInfo t = arrayDeclaration.CurrentScope.GetTypeInfo(arrayDeclaration.BaseTypeID);

            //en este momento es posible que el tipo del array no halla sido creado en el codigo il.
            //este tipo puede ser un array ,un alias o un record ,incluso una clase si se quiere extender esto un poco mas

            //creando el tipo del array
            Type arrayType = CreateTypeNotFounded(arrayDeclaration,t.TypeId).MakeArrayType();

            //annadir el tipo si es necesario.
            string arrayCodeName = arrayDeclaration.CurrentScope.GetTypeInfo(arrayDeclaration.TypeId).CodeName;
            if (!code.DefinedType.ContainsKey(arrayCodeName))
                code.DefinedType.Add(arrayCodeName, arrayType);

            return Unit.Create();
        }

        public override Unit VisitArrayInstantiation(ArrayInstatiationAST arrayInstatiation)
        {
            //tengo que declarar un metodo que sea el que inicialize este array. es decir que le asigne el valor de sizeexp;
            TypeInfo tI = arrayInstatiation.CurrentScope.GetTypeInfo(arrayInstatiation.ArrayTypeIdentifier);
            Type array = code.DefinedType[tI.CodeName];


            ILGenerator il = code.Method.GetILGenerator();

            arrayInstatiation.SizeExp.Accept(this);
            il.Emit(OpCodes.Newarr, array.GetElementType());

            //guardar el array .
            LocalBuilder localArray = il.DeclareLocal(array);
            il.Emit(OpCodes.Stloc, localArray.LocalIndex);

            //inicializar correctamente el array 
            // for (i =0 to SizeExp ) array[i] = InitializationExp
            //TODO: It can be implemented with a generic method that we create (once) in the assembly: Init<T>(T [] elems, T value)
            CreateArrayInitializMethod(arrayInstatiation,localArray, array);

            il.Emit(OpCodes.Ldloc, localArray.LocalIndex);


            if (!code.PushOnStack)
                il.Emit(OpCodes.Pop);

            return Unit.Create();
        }

        public override Unit VisitAssignExpression(AssignExpressionAST assignExpression)
        {
            var assignmentGenerationHelper = GetAssignmentGenerator(assignExpression.LeftExpression);
            //generar la asignacion.
            assignmentGenerationHelper.GenerateCode(code, assignExpression.RightExpression);
            return Unit.Create();
        }


        public override Unit VisitForExpression(ForExpressionAST forExpression)
        {
            //--->
            bool pushOnStack = code.PushOnStack;

            ILGenerator il = code.Method.GetILGenerator();


            //crear la variable del for.
            LocalBuilder varFor = il.DeclareLocal(typeof(int));
            string varCodeName = forExpression.CurrentScope.GetVarNameCode(forExpression.VarId);
            code.DefinedLocal.Add(varCodeName, varFor);

            code.PushOnStack = true;
            forExpression.ExpressionFrom.Accept(this);
            il.Emit(OpCodes.Stloc, varFor.LocalIndex);

            //declarar un variable que es el resultado del for
            LocalBuilder result = null;
            if (!(forExpression.BodyExpressions.ReturnType is NoType))
            {
                string typeCodeName = forExpression.CurrentScope.GetTypeInfo(forExpression.BodyExpressions.ReturnType.TypeID).CodeName;
                result = il.DeclareLocal(code.DefinedType[typeCodeName]);
            }

            //declaracion de las etiquetas de salto
            Label evaluarCond = il.DefineLabel();

            //--->
            Label loopAboveEnd = code.EndCurrentLoop;

            code.EndCurrentLoop = il.DefineLabel();

            //condicion
            il.MarkLabel(evaluarCond);
            //cargas la i
            code.PushOnStack = true;
            il.Emit(OpCodes.Ldloc, varFor.LocalIndex);
            //carga la expresion from
            code.PushOnStack = true;
            forExpression.ExpressionTo.Accept(this);
            // tengo que ver si i<=exp ,pero lo que hago es !(i>exp)
            il.Emit(OpCodes.Cgt);

            //salto al final si no se cumplio la condicion
            il.Emit(OpCodes.Brtrue, code.EndCurrentLoop);

            //body
            code.PushOnStack = pushOnStack;
            forExpression.BodyExpressions.Accept(this);

            if (!(forExpression.BodyExpressions.ReturnType is NoType) && pushOnStack)
            {
                il.Emit(OpCodes.Stloc, result.LocalIndex);
            }

            //incrementar el valor de la variable de iteracion
            il.Emit(OpCodes.Ldloc, varFor.LocalIndex);
            il.Emit(OpCodes.Ldc_I4, 1);
            il.Emit(OpCodes.Add);
            il.Emit(OpCodes.Stloc, varFor.LocalIndex);
            //salta a la condicion
            il.Emit(OpCodes.Br, evaluarCond);
            //lo que viene detras del ciclo.

            il.MarkLabel(code.EndCurrentLoop);
            //<--- reponiendo la marca del posible ciclo sobre mi.
            code.EndCurrentLoop = loopAboveEnd;

            if (!(forExpression.BodyExpressions.ReturnType is NoType) && pushOnStack)
            {
                il.Emit(OpCodes.Ldloc, result.LocalIndex);
            }

            //<---
            code.PushOnStack = pushOnStack;

            return Unit.Create();
        }

        public override Unit VisitFunctionDeclaration(FunctionDeclarationAST functionDeclaration)
        {
            var funDeclGeneratorHelper = new FunctionDeclarationILGeneratorHelper(functionDeclaration);
            //----->
            bool pushOnStack = code.PushOnStack;

            bool isFunction = !string.IsNullOrEmpty(functionDeclaration.ReturnTypeId);

            //ver si tengo que generar una clase que contenga toda la informacion para mis hijas
            MethodBuilder mBuilder;

            //construir la funcion en il con su clase wrapper a las funciones que ella usa
            mBuilder = funDeclGeneratorHelper.GenerateInstanceMethodWithClassWrapper(code);

            //-->
            MethodBuilder temp = code.Method;

            code.Method = mBuilder;

            //-------->
            FunctionInfo temp1 = functionDeclaration.CurrentScope.CurrentFunction;

            functionDeclaration.CurrentScope.CurrentFunction = functionDeclaration.CurrentScope.GetFunction(functionDeclaration.FunctionId);

            code.PushOnStack = isFunction;
            functionDeclaration.ExprInstructions.Accept(this);
            mBuilder.GetILGenerator().Emit(OpCodes.Ret);

            //<--------
            functionDeclaration.CurrentScope.CurrentFunction = temp1;
            //<--
            code.Method = temp;

            //<----
            code.PushOnStack = pushOnStack;


            //crear el tipo que acabo de crear como nested

            string currentWrapper = string.Format("Tg_{0}", funDeclGeneratorHelper.FunctionCodeName());
            var wrapper = (TypeBuilder)code.DefinedType[currentWrapper];
            wrapper.CreateType();


            return Unit.Create();
        }

        public override Unit VisitNegExpression(NegExpressionAST negExpression)
        {
            bool temp = code.PushOnStack;
            code.PushOnStack = true;
            ILGenerator il = code.Method.GetILGenerator();
            //cargando el entero para la pila
            il.Emit(OpCodes.Ldc_I4, -1);

            negExpression.Expression.Accept(this);

            il.Emit(OpCodes.Mul);

            code.PushOnStack = temp;

            if (!code.PushOnStack)
                il.Emit(OpCodes.Pop);

            return Unit.Create();
        }

        public override Unit VisitNilLiteral(NilLiteral nil)
        {
            ILGenerator il = code.Method.GetILGenerator();
            //cargando null para la pila
            il.Emit(OpCodes.Ldnull);
            if (!code.PushOnStack)
                il.Emit(OpCodes.Pop);

            return Unit.Create();
        }

        public override Unit VisitSequence(SequenceExpressionAST sequenceExpression)
        {
            bool pushStack = code.PushOnStack;

            //recorrer las expresiones y generar un codigo para cada una.
            for (int i = 0; i < sequenceExpression.ExpressionList.Count - 1; i++)
            {
                var item = sequenceExpression.ExpressionList[i];
                //si la expresion no es la ultima no debe poner valor en la pila.
                code.PushOnStack = false;
                item.Accept(this);
            }
            code.PushOnStack = pushStack;
            if (sequenceExpression.ExpressionList.Count > 0)
                sequenceExpression.ExpressionList.LastOrDefault()?.Accept(this);

            return Unit.Create();
        }

      

        public override Unit VisitWhileExpression(WhileExpressionAST whileExpression)
        {
            ILGenerator il = code.Method.GetILGenerator();

            //declaracion de las etiquetas de salto
            Label evaluarCond = il.DefineLabel();
            Label bodyInstr = il.DefineLabel();

            //--->
            Label loopAboveEnd = code.EndCurrentLoop;
            code.EndCurrentLoop = il.DefineLabel();
            //salto a la comparacion
            il.Emit(OpCodes.Br, evaluarCond);
            //body
            il.MarkLabel(bodyInstr);
            code.PushOnStack = false;
            whileExpression.BodyExpressions.Accept(this);
            //condicion
            il.MarkLabel(evaluarCond);
            code.PushOnStack = true;
            whileExpression.ExpressionConditional.Accept(this);
            il.Emit(OpCodes.Brtrue, bodyInstr);
            //lo que viene detras del while.
            il.MarkLabel(code.EndCurrentLoop);

            //<--- reponiendo la marca del posible ciclo sobre mi.
            code.EndCurrentLoop = loopAboveEnd;

            return Unit.Create();
        }

        public override Unit VisitBreakStatement(BreakAST breakStm)
        {
            ILGenerator il = code.Method.GetILGenerator();
            //saltando a donde me dice mi padre.
            il.Emit(OpCodes.Br, code.EndCurrentLoop);

            return Unit.Create();
        }

        public override Unit VisitFunctionInvocation(CallFunctionAST functionInvocation)
        {
            var functionGeneratorHelper = new ILCodeGeneratorFuntionInvocationHelper(functionInvocation,this);

            //<--- quedarme con el valor
            bool pushOnStack = code.PushOnStack;

            ILGenerator il = code.Method.GetILGenerator();
            FunctionInfo funInfo = functionInvocation.CurrentScope.GetFunction(functionInvocation.FunctionId);
            bool returnAValue = !(funInfo.FunctionReturnType is NoType);


            //para permitir sobrecargas
            MethodBuilder methodToCall = functionGeneratorHelper.GetMethodToCall(code);


            functionGeneratorHelper.CallToMethod(methodToCall, code);

            //si la funcion retorna una valor y no se espera que ponga este en la pila ,QUITALO
            if (returnAValue && !pushOnStack)
                il.Emit(OpCodes.Pop);

            //--->
            code.PushOnStack = pushOnStack;


            return Unit.Create();
        }

        public override Unit VisitRecordAccess(RecordAccessAST recordAccess)
        {
            ILGenerator il = code.Method.GetILGenerator();
            //--->
            bool pushOnStack = code.PushOnStack;

            //cargando el valor del campo del record
            code.PushOnStack = true;
            recordAccess.ExpressionRecord.Accept(this);

            string typeCodeName = recordAccess.CurrentScope.GetTypeInfo(recordAccess.ExpressionRecord.ReturnType.TypeID).CodeName;
            Type recordType = code.DefinedType[typeCodeName];
            il.Emit(OpCodes.Ldfld, recordType.GetField(recordAccess.FieldId));

            //<---
            if (!pushOnStack)
                il.Emit(OpCodes.Pop);
            code.PushOnStack = pushOnStack;

            return Unit.Create();
        }

        public override Unit VisitRecordInstantiation(RecordInstantiationAST recordInstantiation)
        {
            //--->
            bool pushOnStack = code.PushOnStack;

            //crear un instancia de la clase que representa al record
            string recordCodeName = recordInstantiation.CurrentScope.GetTypeInfo(recordInstantiation.Id).CodeName;
            Type type = code.DefinedType[recordCodeName];

            ILGenerator il = code.Method.GetILGenerator();
            //crear la instancia del objeto
            il.Emit(OpCodes.Newobj, type.GetConstructor(Type.EmptyTypes));

            //Guardar localmente una referencia al record un LocalBuilder dentro del metodo
            LocalBuilder local = il.DeclareLocal(type);
            //guardar la instancia en la variable local.
            il.Emit(OpCodes.Stloc, local.LocalIndex);

            var rt = (RecordType)recordInstantiation.CurrentScope.GetType(recordInstantiation.Id);
            foreach (var item in recordInstantiation.ExpressionValue)
            {
                //cargar la instancia de la clase.
                il.Emit(OpCodes.Ldloc, local.LocalIndex);
                pushOnStack = true;
                //generar el codigo para inicializar el campo
                item.Value.Accept(this);
                //asignarle  el valor al field

                il.Emit(OpCodes.Stfld, type.GetField(item.Key));
            }
            //dejar la instancia del tipo en la pila
            il.Emit(OpCodes.Ldloc, local.LocalIndex);

            if (!pushOnStack)
                il.Emit(OpCodes.Pop);
            //<---
            code.PushOnStack = pushOnStack;

            return Unit.Create();
        }

        public override Unit VisitRecordDeclaration(RecordDeclarationAST recordDeclaration)
        {
            ModuleBuilder mod = code.Module;
            string typeCodeName = recordDeclaration.CurrentScope.GetTypeInfo(recordDeclaration.TypeId).CodeName;
            //definiendo el tipo en IL
            //esto es para pedir el tipo ,si  ya esta me lo devuelve sino me lo crea.
            var type = (TypeBuilder)code.GetTypeBuilderMaybeNotCreated(typeCodeName);

            FieldBuilder field;
            Type itemType;
            foreach (var item in recordDeclaration.Fields)
            {
                //aca es para coger el type del campo              
                // es posible que el tipo de este campo no exista todavia por tanto esta es la funcion a usar.
                itemType = CreateTypeNotFounded(recordDeclaration,item.Value);
                //aca es para crear el campo.
                type.DefineField(item.Key, itemType, FieldAttributes.Public | FieldAttributes.HasDefault);
                //guardar estos campos .
            }
            //crear el tipo
            type.CreateType();
            return Unit.Create();
        }

        public override Unit VisitArgumentList(ArgumentList argumentList)
        {
            //it not necessary to implement it
            throw new NotImplementedException();
        }

        #region Helper methods

        private void code_OnBeginMethod_var(VarDeclarationAST varDecl, ILCode theCode, BeginMethodEventArgs e)
        {
            VarInfo varInfo = varDecl.CurrentScope.GetVarInfo(varDecl.Id);

            var isMyFunction = varDecl.CurrentScope.GetFunction(varInfo.FunctionNameParent).CodeName == e.FunctionCodeName;
            //it is true if the funcion (e.FunctionCodeName) is the same that the function that declares this variable.
            if (isMyFunction)
            {
                ILGenerator il = code.Method.GetILGenerator();
                //--->
                bool pushOnStack = code.PushOnStack;
                code.PushOnStack = true;

                string varCodeName = varInfo.CodeName;
                if (!varInfo.IsUsedForAnotherFunction)
                {
                    if (!varInfo.IsParameterFunction) //significa que es una variable local
                    {
                        varDecl.ExpressionValue.Accept(this);
                        il.Emit(OpCodes.Stloc, code.DefinedLocal[varCodeName].LocalIndex);
                    }
                }
                else
                {
                    if (!varInfo.IsParameterFunction) //significa que es un campo de la clase 
                    {
                        //cargar la instancia de la clase contenedora que tengo como variable local
                        il.Emit(OpCodes.Ldloc_0);
                        varDecl.ExpressionValue.Accept(this);
                        il.Emit(OpCodes.Stfld, code.DefinedField[varCodeName]);
                    }
                }
                //<---
                code.PushOnStack = pushOnStack;
            }
        }


        private ILAssignmentGenerator GetAssignmentGenerator(LHSExpressionAST assignExpression)
        {
            if (assignExpression is ArrayAccessAST)
                return new ILGeneratorArrayAssignment(this, (ArrayAccessAST) assignExpression);
            if(assignExpression is RecordAccessAST)
                return new ILGeneratorRecordFieldAssignment(this, (RecordAccessAST)assignExpression);
            if (assignExpression is VarAST)
                return new ILGeneratorVarAssignment(this, (VarAST)assignExpression);
            throw new NotImplementedException();
        }

        /// <summary>
        /// Este metodo es usado para crear un tipo al cual se hace referencia y es posible que no haya sido previamente creado en el codigo IL
        /// </summary>
        /// <param name="typeDeclaration"></param>
        /// <param name="typeId"></param>
        /// <returns></returns>
        private Type CreateTypeNotFounded(TypeDeclarationAST typeDeclaration, string typeId)
        {
            TypeInfo type = typeDeclaration.CurrentScope.GetTypeInfo(typeId);
            if (code.DefinedType.ContainsKey(type.CodeName))
                return code.DefinedType[type.CodeName];
            TigerType t = type.Type;
            if (t is ArrayType)
            {
                Type baseType = CreateTypeNotFounded(typeDeclaration,((ArrayType)t).BaseType.TypeID);
                Type arrayType = baseType.MakeArrayType();
                code.DefinedType.Add(type.CodeName, arrayType);
                return arrayType;
            }
            if (t is RecordType)
            {
                Type temp = code.Module.DefineType(type.CodeName);
                code.DefinedType.Add(type.CodeName, temp);
                return temp;
            }
            throw new NotImplementedException("Los restantes tipos no estan soportados en tiger");
        }


        private void CreateArrayInitializMethod(ArrayInstatiationAST arrayInstantiation, LocalBuilder localArray, Type arrayType)
        {
            ILGenerator il = code.Method.GetILGenerator();
            //--->
            bool pushOnStack = code.PushOnStack;

            //crear la variable del for.
            LocalBuilder varFor = il.DeclareLocal(typeof(int));

            //generar codigo para cargar el cero.
            il.Emit(OpCodes.Ldc_I4_0);
            // i = 0
            il.Emit(OpCodes.Stloc, varFor.LocalIndex);


            //declaracion de las etiquetas de salto
            Label evaluarCond = il.DefineLabel();
            Label endLoop = il.DefineLabel();

            //condicion
            il.MarkLabel(evaluarCond);
            //cargas la i
            code.PushOnStack = true;
            il.Emit(OpCodes.Ldloc, varFor.LocalIndex);
            //carga la expresion from
            code.PushOnStack = true;
            arrayInstantiation.SizeExp.Accept(this);
            // tengo que ver si i<exp 
            il.Emit(OpCodes.Clt);

            //salto al final si no se cumplio la condicion
            il.Emit(OpCodes.Brfalse, endLoop);

            // a[i] = InitializationExp
            code.PushOnStack = pushOnStack;
            //cargar el array
            il.Emit(OpCodes.Ldloc, localArray.LocalIndex);
            //cargar el indexer
            il.Emit(OpCodes.Ldloc, varFor.LocalIndex);
            //cargar la exp
            arrayInstantiation.InitializationExp.Accept(this);
            //hacer la asignacion
            il.Emit(OpCodes.Stelem, arrayType.IsArray ? arrayType.GetElementType() : arrayType);

            //incrementar el valor de la variable de iteracion
            il.Emit(OpCodes.Ldloc, varFor.LocalIndex);
            il.Emit(OpCodes.Ldc_I4, 1);
            il.Emit(OpCodes.Add);
            il.Emit(OpCodes.Stloc, varFor.LocalIndex);
            //salta a la condicion
            il.Emit(OpCodes.Br, evaluarCond);
            //lo que viene detras del ciclo.

            il.MarkLabel(endLoop);
            //<---
            code.PushOnStack = pushOnStack;
        }

        private void CreateInstanceOfWrapper(LetExpressionAST let)
        {
            string currentFunctionCodeName = let.CurrentScope.CurrentFunction.CodeName;

            TypeCodeInfo typeCodeInfo = code.GetWrapperAsociatteTo(currentFunctionCodeName);

            ILGenerator il = code.Method.GetILGenerator();
            //crear la instancia del objeto que contiene las variable mias que son usadas por otras funciones
            il.Emit(OpCodes.Newobj, typeCodeInfo.DefaultConstructor());
            il.Emit(OpCodes.Stloc_0);

            //asignarle la instancia de mi clase a esta objeto para que tenga la referencia a su padre.
            //locaL_0.parent =  this.

            if (let.CurrentScope.CurrentFunction.FunctionName != "main$")
            {
                FieldBuilder parent = typeCodeInfo.GetField("parent");
                il.Emit(OpCodes.Ldloc_0);
                //cargar el  this
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Stfld, parent);

                //darle los valores  de las variables a los campos de esta clase.
                InitilizateFieldFromParameters(let);
            }
            //obligo a que se lanze el evento que estan esperando los que inicializa las varialbles
            code.ThrowEventForFunction(currentFunctionCodeName);
        }

        /// <summary>
        /// Este metodo asigna los valores de los parametros que son usados por otra funciones a los campos de la clase que 
        /// tengo como variable local
        /// </summary>
        /// <param name="let"></param>
        private void InitilizateFieldFromParameters(LetExpressionAST let)
        {
            ILGenerator il = code.Method.GetILGenerator();
            FunctionInfo funInfo = let.CurrentScope.GetFunction(let.CurrentScope.CurrentFunction.FunctionName);
            for (int i = 0; i < funInfo.ParameterList.Count; i++)
            {
                VarInfo varInfo = let.CurrentScope.GetVarInfo(funInfo.ParameterList[0].Key);
                if (funInfo.VarsUsedForAnotherFunction.Contains(varInfo))
                {
                    //se asume que el wrapper siempre esta como primera variable del metodo.
                    il.Emit(OpCodes.Ldloc_0);
                    il.Emit(OpCodes.Ldarg, i + 1);
                    il.Emit(OpCodes.Stfld, code.DefinedField[varInfo.CodeName]);
                }
            }
        }

     

        #endregion
    }


    public class Unit
    {
        private static Unit _ref;
        private Unit()
        {
        }

        public static Unit Create()
        {
            return _ref ?? (_ref = new Unit());
        }
    }
}