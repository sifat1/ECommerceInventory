namespace ECommerceInventory.Models.Dtos;

public class ProductListDto
{
    public int categoryId;
    public  int? minPrice = 10;
    public int? maxPrice=100;
    public int? page = 1;
    public int? limit = 10;
}