using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Dto;

namespace Fictichos.Constructora.Model
{
    public class Material : Entity, IMongoMask
    {
        [BsonElement("qty")]
        public int Quantity { get; private set; }
        [BsonElement("owner")]
        public string Owner { get; private set; }
        [BsonElement("handler")]
        public string Handler { get; private set; }
        [BsonElement("location")]
        public Address Location { get; private set; }
        [BsonElement("status")]
        public int? Status { get; private set; }
        [BsonElement("price")]
        public double BoughtFor { get; private set; }
        [BsonElement("currentPrice")]
        public double Depreciation { get; private set; }
        [BsonElement("provider")]
        public string Provider { get; private set; }
        [BsonElement("category")]
        public string Category { get; private set; }

        public Material(NewMaterialDto data) : base(data.Name, null)
        {
            Quantity = data.Quantity;
            Location = new(data.Location);
            if (data.Status is not null) Status = data.Status;
            BoughtFor = data.BoughtFor;
            Provider = data.Provider;
            Owner = data.Owner;
            Handler = data.Handler;
            Depreciation = data.Depreciation;
            Category = data.Category;
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
            Category = data.Category ?? Category;
            Location = data.Location is null ? Location : new(data.Location);
        }

        public string AsInventory()
        {
            CurrentInventoryDto data = new()
            {
                Id = this.Id.ToString(),
                Name = this.Name,
                Quantity = this.Quantity
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
            MaterialDto data = new()
            {
                Id = this.Id.ToString(),
                Name = this.Name,
                Quantity = this.Quantity,
                Owner = this.Owner
            };
            return JsonConvert.SerializeObject(data);
        }
    }
}