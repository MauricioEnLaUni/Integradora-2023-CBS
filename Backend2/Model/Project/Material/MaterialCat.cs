using Newtonsoft.Json;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Model
{
    public class MaterialCategory : BaseEntity,
        IQueryMask<MaterialCategory, NewMaterialCategoryDto, UpdatedMatCategoryDto, MaterialCategoryDto>
    {
        public string Name { get; set; } = string.Empty;
        public string? Parent { get; private set; }
        public List<string>? SubCategory { get; private set; }
        public List<string>? Children { get; private set; } = new();

        public MaterialCategory() { }
        private MaterialCategory(NewMaterialCategoryDto data)
        {
            Name = data.Name;
            Parent = data.Parent ?? null;
        }
        public MaterialCategory Instantiate(NewMaterialCategoryDto data)
        {
            return new(data);
        }

        public MaterialCategoryDto ToDto()
        {
            return new()
            {
                Id = Id,
                Name = Name,
                Parent = Parent,
                SubCategory = SubCategory,
                Children = Children
            };
        }

        public string Serialize()
        {
            MaterialCategoryDto temp = ToDto();
            return JsonConvert.SerializeObject(temp);
        }

        public void Update(UpdatedMatCategoryDto data)
        {
            Name = data.Name ?? Name;
            Parent = data.Parent ?? null;
            
            data.SubCategory?.ForEach(SubCategory.UpdateWithIndex);
            data.Children?.ForEach(Children.UpdateWithIndex);
        }
    }
}