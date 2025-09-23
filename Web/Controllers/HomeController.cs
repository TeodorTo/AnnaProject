using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure;
using  Web.ViewModel;
using Infrastructure.Entities;

namespace Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index(Guid? categoryId)
    {
     
        var productsQuery = _context.Products
            .Include(p => p.ProductCategories)
            .ThenInclude(pc => pc.Category)
            .AsQueryable();
        
        if (categoryId.HasValue)
        {
            productsQuery = productsQuery
                .Where(p => p.ProductCategories
                    .Any(pc => pc.CategoryId == categoryId.Value));
        }
        
        var categories = _context.Categories.ToList();
        
        var vm = new ProductsViewModel
        {
            Products = productsQuery.ToList(),
            Categories = categories,
            SelectedCategoryId = categoryId
        };

        return View(vm);
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }
}