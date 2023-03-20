using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Utilities
{
    public interface IQueryMask<T, U, V>
    where T : BaseEntity, IQueryMask<T, U, V>, new()
    where V : DtoBase
    {
        public T Instantiate(U dto);
        public string Serialize();
        public void Update(V data);
    }
}