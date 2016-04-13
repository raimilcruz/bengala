#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args != null && args.Length != 0)
            {
                if (args.Count() == 1)
                {
                    Compile(args[0]);
                }
                else
                    Console.WriteLine("Escriba la opcion /? para obtener el manual de ayuda");
            }
            else
            {
                Console.WriteLine("Escriba el nombre del fichero ");
                string fileName = Console.ReadLine();

                Compile(fileName);
            }
        }

        private static void Compile(string fileName)
        {
            Console.WriteLine("Compiling files ....");

            var bengalaCompiler = new BengalaCompiler();
            List<Message> erroresWarning = bengalaCompiler.Compile(fileName);
            IEnumerable<Message> errores = from e in erroresWarning
                                           where e is ErrorMessage
                                           select e;
            IEnumerable<Message> warning = from w in erroresWarning
                                           where w is WarningMessage
                                           select w;
            if (erroresWarning != null)
            {
                if (erroresWarning.Count == 0)
                    Console.WriteLine("Build Success");
                else
                    Console.WriteLine("{0} errors  {1} warning", errores.Count(), warning.Count());
                foreach (var item in erroresWarning)
                    Console.Error.WriteLine("{0} : line ({1},{2}) {3}",
                                            (item is ErrorMessage) ? "Error" : "Warning", item.Line, item.Column,
                                            item.InnerMessage);
            }
        }
    }
}