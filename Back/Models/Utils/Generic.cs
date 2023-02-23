namespace Fictichos.Constructora.Utils.Generics
{
    public class TQueryParameter<T> where T : struct
    {
        public readonly T _data;
    }

    public interface ILinqSearchable { }
    public interface IPrimitives { }
}