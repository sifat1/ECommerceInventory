using System.ComponentModel.DataAnnotations;

namespace ECommerceInventory.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    [StringLength(100)]
    public string Name { get; set; }
    [StringLength(500)]
    public string Description { get; set; }
    public List<Product> products { get; set; }
}