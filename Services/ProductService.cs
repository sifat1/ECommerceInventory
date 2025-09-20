using ECommerceInventory.Models;
using ECommerceInventory.Models.Dtos;
using Microsoft.EntityFrameworkCore;

public class ProductService
{
    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<Product> GetAllProducts(ProductListDto productDto)
    {
        return _context.Products.Where(p=> 
            (!productDto.minPrice.HasValue || p.Price >= productDto.minPrice.Value) &&
            (!productDto.maxPrice.HasValue || p.Price <= productDto.maxPrice.Value) &&
            (p.CategoryId == productDto.categoryId)
        ).Skip(((productDto.page ?? 1) - 1) * (productDto.limit ?? 10))
         .Take(productDto.limit ?? 10);
    }

    public IQueryable<Product> GetProductsById(int id)
    {
        return _context.Products.Where(p => p.Id == id);
    }
    public void AddProduct(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }
    public async Task UpdateProduct(int id, ProductDto product)
    {
        await _context.Products
            .Where(p => p.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.Name, product.Name)
                .SetProperty(p => p.Description, product.Description )
                .SetProperty(p => p.Price, product.Price )
                .SetProperty(p => p.Stock, product.Stock )
                .SetProperty(p => p.ImageUrl, product.ImageUrl )
            );
    }
    public void DeleteProduct(int productId)
    {
        var product = _context.Products.Find(productId);
        if (product != null)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
        else
        {
            throw new Exception("Product not found");
        }
    }
}