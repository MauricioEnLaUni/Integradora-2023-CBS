using MongoDB.Bson;

namespace Fictichos.Constructora.Repository
{
    public interface IRepositoryService<T, U, V>
    where T : AbstractEntity<T, U, V>, new()
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(ObjectId Id);
        Task<T> CreateAsync(string newItem);
        Task UpdateAsync(T entity);
        Task DeleteAsync(ObjectId Id);
    }
}