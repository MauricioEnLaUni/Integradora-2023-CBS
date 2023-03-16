using MongoDB.Bson;
using Newtonsoft.Json;
using MongoDB.Bson.Serialization.Attributes;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Model
{
    public class Material
        : Entity, IQueryMask<Material, MaterialDto, UpdatedMaterialDto>
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

        public MaterialDto ToDto()
        {
            return new()
            {
                Id = Id,
                Name = Name,
                Quantity = Quantity,
                Owner = Owner
            };
        }

        public string SerializeDto()
        {
            MaterialDto data = ToDto();
            return JsonConvert.SerializeObject(data);
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
            MaterialDto data = ToDto();
            return JsonConvert.SerializeObject(data);
        }
    }
}