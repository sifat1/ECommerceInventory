using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceInventory.Models.Dtos;

public class ProductDto
{
    [StringLength(100)]
    public string? Name { get; set; }
    [StringLength(500)]
    public string? Description { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal? Price { get; set; }
    [Range(0, int.MaxValue)]
    public int? Stock { get; set; }
    public string? ImageUrl { get; set; }
}