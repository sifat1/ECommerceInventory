using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ECommerceInventory.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    [StringLength(100)]
    public string Name { get; set; }
    [StringLength(500)]
    public string Description { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    [Range(0, int.MaxValue)]
    public int Stock { get; set; }
    public string? ImageUrl { get; set; }

    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    
    [JsonIgnore]
    public Category? Category { get; set; }
}