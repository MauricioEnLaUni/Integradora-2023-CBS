using Microsoft.AspNetCore.JsonPatch;

using MongoDB.Bson;

namespace Fictichos.Constructora.Dto
{
    public record PatchContainer<T> : DtoBase
    where T : class
    {
        public JsonPatchDocument<T> value { get; set; } = new();
    }
}