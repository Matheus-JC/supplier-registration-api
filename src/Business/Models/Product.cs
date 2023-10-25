namespace SupplierRegServer.Business.Models;

public class Product : Entity
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Value { get; set; }
    public DateTime CreationDate { get; set; }
    public bool Active { get; set; }

    public required Supplier Supplier { get; set; }
}