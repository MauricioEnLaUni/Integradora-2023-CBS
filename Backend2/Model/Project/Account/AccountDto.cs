using Fictichos.Constructora.Utilities;
using MongoDB.Bson;

namespace Fictichos.Constructora.Dto
{
    public record NewAccountDto
    {
        public string Name { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
    }

    public record UpdatedAccountDto : DtoBase
    {
        public string? Name { get; set; } = string.Empty;
        public string? Owner { get; set; }
        public List<IndexedObjectUpdate<NewPaymentDto, UpdatedPaymentDto>>?
            Payments { get; set; }
    }

    public record AccountDto
    {
        public string Id { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<PaymentsDto> Payments { get; set; } = new();
        public string Owner { get; set; } = string.Empty;
    }

    public record PaymentsDto
    {
        public string Id { get; set; } = string.Empty;
        public double Amount { get; set; }
        public string Concept { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime Due { get; set; }
        public bool Complete { get; set; } = false;
    }

    public record NewPaymentDto
    {
        public string Concept { get; set; } = string.Empty;
        public double Amount { get; set; }
        public DateTime Due { get; set; }
    }

    public record UpdatedPaymentDto : DtoBase
    {
        public string? Concept;
        public DateTime? Due;
        public double? Amount;
        public bool? Complete;
        public bool? Direction;
    }
}