using MongoDB.Driver;

using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Dto;

namespace Fictichos.Constructora.Repository;

public class MaterialCategoryService
{
    private readonly IMongoCollection<MaterialCategory> _categoryCollection;
    
    public MaterialCategoryService(MongoSettings container)
    {
        _categoryCollection = container.Client.GetDatabase("cbs")
            .GetCollection<MaterialCategory>("categories");
    }

    public HTTPResult<string> ValidateDelete(string data)
    {
        FilterDefinition<MaterialCategory> filter = Builders<MaterialCategory>
            .Filter
            .Eq(x => x.Id, data);
        MaterialCategory? candidate = _categoryCollection
            .Find(filter)
            .SingleOrDefault();
        if (candidate is null) return new() { Code = 404 };


        if (candidate.Children.Count > 0
            || candidate.SubCategory.Count > 0)
                return new() { Code = 409 };
        
        HTTPResult<string> result = new()
            { Code = 204, Value = data };
        
        return result;
    }

    public HTTPResult<MaterialCategory> GetCategory(string data)
    {
        FilterDefinition<MaterialCategory> filter =
            Builders<MaterialCategory>.Filter.Eq(x => x.Id, data);
        MaterialCategory? category = _categoryCollection.Find(filter)
            .SingleOrDefault();
        if (category is null) return new() { Code = 404 };

        return new() { Code = 200, Value = category };
    }

    #region Validate Update
    private string? ParentRoot(string? data)
    {
        string? parent = null;
        if (data != "root") parent = data;

        return parent;
    }

    private HTTPResult<string>
        GetParent(string? oldParent, string? newParent)
    {
        HTTPResult<MaterialCategory> d;
        if (newParent is null && oldParent is null)
            return new() { Code = 200, Value = "root" };
        else if (oldParent is not null)
        {
            d = GetCategory(oldParent);
            if (d.Value is null) return new() { Code = 404 };
            return new() { Code = 200, Value = d.Value.Id };
        }
        else
        {
            d = GetCategory(newParent!);
            if (d.Value is null) return new() { Code = 404 };
            return new() { Code = 200, Value = d.Value.Id };
        }
    }

    private async Task<bool> NameIsUnique(string[] data)
    {
        FilterDefinition<MaterialCategory> filter = 
            Builders<MaterialCategory>.Filter
                .Eq(x => x.Name, data[0]) & Builders<MaterialCategory>.Filter
                .Eq(x => x.Parent, data[1]);
        List<MaterialCategory> raw = await _categoryCollection.Find(filter)
            .ToListAsync();
        if (raw.Count > 0) return false;

        return true;
    }

    public async Task<HTTPResult<UpdatedMatCategoryDto>>
        ValidateUpdate(UpdatedMatCategoryDto data)
    {
        if (data.Name is null && data.Parent is null
            && data.SubCategory is null && data.Children is null)
                return new() { Code = 400 };
        
        HTTPResult<MaterialCategory> oldDocument = GetCategory(data.Id);
        if (oldDocument.Code != 200) return new() { Code = oldDocument.Code };
        
        UpdatedMatCategoryDto sanitized = data;

        string? parent = GetParent(oldDocument.Value!.Parent, data.Parent)
            .Value;
        if (parent is null) return new() { Code = 400 };
        
        if (data.Name is not null)
        sanitized.Name = await NameIsUnique(
                new string[] { data.Name, ParentRoot(parent) }) ?
                    data.Name : null;
        
        
        
        return new() { Code = 200, Value = sanitized };
    }
    
    #endregion ValidateUpdate
}