using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using Fitichos.Constructora.Repository;

namespace Fictichos.Constructora.Model
{
    public class Payment
    {
        public ObjectId Id { get; init; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public bool Complete { get; set; }

        public Payment(double qty, DateTime closed, bool? paid)
        {
            Id = ObjectId.GenerateNewId();
            Amount = qty;
            Date = closed;
            Complete = paid ?? false;
        }
    }
}