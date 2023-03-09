namespace Fictichos.Constructora.Repository
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T? GetById(string Id);
        Task CreateAsync(T newItem);
        Task UpdateAsync(T entity);
        Task DeleteAsync(string Id);
    }
}