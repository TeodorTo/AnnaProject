namespace Infrastructure.Entities;

public class OrderItem
{
    public Guid Id                    { get; set; } = Guid.NewGuid();
    public Guid OrderId               { get; set; }
    public Order Order                { get; set; } = null!;
    public Guid ProductId             { get; set; }
    public string ProductNameSnapshot { get; set; } = null!;
    public decimal UnitPriceSnapshot  { get; set; }
    public int Quantity               { get; set; }
    public decimal LineTotal => UnitPriceSnapshot * Quantity;
}