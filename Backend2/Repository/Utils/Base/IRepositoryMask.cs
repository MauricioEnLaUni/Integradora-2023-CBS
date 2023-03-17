using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Utilities
{
    public interface IRepositoryMask<T, U, V, W>
    where T : BaseEntity, IRepositoryMask<T, U, V, W>
    where V : DtoBase
    {
        public T FakeConstructor(string dto);
        public string SerializeDto();
        public U ToDto();
        public void Update(V data);
    }
}