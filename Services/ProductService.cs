using ECommerceInventory.Models;
using ECommerceInventory.Models.Dtos;

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
    public void UpdateProduct(Product product)
    {
        _context.Products.Update(product);
        _context.SaveChanges();
    }
    public void DeleteProduct(int productId)
    {
        var product = _context.Products.Find(productId);
        if (product != null)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}