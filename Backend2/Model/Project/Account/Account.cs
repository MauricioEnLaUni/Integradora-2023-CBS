using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Model
{
    /// <summary>
    /// Represents monetary transaction in which the user is involved.
    /// </summary>
    public class Account : BaseEntity,
        IQueryMask<Account, NewAccountDto, UpdatedAccountDto>
    {
        public string Name { get; set; } = string.Empty;
        public List<Payment> Payments { get; set; } = new();
        public string Owner { get; set; } = string.Empty;
        
        public Account() { }
        public Account(NewAccountDto data)
        {
            Owner = data.Owner;
        }

        public Account Instantiate(NewAccountDto data)
        {
            return new(data);
        }

        public AccountDto ToDto()
        {
            List<PaymentDto> list = new();
            Payments.ForEach(e => {
                list.Add(e.ToDto());
            });
            return new()
            {
                Id = Id,
                CreatedAt = CreatedAt,
                Payments = list,
                Owner = Owner
            };
        }

        public string Serialize()
        {
            AccountDto data = ToDto();
            return JsonConvert.SerializeObject(data);
        }

        public void Update(UpdatedAccountDto data)
        {
            Name = data.Name ?? Name;
            Owner = data.Owner ?? Owner;
            data.Payments?.ForEach(Payments.UpdateObjectWithIndex<Payment, NewPaymentDto, UpdatedPaymentDto>);
        }
    }
}