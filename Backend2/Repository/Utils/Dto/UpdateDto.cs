using Fictichos.Constructora.Dto;

namespace Fictichos.Constructora.Utilities
{
    public record IUpdateDto : DtoBase
    {
        public Dictionary<string, dynamic> Changes { get; init; } = new();
        public Dictionary<string, dynamic> ActionsCache { get; init; } = new();
    }
}