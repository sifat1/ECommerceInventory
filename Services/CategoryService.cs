using ECommerceInventory.Models;

public class CategoryService
{
    private readonly ApplicationDbContext _context;

    public CategoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<Category> GetAllCategories()
    {
        return _context.Categories;
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
    public void DeleteCategory(int categoryId)
    {
        var category = _context.Categories.Find(categoryId);
        if (category != null)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}