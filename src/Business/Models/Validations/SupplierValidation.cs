using FluentValidation;
using SupplierRegServer.Business.Models.Validations.Documents;

namespace SupplierRegServer.Business.Models.Validations;

public class SupplierValidation : AbstractValidator<Supplier>
{
    public SupplierValidation()
    {
        RuleFor(s => s.Name)
            .NotEmpty().WithMessage("The field {PropertyName} needs to be informed")
            .Length(2, 200).WithMessage("The field {PropertyName} must be between {MinLength} and {MaxLength} characters");
        
        RuleFor(s => s.SupplierType)
            .IsInEnum()
            .WithMessage("The {PropertyName} must be 1 (Physical Person) or 2 (Legal Person)");

        When(s => s.SupplierType == SupplierType.PhysicalPerson, () =>
        {
            RuleFor(s => s.Document.Length).Equal(CpfValidator.CpfLength)
                .WithMessage("The field must have {ComparisonValue} characteres and was provided {PropertyValue}");

            RuleFor(s => CpfValidator.Validate(s.Document)).Equal(true)
                .WithMessage("The document provided is invalid");
        });

        When(s => s.SupplierType == SupplierType.LegalPerson, () =>
        {
            RuleFor(s => s.Document.Length).Equal(CnpjValidator.CnpjLength)
                .WithMessage("The field must have {ComparisonValue} characteres and was provided {PropertyValue}");

            RuleFor(s => CnpjValidator.Validate(s.Document)).Equal(true)
                .WithMessage("The document provided is invalid");
        });
    }
}
