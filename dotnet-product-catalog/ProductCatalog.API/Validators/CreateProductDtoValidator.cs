using FluentValidation;
using ProductCatalog.API.Dtos;

namespace ProductCatalog.API.Validators;

public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product names cannot be empty!")
            .Length(2, 50).WithMessage("The product name must be between 2 and 50 characters long.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("The price must be greater than 0.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("You must select a valid category.");
    }
}