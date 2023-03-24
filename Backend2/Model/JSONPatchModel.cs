using Microsoft.AspNetCore.JsonPatch;

namespace Fictichos.Constructora.Dto
{
    public record PatchContainer<T> : DtoBase
    where T : class
    {
        public JsonPatchDocument<T> Value { get; set; } = new();
    }
}