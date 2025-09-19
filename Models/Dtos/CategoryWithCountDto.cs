namespace ECommerceInventory.Models.Dtos;

public class CategoryWithCountDto
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public int ProductCount { get; set; }
}