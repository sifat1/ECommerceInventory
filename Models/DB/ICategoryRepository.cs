using ECommerceInventory.Models.Dtos;

namespace ECommerceInventory.Models.DB;

public interface ICategoryRepository
{
    IQueryable<CategoryWithCountDto> GetCategoryCounts();
    IQueryable<Category> GetById(int id);
    Task AddAsync(Category category);
    Task UpdateAsync(Category category);
    Task<bool> DeleteAsync(int categoryId);
}