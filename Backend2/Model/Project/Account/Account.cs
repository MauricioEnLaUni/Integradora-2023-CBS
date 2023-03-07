using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using Fitichos.Constructora.Repository;
using Fitichos.Constructora.Dto;

namespace Fictichos.Constructora.Model
{
    public class Account : Entity
    {
        [BsonElement("payments")]
        private List<Payment> Payments { get; set; } = new();
        [BsonElement("owner")]
        public string Owner { get; set; } = string.Empty;
        

        public Account(NewAccountDto data) : base(data.Name, null)
        {
            Owner = data.Owner;
        }
    }
}