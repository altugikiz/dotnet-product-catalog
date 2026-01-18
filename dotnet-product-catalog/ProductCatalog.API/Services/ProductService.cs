using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.API.Data;
using ProductCatalog.API.Dtos;
using ProductCatalog.API.Exceptions;
using ProductCatalog.API.Models;

namespace ProductCatalog.API.Services;

public class ProductService(AppDbContext context, IValidator<CreateProductDto> validator, IMapper mapper) : IProductService
{
    public async Task<List<ProductDto>> GetAllAsync()
    {
        var products = await context.Products
            .Include(p => p.Category)
            .ToListAsync();

        // (products) -> DTO list
        return mapper.Map<List<ProductDto>>(products);
    }

    public async Task<ProductDto> CreateAsync(CreateProductDto request)
    {
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new BusinessException($"Validation Error: {errors}");
        }


        var category = await context.Categories.FindAsync(request.CategoryId);
        if (category == null)
        {
            throw new NotFoundException("No category was found with the specified ID!");
        }

        var product = mapper.Map<Product>(request);

        context.Products.Add(product);
        await context.SaveChangesAsync();

        return mapper.Map<ProductDto>(product);
    }
}