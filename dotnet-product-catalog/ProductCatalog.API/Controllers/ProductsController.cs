using Microsoft.AspNetCore.Mvc;
using ProductCatalog.API.Dtos;
using ProductCatalog.API.Services;
using ProductCatalog.API.Wrappers;

namespace ProductCatalog.API.Controllers;

[Route("api/[controller]")] // URL : api/products
[ApiController]
public class ProductsController(IProductService productService) : ControllerBase
{
    // 1. List all of products ( GET: api/products )
    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<ProductDto>>>> GetAll()
    {
        // LINQ for JOIN Operations
        var products = await productService.GetAllAsync();
        var response = ApiResponse<List<ProductDto>>.SuccessResult(products);
        return Ok(products); // HTTP 200
    }

    // 2. Add to new products ( POST: api/products )
    [HttpPost]
    public async Task<ActionResult<ApiResponse<ProductDto>>> Create(CreateProductDto request)
    {
            var product = await productService.CreateAsync(request);

        // Created (201) 
        var response = ApiResponse<ProductDto>.SuccessResult(product, 201, "The product has been created successfully.");

        return StatusCode(201, response);
    }
}