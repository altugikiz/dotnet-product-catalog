using ProductCatalog.API.Dtos;

namespace ProductCatalog.API.Services;

public interface IProductService
{
    // Tasks mean async operations
    Task<List<ProductDto>> GetAllAsync();
    Task<ProductDto> CreateAsync(CreateProductDto request);
}