using Newtonsoft.Json;

using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Dto;

namespace Fictichos.Constructora.Model;

public class Area : BaseEntity,
        IQueryMask<Area, NewAreaDto, UpdatedAreaDto>
{
    internal string Name { get; set; } = string.Empty;
    internal string? Parent { get; set; }
    internal string? Head { get; set; } = string.Empty;
    internal List<string> Children { get; set; } = new();

    public Area() { }
    private Area(NewAreaDto data)
    {
        Name = data.Name;
        Parent = data.Parent ?? null;
        Head = data.Head ?? null;
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
        Parent = data.Parent ?? Parent;
        Head = data.Head ?? Head;
    }
}