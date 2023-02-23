using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fictichos.Constructora.Models
{
    public class Account : Entity
    {
        [BsonElement("payments")]
        private List<Payment> Payments { get; set; } = new List<Payment>();

        public Account(string concept, DateTime created) : base(concept, created) { }
        public Account(string concept) : base(concept) { }
        
        public class Payment
        {
            private string Id { get; init; }
            private double Amount { get; set; }
            private DateTime Date { get; set; }
            private bool Complete { get; set; }

            private Payment(double qty, DateTime closed, bool? paid)
            {
                Id = "0";
                Amount = qty;
                Date = closed;
                Complete = paid ?? false;
            }

            private Payment(string id, double qty, DateTime closed, bool? paid)
            {
                Id = id;
                Amount = qty;
                Date = closed;
                Complete = paid ?? false;
            }
        }
    }
}