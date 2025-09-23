namespace Infrastructure.Entities;

public class Customer
{
    public string Id              { get; set; } = Guid.NewGuid().ToString();
    
    public string UserId          { get; set; } = null!;
    
    public ApplicationUser User   { get; set; } = null!;
    
    public string? Phone          { get; set; }
    
    public Guid? DefaultAddressId { get; set; }
    
    public ICollection<Address> Addresses { get; set; } = new List<Address>();
}