using MongoDB.Bson;
using Newtonsoft.Json;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Model
{
    public class MaterialCategory : Entity
    {
        public ObjectId? Parent { get; private set; }
        public List<ObjectId> Children { get; private set; } = new();

        public MaterialCategory(NewMaterialCategoryDto data) : base(data.Name, null)
        {
            Parent = data.Parent is null ? null : new(data.Parent);
        }

        public string AsDto()
        {
            MaterialCategoryDto temp = new()
            {
                Id = Id,
                Name = Name,
                Parent = Parent,
                Children = Children,
            };
            return JsonConvert.SerializeObject(temp);
        }

        public void Update(UpdateMatCategoryDto data)
        {
            Name = data.Name ?? Name;
            Parent = data.Parent is null ? Parent : new ObjectId(data.Parent);
            if (data.Children is not null)
            {
                SetChildren(data);
            }
        }

        public void SetChildren(UpdateMatCategoryDto data)
        {
            if (data.UpdateFlag is null)
            {
                Children = (from c in data.Children
                select new ObjectId(c)).ToList();
            } else if ((bool)data.UpdateFlag)
            {
                Children.Add(new ObjectId(data.Children![0]));
            } else
            {
                Children.Remove(new ObjectId(data.Children![0]));
            }
        }
    }
}