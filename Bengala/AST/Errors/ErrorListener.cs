using System.Collections.Generic;

namespace Bengala.AST.Errors
{
    public interface IErrorListener {
        void Add(ErrorMessage msg);
        void Insert(int pos, ErrorMessage msg);
        int Count { get;}
    }

    public class BengalaBaseErrorListener : IErrorListener
    {
        readonly List<ErrorMessage> _errors = new List<ErrorMessage>();

        public IEnumerable<ErrorMessage> Errors => _errors;

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