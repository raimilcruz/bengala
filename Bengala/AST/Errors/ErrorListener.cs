using System.Collections.Generic;

namespace Bengala.AST.Errors
{
    public interface IErrorListener {
        void Add(AnalysisError msg);
        void Insert(int pos, AnalysisError msg);
        int Count { get;}
    }

    public class BengalaBaseErrorListener : IErrorListener
    {
        readonly List<AnalysisError> _errors = new List<AnalysisError>();

        public IEnumerable<AnalysisError> Errors => _errors;

        public void Insert(int pos, AnalysisError msg)
        {
            _errors.Insert(pos, msg);
        }

        public int Count => _errors.Count;

        public void Add(AnalysisError msg)
        {
            _errors.Add(msg);
        }
    }
}