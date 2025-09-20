using Swashbuckle.AspNetCore.Filters;

namespace ECommerceInventory.Models.Examples;

public class ProductExample : IExamplesProvider<Product>
{
    public Product GetExamples() =>
        new Product
        {
            Id = 1,
            Name = "Sample Phone",
            Description = "A demo product",
            Price = 199.99M,
            Stock = 50,
            CategoryId = 2,
            ImageUrl = "https://example.com/phone.jpg"
        };
}