#region Usings

using System.Collections.Generic;
using Bengala.AST.CodeGenerationUtils;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala.AST
{
    /// <summary>
    /// Representa la declaracion de un alias:
    /// type t = "TipoYaDefinido"
    /// </summary>
    public class AliasAST : TypeDeclarationAST
    {
        #region Field and Properties

        /// <summary>
        /// Devuelve el typeId del tipo al cual se le hizo el alias.
        /// </summary>
        public string AliasToWho { get; private set; }

        #endregion

        #region  Constructors

        /// <summary>
        /// </summary>
        /// <param name="typeId">nombre del tipo q se crea</param>
        /// <param name="aliasToWho">nombre del tipo al cual se hace el Alias</param>
        /// <param name="line">Linea correspondiente en el codigo</param>
        /// <param name="col">Columna correspondiente en el codigo</param>
        public AliasAST(string typeId, string aliasToWho, int line, int col)
            : base(typeId, line, col)
        {
            AliasToWho = aliasToWho;
        }

        #endregion

        #region InstanceMethods

        public override bool CheckSemantic(Scope scope, List<Message> listError)
        {
            //se asume que no habra problema
            ReturnType = TigerType.GetType<NoType>();
            //la clase base verifica q el id del type sea valido
            //aqui si se ve q el return true hace falta
            if (base.CheckSemantic(scope, listError))
            {
                TigerType tt;
                //se verifica que exista el tipo del cual se esta creando un alias
                if (scope.HasType(AliasToWho, out tt) != ScopeLocation.NotDeclared)
                {
                    //se anade una nueva entrada del mismo type, lo q con otro id
                    scope.AddAlias(TypeId, AliasToWho);
                    return true;
                }
                int savedErrorPos = listError.Count;
                //manejador de evento
                scope.TypeAdded += (sender, args) =>
                                       {
                                           if (args.TypeName == AliasToWho)
                                               scope.AddType(TypeId, args.NewType);
                                       };

                //manejador de evento
                scope.FinalizeScope += (sender, args) =>
                                           {
                                               if (sender.HasType(AliasToWho) == ScopeLocation.NotDeclared)
                                               {
                                                   listError.Insert(savedErrorPos,
                                                                    new ErrorMessage(
                                                                        string.Format(
                                                                            Message.LoadMessage("TypeUndecl"),
                                                                            AliasToWho), Line, Columns));
                                                   ReturnType = TigerType.GetType<ErrorType>();
                                               }
                                           };
                return true;
            }
            return false;
        }

        public override void GenerateCode(ILCode code)
        {
            //el alias no genera codigo
        }

        #endregion
    }
}