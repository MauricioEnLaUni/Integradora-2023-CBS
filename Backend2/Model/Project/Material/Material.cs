using MongoDB.Bson.Serialization.Attributes;

using Fitichos.Constructora.Repository;
using Fitichos.Constructora.Dto;

namespace Fictichos.Constructora.Model
{
    public class Material : Entity
    {
        [BsonElement("qty")]
        public int Quantity { get; set; } = 0;
        [BsonElement("location")]
        public Address Location { get; set; }
        [BsonElement("status")]
        public int? Status { get; set; } = 0;
        [BsonElement("price")]
        public double BoughtFor { get; set; } = 0;

        public Material(NewMaterialDto data) : base(data.Name, null)
        {
            Quantity = data.Quantity;
            Location = new(data.Location);
            if (data.Status is not null) Status = data.Status;
            BoughtFor = data.BoughtFor;
        }
    }
}