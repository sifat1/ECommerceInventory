using ECommerceInventory.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace ECommerceInventory.Models.DB;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<CategoryWithCountDto> GetCategoryCounts()
    {
        return from category in _context.Categories
            join product in _context.Products
                on category.Id equals product.CategoryId
                into categoryGroup
            select new CategoryWithCountDto
            {
                CategoryId = category.Id,
                CategoryName = category.Name,
                ProductCount = categoryGroup.Count()
            };
    }

    public IQueryable<Category> GetById(int id)
    {
        return _context.Categories.Where(c => c.Id == id);
    }

    public async Task AddAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int categoryId)
    {
        var category = await _context.Categories
            .Include(c => c.products)
            .FirstOrDefaultAsync(c => c.Id == categoryId);

        if (category == null) return false;

        if (category.products != null && category.products.Any())
            throw new InvalidOperationException("Cannot delete category with linked products.");

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return true;
    }
}