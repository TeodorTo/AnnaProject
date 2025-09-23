using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Web.Sevices;


public class CartService : ICartService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string SessionKey = "Cart";

    public CartService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void AddToCart(OrderItem item)
    {
        var items = GetCartItems();
        var existing = items.FirstOrDefault(x => x.ProductId == item.ProductId);
        if (existing != null)
        {
            existing.Quantity += item.Quantity;
        }
        else
        {
            items.Add(item);
        }
        SaveCart(items);
    }

    public void RemoveFromCart(Guid productId)
    {
        var items = GetCartItems();
        items.RemoveAll(x => x.ProductId == productId);
        SaveCart(items);
    }

    public List<OrderItem> GetCartItems()
    {
        var session = _httpContextAccessor.HttpContext?.Session;
        var json = session?.GetString(SessionKey);
        return json == null ? new List<OrderItem>() : JsonSerializer.Deserialize<List<OrderItem>>(json)!;
    }

    public void ClearCart()
    {
        _httpContextAccessor.HttpContext?.Session.Remove(SessionKey);
    }

    private void SaveCart(List<OrderItem> items)
    {
        var session = _httpContextAccessor.HttpContext?.Session;
        session?.SetString(SessionKey, JsonSerializer.Serialize(items));
    }
}