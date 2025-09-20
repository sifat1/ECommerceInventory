using ECommerceInventory.Models;
using ECommerceInventory.Models.Dtos;
using ECommerceInventory.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceInventory.Controllers;

[Route("api/")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;
    private readonly ElasticServices _elasticServices;
    public ProductController(ProductService productService, ElasticServices elasticServices)
    {
        _productService = productService;
        _elasticServices = elasticServices;
    }
    
    [HttpPost]
    [Route("products")]
    public async Task<IActionResult> AddProduct(Product product)
    {
        try
        {
            await _productService.AddProductAsync(product);
            await _elasticServices.CreateProductAsync(product);
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
            _productService.UpdateProductAsync(id,product);
            return Ok(new { message = "Product updated successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete]
    [Route("products/{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        try
        {
            await _productService.DeleteProductAsync(id);
            await _elasticServices.DeleteProductAsync(id);
            return Ok(new { message = "Product deleted successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpGet]
    [Route("products/{query:alpha}")]
    public async Task<IActionResult> SearchProduct(string query)
    {
        try
        {
            var result = await _elasticServices.SearchProductsAsync(query);
            if(result.Count==0) return NotFound();
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }
}