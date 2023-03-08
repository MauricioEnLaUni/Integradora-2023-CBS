using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using Fitichos.Constructora.Dto;
using Fitichos.Constructora.Repository;

namespace Fictichos.Constructora.Model
{
    public class Payment : Entity
    {
        [BsonElement("amount")]
        public double Amount { get; set; }
        [BsonElement("complete")]
        public bool Complete { get; set; }
        // in out

        public Payment(NewPaymentDto data) : base(data.Name, data.Due)
        {
            Amount = data.Amount;
        }

        public void Update(UpdatePaymentDto data)
        {

        }
    }
}