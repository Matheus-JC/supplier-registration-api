using FluentValidation;

namespace SupplierRegServer.Business.Models.Validations;

public class AddressValidation : AbstractValidator<Address>
{
    public AddressValidation()
    {
        RuleFor(a => a.PublicArea)
            .NotEmpty().WithMessage("The field {PropertyName} needs to be informed")
            .Length(2, 200).WithMessage("The field {PropertyName} must be between {MinLength} and {MaxLength} characters");

        RuleFor(a => a.Neighborhood)
            .NotEmpty().WithMessage("The field {PropertyName} needs to be informed")
            .Length(2, 100).WithMessage("The field {PropertyName} must be between {MinLength} and {MaxLength} characters");

        RuleFor(a => a.ZipCode)
            .NotEmpty().WithMessage("The field {PropertyName} needs to be informed")
            .Length(8).WithMessage("The field {PropertyName} must have {MaxLength} characters");

        RuleFor(a => a.Number)
            .NotEmpty().WithMessage("The field {PropertyName} needs to be informed")
            .Length(1, 50).WithMessage("The field {PropertyName} must have {MaxLength} characters");

        RuleFor(a => a.City)
            .NotEmpty().WithMessage("The field {PropertyName} needs to be informed")
            .Length(2, 100).WithMessage("The field {PropertyName} must be between {MinLength} and {MaxLength} characters");

        RuleFor(a => a.State)
            .NotEmpty().WithMessage("The field {PropertyName} needs to be informed")
            .Length(2, 50).WithMessage("The field {PropertyName} must be between {MinLength} and {MaxLength} characters");

        RuleFor(a => a.Country)
            .NotEmpty().WithMessage("The field {PropertyName} needs to be informed")
            .Length(2, 50).WithMessage("The field {PropertyName} must be between {MinLength} and {MaxLength} characters");
    }
}
