using Infrastructure;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Sevices;

public class CartController : Controller
{
    private readonly ICartService _cartService;
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;

    public CartController(ICartService cartService, ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
    {
        _cartService = cartService;
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var items = _cartService.GetCartItems();
        ViewBag.Total = items.Sum(x => x.LineTotal);
        return View(items);
    }

    [HttpPost]
    public IActionResult Add(Guid productId, string productName, decimal price, int quantity = 1)
    {
        var item = new OrderItem
        {
            ProductId = productId,
            ProductNameSnapshot = productName,
            UnitPriceSnapshot = price,
            Quantity = quantity
        };
        _cartService.AddToCart(item);
        return NoContent();
    }

    [HttpPost]
    public IActionResult Remove(Guid productId)
    {
        _cartService.RemoveFromCart(productId);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Checkout(string shippingCity, string shippingStreet, string? shippingZip)
    {
        var items = _cartService.GetCartItems();
        if (!items.Any())
            return RedirectToAction("Index");

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return NotFound(); 

        var order = new Order
        {
            CustomerId = user.Id,
            ShippingCity = shippingCity,
            ShippingStreet = shippingStreet,
            ShippingZip = shippingZip,
            OrderStatus = OrderStatus.Pending,
            TotalAmount = items.Sum(x => x.LineTotal),
            Items = items
        };

        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();

        _cartService.ClearCart();
        return RedirectToAction("Index", "Home");
    }
}
