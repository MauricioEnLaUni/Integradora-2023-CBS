using Newtonsoft.Json;

using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Dto;

namespace Fictichos.Constructora.Model;

public class Area : BaseEntity,
        IQueryMask<Area, NewAreaDto, UpdatedAreaDto>
{
    public string Name { get; set; } = string.Empty;
    public string Head { get; set; } = string.Empty;
    public string Collection { get; set; } = string.Empty;

    public Area() { }
    private Area(NewAreaDto data)
    {
        Name = data.Name;
        Head = data.Head;
        Collection = data.Collection;
    }

    public Area Instantiate(NewAreaDto data)
    {
        return new(data);
    }

    public AreaDto ToDto()
    {
        return new()
        {
            Name = Name,
            Head = Head
        };
    }

    public string Serialize()
    {
        AreaDto data = ToDto();
        return JsonConvert.SerializeObject(data);
    }

    public void Update(UpdatedAreaDto data)
    {
        Name = data.Name ?? Name;
        Head = data.Head ?? Head;
        Collection = data.Collection ?? Collection;
    }
}