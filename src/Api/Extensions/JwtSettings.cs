namespace SupplierRegServer.Api.Extensions;

public class JwtSettings
{
    public required string Secret { get; set; }
    public int ExpirationHours { get; set; }
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
}
