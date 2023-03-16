using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Model
{
    public class Payment : Entity,
        IQueryMask<Payment, PaymentsDto, UpdatedPaymentDto>
    {
        [BsonElement("amount")]
        public double Amount { get; set; }
        [BsonElement("complete")]
        public bool Complete { get; set; }
        // in out

        public Payment() { }
        public Payment(NewPaymentDto data)
        {
            Closed = data.Due;
            Amount = data.Amount;
        }

        public Payment FakeConstructor(string dto)
        {
            try
            {
                return new Payment(JsonConvert
                    .DeserializeObject<NewPaymentDto>(dto, new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Error
                })!);
            }
            catch
            {
                throw new JsonSerializationException();
            }
        }

        public PaymentsDto ToDto()
        {
            return new()
            {
                Id = Id,
                Name = Name,
                Amount = Amount,
                Due = (DateTime)Closed!
            };
        }

        public string SerializeDto()
        {
            PaymentsDto data = ToDto();
            return JsonConvert.SerializeObject(data);
        }

        public void Update(UpdatedPaymentDto data)
        {

        }
    }
}