using System.ComponentModel.DataAnnotations;

namespace SupplierRegServer.Api.DTOs;

public class ProductDTO
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "The field {0} needs to be informed")]
    public Guid SupplierId { get; set; }

    [Required(ErrorMessage = "The field {0} needs to be informed")]
    [StringLength(200, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
    public required string Name { get; set; }

    [Required(ErrorMessage = "The field {0} needs to be informed")]
    [StringLength(1000, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
    public required string Description { get; set; }

    [Required(ErrorMessage = "The field {0} needs to be informed")]
    public required decimal Value { get; set; }

    public DateTime CreationDate { get; set; }

    public bool Active { get; set; }

    public string? SupplierName { get; set; }
}
