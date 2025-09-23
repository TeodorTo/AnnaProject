using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers;

public class ShopController : Controller
{
    private readonly ApplicationDbContext _context;

    public ShopController(ApplicationDbContext context)
    {
        _context = context;
    }

    
    // Показва всички категории
    public async Task<IActionResult> Index()
    {
        var categories = await _context.Categories.ToListAsync();
        return View(categories);
    }

    // Показва продуктите от избраната категория
    public async Task<IActionResult> Products(Guid categoryId)
    {
        var category = await _context.Categories
            .Include(c => c.ProductCategories)
            .ThenInclude(pc => pc.Product)
            .FirstOrDefaultAsync(c => c.Id == categoryId);

        if (category == null) return NotFound();

        return View(category);
    }

    // Детайли за продукт
    public async Task<IActionResult> ProductDetails(Guid id)
    {
        var product = await _context.Products
            .Include(p => p.ProductCategories)
            .ThenInclude(pc => pc.Category)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null) return NotFound();

        return View(product);
    }
}