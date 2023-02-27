using MongoDB.Bson.Serialization.Attributes;

namespace Fictichos.Constructora.Models
{
    public class Material : Entity
    {
        [BsonElement("qty")]
        public int Quantity { get; set; }
        [BsonElement("location")]
        public Address Location { get; set; }
        [BsonElement("status")]
        public int Status { get; set; }
        [BsonElement("cost")]
        public double BoughtFor { get; set; }

        public Material(string name, int qty, Address ad, int status, double price) :  base(name)
        {
            Quantity = qty;
            Location = ad;
            Status = status;
            BoughtFor = price;
        }
    }
}