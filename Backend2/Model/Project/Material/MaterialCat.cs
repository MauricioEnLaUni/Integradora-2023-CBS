using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Model
{
    public class MaterialCategory : Entity
    {
        private string Type { get; set; }
        private List<MaterialCategory> SubCategories { get; set; } = new();
        private List<Material> Children { get; set; } = new();

        public MaterialCategory(NewMaterialCategoryDto data) : base(data.Name, null)
        {
            Type = data.Type;
        }
    }
}