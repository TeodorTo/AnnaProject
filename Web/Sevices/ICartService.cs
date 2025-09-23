using Infrastructure.Entities;

namespace Web.Sevices;

public interface ICartService
{
    void AddToCart(OrderItem item);
    void RemoveFromCart(Guid productId);
    List<OrderItem> GetCartItems();
    void ClearCart();
}