using ECommerceInventory.Models;
using ECommerceInventory.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceInventory.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    [HttpPost]
    public async Task<Product> products(Product product)
    {
        
    }
    
    [HttpGet]
    public async Task<List<Product>> products([FromQuery] ProductDto productDto)
    {
        
    }
    
    [HttpGet]
    public async Task<Product> products(int id)
    {
        
    }
    
    [HttpPut]
    public async Task<Product> products(int id)
    {
        
    }
    
    [HttpDelete]
    public async Task<Product> products(int id)
    {
        
    }
}