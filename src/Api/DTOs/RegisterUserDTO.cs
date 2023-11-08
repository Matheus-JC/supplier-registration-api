using System.ComponentModel.DataAnnotations;

namespace SupplierRegServer.Api.DTOs;

public class RegisterUserDTO
{
    [Required(ErrorMessage = "The field {0} needs to be informed")]
    [EmailAddress(ErrorMessage = "The field {0} is in an invalid format")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "The field {0} needs to be informed")]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 6)]
    public required string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords don't match")]
    public required string ConfirmPassword { get; set; }
}
