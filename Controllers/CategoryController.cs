using ECommerceInventory.Models;
using ECommerceInventory.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Route("api/")]
[ApiController]
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
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);
        try
        {
            _categoryService.AddCategory(category);
            return CreatedAtAction(nameof(GetCategoryById), new {category.Id
        }, category);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }

    }

    [HttpGet]
    [Route("categories")]
    public async Task<ActionResult<List<CategoryWithCountDto>>> GetAllCategories()
    {
        List<CategoryWithCountDto> categories = await _categoryService.GetCategoryCounts().ToListAsync();
        if (categories == null) return NotFound();
        return Ok(categories);
    }

    [HttpGet]
    [Route("categories/{id}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
            Category category = await _categoryService.GetCategoryById(id).FirstOrDefaultAsync();
            if (category == null) return NotFound();
            return Ok(category);
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
        catch (DbUpdateException ex)
        {
            return Conflict(new { message = ex.InnerException?.Message ?? ex.Message });
        }
    }

    [HttpDelete]
    [Route("categories/{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        try
        {
            var deleted = await _categoryService.DeleteCategory(id);
            if (!deleted)
                return NotFound(new { message = "Category not found" });

            return Ok(new { message = "Category deleted successfully" });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(new { message = ex.InnerException?.Message ?? ex.Message });
        }
    }
}