using MongoDB.Bson.Serialization.Attributes;

using Fitichos.Constructora.Repository;
using Fitichos.Constructora.Dto;

namespace Fictichos.Constructora.Model
{
    public class Material : Entity
    {
        [BsonElement("qty")]
        public int Quantity { get; set; }
        [BsonElement("owner")]
        public string Owner { get; set; }
        [BsonElement("brand")]
        public string Brand { get; set; }
        [BsonElement("location")]
        public Address Location { get; set; }
        [BsonElement("status")]
        public int? Status { get; set; }
        [BsonElement("price")]
        public double BoughtFor { get; set; }
        [BsonElement("provider")]
        public string Provider { get; set; }

        public Material(NewMaterialDto data) : base(data.Name, null)
        {
            Quantity = data.Quantity;
            Location = new(data.Location);
            if (data.Status is not null) Status = data.Status;
            BoughtFor = data.BoughtFor;
            Brand = data.Brand;
            Provider = data.Provider;
            Owner = data.Owner;
        }

        public void Update(UpdatedMaterialDto data)
        {
            if (data.Quantity is not null) Quantity = (int)data.Quantity;
            if (data.Status is not null) Status = data.Status;
            if (data.BoughtFor is not null) BoughtFor = (double)data.BoughtFor;
            if (data.Closed is not null) Closed = data.Closed;
        }
    }
}