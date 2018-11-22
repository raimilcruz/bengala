using System.Collections.Generic;
using System.Linq;

namespace Bengala.AST.Errors
{
    public interface IErrorListener {
        void Add(ErrorMessage msg);
        void Add(WarningMessage msg);

        void Insert(int pos, ErrorMessage msg);
        int Count { get;}
    }
    public class BengalaBaseErrorListener : IErrorListener
    {
        readonly List<Message> _errors = new List<Message>();

        public IEnumerable<ErrorMessage> Errors
        {
            get
            {
                return _errors.Where(x => x is ErrorMessage).Cast<ErrorMessage>();
            }
        }

        public IEnumerable<WarningMessage> Warnings
        {
            get
            {
                return _errors.Where(x => x is WarningMessage).Cast<WarningMessage>();
            }

        }

        public void Add(WarningMessage msg)
        {
            _errors.Add(msg);
        }

        public void Insert(int pos, ErrorMessage msg)
        {
            _errors.Insert(pos, msg);
        }

        public int Count => _errors.Count;

        public void Add(ErrorMessage msg)
        {
            _errors.Add(msg);
        }
    }
}