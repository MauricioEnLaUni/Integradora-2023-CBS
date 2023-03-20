using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Utilities
{
    public interface IRepositoryMask<T, U, V>
    where T : BaseEntity, IRepositoryMask<T, U, V>
    {
        public abstract T Instantiate(string dto);
        public abstract T Instantiate(V dto);
        public string SerializeDto();
        public U ToDto();
        public void Update(IUpdateDto<T> data);
    }
}