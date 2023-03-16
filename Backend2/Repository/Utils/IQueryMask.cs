using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Utilities
{
    public interface IQueryMask<T, U, V>
    where T : Entity, IQueryMask<T, U, V>, new()
    where V : DtoBase
    {
        public T FakeConstructor(string dto);
        public string SerializeDto();
        public U ToDto();
        public void Update(V data);
    }
}