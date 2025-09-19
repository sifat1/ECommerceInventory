using ECommerceInventory.Models;
using ECommerceInventory.Models.Dtos;
using Microsoft.EntityFrameworkCore;

public class CategoryService
{
    private readonly ApplicationDbContext _context;

    public CategoryService(ApplicationDbContext context)
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

    public IQueryable<Category> GetCategoryById(int id)
    {
        return _context.Categories.Where(c => c.Id == id);
    }
    public void AddCategory(Category category)
    {
        _context.Categories.Add(category);
        _context.SaveChanges();
    }
    public void UpdateCategory(Category category)
    {
        _context.Categories.Update(category);
        _context.SaveChanges();
    }
    public async Task<bool> DeleteCategory(int categoryId)
    {
        var category = await _context.Categories
            .Include(c => c.products)
            .FirstOrDefaultAsync(c => c.Id == categoryId);

        if (category == null) return false;

        if (category.products != null && category.products.Any())
        {
            throw new InvalidOperationException("Cannot delete category with linked products.");
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return true;
    }
}