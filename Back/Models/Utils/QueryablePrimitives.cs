namespace Fictichos.Constructora.Utils.Generics
{
    public class QueryableInt : IPrimitives
    {
        public readonly int _data;
        public QueryableInt(int data) { _data = data; }
    }

    public class QueryableString : IPrimitives
    {
        public readonly string _data;
        public QueryableString(string data) { _data = data; }
    }

    public class QueryableBool : IPrimitives
    {
        public readonly bool _data;
        public QueryableBool(bool data) { _data = data; }
    }
}