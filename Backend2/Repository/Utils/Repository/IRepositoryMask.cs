using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Utilities
{
    public interface IRepositoryMask<T, U, V, W>
    where T : BaseEntity, IRepositoryMask<T, U, V, W>
    where W : DtoBase
    {
        public abstract T Instantiate(string dto);
        public abstract T Instantiate(V dto);
        public string SerializeDto();
        public U ToDto();
        public void Update(W data);
    }
}