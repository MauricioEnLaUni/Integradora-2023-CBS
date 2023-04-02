using MongoDB.Driver;

namespace Fictichos.Constructora.Dto;

internal record UpdateDto<T>
{
    internal FilterDefinition<T> filter;
    internal UpdateDefinition<T> update;

    internal UpdateDto(FilterDefinition<T> f, UpdateDefinition<T> u)
    {
        filter = f;
        update = u;
    }
}