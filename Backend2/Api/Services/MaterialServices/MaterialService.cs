using MongoDB.Driver;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Repository;

public class MaterialService
{
    private readonly IMongoCollection<Material> _materialCollection;

    public MaterialService(MongoSettings container)
    {
        _materialCollection = container.Client.GetDatabase("cbs")
            .GetCollection<Material>("material");
    }

    #region NewMaterial

    public NewMaterialDto? ValidateNew(NewMaterialDto data)
    {
        NewMaterialDto output = data;
        output.Quantity = data.Quantity < 0 ? 0 : data.Quantity;
        if (data.Status < 0 || data.Status > 4) output.Status = 0;
        if (data.Purpose < 0 || data.Purpose > 5) output.Purpose = 0;

        return output;
    }
    #endregion

    #region UpdatedMaterial
    public UpdatedMaterialDto? ValidateUpdate(UpdatedMaterialDto data)
    {
        UpdatedMaterialDto result = data;
        if (data.Quantity is not null && data.Quantity < 0)
            return null;
        if (data.Status is not null && (data.Status < 0 || data.Status > 4))
            return null;
        if (data.Purpose is not null && (data.Purpose < 0 || data.Purpose > 5))
            return null;
        if (data.Depreciation > 1 || data.Depreciation < 0) return null;

        return result;
    }

    #endregion

    #region Getters
    public List<Material> GetByFilter(FilterDefinition<Material> filter)
    {
        return _materialCollection.Find(filter).ToList();
    }

    public async Task<List<Material>> GetByFilterAsync(FilterDefinition<Material> filter)
    {
        return await _materialCollection.Find(filter).ToListAsync();
    }

    public async Task<Material?> GetOneByFilterAsync(FilterDefinition<Material> filter)
    {
        return await _materialCollection.Find(filter).SingleOrDefaultAsync();
    }

    public Material? GetOneByFilter(FilterDefinition<Material> filter)
    {
        return _materialCollection.Find(filter).SingleOrDefault();
    }
    #endregion
}