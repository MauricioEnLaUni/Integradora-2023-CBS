namespace Fitichos.Constructora.Repository
{
    public interface IRepository<T>
    where T : Entity
    {
        List<T> GetAll();
        T? GetById(string Id);
        Task CreateAsync(T newItem);
        Task UpdateAsync(T entity);
        Task DeleteAsync(string Id);
    }
}