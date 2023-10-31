using System.ComponentModel.DataAnnotations;

namespace SupplierRegServer.Api.DTOs;

public class AddressDTO
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "The field {0} needs to be informed")]
    [StringLength(200, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
    public required string PublicArea { get; set; }

    [Required(ErrorMessage = "The field {0} needs to be informed")]
    [StringLength(50, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
    public required string Number { get; set; }

    [Required(ErrorMessage = "The field {0} needs to be informed")]
    [StringLength(100, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
    public required string Neighborhood { get; set; }

    [Required(ErrorMessage = "The field {0} needs to be informed")]
    [StringLength(100, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
    public required string City { get; set; }

    [Required(ErrorMessage = "The field {0} needs to be informed")]
    [StringLength(50, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
    public required string State { get; set; }

    [Required(ErrorMessage = "The field {0} needs to be informed")]
    [StringLength(50, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
    public required string Country { get; set; }

    [Required(ErrorMessage = "The field {0} needs to be informed")]
    [StringLength(8, ErrorMessage = "he field {0} must have {1} characters", MinimumLength = 8)]
    public required string ZipCode { get; set; }

    [StringLength(50, ErrorMessage = "he field {0} must have {1} characters", MinimumLength = 2)]
    public string? Complement { get; set; }

    public Guid SupplierId { get; set; }
}
