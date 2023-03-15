using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Model
{
    public class Account : Entity, IQueryMask<Account, AccountDto>
    {
        [BsonElement("payments")]
        public List<Payment> Payments { get; set; } = new();
        [BsonElement("owner")]
        public ObjectId Owner { get; set; }
        
        public Account() { }
        public Account(NewAccountDto data)
        {
            Owner = data.Owner;
        }

        public Account FakeConstructor(string dto)
        {
            try
            {
                return new Account(JsonConvert
                    .DeserializeObject<NewAccountDto>(dto, new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Error
                })!);
            }
            catch
            {
                throw new JsonSerializationException();
            }
        }

        public AccountDto ToDto()
        {
            List<PaymentsDto> list = new();
            Payments.ForEach(e => {
                list.Add(new(e));
            });
            return new()
            {
                Id = Id,
                CreatedAt = CreatedAt,
                Payments = list,
                Owner = Owner
            };
        }

        public string SerializeDto()
        {
            AccountDto data = ToDto();
            return JsonConvert.SerializeObject(data);
        }

        public void Update(UpdateAccountDto data)
        {
            Name = data.Name ?? Name;
            Owner = data.Owner ?? Owner;
        }
    }
}