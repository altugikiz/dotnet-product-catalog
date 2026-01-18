namespace ProductCatalog.API.Dtos;

// Input of User (Process of Adding Item)
public record CreateProductDto(
    string Name,
    decimal Price,
    int CategoryId
);

// Output of User (Process of Listing Item)
public record ProductDto(
    int Id,
    string Name,
    decimal Price,
    string CategoryName
);