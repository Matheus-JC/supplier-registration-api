namespace SupplierRegServer.Business.Models;

public class Address : Entity
{
    public required string PublicArea { get; set; }
    public required string Number { get; set; }
    public required string ZipCode { get; set; }
    public required string Neighborhood { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public required string Country { get; set; }
    public string? Complement { get; set; }
    public required Supplier Supplier { get; set; }

}