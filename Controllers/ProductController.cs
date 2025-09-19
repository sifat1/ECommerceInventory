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
            return CreatedAtAction(nameof(GetProductsById), new { product.Id }, product);
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
    public async Task<ActionResult<Product>> GetProductsById(int id)
    {
        Product product = await _productService.GetProductsById(id).FirstOrDefaultAsync();
        if (product != null) NotFound(new { message = "Product not found" });
        return Ok(product);
    }

    [HttpPut]
    [Route("products/{id}")]
    public IActionResult UpdateProduct(int id, ProductDto product)
    {
        try
        {
            _productService.UpdateProduct(id,product);
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