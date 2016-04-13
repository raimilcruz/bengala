#region Usings

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Bengala.AST.CodeGenerationUtils;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Representa la declaracion de un record: type REC = {uncampo:int, otrocampo:REC}
    /// </summary>
    public class RecordDeclarationAST : TypeDeclarationAST
    {
        #region Fields and Properties

        /// <summary>
        /// Devuelve una lista con los campos que contiene el record
        /// </summary>
        public List<KeyValuePair<string, string>> Fields { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Inicializa una nueva instancia de record declaration
        /// </summary>
        /// <param name="typeID">Nombre del record que se declara</param>
        /// <param name="fields">lista de campos de la forma [field:type]</param>
        /// <param name="line">Linea correspondiente en el codigo</param>
        /// <param name="col">Columna correspondiente en el codigo</param>
        public RecordDeclarationAST(string typeID, List<KeyValuePair<string, string>> fields, int line, int col)
            : base(typeID, line, col)
        {
            Fields = fields ?? new List<KeyValuePair<string, string>>();
        }

        #endregion

        #region Instance Methods

        public override bool CheckSemantic(Scope scope, List<Message> listError)
        {
            CurrentScope = scope;

            //se asuma que no habra problemas
            ReturnType = TigerType.GetType<NoType>();
            //la clase base verifica el ID del type
            if (base.CheckSemantic(scope, listError))
            {
                var rt = new RecordType(TypeId);
                //se anade el record creado al scope para q puedan haber records recursivos en su def
                scope.AddType(TypeId, rt);
                //se verifica cada una de las declaraciones de los campos del record
                int savedErrorPos = listError.Count;
                foreach (var kvp in Fields)
                {
                    if (!rt.Contains(kvp.Key))
                    {
                        TigerType tt;
                        if (scope.HasType(kvp.Value, out tt) == ScopeLocation.NotDeclared)
                        {
                            KeyValuePair<string, string> savedKvp = kvp;
                            scope.TypeAdded += (sender, args) =>
                                                   {
                                                       if (args.TypeName == savedKvp.Value)
                                                           rt.AddField(savedKvp.Key, args.NewType);
                                                   };
                            scope.FinalizeScope += (sender, args) =>
                                                       {
                                                           if (sender.HasType(savedKvp.Value) ==
                                                               ScopeLocation.NotDeclared)
                                                           {
                                                               listError.Insert(savedErrorPos,
                                                                                new ErrorMessage(
                                                                                    string.Format(
                                                                                        Message.LoadMessage("TypeUndecl"),
                                                                                        savedKvp.Value), Line, Columns));
                                                               ReturnType = TigerType.GetType<ErrorType>();
                                                           }
                                                       };
                        }
                        else
                        {
                            rt.AddField(kvp.Key, tt);
                        }
                    }
                    else
                    {
                        listError.Add(new ErrorMessage(string.Format(Message.LoadMessage("RecDecl"), kvp.Key, TypeId),
                                                       Line, Columns));
                    }
                }
                //TODO aqui se ve el prob con los ret Types y los return true pq no se puede decir nada en este momento
                return true;
            }
            return false;
        }

        #region Generacion de codigo

        public override void GenerateCode(ILCode code)
        {
            ModuleBuilder mod = code.Module;
            string typeCodeName = CurrentScope.GetTypeInfo(TypeId).CodeName;
            //definiendo el tipo en IL
            //esto es para pedir el tipo ,si  ya esta me lo devuelve sino me lo crea.
            var type = (TypeBuilder) code.GetTypeBuilderMaybeNotCreated(typeCodeName);

            FieldBuilder field;
            Type itemType;
            foreach (var item in Fields)
            {
                //aca es para coger el type del campo              
                // es posible que el tipo de este campo no exista todavia por tanto esta es la funcion a usar.
                itemType = CreateTypeNotFounded(item.Value, code);
                //aca es para crear el campo.
                type.DefineField(item.Key, itemType, FieldAttributes.Public | FieldAttributes.HasDefault);
                //guardar estos campos .
            }
            //crear el tipo
            type.CreateType();
        }

        #endregion

        #endregion
    }
}