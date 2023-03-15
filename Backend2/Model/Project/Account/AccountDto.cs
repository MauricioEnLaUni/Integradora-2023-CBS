using MongoDB.Bson;

namespace Fictichos.Constructora.Dto
{
    public record NewAccountDto
    {
        public string Name { get; set; } = string.Empty;
        public ObjectId Owner { get; set; }
    }

    public record UpdateAccountDto
    {
        public string? Name { get; set; } = string.Empty;
        public ObjectId? Owner { get; set; }
    }

    public record AccountDto
    {
        public ObjectId Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<PaymentsDto> Payments { get; set; } = new();
        public ObjectId Owner { get; set; }
    }

    public record PaymentsDto
    {
        public ObjectId Id { get; set; }
        public double Amount { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime Due { get; set; }
        public bool Complete { get; set; } = false;
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