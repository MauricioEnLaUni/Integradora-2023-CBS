using MongoDB.Driver;

namespace Fictichos.Constructora.Repository;

public static class RepositoryExtensions
{
    public async static Task<T> CreateAsync<T, U, V>(
        this IMongoCollection<T> collection,
        U data
    )
    where T : BaseEntity, Utilities.IQueryMask<T, U, V>, new()
    {
        T newItem = new T().Instantiate(data);
        await collection.InsertOneAsync(newItem);
        return newItem;
    }

    public async static Task DeleteAsync<T>(
        this IMongoCollection<T> collection,
        string id)
    where T : BaseEntity
    {
        var filter = Builders<T>.Filter.Eq(e => e.Id, id);
            await collection.DeleteOneAsync(filter);
    }

    public static async Task<List<T>> GeyByFilterAsync<T>(
        this IMongoCollection<T> collection,
        FilterDefinition<T> filter)
    {
        return await collection.Find(filter).ToListAsync();
    }

    public static async Task<T?> GetOneByFilterAsync<T>(
        this IMongoCollection<T> collection,
        FilterDefinition<T> filter)
    {
        return await collection.Find(filter).SingleOrDefaultAsync();
    }
}