using SupplierRegServer.Api.DTOs;

namespace Api.DTOs;

public class LoginResponseDTO
{
    public required string AccessToken { get; set; }
    public required double ExpiresIn { get; set; }
    public UserDTO? User { get; set; }
}
