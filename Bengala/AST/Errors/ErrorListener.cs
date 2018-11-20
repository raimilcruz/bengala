namespace Bengala.AST.Errors
{
    public abstract class ErrorListener {
        public abstract void Add(ErrorMessage msg);
        public abstract void Add(WarningMessage msg);

        public abstract void Insert(int pos, ErrorMessage msg);

        public abstract int Count { get;}

    }
}