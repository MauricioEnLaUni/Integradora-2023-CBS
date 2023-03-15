using System.Collections.Generic;

using MongoDB.Bson;
using Newtonsoft.Json;
using MongoDB.Bson.Serialization.Attributes;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Model
{
    public class MaterialCategory : Entity, IQueryMask<MaterialCategory>
    {
        private ObjectId? Parent { get; set; }
        private List<MaterialCategory>? SubCategory { get; set; }
        private List<Material>? Children { get; set; } = new();

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

        private MaterialCategoryDto DtoInternal()
        {
            List<string>? cats = null;
            List<string>? mats = null;
            if (SubCategory is not null)
            {
                cats = new();
                SubCategory.ForEach(e => {
                    cats.Add(JsonConvert.SerializeObject(SubCategory));
                });
            }
            if (Children is not null)
            {
                mats = new();
                Children.ForEach(e => {
                    mats.Add(JsonConvert.SerializeObject(Children));
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

        public string AsDto()
        {
            MaterialCategoryDto temp = DtoInternal();
            return JsonConvert.SerializeObject(temp);
        }

        public void Update(UpdateMatCategoryDto data)
        {
            Name = data.Name ?? Name;
            Parent = data.Parent ?? null;
        }

        private class Material : Entity
        {
            [BsonElement("qty")]
            public int Quantity { get; private set; }
            [BsonElement("owner")]
            public ObjectId Owner { get; private set; }
            [BsonElement("handler")]
            public ObjectId Handler { get; private set; }
            [BsonElement("location")]
            public Address Location { get; private set; } = new();
            [BsonElement("status")]
            public int? Status { get; private set; }
            [BsonElement("price")]
            public double BoughtFor { get; private set; }
            [BsonElement("currentPrice")]
            public double Depreciation { get; private set; }
            [BsonElement("provider")]
            public ObjectId Provider { get; private set; }

            public Material() { }
            private Material(NewMaterialDto data)
            {
                Name = data.Name;
                Quantity = data.Quantity;
                Location = new Address().FakeConstructor(data.Location);
                if (data.Status is not null) Status = data.Status;
                BoughtFor = data.BoughtFor;
                Provider = data.Provider;
                Owner = data.Owner;
                Handler = data.Handler;
                Depreciation = data.Depreciation;
            }
            public Material FakeConstructor(string dto)
            {
                return new Material(JsonConvert
                    .DeserializeObject<NewMaterialDto>(dto, new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Error
                })!);
            }

            public void Update(UpdatedMaterialDto data)
            {
                Quantity = data.Quantity ?? Quantity;
                Status = data.Status ?? Status;
                BoughtFor = data.BoughtFor ?? BoughtFor;
                Provider = data.Provider ?? Provider;
                Owner = data.Owner ?? Handler;
                Handler = data.Handler ?? Handler;
                Depreciation = data.Depreciation ?? Depreciation;
                Location = data.Location is null ?
                    Location : new Address().FakeConstructor(data.Location);
            }

            public MaterialDto DtoInternal()
            {
                return new()
                {
                    Id = Id,
                    Name = Name,
                    Quantity = Quantity,
                    Owner = Owner
                };
            }

            public string AsInventory()
            {
                CurrentInventoryDto data = new()
                {
                    Id = Id,
                    Name = Name,
                    Quantity = Quantity
                };
                return JsonConvert.SerializeObject(data);
            }

            public string AsMaintenance()
            {
                MaterialMaintenanceDto data = new()
                {
                    Id = Id,
                    Status = Status ?? 0
                };
                return JsonConvert.SerializeObject(data);
            }

            public string AsOverview()
            {
                MaterialDto data = DtoInternal();
                return JsonConvert.SerializeObject(data);
            }
        }
    }
}