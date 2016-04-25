using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bengala;
using Bengala.AST;
using Bengala.AST.SemanticsUtils;

namespace Bengala.Tests
{
    class PrinterErrorListener : ErrorListener
    {
        List<ErrorMessage> _errors = new List<ErrorMessage>();
        List<WarningMessage> _warnings = new List<WarningMessage>();

        public List<ErrorMessage> Errors
        {
            get
            {
                return _errors;
            }            
        }

        public List<WarningMessage> Warnings
        {
            get
            {
                return _warnings;
            }
           
        }

        public override void Add(WarningMessage msg)
        {
            Warnings.Add(msg);
        }

        public override void Add(ErrorMessage msg)
        {
            Errors.Add(msg);
        }
    }
}
