namespace Fitichos.Constructora.Dto
{
    public record NewAccountDto
    {
        public string Name { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
    }

    public record PaymentsDto
    {
        public double Amount { get; set; }
        public DateTime Created { get; set; }
        public bool Complete { get; set; }
        public double Rate { get; set; }
    }

    public record NewPaymentDto
    {
        
    }
}