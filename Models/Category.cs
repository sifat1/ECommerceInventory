using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ECommerceInventory.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    [StringLength(100)]
    public string Name { get; set; }
    [StringLength(500)]
    public string Description { get; set; }
    
    [JsonIgnore]
    public List<Product>? products { get; set; }
}