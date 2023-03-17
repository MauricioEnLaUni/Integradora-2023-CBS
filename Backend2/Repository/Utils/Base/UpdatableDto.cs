using Fictichos.Constructora.Dto;

namespace Fictichos.Constructora.Utilities
{
    public record UpdatableDto : DtoBase
    {
        public Dictionary<string, dynamic> Changes { get; set; } = new();
    }
}