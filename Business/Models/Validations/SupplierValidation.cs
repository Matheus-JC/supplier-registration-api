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

        When(s => s.SupplierType == SupplierType.PhysicalPerson, () =>
        {
            RuleFor(f => f.Document.Length).Equal(CpfValidator.CpfLength)
                .WithMessage("The field must have {ComparisonValue} characteres and was provided {PropertyValue}");

            RuleFor(f => CpfValidator.Validate(f.Document)).Equal(true)
                .WithMessage("The document provided is invalid");
        });

        When(s => s.SupplierType == SupplierType.LegalPerson, () =>
        {
            RuleFor(f => f.Document.Length).Equal(CnpjValidator.CnpjLength)
                .WithMessage("The field must have {ComparisonValue} characteres and was provided {PropertyValue}");

            RuleFor(f => CnpjValidator.Validate(f.Document)).Equal(true)
                .WithMessage("The document provided is invalid");
        });
    }
}
