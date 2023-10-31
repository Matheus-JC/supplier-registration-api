namespace SupplierRegServer.Business.Models;

public class Supplier : Entity
{
    public required string Name { get; set; }
    public required string Document { get; set; }
    public bool Active { get; set; }
    public SupplierType SupplierType { get; set; }
    public Address? Address { get; set; }
    public IEnumerable<Product>? Products { get; set; }
}