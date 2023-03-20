using Fictichos.Constructora.Dto;

namespace Fictichos.Constructora.Repository
{
    public interface IRepositoryService<T, U, V, W>
    where T : AbstractEntity<T, U, V, W>, new()
    where W : DtoBase
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(string Id);
        Task<T> CreateAsync(string newItem);
        Task UpdateAsync(T entity);
        Task DeleteAsync(string Id);
    }
}