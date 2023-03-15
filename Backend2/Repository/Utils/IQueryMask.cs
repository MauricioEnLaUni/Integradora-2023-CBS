using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Utilities
{
    public interface IQueryMask<T, U>
    where T : Entity, IQueryMask<T, U>, new()
    {
        public T FakeConstructor(string dto);
        public string SerializeDto();
        public U ToDto();
    }
}