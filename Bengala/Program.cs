#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Bengala.AST.Errors;
using Bengala.AST.SemanticsUtils;

#endregion

namespace Bengala
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //TODO: Use a package to provide a better parser for CLI options
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
            Console.WriteLine("Analyzing files ....");

            var bengalaCompiler = new BengalaCompiler();
            var erroresWarning = bengalaCompiler.Compile(fileName);

            //TODO: Implement proper error printing (taking into account the error severity)
            if (erroresWarning != null)
            {
                if (erroresWarning.Count == 0)
                    Console.WriteLine("Build Success");
                else
                    Console.WriteLine("{0} issues",erroresWarning.Count);
                foreach (var item in erroresWarning)
                    Console.Error.WriteLine("{0} : line ({1},{2}) {3}",
                                            (item is ErrorMessage) ? "Error" : "Warning", item.Line, item.Column,
                                            item.InnerMessage);
            }
        }
    }
}