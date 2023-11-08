namespace SupplierRegServer.Api.DTOs;

public class UserDTO
{
    public string? Id { get; set; }
    public string? Email { get; set; }
    public IEnumerable<ClaimDTO>? Claims { get; set; }
}
