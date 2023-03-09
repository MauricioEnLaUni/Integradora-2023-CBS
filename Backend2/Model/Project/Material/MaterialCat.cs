using Fictichos.Constructora.Dto;

namespace Fictichos.Constructora.Model
{
    public class MaterialCategory
    {
        private Definition Definition { get; set; }
        private List<MaterialCategory> SubCategories { get; set; } = new();
        private List<Material> Children { get; set; } = new();

        public MaterialCategory(NewMaterialCategoryDto data)
        {
            Definition = data.Definition;
        }

        private class Definition
        {
            public string Name { get; private set; }
            public string Type { get; private set; }
        }
    }
}