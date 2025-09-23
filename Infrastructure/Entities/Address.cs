namespace Infrastructure.Entities;

public class Address
{
    public Guid Id              { get; set; } = Guid.NewGuid();
    
    public string CustomerId    { get; set; } = null!;
    
    public Customer Customer    { get; set; } = null!;
    
    public string City          { get; set; } = null!;
    
    public string Street        { get; set; } = null!;
    
    public string? ZipCode      { get; set; }
    
    public string? Notes        { get; set; }
    
}
