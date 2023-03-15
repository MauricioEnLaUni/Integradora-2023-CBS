using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Fictichos.Constructora.Dto
{
    /// <summary>
    /// Payload for a constructor.
    /// </summary>
    public record NewMaterialDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int Quantity { get; set; } = 0;
        [Required]
        public string Location { get; set; } = string.Empty;
        public int? Status { get; set; } = 0;
        [Required]
        public double BoughtFor { get; set; } = 0;
        [Required]
        public ObjectId Provider { get; set; }
        [Required]
        public ObjectId Owner { get; set; }
        [Required]
        public ObjectId Handler { get; set; }
        [Required]
        public double Depreciation { get; set; }
        [Required]
        public double Brand { get; set; }
        [Required]
        public ObjectId Category { get; set; }
    }

    /// <summary>
    /// Update payload.
    /// </summary>
    /// <remarks>
    /// Contains all editable class members as nulls, if a field isn't 
    /// null the adopts it as the updated value.
    /// </remarks>
    public record UpdatedMaterialDto
    {
        [Required]
        public string Id { get; set; } = string.Empty;
        public int? Quantity { get; set; }
        public int? Status { get; set; }
        public double? BoughtFor { get; set; }
        public DateTime? Closed { get; set; }
        public ObjectId? Provider { get; set; }
        public ObjectId? Owner { get; set; }
        public ObjectId? Handler { get; set; }
        public double? Depreciation { get; set; }
        public string? Location { get; set; }
        public string? Category { get; set; }
    }

    /// <summary>
    /// Result of searching for Item condition.
    /// </summary>
    /// <returns>
    /// Object containing Id and Status of a Material Input.
    /// </returns>
    public record MaterialMaintenanceDto
    {
        public ObjectId Id { get; set; } = new();
        public int Status { get; set; }
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
        public ObjectId Id { get; set;}
        public string Name { get; set; } = string.Empty;
        public ObjectId Brand { get; set; }
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
        public ObjectId Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ObjectId Owner { get; set; }
        public ObjectId Brand { get; set; }
        public int Quantity { get; set; } = 0;
    }

    public record NewMaterialCategoryDto
    {
        public string Name { get; set; } = string.Empty;
        public ObjectId? Parent { get; set; }
    }

    public record MaterialCategoryDto
    {
        public ObjectId Id { get; set; } = new();
        public string Name { get; set; } = string.Empty;
        public ObjectId? Parent { get; set; }
        public List<MaterialCategoryDto>? SubCategory { get; set; } = new();
        public List<MaterialDto>? Children { get; set; } = new();
    }

    public record UpdateMatCategoryDto
    {
        public ObjectId Id { get; set; }
        public string? Name { get; set; }
        public ObjectId? Parent { get; set; }
    }
}