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
        List<Message> _errors = new List<Message>();

        public IEnumerable<ErrorMessage> Errors
        {
            get
            {
                return _errors.Where(x=>x is ErrorMessage).Cast<ErrorMessage>();
            }            
        }

        public IEnumerable<WarningMessage> Warnings
        {
            get
            {
                return _errors.Where(x=> x is WarningMessage).Cast<WarningMessage>();
            }
           
        }

        public override void Add(WarningMessage msg)
        {
            _errors.Add(msg);
        }

        public override void Insert(int pos, ErrorMessage msg)
        {
            _errors.Insert(pos,msg);
        }

        public override int Count => _errors.Count;

        public override void Add(ErrorMessage msg)
        {
            _errors.Add(msg);
        }
    }
}
