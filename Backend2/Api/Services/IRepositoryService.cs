namespace Fictichos.Constructora.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRepositoryService<T, U>
    where T : BaseEntity, new()
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(string Id);
        Task<T> CreateAsync(U newItem);
        Task UpdateAsync(T entity);
        Task DeleteAsync(string Id);
    }
}