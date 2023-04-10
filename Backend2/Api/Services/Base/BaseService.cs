using MongoDB.Driver;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Utilities.MongoDB;

namespace Fictichos.Constructora.Repository;

public class BaseService<TModel, TNewDto, TUpdateDto>
    where TModel : BaseEntity, IQueryMask<TModel, TNewDto, TUpdateDto>, new()
    where TUpdateDto : DtoBase
{
    protected readonly IMongoCollection<TModel> _mainCollection;

    public BaseService(
        MongoSettings container, string mainCollectionName)
    {
        _mainCollection = container.Client.GetDatabase("cbs")
            .GetCollection<TModel>(mainCollectionName);
    }

    internal TModel InsertOne(TNewDto data)
    {
        TModel result = new TModel().Instantiate(data);
        _mainCollection.InsertOne(result);
        return result;
    }

    internal async Task<TModel> InsertOneAsync(TNewDto data)
    {
        TModel result = new TModel().Instantiate(data);
        await _mainCollection.InsertOneAsync(result);
        return result;
    }

    internal TModel? GetOneBy(FilterDefinition<TModel> filter)
    {
        return _mainCollection
            .Find(filter)
            .SingleOrDefault();
    }

    internal async Task<TModel?> GetOneByAsync(
        FilterDefinition<TModel> filter)
    {
        return await _mainCollection
            .Find(filter)
            .SingleOrDefaultAsync();
    }

    internal List<TModel> GetBy(FilterDefinition<TModel> filter)
    {
        return _mainCollection
            .Find(filter)
            .ToList();
    }

    internal async Task<List<TModel>> GetByAsync(
        FilterDefinition<TModel> filter)
    {
        return await _mainCollection
            .Find(filter)
            .ToListAsync();
    }

    internal void DeleteOne(FilterDefinition<TModel> filter)
    {
        _mainCollection.DeleteOne(filter);
    }

    internal async Task DeleteOneAsync(
        FilterDefinition<TModel> filter)
    {
        await _mainCollection.DeleteOneAsync(filter);
    }

    internal void DeleteMany(FilterDefinition<TModel> filter)
    {
        _mainCollection.DeleteMany(filter);
    }

    internal async Task DeleteManyAsync(FilterDefinition<TModel> filter)
    {
        await _mainCollection.DeleteManyAsync(filter);
    }

    internal void Clear()
    {
        _mainCollection.DeleteMany(Filter.Empty<TModel>());
    }

    internal void ReplaceOne(FilterDefinition<TModel> filter, TModel data)
    {
        _mainCollection.ReplaceOne(filter, data);
    }

    internal void Update(UpdateDto<TModel> data)
    {
        _mainCollection.UpdateOne(data.filter, data.update);
    }
}