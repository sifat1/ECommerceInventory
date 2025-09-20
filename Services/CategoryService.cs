using ECommerceInventory.Models;
using ECommerceInventory.Models.DB;
using ECommerceInventory.Models.Dtos;

namespace ECommerceInventory.Services;

public class CategoryService
{
    private readonly ICategoryRepository _categoryRepo;

    public CategoryService(ICategoryRepository categoryRepo)
    {
        _categoryRepo = categoryRepo;
    }

    public Task AddCategoryAsync(Category category) => _categoryRepo.AddAsync(category);

    public Task<bool> DeleteCategoryAsync(int id) => _categoryRepo.DeleteAsync(id);

    public IQueryable<CategoryWithCountDto> GetCategoryCounts() => _categoryRepo.GetCategoryCounts();

    public IQueryable<Category> GetCategoryById(int id) => _categoryRepo.GetById(id);
    
    public Task UpdateCategoryAsync(Category category) =>
        _categoryRepo.UpdateAsync(category);

}