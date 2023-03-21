using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Utilities
{
    public interface IQueryMask<T, U, V, W>
    where T : BaseEntity, IQueryMask<T, U, V, W>, new()
    where V : DtoBase
    {
        public T Instantiate(U dto);
        public string Serialize();
        public W ToDto();
        public void Update(V data);
    }
}