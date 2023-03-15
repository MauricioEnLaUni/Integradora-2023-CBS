using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Utilities
{
    public interface IQueryMask<T>
    where T : Entity, IQueryMask<T>, new()
    {
        public T FakeConstructor(string dto);
        public string AsDto();
    }
}