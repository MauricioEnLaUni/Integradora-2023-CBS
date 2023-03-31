using Fictichos.Constructora.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Fictichos.Constructora.Dto;

/// <summary>
/// Payload for a constructor.
/// </summary>
public record NewMaterialDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public int Quantity { get; set; } = 0;
    public int Status { get; set; } = 0;
    public int Purpose { get; set; } = 0;
    [Required]
    public double BoughtFor { get; set; } = 0;
    [Required]
    public string Provider { get; set; } = string.Empty;
    [Required]
    public string Owner { get; set; } = string.Empty;
    [Required]
    public string Handler { get; set; } = string.Empty;
    [Required]
    public string Category { get; set; } = string.Empty;
}

/// <summary>
/// Update payload.
/// </summary>
/// <remarks>
/// Contains all editable class members as nulls, if a field isn't 
/// null the adopts it as the updated value.
/// </remarks>
public record UpdatedMaterialDto : DtoBase
{
    public int? Quantity { get; set; }
    public int? Status { get; set; }
    public double? BoughtFor { get; set; }
    public string? Provider { get; set; } = string.Empty;
    public string? Owner { get; set; } = string.Empty;
    public string? Handler { get; set; } = string.Empty;
    public double? Depreciation { get; set; }
    public NewAddressDto? Location { get; set; }
    public string? Category { get; set; }
    public int? Purpose { get; set; }
}

/// <summary>
/// Result of searching for Item condition.
/// </summary>
/// <returns>
/// Object containing Id and Status of a Material Input.
/// </returns>
public record MaterialMaintenanceDto
{
    public string Id { get; set; } = string.Empty;
    public int Status { get; set; }
    public int Purpose { get; set; }
}

/// <summary>
/// Material Overview for listing existences.
/// </summary>
/// <remarks>
/// Creates an object to give an overview of a project or a location
/// current material's stores.
/// </remarks>
public record CurrentInventoryDto
{
    public string Id { get; set;} = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public int Quantity { get; set; } = 0;
}

public record OwnerMaterialDto
{
    public Dictionary<string, List<string>> Materials { get; set; } = new()
    {
        { "", new() }
    };
}

public record MaterialDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Owner { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public int Quantity { get; set; } = 0;
}

public record NewMaterialCategoryDto
{
    public string Name { get; set; } = string.Empty;
    public string? Parent { get; set; }
}

public record MaterialCategoryDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Parent { get; set; }
    public List<string>? SubCategory { get; set; } = new();
    public List<string>? Children { get; set; } = new();
}

public record UpdatedMatCategoryDto : DtoBase
{
    public string? Name { get; set; }
    public string? Parent { get; set; }
    public List<UpdateList<string>>? SubCategory { get; set; }
    public List<UpdateList<string>>? Children { get; set; }
}