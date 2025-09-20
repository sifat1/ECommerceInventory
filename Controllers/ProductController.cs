using ECommerceInventory.Models;
using ECommerceInventory.Models.Dtos;
using ECommerceInventory.Models.Examples;
using ECommerceInventory.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace ECommerceInventory.Controllers;

[Route("api/")]
[ApiController]
[Authorize]
[Tags("Products")]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;
    private readonly FileUploadService _uploadService;
    private readonly ElasticServices _elasticServices;
    public ProductController(ProductService productService, ElasticServices elasticServices, FileUploadService  uploadService)
    {
        _productService = productService;
        _elasticServices = elasticServices;
        _uploadService = uploadService;
    }
    
    [HttpPost]
    [Route("products")]
    [SwaggerOperation(Summary = "Create product", Description = "Adds a new product and indexes it in Elasticsearch")]
    [SwaggerResponse(201, "Product created", typeof(Product))]
    public async Task<IActionResult> AddProduct(ProductDto productDto)
    {
        try
        {
            if (productDto.Image != null)
            {
                productDto.ImageUrl = await _uploadService.UploadImageAsync(productDto.Image);
            }
            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Stock = productDto.Stock,
                CategoryId = productDto.CategoryId,
                ImageUrl = productDto.ImageUrl
            };
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
    [SwaggerResponse(200, "List of products", typeof(IEnumerable<Product>))]
    [SwaggerResponseExample(200, typeof(List<Product>))]
    [SwaggerResponseExample(201, typeof(ProductExample))]
    public async Task<List<Product>> GetAllProducts([FromQuery] ProductListDto productlistDto)
    {
        List<Product> products = await _productService.GetAllProducts(productlistDto).ToListAsync();
        return products ?? new List<Product>();
    }

    [HttpGet]
    [Route("products/{id}")]
    [SwaggerResponse(200, "Product details", typeof(Product))]
    [SwaggerResponse(404, "Product not found")]
    public async Task<ActionResult<Product>> GetProductsById(int id)
    {
        Product product = await _productService.GetProductsById(id).FirstOrDefaultAsync();
        if (product != null) NotFound(new { message = "Product not found" });
        return Ok(product);
    }

    [HttpPut]
    [Route("products/{id}")]
    [SwaggerResponse(200, "Product updated")]
    [SwaggerResponse(400, "Invalid data")]
    public async Task<IActionResult> UpdateProduct(int id, [FromQuery] ProductDto product)
    {
        try
        {
            if (product.Image != null)
            {
                product.ImageUrl = await _uploadService.UploadImageAsync(product.Image);
            }
            await _productService.UpdateProductAsync(id,product);
            return Ok(new { message = "Product updated successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete]
    [Route("products/{id}")]
    [SwaggerResponse(200, "Product deleted")]
    [SwaggerResponse(404, "Product not found")]
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
    [SwaggerOperation(Summary = "Search Product", Description = "Searches Product using elastic search. Searches in Product name and details")]
    [SwaggerResponse(200, "Search results", typeof(IEnumerable<Product>))]
    [SwaggerResponse(404, "No products found")]
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