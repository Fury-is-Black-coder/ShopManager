using System.ComponentModel.DataAnnotations;

namespace ShopManager.DTOs;

public class UpdateProductDto
{
    [Required, MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }
}
