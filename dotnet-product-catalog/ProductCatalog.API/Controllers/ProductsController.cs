using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.API.Dtos;
using ProductCatalog.API.Services;

namespace ProductCatalog.API.Controllers;

[Route("api/[controller]")] // URL : api/products
[ApiController]
public class ProductsController(IProductService productService) : ControllerBase
{
    // 1. List all of products ( GET: api/products )
    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> GetAll()
    {
        // LINQ for JOIN Operations
        var products = await productService.GetAllAsync();
        return Ok(products); // HTTP 200
    }

    // 2. Add to new products ( POST: api/products )
    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create(CreateProductDto request)
    {
        try
        {
            var result = await productService.CreateAsync(request);
            return StatusCode(201, result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}