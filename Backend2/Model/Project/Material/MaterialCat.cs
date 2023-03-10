using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Model
{
    public class MaterialCategory : Entity
    {
        public string Type { get; private set; }
        public string Parent { get; private set; }
        public List<MaterialCategory> SubCategories { get; private set; } = new();
        public List<Material> Children { get; private set; } = new();

        public MaterialCategory(NewMaterialCategoryDto data) : base(data.Name, null)
        {
            Type = data.Type;
            Parent = data.Parent;
        }
    }
}