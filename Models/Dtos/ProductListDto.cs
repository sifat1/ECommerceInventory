using Microsoft.AspNetCore.Mvc;

namespace ECommerceInventory.Models.Dtos;

public class ProductListDto
{
    [FromQuery(Name = "categoryId")]
    public int categoryId { get; set; }

    [FromQuery(Name = "minPrice")]
    public int? minPrice { get; set; } = 10;
    
    [FromQuery(Name = "maxPrice")]
    public int? maxPrice { get; set; } = 100;

    [FromQuery(Name = "page")]
    public int? page { get; set; } = 1;
    
    [FromQuery(Name = "limit")]
    public int? limit { get; set; } = 10;
}