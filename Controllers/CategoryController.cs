using ECommerceInventory.Models;
using ECommerceInventory.Models.Dtos;
using ECommerceInventory.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace ECommerceInventory.Controllers;

[Route("api/")]
[ApiController]
[Authorize]
[Tags("Categories")]
public class CategoryController : ControllerBase
{
    private readonly CategoryService _categoryService;

    public CategoryController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    [Route("categories")]
    [SwaggerOperation(Summary = "Create Category", Description = "Adds a new Category")]
    [ProducesResponseType(typeof(Category), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AddCategory(Category category)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);
        try
        {
            _categoryService.AddCategoryAsync(category);
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
    [ProducesResponseType(typeof(List<CategoryWithCountDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<CategoryWithCountDto>>> GetAllCategories()
    {
        List<CategoryWithCountDto> categories = await _categoryService.GetCategoryCounts().ToListAsync();
        if (categories == null) return NotFound();
        return Ok(categories);
    }

    [HttpGet]
    [Route("categories/{id}")]
    [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        Category category = await _categoryService.GetCategoryById(id).FirstOrDefaultAsync();
        if (category == null) return NotFound();
        return Ok(category);
    }

    [HttpPut]
    [Route("categories")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult UpdateCategory(Category category)
    {
        try
        {
            _categoryService.UpdateCategoryAsync(category);
            return Ok(new { message = "Category updated successfully" });
        }
        catch (DbUpdateException ex)
        {
            return Conflict(new { message = ex.InnerException?.Message ?? ex.Message });
        }
    }

    [HttpDelete]
    [Route("categories/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        try
        {
            var deleted = await _categoryService.DeleteCategoryAsync(id);
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