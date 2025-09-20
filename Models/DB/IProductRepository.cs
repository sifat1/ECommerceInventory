using ECommerceInventory.Models.Dtos;

namespace ECommerceInventory.Models.DB;

public interface IProductRepository
{
    IQueryable<Product> GetAll(ProductListDto filter);
    IQueryable<Product> GetById(int id);
    Task AddAsync(Product product);
    Task UpdateAsync(int id, ProductDto product);
    Task DeleteAsync(int id);
}