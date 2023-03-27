using MongoDB.Driver;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Repository;

public class MaterialService
{
    public IMongoCollection<Material> MaterialCollection { get; set; }
    public IMongoCollection<MaterialCategory> CategoryCollection { get; set; }

    public MaterialService(MongoSettings container)
    {
        MaterialCollection = container.Client.GetDatabase("cbs")
            .GetCollection<Material>("material");
        CategoryCollection = container.Client.GetDatabase("cbs")
            .GetCollection<MaterialCategory>("materialCategories");
    }

    public List<TDto> ToDtoList<TModel, TDto>(List<TModel> model)
    {
        List<TDto> result = new();
        model.ForEach(e => {
            result.Add(e.To<TModel, TDto>());
        });
        return result;
    }
}