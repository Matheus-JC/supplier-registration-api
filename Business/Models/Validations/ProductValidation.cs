using FluentValidation;

namespace SupplierRegServer.Business.Models.Validations;

public class ProductValidation : AbstractValidator<Product>
{
    public ProductValidation()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("The field {PropertyName} needs to be informed")
            .Length(2, 200).WithMessage("The field {PropertyName} must be between {MinLength} and {MaxLength} characters");

        RuleFor(p => p.Description)
            .NotEmpty().WithMessage("The field {PropertyName} needs to be informed")
            .Length(2, 1000).WithMessage("The field {PropertyName} must be between {MinLength} and {MaxLength} characters");

        RuleFor(p => p.Value)
            .GreaterThan(0).WithMessage("the field {PropertyName} must be greater than {ComparisonValue}");
    }
}
