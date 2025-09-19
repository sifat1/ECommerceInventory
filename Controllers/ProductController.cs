using ECommerceInventory.Models;
using ECommerceInventory.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceInventory.Controllers;

[Route("api/")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;
    public ProductController(ProductService productService)
    {
        _productService = productService;
    }
    
    [HttpPost]
    [Route("products")]
    public IActionResult AddProduct(Product product)
    {
        try
        {
            _productService.AddProduct(product);
            return Ok(new { message = "Product added successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }

    }

    [HttpGet]
    [Route("products")]
    public async Task<List<Product>> GetAllProducts([FromQuery] ProductListDto productlistDto)
    {
        List<Product> products = await _productService.GetAllProducts(productlistDto).ToListAsync();
        return products ?? new List<Product>();
    }

    [HttpGet]
    [Route("products/{id}")]
    public async Task<Product> GetProductsById(int id)
    {
        Product product = await _productService.GetProductsById(id).FirstOrDefaultAsync();
        return product ?? null;
    }

    [HttpPut]
    [Route("products")]
    public IActionResult UpdateProduct(Product product)
    {
        try
        {
            _productService.UpdateProduct(product);
            return Ok(new { message = "Product updated successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete]
    [Route("products/{id}")]
    public IActionResult DeleteProduct(int id)
    {
        try
        {
            _productService.DeleteProduct(id);
            return Ok(new { message = "Product deleted successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}