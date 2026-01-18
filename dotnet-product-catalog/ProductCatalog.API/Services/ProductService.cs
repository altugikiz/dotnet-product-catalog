using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.API.Data;
using ProductCatalog.API.Dtos;
using ProductCatalog.API.Models;

namespace ProductCatalog.API.Services;

public class ProductService(AppDbContext context, IValidator<CreateProductDto> validator) : IProductService
{
    public async Task<List<ProductDto>> GetAllAsync()
    {
        return await context.Products
            .Include(p => p.Category)
            .Select(p => new ProductDto(
                p.Id,
                p.Name,
                p.Price,
                p.Category != null ? p.Category.Name : "No"
            ))
            .ToListAsync();
    }

    public async Task<ProductDto> CreateAsync(CreateProductDto request)
    {
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new Exception($"Validation Error: {errors}");
        }

        
        var category = await context.Categories.FindAsync(request.CategoryId);
        if (category == null)
        {
            throw new Exception("Category not found!");
        }

        var product = new Product
        {
            Name = request.Name,
            Price = request.Price,
            CategoryId = request.CategoryId
        };

        context.Products.Add(product);
        await context.SaveChangesAsync();

        return new ProductDto(
            product.Id,
            product.Name,
            product.Price,
            category.Name
        );
    }
}