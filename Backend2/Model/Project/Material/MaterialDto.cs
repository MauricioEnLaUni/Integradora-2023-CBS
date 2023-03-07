using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

using Fictichos.Constructora.Model;

namespace Fitichos.Constructora.Dto
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
        public NewAddressDto Location { get; set; } = new();
        public int? Status { get; set; } = 0;
        [Required]
        public double BoughtFor { get; set; } = 0;
        [Required]
        public string Brand { get; set; } = string.Empty;
        [Required]
        public string Provider { get; set; } = string.Empty;
        [Required]
        public string Owner { get; set; } = string.Empty;
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
        public string? Provider { get; set; }
        public string? Owner { get; set; }
        public string? Brand { get; set; }
    }

    /// <summary>
    /// Result of searching for Item condition.
    /// </summary>
    /// <returns>
    /// Object containing Id and Status of a Material Input.
    /// </returns>
    public record MaterialMaintenanceDto
    {
        public ObjectId Id { get; set; }
        public int Status { get; set; }

        public MaterialMaintenanceDto(Material data)
        {
            Id = data.Id;
            Status = data.Status ?? 0;
        }
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
        public string Name { get; set; }
        public string Brand { get; set; }
        public int Quantity { get; set; }

        public CurrentInventoryDto(Material data)
        {
            Id = data.Id;
            Name = data.Name;
            Quantity = data.Quantity;
            Brand = data.Brand;
        }
    }
    
    public record OwnerMaterialDto
    {
        public Dictionary<string, List<string>> Materials { get; set; } = new()
        {
            { "", new() }
        };
    }
}