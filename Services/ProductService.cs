using ECommerceInventory.Models;
using ECommerceInventory.Models.DB;
using ECommerceInventory.Models.Dtos;

namespace ECommerceInventory.Services;

public class ProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public IQueryable<Product> GetAllProducts(ProductListDto filter) =>
        _repository.GetAll(filter);

    public IQueryable<Product> GetProductsById(int id) =>
        _repository.GetById(id);

    public Task AddProductAsync(Product product) =>
        _repository.AddAsync(product);

    public Task UpdateProductAsync(int id, ProductDto product) =>
        _repository.UpdateAsync(id, product);

    public Task DeleteProductAsync(int id) =>
        _repository.DeleteAsync(id);
}