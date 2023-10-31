using System.ComponentModel.DataAnnotations;

namespace SupplierRegServer.Api.DTOs;

public class SupplierDTO
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "The field {0} needs to be informed")]
    [StringLength(100, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
    public required string Name { get; set; }

    [Required(ErrorMessage = "The field {0} needs to be informed")]
    [StringLength(14, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 11)]
    public required string Document { get; set; }

    public bool Active { get; set; }

    public int SupplierType { get; set; }

    public AddressDTO? Address { get; set; }

    public IEnumerable<ProductDTO>? Products { get; set; }
}
