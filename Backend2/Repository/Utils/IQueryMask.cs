namespace Fictichos.Constructora.Utilities
{
    public interface IQueryMask<T>
    {
        public T FakeConstructor(string dto);
        public string AsDto();
    }
}