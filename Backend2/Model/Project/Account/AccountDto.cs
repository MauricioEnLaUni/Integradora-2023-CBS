using Fictichos.Constructora.Model;

namespace Fitichos.Constructora.Dto
{
    public record NewAccountDto
    {
        public string Name { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
    }

    public record UpdateAccountDto
    {
        public string? Name { get; set; } = string.Empty;
        public string? Owner { get; set; } = string.Empty;
    }

    public record AccountDto
    {
        public List<PaymentsDto> Payments { get; set; } = new();
    }

    public record PaymentsDto
    {
        public double Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Complete { get; set; }

        public PaymentsDto(Payment data)
        {
            Amount = data.Amount;
            CreatedAt = data.CreatedAt;
            Complete = data.Complete;
        }
    }

    public record NewPaymentDto
    {
        public string Name { get; set; } = string.Empty;
        public double Amount { get; set; }
        public DateTime Due { get; set; }
    }

    public record UpdatePaymentDto
    {
        public string Id { get; set; } = string.Empty;
        public string? Name;
        public DateTime? Due;
        public double? Amount;
    }
}