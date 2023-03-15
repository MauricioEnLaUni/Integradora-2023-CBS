using MongoDB.Bson;
using Newtonsoft.Json;
using MongoDB.Bson.Serialization.Attributes;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Model
{
    public class MaterialCategory : Entity, IQueryMask<MaterialCategory, MaterialCategoryDto>
    {
        [BsonElement("parent")]
        public ObjectId? Parent { get; private set; }
        [BsonElement("subcategory")]
        public List<MaterialCategory>? SubCategory { get; private set; }
        [BsonElement("children")]
        public List<Material>? Children { get; private set; } = new();

        public MaterialCategory() { }
        private MaterialCategory(NewMaterialCategoryDto data)
        {
            Name = data.Name;
            Parent = data.Parent ?? null;
        }
        public MaterialCategory FakeConstructor(string dto)
        {
            try
            {
                return new MaterialCategory(JsonConvert
                    .DeserializeObject<NewMaterialCategoryDto>(dto, new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Error
                })!);
            }
            catch
            {
                throw new JsonSerializationException();
            }
        }

        public MaterialCategoryDto ToDto()
        {
            List<MaterialCategoryDto>? cats = null;
            List<MaterialDto>? mats = null;
            if (SubCategory is not null)
            {
                cats = new();
                SubCategory.ForEach(e => {
                    cats.Add(e.ToDto());
                });
            }
            if (Children is not null)
            {
                mats = new();
                Children.ForEach(e => {
                    mats.Add(e.ToDto());
                });
            }
            return new()
            {
                Id = Id,
                Name = Name,
                Parent = Parent,
                SubCategory = cats,
                Children = mats
            };
        }

        public string SerializeDto()
        {
            MaterialCategoryDto temp = ToDto();
            return JsonConvert.SerializeObject(temp);
        }

        public void Update(UpdateMatCategoryDto data)
        {
            Name = data.Name ?? Name;
            Parent = data.Parent ?? null;
        }
    }
}