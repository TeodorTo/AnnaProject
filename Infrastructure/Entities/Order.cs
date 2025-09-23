namespace Infrastructure.Entities;

    public enum OrderStatus { Pending, Completed, Cancelled }
    

    public class Order
    {
        public Guid Id                  { get; set; } = Guid.NewGuid();
        public string CustomerId        { get; set; } = null!;
        public Customer Customer        { get; set; } = null!;
        public OrderStatus OrderStatus  { get; set; } = OrderStatus.Pending;
        public DateTime CreatedAtUtc    { get; set; } = DateTime.UtcNow;
        public DateTime? ConfirmedAtUtc { get; set; }
        public DateTime? CancelledAtUtc { get; set; }
        public string PaymentMethod     { get; set; } = "CashOnDelivery";
        // Snapshot shipping fields (recommended for diploma)
        public string ShippingCity      { get; set; } = null!;
        public string ShippingStreet    { get; set; } = null!;
        public string? ShippingZip      { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
