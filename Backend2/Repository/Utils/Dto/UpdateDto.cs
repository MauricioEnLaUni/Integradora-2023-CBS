using Fictichos.Constructora.Dto;

namespace Fictichos.Constructora.Utilities
{
    public record IUpdateDto<T> : UpdateBase
    {
        public Dictionary<string, Action<T, dynamic>> ActionsCache { get; set; }
            = new();
    }

    public record UpdateBase : DtoBase
    {
        public Dictionary<string, dynamic> Changes { get; init; } = new();
        public Dictionary<string, UpdateBase> ObjectChanges { get; init; }
            = new();
    }
}