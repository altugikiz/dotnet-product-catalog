using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.API.Data;
using ProductCatalog.API.Dtos;
using ProductCatalog.API.Models;

namespace ProductCatalog.API.Controllers;

[Route("api/[controller]")] // URL : api/products
[ApiController]
public class ProductsController(AppDbContext context) : ControllerBase
{
    // 1. List all of products ( GET: api/products )
    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> GetAll()
    {
        // LINQ for JOIN Operations
        var products = await context.Products
            .Include(p => p.Category) // Eager Loading ( SQL JOIN )
            .Select(p => new ProductDto(  // Entity -> DTO
                p.Id,
                p.Name,
                p.Price,
                p.Category != null ? p.Category.Name : "Uncategorized" // Null Check
            ))
            .ToListAsync();

        return Ok(products); // HTTP 200
    }

    // 2. Add to new products ( POST: api/products )
    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create(CreateProductDto request)
    {
        // Is there any category?
        var category = await context.Categories.FindAsync(request.CategoryId);
        if(category == null)
        {
            return BadRequest("Invalid Category ID!"); // HTTP 400
        }

        // DTO -> Entity
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price,
            CategoryId = request.CategoryId
        };

        // To add to database
        context.Products.Add(product);
        await context.SaveChangesAsync(); 

        // Return created data
        var responseDto = new ProductDto(
            product.Id,
            product.Name,
            product.Price,
            product.Name
        );

        // HTTP 201 created and add location information to Header
        return CreatedAtAction(nameof(GetAll), new { id = product.Id }, responseDto);
    }
}