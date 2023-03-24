using Newtonsoft.Json;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Model
{
    public class Payment : BaseEntity,
        IQueryMask<Payment, NewPaymentDto, UpdatedPaymentDto>
    {
        public string Concept { get; set; } = string.Empty;
        public double Amount { get; set; }
        public bool Complete { get; set; }
        public bool Direction { get; set; }
        public DateTime Due { get; set; }

        public Payment() { }
        public Payment(NewPaymentDto data)
        {
            Due = data.Due;
            Amount = data.Amount;
        }

        public Payment Instantiate(NewPaymentDto data)
        {
            return new(data);
        }

        public PaymentDto ToDto()
        {
            return new()
            {
                Id = Id,
                Concept = Concept,
                Amount = Amount,
                Due = Due
            };
        }

        public string Serialize()
        {
            PaymentDto data = ToDto();
            return JsonConvert.SerializeObject(data);
        }

        public void Update(UpdatedPaymentDto data)
        {
            Concept = data.Concept ?? Concept;
            Amount = data.Amount ?? Amount;
            Complete = data.Complete ?? Complete;
            Direction = data.Direction ?? Direction;
            Due = data.Due ?? Due;
        }
    }
}