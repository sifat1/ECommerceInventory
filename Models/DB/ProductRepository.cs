using ECommerceInventory.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace ECommerceInventory.Models.DB;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<Product> GetAll(ProductListDto filter)
    {
        return _context.Products
            .Where(p =>
                (!filter.minPrice.HasValue || p.Price >= filter.minPrice.Value) &&
                (!filter.maxPrice.HasValue || p.Price <= filter.maxPrice.Value) &&
                (p.CategoryId == filter.categoryId))
            .Skip(((filter.page ?? 1) - 1) * (filter.limit ?? 10))
            .Take(filter.limit ?? 10);
    }

    public IQueryable<Product> GetById(int id)
    {
        return _context.Products.Where(p => p.Id == id);
    }

    public async Task AddAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, ProductDto product)
    {
        await _context.Products
            .Where(p => p.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.Name, product.Name)
                .SetProperty(p => p.Description, product.Description)
                .SetProperty(p => p.Price, product.Price)
                .SetProperty(p => p.Stock, product.Stock)
                .SetProperty(p => p.ImageUrl, product.ImageUrl));
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            throw new KeyNotFoundException("Product not found");

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}