using Newtonsoft.Json;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Model;

public class Material
    : BaseEntity, IQueryMask<Material, NewMaterialDto, UpdatedMaterialDto>
{
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; private set; }
    public string Owner { get; private set; } = string.Empty;
    public string Handler { get; private set; } = string.Empty;
    public string Parent { get; private set; } = string.Empty;
    public Address Location { get; private set; } = new();
    public int Status { get; private set; } = 0;
    public int Purpose { get; private set; } = 0;
    public double BoughtFor { get; private set; }
    public double Depreciation { get; private set; }
    public string Provider { get; private set; } = string.Empty;

    public Material() { }
    private Material(NewMaterialDto data)
    {
        Name = data.Name;
        Quantity = data.Quantity;
        Status = data.Status;
        BoughtFor = data.BoughtFor;
        Provider = data.Provider;
        Owner = data.Owner;
        Handler = data.Handler;
        Depreciation = 0;
    }
    public Material Instantiate(NewMaterialDto data)
    {
        return new(data);
    }

    public void Update(UpdatedMaterialDto data)
    {
        Quantity = data.Quantity ?? Quantity;
        Status = data.Status ?? Status;
        BoughtFor = data.BoughtFor ?? BoughtFor;
        Provider = data.Provider ?? Provider;
        Owner = data.Owner ?? Handler;
        Handler = data.Handler ?? Handler;
        Depreciation = data.Depreciation ?? Depreciation;

        Location = data.Location is null ? Location :
            new Address(data.Location);
    }

    public MaterialDto ToDto()
    {
        return new()
        {
            Id = Id,
            Name = Name,
            Quantity = Quantity,
            Owner = Owner
        };
    }

    public string Serialize()
    {
        MaterialDto data = ToDto();
        return JsonConvert.SerializeObject(data);
    }

    public string AsInventory()
    {
        CurrentInventoryDto data = new()
        {
            Id = Id,
            Name = Name,
            Quantity = Quantity
        };
        return JsonConvert.SerializeObject(data);
    }

    public string AsMaintenance()
    {
        MaterialMaintenanceDto data = new()
        {
            Id = Id,
            Status = Status,
            Purpose = Purpose
        };
        return JsonConvert.SerializeObject(data);
    }

    public string AsOverview()
    {
        MaterialDto data = ToDto();
        return JsonConvert.SerializeObject(data);
    }
}