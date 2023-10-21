namespace SupplierRegServer.Business.Models;

public class Address : Entity
{
    public string? PublicArea { get; set; }
    public string? Number { get; set; }
    public string? ZipCode { get; set; }
    public string? Complement { get; set; }
    public string? Neighborhood { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
}