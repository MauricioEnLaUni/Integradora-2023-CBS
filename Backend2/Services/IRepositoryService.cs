using MongoDB.Bson;

namespace Fictichos.Constructora.Repository
{
    public interface IRepositoryService<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(ObjectId Id);
        Task<T> CreateAsync(string newItem);
        Task UpdateAsync(T entity);
        Task DeleteAsync(ObjectId Id);
    }
}