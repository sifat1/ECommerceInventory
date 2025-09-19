using ECommerceInventory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class CategoryController : ControllerBase
{
    private readonly CategoryService _categoryService;

    public CategoryController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    [Route("categories")]
    public IActionResult AddCategory(Category category)
    {
        try
        {
            _categoryService.AddCategory(category);
            return Ok(new { message = "Category added successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }

    }

    [HttpGet]
    [Route("categories")]
    public async Task<List<Category>> GetAllCategories()
    {
        List<Category> categories = await _categoryService.GetAllCategories().ToListAsync();
        return categories ?? new List<Category>();
    }

    [HttpGet]
    [Route("categories/{id}")]
    public async Task<Category> GetCategoryById(int id)
    {
        Category category = await _categoryService.GetCategoryById(id).FirstOrDefaultAsync();
        return category ?? null;
    }

    [HttpPut]
    [Route("categories")]
    public IActionResult UpdateCategory(Category category)
    {
        try
        {
            _categoryService.UpdateCategory(category);
            return Ok(new { message = "Category updated successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete]
    [Route("categories/{id}")]
    public IActionResult DeleteCategory(int id)
    {
        try
        {
            _categoryService.DeleteCategory(id);
            return Ok(new { message = "Category deleted successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}