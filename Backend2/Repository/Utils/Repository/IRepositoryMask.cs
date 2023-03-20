using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Utilities
{
    public interface IRepositoryMask<T, U, V>
    where T : BaseEntity, IRepositoryMask<T, U, V>
    where V : DtoBase
    {
        public abstract T Instantiate(U dto);
        public string SerializeDto();
        public void Update(V data);
    }
}