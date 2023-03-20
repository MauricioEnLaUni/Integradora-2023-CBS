using Fictichos.Constructora.Dto;

namespace Fictichos.Constructora.Utilities
{
    public record UpdateObject<T> : UpdateDto
    {
        public Dictionary<string, Action<object?, object?>> ActionsCache { get; set; }
            = new();
        public List<UpdateObject<T>> Embedded { get; set; } = new();
    }

    public record UpdateDto : DtoBase
    {
        public Dictionary<string, dynamic> Changes { get; init; } = new();
        public Dictionary<string, UpdateDto> ObjectChanges { get; init; }
            = new();
    }
}