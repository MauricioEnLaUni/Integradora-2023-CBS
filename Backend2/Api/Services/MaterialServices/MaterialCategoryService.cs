using MongoDB.Driver;

using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Utilities.MongoDB;

namespace Fictichos.Constructora.Repository;

public class MaterialCategoryService
    : BaseService<MaterialCategory, NewMaterialCategoryDto, UpdatedMatCategoryDto>
{
    private const string MAINCOLLECTION = "categories";
    
    public MaterialCategoryService(MongoSettings container)
        : base(container, MAINCOLLECTION) { }

    public HTTPResult<string> ValidateDelete(string data)
    {
        FilterDefinition<MaterialCategory> filter = Builders<MaterialCategory>
            .Filter
            .Eq(x => x.Id, data);
        MaterialCategory? candidate = _mainCollection
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

    // Gets all members without a parent
    public List<MaterialCategory> GetRoots()
    {
        List<MaterialCategory> result = new();
        FilterDefinition<MaterialCategory> filter = Builders<MaterialCategory>
            .Filter
            .Where(x => x.Parent == null);
        result = GetBy(filter).ToList();
        return result;
    }

    public MaterialCategory? GetCategory(string data)
    {
        MaterialCategory? category =
            GetOneBy(Filter.ById<MaterialCategory>(data));
        if (category is null) return null;

        return category;
    }

    #region Validate Update
    private string? ParentRoot(string? data)
    {
        string? parent = null;
        if (data != "root") parent = data;

        return parent;
    }

    private string?
        GetParent(string? oldParent, string? newParent)
    {
        MaterialCategory? element;
        if (newParent is null && oldParent is null)
            return "root";
        else if (oldParent is not null)
        {
            element = GetCategory(oldParent);
            if (element is null) return null;
            return element.Id;
        }
        else
        {
            element = GetCategory(newParent!);
            if (element is null) return null;
            return element.Id;
        }
    }

    private async Task<bool> NameIsUnique(string?[] data)
    {
        FilterDefinition<MaterialCategory> filter = 
            Builders<MaterialCategory>.Filter
                .Eq(x => x.Name, data[0]) & Builders<MaterialCategory>.Filter
                .Eq(x => x.Parent, data[1]);
        List<MaterialCategory> raw = await _mainCollection.Find(filter)
            .ToListAsync();
        if (raw.Count > 0) return false;

        return true;
    }

    public List<UpdateList<string>>? ValidateSubcategory(
        List<UpdateList<string>>? data,
        string oldData)
    {
        if (data is null) return null;
        List<UpdateList<string>>? result = data;
        result.ForEach(async (e) => {
            if (e.Operation != 1)
            {
                if (!await NameIsUnique(new string?[] {
                    e.NewItem, ParentRoot(oldData)
                })) result.Remove(e);
            } else 
            {
                if (e.Key > result.Count)
                    result.Remove(e);
            }
        });
        return result;
    }

    public async Task<UpdatedMatCategoryDto?>
        ValidateUpdate(UpdatedMatCategoryDto data)
    {
        if (data.Name is null && data.Parent is null
            && data.SubCategory is null && data.Children is null)
                return null;
        
        MaterialCategory? oldDocument = GetCategory(data.Id);
        if (oldDocument is null) return null;
        
        string? parent = GetParent(oldDocument.Parent, data.Parent);
        if (parent is null) return null;
        
        UpdatedMatCategoryDto sanitized = data;

        if (data.Name is not null)
        sanitized.Name = await NameIsUnique(
                new string?[] { data.Name, ParentRoot(parent) }) ?
                    data.Name : null;

        if (sanitized.SubCategory is not null)
            sanitized.SubCategory =
                ValidateSubcategory(sanitized.SubCategory, oldDocument.Id);

        return sanitized;
    }
    
    #endregion ValidateUpdate

    #region Validate New

    public async Task<HTTPResult<NewMaterialCategoryDto>>
        ValidateNew(NewMaterialCategoryDto data, string oldParent)
    {
        string? parent = GetParent(oldParent, data.Parent);
        if (parent is null) return new() { Code = 400 };
        
        if (!await NameIsUnique(
            new string?[] { data.Name, ParentRoot(parent) }))
                return new() { Code = 400 };

        return new() { Code = 200, Value = data };
    }

    #endregion
}