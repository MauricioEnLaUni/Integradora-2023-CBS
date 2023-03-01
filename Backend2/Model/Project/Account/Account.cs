using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using Fitichos.Constructora.Repository;

namespace Fictichos.Constructora.Model
{
    public class Account : Entity
    {
        [BsonElement("payments")]
        private List<Payment> Payments { get; set; } = new List<Payment>();

        public Account(string concept, DateTime? closes) : base(concept, closes) { }
        
        public class Payment
        {
            private ObjectId Id { get; init; }
            private double Amount { get; set; }
            private DateTime Date { get; set; }
            private bool Complete { get; set; }

            private Payment(double qty, DateTime closed, bool? paid)
            {
                Id = ObjectId.GenerateNewId();
                Amount = qty;
                Date = closed;
                Complete = paid ?? false;
            }
        }
    }
}